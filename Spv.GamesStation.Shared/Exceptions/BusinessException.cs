using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;

namespace Spv.GamesStation.Shared.Exceptions
{
    [Serializable]
    public class BusinessException : System.Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        /// <summary>
        ///     ErrorCodeNumber
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        ///     BusinessException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="statusCode"></param>
        public BusinessException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public BusinessException(string message) : this(message, HttpStatusCode.UnprocessableEntity)
        {
        }


        public BusinessException(IDictionary<string, string[]> errores, HttpStatusCode statusCode, string message) : base(message)
        {
            Errors = errores;
            StatusCode = statusCode;
        }

        protected BusinessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class ValidacionCabeceraArchivoIncorrectoException : System.Exception
    {
        public ValidacionCabeceraArchivoIncorrectoException(string message) : base(message)
        {
        }

        protected ValidacionCabeceraArchivoIncorrectoException(SerializationInfo info, StreamingContext context) : base(
            info, context)
        {
        }
    }

    [Serializable]
    public class ValidacionLoteIncorrectoException : System.Exception
    {
        public ValidacionLoteIncorrectoException(string message) : base(message)
        {
        }

        protected ValidacionLoteIncorrectoException(SerializationInfo info, StreamingContext context) : base(info,
            context)
        {
        }
    }
}
