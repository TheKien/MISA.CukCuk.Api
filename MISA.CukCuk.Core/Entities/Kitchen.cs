using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Entities
{
    public class Kitchen : BaseEntity
    {
        /// <summary>
        /// Khoá chính
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public Guid KitchenId { get; set; }

        /// <summary>
        /// Tên bếp
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public string KitchenName { get; set; }
    }
}
