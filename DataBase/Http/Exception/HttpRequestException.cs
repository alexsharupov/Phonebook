using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Http.Exception
{
    internal class HttpUserRequestException : HttpRequestException
    {
        public HttpUserRequestException(HttpRequestException e) : base($"Error {e.Message}") { }
    }
}
