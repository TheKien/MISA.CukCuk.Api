using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.CukCuk.Core.Enums.Enum;

namespace MISA.CukCuk.Core.Entities
{
    public class ServiceResult
    {
        /// <summary>
        /// Dữ liệu trả về của service
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public object Data { get; set; }

        /// <summary>
        /// Thông báo kết quả trả về của service
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public string Messenger { get; set; }

        /// <summary>
        /// Mã Code của service
        /// </summary>
        /// CreatedBy: TTKien(14/01/2022)
        public StatusCode StatusCode { get; set; }
    }
}
