using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Attributes
{
    public class AttributeValidate
    {
        /// <summary>
        /// Atrribute để đặt tên cho property - sử dụng để đặt tên
        /// </summary>
        /// CreateBy: TTKien(14/01/2022)
        [AttributeUsage(AttributeTargets.Property)]
        public class PropertyName : Attribute
        {
            public string Name;
            public PropertyName(string name)
            {
                this.Name = name;
            }
        }
        /// <summary>
        /// Atrribute cung cấp cho các property bắt buộc nhập - sử dụng để đánh dấu
        /// </summary>
        /// CreateBy: TTKien(14/01/2022)
        [AttributeUsage(AttributeTargets.Property)]
        public class NotEmpty : Attribute
        {
        }

        /// <summary>
        /// Atrribute cung cấp cho các property không được trùng trong database - sử dụng để đánh dấu
        /// </summary>
        /// CreateBy: TTKien(14/01/2022)
        [AttributeUsage(AttributeTargets.Property)]
        public class Unique : Attribute
        {
        }
        /// <summary>
        /// Atrribute cung cấp cho các property là số điện thoại - sử dụng để đánh dấu
        /// </summary>
        /// CreateBy: TTKien(21/12/2021)
        [AttributeUsage(AttributeTargets.Property)]
        public class PhoneNumber : Attribute
        {
        }

        /// <summary>
        /// Atrribute cung cấp cho các property là email - sử dụng để đánh dấu
        /// </summary>
        /// CreateBy: TTKien(21/12/2021)
        [AttributeUsage(AttributeTargets.Property)]
        public class Email : Attribute
        {
        }
    }
}
