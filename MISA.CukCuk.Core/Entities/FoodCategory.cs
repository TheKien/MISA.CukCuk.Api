using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Entities
{
    public class FoodCategory : BaseEntity
    {
        /// <summary>
        /// Khoá chính
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public Guid FoodCategoryId { get; set; }

        /// <summary>
        /// Tên loại món ăn
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public string FoodCategoryName { get; set; }
    }
}
