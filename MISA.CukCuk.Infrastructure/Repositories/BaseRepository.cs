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
using static MISA.CukCuk.Core.Attributes.AttributeKey;
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
        /// Hảm mở kết nối với Database
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        protected void DbConnetionOpen()
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
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

        #region Methods
        public virtual IEnumerable<TEntity> Get()
        {
            DbConnetionOpen();
            var storeName = string.Format(ProcGetAll, _className);
            var entites = _dbConnection.Query<TEntity>(storeName, commandType: CommandType.StoredProcedure);
            return entites;
        }

        public virtual TEntity Get(Guid entityId)
        {
            DbConnetionOpen();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"${_className}Id", entityId);
            var storeName = string.Format(ProcGetById, _className);
            var entity = _dbConnection.QueryFirstOrDefault(storeName, parameters, commandType: CommandType.StoredProcedure);
            return entity;
        }

        public virtual int Insert(BaseEntity entity)
        {
            DbConnetionOpen();
            var parameters = MappingDbType(entity);
            var storeName = string.Format(ProcInsert, entity.GetType().Name);
            var rowEffects = _dbConnection.Execute(storeName, parameters, commandType: CommandType.StoredProcedure);
            return rowEffects;
        }

        public virtual int Insert(BaseEntity entity, IDbTransaction transaction)
        {
            var parameters = MappingDbType(entity);
            var storeName = string.Format(ProcInsert, entity.GetType().Name);
            var connection = transaction.Connection;
            var rowEffects = connection.Execute(storeName, parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
            return rowEffects;
        }

        public virtual int Update(BaseEntity entity, Guid entityId)
        {
            DbConnetionOpen();
            var parameters = MappingDbType(entity);
            var storeName = string.Format(ProcUpdate, entity.GetType().Name);
            var rowEffects = _dbConnection.Execute(storeName, parameters, commandType: CommandType.StoredProcedure);
            return rowEffects;
        }

        public virtual int Update(BaseEntity entity, IDbTransaction transaction)
        {
            var parameters = MappingDbType(entity);
            var storeName = string.Format(ProcUpdate, entity.GetType().Name);
            var connection = transaction.Connection;
            var rowEffects = connection.Execute(storeName, parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
            return rowEffects;
        }

        public virtual int Delete(Guid entityId)
        {
            DbConnetionOpen();
            DynamicParameters parameters = new DynamicParameters();
            var where = $"{_className}Id = '{entityId}'";
            parameters.Add("$Where", where);
            parameters.Add("$TableName", _className);
            var storeName = string.Format(ProcDelete, _className);
            var rowEffects = _dbConnection.Execute(storeName, parameters, commandType: CommandType.StoredProcedure);
            return rowEffects;
        }

        public virtual int Delete(BaseEntity entity, IDbTransaction transaction)
        {
            DynamicParameters parameters = new DynamicParameters();
            //var propertyId = entity.GetType().GetProperties().Where(p => p.IsDefined(typeof(Key), true)));
            //var where = $"{entity.GetType().Name}Id = '{propertyId.GetValue(entity)}'";

            //if (propertyId == null)
            //{
            var where = string.Empty;
            var foreignKeys = entity.GetType().GetProperties().Where(p => p.IsDefined(typeof(ForeigeKey), true)).ToList();
            foreach (var fk in foreignKeys)
            {
                where += $" {fk.Name}='{fk.GetValue(entity)}' AND";
            }
            where = where.Substring(0, where.LastIndexOf(" "));
            //}
            parameters.Add("$Where", where);
            parameters.Add("$TableName", entity.GetType().Name);
            var connection = transaction.Connection;
            var storeName = "Proc_DeleteEntity";
            var rowEffects = connection.Execute(storeName, parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
            return rowEffects;
        }

        /// <summary>
        /// Mapping dữ liệu và set giá trị parameter
        /// </summary>
        /// <param name="entity">Đối tượng mapping</param>
        /// <returns>parameters chứ giá trị của đối tượng</returns>
        private DynamicParameters MappingDbType(BaseEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();

            // Lấy ra các property tương ứng với đối tượng trừ các trường chỉ để hiển thị:
            var properties = entity.GetType().GetProperties().Where(p => !p.IsDefined(typeof(ReadOnly), true)).ToList();
            // Nếu là update bỏ qua các trường không được update
            if (entity.EditMode == Core.Enums.Enum.EditMode.Update)
            {
                properties = properties.Where(p => !p.IsDefined(typeof(NotUpdated), true)).ToList();
            }
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
            var propertyName = property.Name;
            var propertyValue = property.GetValue(entity);
            DynamicParameters parameters = new DynamicParameters();
            // build where
            string where;
            if (property.PropertyType == typeof(int)) { where = $"{ propertyName } = { propertyValue }"; }
            else { where = $"{ propertyName } = '{ propertyValue }'"; }
            parameters.Add("$Where", where);
            parameters.Add("$TableName", _className);
            var storeName = "Proc_GetEntityByProperty";
            var entityExist = _dbConnection.QueryFirstOrDefault(storeName, parameters, commandType: CommandType.StoredProcedure);
            if (entityExist == null) return false;
            return true;
        }

        #endregion
    }
}
