using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCash
{
    internal class ErrorResponse
    {
        public int ErrorCode { get; set; }

        public string Message { get; set; }
    }
}