using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.CukCuk.Core.Attributes.AttributeCRUD;
using static MISA.CukCuk.Core.Enums.Enum;

namespace MISA.CukCuk.Core.Entities
{
    public class BaseEntity
    {
        /// <summary>
        /// Thời gian tạo bản ghi
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        [NotUpdated]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Tên người người tạo bản ghi
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        [NotUpdated]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Thời gian sửa đổi bản ghi lần gần nhất
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Tên người sửa đổi bản ghi lần gần nhất
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Chế độ chỉnh sửa
        /// </summary>
        [ReadOnly]
        public EditMode EditMode { get; set; }
    }
}
