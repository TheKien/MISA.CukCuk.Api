using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Core.Interfaces.Infrastructure;
using MISA.CukCuk.Core.Interfaces.Service;
using System;

namespace MISA.CukCuk.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<Entity> : ControllerBase
    {
        IBaseService<Entity> _baseService;
        public BaseController(IBaseService<Entity> baseService)
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
            catch (Exception ex)
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
        public IActionResult GetById(string entityId)
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
        public IActionResult Insert(Entity entity)
        {
            var serviceResult = _baseService.Insert(entity);
            return Ok(serviceResult);
        }

        /// <summary>
        /// Sửa 1 bản ghi theo khoá chính
        /// </summary>
        /// <param name="entity">Khoá chính của bản ghi cần sửa đổi</param>
        /// <param name="entityId">Đối tượng cần thêm</param>
        /// <returns>Bản ghi được cập nhật thành công</returns>
        /// CreateBy: TTKien(14/01/2022)
        [HttpPut("{entityId}")]
        public IActionResult Update([FromBody] Entity entity, [FromRoute] Guid entityId)
        {
            var serviceResult = _baseService.Update(entity, entityId);
            return Ok(serviceResult);
        }

        /// <summary>
        /// Lấy 1 bản ghi theo khoá chính
        /// </summary>
        /// <returns>Trả về 1 bản ghi cần lấy</returns>
        /// CreateBy: TTKien(14/01/2022)
        [HttpDelete("{entityId}")]
        public IActionResult Delete(string entityId)
        {
            var serviceResult = _baseService.Delete(entityId);
            return Ok(serviceResult);

        }
    }
}
