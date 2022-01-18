using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.CukCuk.Core.Constants;
using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static MISA.CukCuk.Core.Attributes.AttributeCRUD;
using static MISA.CukCuk.Core.Attributes.AttributeValidate;

namespace MISA.CukCuk.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        #region Declare
        protected string _connectionString = string.Empty;
        protected IDbConnection _dbConnection = null;
        string _className;
        private const string ProcInsert = "Proc_Insert{0}";

        private const string ProcUpdate = "Proc_Update{0}";

        private const string ProcDelete = "Proc_Delete{0}";

        private const string ProcGetAll = "Proc_Get{0}s";

        private const string ProcGetById = "Proc_Get{0}ById";
        #endregion

        #region Contructor
        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString(ConnectionStringKey.DB);
            _dbConnection = new MySqlConnection(_connectionString);
            _className = typeof(TEntity).Name;
        }
        #endregion

        #region Connection
        /// <summary>
        ///  Hảm mở kết nối với Database
        /// </summary>
        public void DbConnetionOpen()
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
        }
        #endregion 

        #region Methods
        public virtual IEnumerable<TEntity> Get()
        {
            var storeName = string.Format(ProcGetAll, _className);
            var entites = _dbConnection.Query<TEntity>(storeName, commandType: CommandType.StoredProcedure);
            return entites;
        }

        public virtual TEntity Get(Guid entityId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"${_className}Id", entityId);
            var storeName = string.Format(ProcGetById, _className);
            var entity = _dbConnection.QueryFirstOrDefault(storeName, parameters, commandType: CommandType.StoredProcedure);
            return entity;
        }

        public virtual int Insert(TEntity entity)
        {
            var parameters = MappingDbType(entity);
            var storeName = string.Format(ProcInsert, _className);
            var rowEffects = _dbConnection.Execute(storeName, param: parameters);
            return rowEffects;
        }

        public int Insert(TEntity entity, IDbTransaction transaction)
        {
            return 1;
        }

        public virtual int Update(TEntity entity, Guid entityId)
        {
            // Khởi tạo kết nối với database:
            DbConnetionOpen();

            var rowEffects = 0;
            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    // Thực thi lấy dữ liệu trong Database:
                    DynamicParameters parameters = new DynamicParameters();
                    var propertys = "";
                    // Lấy ra các giá trị của property tương ứng với đối tượng:
                    var props = typeof(TEntity).GetProperties();
                    // Duyệt từng property
                    foreach (var prop in props)
                    {
                        // Lấy tên của property:
                        var propName = prop.Name;
                        // Lấy giá trị tương ứng với đối tượng:
                        var propValue = prop.GetValue(entity);
                        // Lấy kiểu giá trị của property:
                        var propType = prop.PropertyType;
                        // Nếu property chỉ để hiển thị hoặc không được update -> bỏ qua khi update:
                        if (prop.IsDefined(typeof(ReadOnly), true) || prop.IsDefined(typeof(NotUpdated), true))
                        {
                            continue;
                        }
                        // Cập nhật thời gian sửa bằng ngày hiện tại:
                        if (propName == "ModifiedDate" && propType == typeof(DateTime))
                        {
                            propValue = DateTime.Now;
                        }
                        propertys += $"{ propName } = @{ propName },";
                        parameters.Add($"@{propName}", propValue);
                    }
                    propertys = propertys.Substring(0, propertys.Length - 1);
                    var sql = $"UPDATE { _className }  SET { propertys } WHERE { _className }Id = '{ entityId }'";
                    rowEffects = _dbConnection.Execute(sql, param: parameters, transaction: transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

            //Đóng kết nối:
            Dispose();
            return rowEffects;
        }

        public virtual int Delete(string entityId)
        {
            // Khởi tạo kết nối với database:
            DbConnetionOpen();

            var rowEffects = 0;
            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add($"@{_className}Id", entityId);
                    var storeName = string.Format(ProcDelete, _className);
                    rowEffects = _dbConnection.Execute(storeName, parameters, transaction: transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

            //Đóng kết nối
            Dispose();
            return rowEffects;
        }


        private DynamicParameters MappingDbType(TEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();

            // Lấy ra các giá trị của property tương ứng với đối tượng:
            var properties = typeof(TEntity).GetProperties().Where(p => !p.IsDefined(typeof(ReadOnly), true)).ToList();
            foreach (var prop in properties)
            {
                var propName = prop.Name;
                var propValue = prop.GetValue(entity);
                var propType = prop.PropertyType;
                // mapping với kiểu bool
                if (propType == typeof(bool) && propType != null)
                {
                    propValue = ((bool)propValue == true ? 1 : 0);
                    parameters.Add($"${propName}", propValue, DbType.Int32);
                }
                else // các kiểu dữ liệu còn lại
                {
                    parameters.Add($"${propName}", propValue);
                }
            }
            return parameters;
        }

        public bool IsExist(TEntity entity, PropertyInfo property)
        {
            // Khởi tạo kết nối với database:
            DbConnetionOpen();

            // lấy tên property:
            var propertyName = property.Name;
            // lấy giá trị property
            var propertyValue = property.GetValue(entity);

            var param = new DynamicParameters();
            param.Add($"@{propertyName}", propertyValue);
            var sql = $"SELECT * FROM { _className } WHERE { propertyName } = '{ propertyValue }'";
            var entityExist = _dbConnection.Query(sql, param: param).FirstOrDefault();
            if (entityExist != null)
            {
                //Đóng kết nối
                Dispose();
                return true;
            }

            //Đóng kết nối
            Dispose();
            return false;
        }

        /// <summary>
        /// Hàm tắt kết nối với DB khi để tiết kiệm tại tài nguyên
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        protected void Dispose()
        {
            if (_dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close();
            }
        }
        #endregion
    }
}
