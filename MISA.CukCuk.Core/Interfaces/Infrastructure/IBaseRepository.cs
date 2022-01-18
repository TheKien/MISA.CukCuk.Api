using MISA.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Infrastructure
{
    public interface IBaseRepository<TEntity>
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
        TEntity Get(Guid entityId);

        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm mới</param>
        /// <returns>Thêm mới bản ghi thành công</returns>
        /// CreatedBy: TTKien(14/01/2021)
        int Insert(TEntity entity);

        /// <summary>
        /// Thêm mới 1 bản ghi sử dụng transaction
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm mới</param>
        /// <returns>Thêm mới bản ghi thành công</returns>
        /// CreatedBy: TTKien(14/01/2021)
        int Insert(TEntity entity, IDbTransaction transaction);

        /// <summary>
        /// Cập nhật thông tin 1 bản ghi trong bảng thông qua khoá chính của bản ghi (id)
        /// </summary>
        /// <param name="entity">Thông tin mới cần cập nhật</param>
        /// <param name="entityId">Id để lấy về đối tượng cần cập nhật</param>
        /// <returns>Cập nhật bản ghi thành công</returns>
        /// CreatedBy: TTKien(14/01/2021)
        int Update(TEntity entity, Guid entityId);

        /// <summary>
        /// Xoá 1 bản ghi theo khoá chính
        /// </summary>
        /// <param name="entityId">Khoá chính để lấy bản ghi cần xoá</param>
        /// <returns>Xoá 1 bản ghi thành công</returns>
        /// CreatedBy: TTKien(14/01/2021)
        int Delete(string entityId);

        /// <summary>
        /// Kiểm tra giá trị 1 thuộc tính của 1 đối tượng đã tồn tại trong database chưa?
        /// </summary>
        /// <param name="propertyName">Tên thuộc tính</param>
        /// <param name="propertyValue">Giá trị của tính</param>
        /// <returns>
        /// True - Nếu trả về đối tượng có giá trị của thuộc tính đó đã tồn trong database. 
        /// False - Nếu không trả về đối tượng nào
        /// </returns>
        /// CreatedBy: TTKien(14/01/2021)
        bool IsExist(TEntity entity, PropertyInfo property);
    }
}
