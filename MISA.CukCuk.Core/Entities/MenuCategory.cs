using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Entities
{
    public class MenuCategory : BaseEntity
    {
        /// <summary>
        /// Khoá chính
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public Guid MenuCategoryId { get; set; }

        /// <summary>
        /// Tên nhóm thực đơn
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public string MenuCategoryName { get; set; }
    }
}
