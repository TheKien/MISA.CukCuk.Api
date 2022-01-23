using MISA.CukCuk.Core.Interfaces.Infrastructure;
using MISA.CukCuk.Core.Interfaces.Service;
using MISA.CukCuk.Core.Properties;
using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MISA.CukCuk.Core.Attributes.AttributeCRUD;
using static MISA.CukCuk.Core.Attributes.AttributeValidate;

namespace MISA.CukCuk.Core.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity>
    {
        #region Declare
        IBaseRepository<TEntity> _baseRepository;
        ServiceResult _serviceResult;
        #endregion

        #region Contructor
        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult() { Status = Resources.Status_Success };
        }
        #endregion

        #region Methods
        public IEnumerable<TEntity> Get()
        {
            return _baseRepository.Get();
        }

        public object Get(string entityId)
        {
            var guidEntityId = ConvertStringToGuid(entityId);
            if (guidEntityId == Guid.Empty)
            {
                _serviceResult.Data = string.Empty;
                _serviceResult.Messenger = Resources.Error_Msg_InValid_Guid;
                _serviceResult.Status = Resources.Status_Warning;
                return _serviceResult;
            }
            var entity = _baseRepository.Get(guidEntityId);
            if (entity == null)
            {
                _serviceResult.Data = string.Empty;
                _serviceResult.Messenger = Resources.Warning_Msg_Entity_Null;
                _serviceResult.Status = Resources.Status_Warning;
                return _serviceResult;
            }
            return entity;
        }


        public virtual ServiceResult Insert(TEntity entity)
        {
            // validate chung - base xử lý:
            var msgErr = Validate(entity, Guid.Empty);
            // validate đặc thù cho từng đối tượng -> các service con tự xử lý:
            if (msgErr.Count == 0)
            {
                msgErr = ValidateCustom(entity);
            }
            if (msgErr.Count == 0)
            {
                BaseEntity baseEntity = entity as BaseEntity;
                var rowEffecs = _baseRepository.Insert(baseEntity);
                _serviceResult.Data = entity;
                _serviceResult.Messenger = string.Format(Resources.Success_Msg_Insert, rowEffecs);
            }
            else
            {
                _serviceResult.Data = msgErr;
                _serviceResult.Messenger = string.Format(Resources.Success_Msg_Insert, 0);
                _serviceResult.Status = Resources.Status_Warning;
            }
            return _serviceResult;
        }

        public virtual ServiceResult Update(TEntity entity, Guid entityId)
        {
            // validate chung - base xử lý:
            var msgErr = Validate(entity, entityId);
            // validate đặc thù cho từng đối tượng -> các service con tự xử lý:
            if (msgErr.Count == 0)
            {
                msgErr = ValidateCustom(entity);
            }
            if (msgErr.Count == 0)
            {
                BaseEntity baseEntity = entity as BaseEntity;
                _serviceResult.Data = _baseRepository.Update(baseEntity, entityId);
                List<string> msgList = new List<string>();
                _serviceResult.Messenger = string.Format(Resources.Success_Msg_Update, _serviceResult.Data);
            }
            else
            {
                _serviceResult.Data = msgErr;
                _serviceResult.Messenger = string.Format(Resources.Success_Msg_Update, 0);
                _serviceResult.Status = Resources.Status_Warning;
            }
            return _serviceResult;
        }

        public ServiceResult Delete(string entityId)
        {
            var guidEntityId = ConvertStringToGuid(entityId);
            if (guidEntityId == Guid.Empty)
            {
                _serviceResult.Data = 0;
                _serviceResult.Messenger = Resources.Error_Msg_InValid_Guid;
            }
            else
            {
                _serviceResult.Data = _baseRepository.Delete(guidEntityId);
                _serviceResult.Messenger = Resources.HttpCode_200;
            }
            _serviceResult.Data = _baseRepository.Delete(guidEntityId);
            _serviceResult.Messenger = Resources.HttpCode_200;
            return _serviceResult;

        }

        /// <summary>
        /// Convert string thành mã guid
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private Guid ConvertStringToGuid(string str)
        {
            if (str.Length == 36)
            {
                return Guid.Parse(str);
            }
            else
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Validate dữ liệu chung
        /// </summary>
        /// <param name="entity">Thông tin đối tượng kiểm tra</param>
        /// <returns>True - Nếu đối tượng thoả mãn các điều kiện, False - Nếu đối tượng không thoá mãn ít nhất 1 điều kiện</returns>
        /// CreatedBy: TTKien(14/01/2022)
        protected List<string> Validate(TEntity entity, Guid entityId)
        {
            List<string> msgErr = new List<string>();
            var entityOld = new Object();
            var properties = typeof(TEntity).GetProperties().Where(p => !p.IsDefined(typeof(ReadOnly), true)).ToList();
            if (entityId != Guid.Empty)
            {
                entityOld = _baseRepository.Get(entityId);
                properties = properties.Where(p => !p.IsDefined(typeof(NotUpdated), true)).ToList();
            }
            if (entityOld == null)
            {
                msgErr.Add(Resources.Exceptione_Msg_IsExistEntity);
                return msgErr;
            }
            foreach (var prop in properties)
            {
                var propNameOrginal = prop.Name;
                var propNameDisplay = propNameOrginal;
                var propValue = prop.GetValue(entity);
                var propPropertyNames = prop.GetCustomAttributes(typeof(PropertyName), true);
                // Gắn tên hiển thị
                if (propPropertyNames.Length > 0)
                {
                    propNameDisplay = (propPropertyNames[0] as PropertyName).Name;
                }

                // Nếu property có attibute để đánh dấu bắt buộc (NotEmpty):
                if (prop.IsDefined(typeof(NotEmpty), true))
                {
                    // Nếu thông tin bắt buộc nhập thì hiển thị cảnh báo hoặc đánh dấu trạng thái không hợp lệ:
                    if (propValue == null || string.IsNullOrEmpty(propValue.ToString().Trim()))
                    {
                        msgErr.Add(string.Format(Resources.Error_Msg_InValid_NotEmty, propNameDisplay));
                    }
                }

                if (entityId != Guid.Empty)
                {
                    // Lấy giá trị cũ tương ứng với đối tượng
                    var propValueOld = prop.GetValue(entityOld);
                    if (propValue == null|| propValueOld == null)
                    {
                        continue;
                    }
                    // So sánh
                    if (String.Compare(propValue.ToString().Trim(), propValueOld.ToString().Trim()) == 0)
                    {
                        continue;
                    }
                }

                // Nếu property không null
                if (!(propValue == null || string.IsNullOrEmpty(propValue.ToString().Trim())))
                {
                    // Kiểm tra tồn tại:
                    if (prop.IsDefined(typeof(Unique), true))
                    {
                        // Kiểm tra csdl:
                        bool isExist = _baseRepository.IsExist(entity, prop);
                        // Nếu giá trị đã tồn tại trong csdl:
                        if (isExist)
                        {
                            msgErr.Add(string.Format(Resources.Error_Msg_InValid_Unique, propNameDisplay, propValue));
                        }
                    }

                }
            }
            return msgErr;
        }

        /// <summary>
        /// Validate dữ liệu đặc thù của từng đối tượng
        /// </summary>
        /// <param name="entity">Thông tin đối tượng kiểm tra</param>
        /// <returns>True - Nếu đối tượng thoả mãn các điều kiện, False - Nếu đối tượng không thoá mãn điều kiện</returns>
        /// CreatedBy: TTKien(14/01/2022)
        protected virtual List<string> ValidateCustom(TEntity entity)
        {
            return new List<string>();
        }
        #endregion
    }
}
