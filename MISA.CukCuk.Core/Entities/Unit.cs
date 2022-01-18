using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Entities
{
    public class Unit : BaseEntity
    {
        /// <summary>
        /// Khoá chính
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public Guid UnitId { get; set; }

        /// <summary>
        /// Tên loại món ăn
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public string UnitName { get; set; }
    }
}
