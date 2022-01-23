using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interfaces.Infrastructure;
using MISA.CukCuk.Core.Interfaces.Service;
using System;

namespace MISA.CukCuk.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<TEntity> : ControllerBase
    {
        readonly IBaseService<TEntity> _baseService;
        public BaseController(IBaseService<TEntity> baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// Lầy toàn bộ bản ghi
        /// </summary>
        /// <returns>Toàn bộ bản ghi</returns>
        /// CreateBy: TTKien(14/01/2022)
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var entites = _baseService.Get();
                return Ok(entites);
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Lấy 1 bản ghi theo khoá chính
        /// </summary>
        /// <returns>Trả về 1 bản ghi cần lấy</returns>
        /// CreateBy: TTKien(14/01/2022)
        [HttpGet("{entityId}")]
        public IActionResult GetById([FromRoute] string entityId)
        {
            try
            {
                var entity = _baseService.Get(entityId);
                return Ok(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Thêm 1 bản ghi vào database
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <returns>Thêm bản ghi thành công</returns>
        /// CreateBy: TTKien(14/01/2022)
        [HttpPost]
        public IActionResult Inser([FromBody] TEntity entity)
        {
            try
            {
                ServiceResult serviceResult = _baseService.Insert(entity);
                return Ok(serviceResult);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Sửa 1 bản ghi theo khoá chính
        /// </summary>
        /// <param name="entity">Khoá chính của bản ghi cần sửa đổi</param>
        /// <param name="entityId">Đối tượng cần thêm</param>
        /// <returns>Bản ghi được cập nhật thành công</returns>
        /// CreateBy: TTKien(14/01/2022)
        [HttpPut("{entityId}")]
        public IActionResult Update([FromBody] TEntity entity, [FromRoute] Guid entityId)
        {
            try
            {
                ServiceResult serviceResult = _baseService.Update(entity, entityId);
                return Ok(serviceResult);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lấy 1 bản ghi theo khoá chính
        /// </summary>
        /// <returns>Trả về 1 bản ghi cần lấy</returns>
        /// CreateBy: TTKien(14/01/2022)
        [HttpDelete("{entityId}")]
        public IActionResult Delete([FromRoute]string entityId)
        {
            try
            {
                ServiceResult serviceResult = _baseService.Delete(entityId);
                return Ok(serviceResult);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
