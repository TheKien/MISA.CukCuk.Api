using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Exeptions
{
    public class MISAResponseHttpExcepsion : Exception
    {
        public MISAResponseHttpExcepsion(object value = null, int statusCode = 500)
        {
            (Value) = (value);
            (StatusCode) = (statusCode);
        }
        public int StatusCode { get; set; } 

        public object Value { get; set; }
    }
}
