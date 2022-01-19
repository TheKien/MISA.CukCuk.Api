using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Attributes
{
    public class AttributeKey
    {
        /// <summary>
        /// Atrribute cung cấp cho các property khoá chính - sử dụng để đánh dấu
        /// </summary>
        /// CreateBy: TTKien(14/01/2022)
        [AttributeUsage(AttributeTargets.Property)]
        public class Key : Attribute
        {
        }

        /// <summary>
        /// Atrribute cung cấp cho các property khoá ngoại - sử dụng để đánh dấu
        /// </summary>
        /// CreateBy: TTKien(14/01/2022)
        [AttributeUsage(AttributeTargets.Property)]
        public class ForeigeKey : Attribute
        {
        }
    }
}
