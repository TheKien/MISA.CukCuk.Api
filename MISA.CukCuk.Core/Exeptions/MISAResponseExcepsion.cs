using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MISA.CukCuk.Core.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.CukCuk.Core.Enums.Enum;

namespace MISA.CukCuk.Core.Exceptions
{
    public class MISAResponseException : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is MISAResponseNotValidException exception)
            {
                var result = new
                {
                    devMsg = MISA.CukCuk.Core.Properties.Resources.Exception_Msg_InValid,
                    userMsg = MISA.CukCuk.Core.Properties.Resources.Exception_User_Msg_Error,
                    data = exception.Value,
                    moreInfo = ""
                };
                context.Result = new ObjectResult(result)
                {
                    StatusCode = (int)StatusCode.ErrorBadRequest    
                };
                context.ExceptionHandled = true;
            }

            else if (context.Exception is MISAResponseHttpExcepsion httpException)
            {
                var result = new
                {
                    devMsg = context.Exception.Message,
                    userMsg = MISA.CukCuk.Core.Properties.Resources.Exception_User_Msg_Error,
                    data = httpException.Value,
                    moreInfo = ""
                };
                context.Result = new ObjectResult(result)
                {
                    StatusCode = httpException.StatusCode,
                };
                context.ExceptionHandled = true;
            }

            else if (context.Exception != null)
            {
                var result = new
                {
                    devMsg = context.Exception.Message,
                    userMsg = MISA.CukCuk.Core.Properties.Resources.Exception_User_Msg_Error,
                    data = DBNull.Value,
                    moreInfo = ""
                };

                context.Result = new ObjectResult(result)
                {
                    StatusCode = (int)StatusCode.ErrorInternalServer
                };

                context.ExceptionHandled = true;
            }
        }
    }
}
