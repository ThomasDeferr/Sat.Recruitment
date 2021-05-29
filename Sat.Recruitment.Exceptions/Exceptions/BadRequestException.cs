using System.Net;
using Sat.Recruitment.Exceptions.Models;

namespace Sat.Recruitment.Exceptions.Controller
{
    public class BadRequestException : HttpResponseException
    {
        public BadRequestException() : this(null, null) 
        { 
        }

        public BadRequestException(string message) : this(message, null) 
        { 
        }

        public BadRequestException(string message, string error) 
            : base(HttpStatusCode.BadRequest, message, error) 
        { 
        }
    }
}
