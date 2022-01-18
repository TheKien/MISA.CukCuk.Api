using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Attributes
{
    public class AttributeCRUD
    {
        /// <summary>
        /// Atrribute cung cấp cho các property chỉ dùng để hiển thị - sử dụng để đánh dấu
        /// </summary>
        /// CreateBy: TTKien(14/01/2022)
        [AttributeUsage(AttributeTargets.Property)]
        public class ReadOnly : Attribute
        {
        }

        /// <summary>
        /// Atrribute cung cấp cho các property không được sửa - sử dụng để đánh dấu
        /// </summary>
        /// CreateBy: TTKien(14/01/2022)
        [AttributeUsage(AttributeTargets.Property)]
        public class NotUpdated : Attribute
        {
        }
    }
}
