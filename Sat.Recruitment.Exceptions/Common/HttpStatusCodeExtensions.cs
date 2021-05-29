using System.Net;

namespace Sat.Recruitment.Exceptions.Common
{
    public static class HttpStatusCodeExtensions
    {
        public static int GetValue(this HttpStatusCode httpStatusCode)
        {
            return (int)httpStatusCode;
        }

        public static bool IsSuccessStatusCode(this HttpStatusCode httpStatusCode)
        {
            return (httpStatusCode.GetValue() >= 200 && httpStatusCode.GetValue() <= 299);
        }

        public static string GetDefaultMessage(this HttpStatusCode httpStatusCode)
        {
            switch (httpStatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    return "Unauthorized Message";
                case HttpStatusCode.BadRequest:
                    return "BadRequest Message";
                case HttpStatusCode.NotFound:
                    return "NotFound Message";
                case HttpStatusCode.InternalServerError:
                    return "InternalServerError Message";
                default:
                    return "";
            }
        }

        public static string GetDefaultExceptionMessage(this HttpStatusCode httpStatusCode)
        {
            switch (httpStatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    return "unauthorized";
                case HttpStatusCode.BadRequest:
                    return "bad_request";
                case HttpStatusCode.NotFound:
                    return "not_found";
                case HttpStatusCode.InternalServerError:
                    return "internal_server_error";
                default:
                    return "";
            }
        }
    }
}
