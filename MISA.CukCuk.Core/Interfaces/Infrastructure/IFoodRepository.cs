using MISA.CukCuk.Core.Dtos;
using MISA.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interfaces.Infrastructure
{
    public interface IFoodRepository : IBaseRepository<Food>
    {
        /// <summary>
        /// Phân trang tìm kiếm và sắp xếp
        /// </summary>
        /// <param name="pageIndex">Số trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi hiển thị trên 1 trang</param>
        /// <param name="objectFilters">Danh sách điệu kiện tìm kiếm</param>
        /// <returns>Danh sách bản ghi cần tìm kiếm</returns>
        /// CreatedBy: TTKien(16/01/2022)
        object GetPagingFilterSort(int pageIndex, int pageSize, List<ObjectFilter> objectFilters, ObjectSort objectSort);
    }
}
