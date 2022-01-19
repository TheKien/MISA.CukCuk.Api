using MISA.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Service
{
    public interface IBaseService<TEntity>
    {
        /// <summary>
        /// Lấy tất cả bản ghi của bảng
        /// </summary>
        /// <returns>Trả về tất cả bản ghi của bảng theo thứ tự ngày tạo gần đây nhất</returns>
        /// CreatedBy: TTKien(14/01/2021)
        IEnumerable<TEntity> Get();

        /// <summary>
        /// Lấy 1 bản ghi theo khoá chính
        /// </summary>
        /// <param name="entityId">Khoá chính của bản ghi cần lấy</param>
        /// <returns>Trả về 1 bản ghi cần lấy</returns>
        /// CreatedBy: TTKien(14/01/2021)
        object Get(string entityId);

        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm mới</param>
        /// <returns>Thêm mới bản ghi thành công</returns>
        /// CreatedBy: TTKien(14/01/2022)
        ServiceResult Insert(TEntity entity);

        /// <summary>
        /// Cập nhật thông tin 1 bản ghi trong bảng thông qua khoá chính của bản ghi (id)
        /// </summary>
        /// <param name="entity">Thông tin mới cần cập nhật</param>
        /// <param name="entityId">Id để lấy về đối tượng cần cập nhật</param>
        /// <returns>Cập nhật bản ghi thành công</returns>
        /// CreatedBy: TTKien(14/01/2022)
        ServiceResult Update(TEntity entity, Guid entityId);

        /// <summary>
        /// Xoá 1 bản ghi theo khoá chính
        /// </summary>
        /// <param name="entityId">Khoá chính</param>
        /// <returns>Xoá bản ghi thành công</returns>
        /// CreatedBy: TTKien(14/01/2022)
        ServiceResult Delete(string entityId);
    }
}
