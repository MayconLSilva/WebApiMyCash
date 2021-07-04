using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCash
{
    internal class NotFoundResponseExample
    {
        public object GetExamples()
        {
            return new ErrorResponse { ErrorCode = 404, Message = "Could not find the resource" };
        }
    }
}