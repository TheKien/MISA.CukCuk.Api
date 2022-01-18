using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Entities
{
    public class Modifier : BaseEntity
    {
        /// <summary>
        /// Khoá chính
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public Guid ModifierId { get; set; }

        /// <summary>
        /// Tên loại món ăn
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public string ModifierName { get; set; }

        /// <summary>
        /// Phí bổ sung
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public int AdditionalCharge { get; set; }  
    }
}
