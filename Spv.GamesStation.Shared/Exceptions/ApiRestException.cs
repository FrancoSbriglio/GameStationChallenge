using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace Spv.GamesStation.Shared.Exceptions
{
    [Serializable]
    public class ApiRestException : HttpResponseException
    {
        private const string ExceptionInner = "InnerException";

        public ApiRestException(HttpError httpError, HttpResponseMessage response) : base(response)
        {
            HttpError = httpError;
        }

        public ApiRestException(HttpResponseMessage response) : base(response)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the System.Web.Http.HttpResponseException class.
        /// </summary>
        /// <param name="statusCode"></param>
        public ApiRestException(HttpStatusCode statusCode) : base(statusCode)
        {
        }

        public HttpError HttpError { get; }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine + Unwind(HttpError);
        }

        /// <summary>
        ///     Recursively unwind the HttpError
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        private string Unwind(HttpError error)
        {
            var rv = string.Empty;
            if (error != null)
            {
                rv =
                    $"{Environment.NewLine}------Exception from Server-----{Environment.NewLine}{Environment.NewLine}{string.Join(Environment.NewLine, error.Where(g => g.Key != ExceptionInner).Select(g => $"{g.Key}:  {g.Value}"))}";

                if (error.InnerException != null)
                {
                    rv = rv + Unwind(error.InnerException);
                }
                else if (error.ContainsKey(ExceptionInner))
                {
                    if (error[ExceptionInner] is JObject inner)
                        try
                        {
                            rv = rv + Unwind(inner.ToObject<HttpError>());
                        }
                        catch
                        {
                            //In case the inner exception is not an HttpError, we will
                            //simply do a ToString() to get the text
                            rv = rv + error[ExceptionInner];
                        }
                    else
                        //In case the inner exception is not an HttpError, we will
                        //simply do a ToString() to get the text
                        rv = rv + error[ExceptionInner];
                }
            }

            return rv;
        }
    }
}
