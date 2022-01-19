using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Enums
{
    public class Enum
    {
        /// <summary>
        /// Thực thi lấy ra Resources tương ứng với Enum
        /// </summary>
        /// <typeparam name="T">Đối tượng Enum</typeparam>
        /// <param name="misaEnum">Đối tượng Enum</param>
        /// <returns>Resources tương ứng</returns>
        /// CreatedBy: TTKien(14/01/2022)
        public static string GetEnumTextByEnumName<T>(T misaEnum)
        {
            var enumPropertyName = misaEnum.ToString();
            var enumName = misaEnum.GetType().Name;
            var resourceText = Properties.Resources.ResourceManager.GetString($"Enum_{enumName}_{enumPropertyName}");
            return resourceText;
        }

        /// <summary>
        /// StatusCode để xác định trạng thái
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public enum StatusCode
        {
            /// <summary>
            /// Dữ liệu thành công
            /// </summary>
            Success = 200,
            /// <summary>
            /// Thêm bản ghi thành công
            /// </summary>
            Created = 201,
            /// <summary>
            /// Không nội dung
            /// </summary>
            NoContent = 204,
            /// <summary>
            /// Lỗi dữ liệu không hợp lệ
            /// </summary>
            ErrorBadRequest = 400,

            /// <summary>
            /// Lỗi Internal Server
            /// </summary>
            ErrorInternalServer = 500,
        }

        /// <summary>
        /// Trạng thái của đối tượng
        /// </summary>
        public enum EditMode
        {
            /// <summary>
            /// Không
            /// </summary>
            None = 0,
            /// <summary>
            /// Thêm mới
            /// </summary>
            Add = 1,
            /// <summary>
            /// Sửa
            /// </summary>
            Update = 2,
            /// <summary>
            /// Xoá
            /// </summary>
            Delete = 3
        }

        /// <summary>
        /// Trạng thái xoá
        /// </summary>
        public enum DeleteRule
        {
            /// <summary>
            /// Không làm gì khi dữ liệu cha bị xoá
            /// </summary>
            NoAction = 0,
            /// <summary>
            /// Xoá con khi cha bị xoá
            /// </summary>
            Cascade = 1,
            /// <summary>
            /// Con null khi cha bị xoá
            /// </summary>
            SetNull = 2,
            /// <summary>
            /// Chuyển thành dữ liệu mặc định khi cha bị xoá
            /// </summary>
            Retrict = 3
        }
    }
}
