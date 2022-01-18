using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Entities
{
    public class Image
    {
        /// <summary>
        /// Khoá chính
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public Guid KitchenId { get; set; }

        /// <summary>
        /// Tên file
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public string KitchenName { get; set; }
    }
}
