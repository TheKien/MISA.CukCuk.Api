 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Enums
{
    public class EnumFood
    {
        /// <summary>
        /// Enum - Thứ tự món
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public enum FoodOrder
        {
            /// <summary>
            /// Món khai vị
            /// </summary>
            Appetizer = 0,

            /// <summary>
            /// Món chính
            /// </summary>
            MainCourse = 1,

            /// <summary>
            /// Món tráng miệng
            /// </summary>
            Dessert = 2
        }

        /// <summary>
        /// Enum - Hiển thị trên menu
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public enum FoodStatus
        {
            /// <summary>
            /// Hiển thị
            /// </summary>
            Display = 0,

            /// <summary>
            /// Không hiển thị
            /// </summary>
            NotDisplay = 1
        }
    }
}
