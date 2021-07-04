using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCash
{
    internal class InternalServerResponseExample
    {
        public object GetExamples()
        {
            return new ErrorResponse { ErrorCode = 500, Message = "An unexpected error occurred" };
        }
    }
}