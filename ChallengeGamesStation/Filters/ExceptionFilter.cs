using System.Diagnostics.CodeAnalysis;
using System.Net;
using ChallengeGamesStation.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Spv.GamesStation.Shared.Exceptions;

namespace ChallengeGamesStation.Filters
{
    [ExcludeFromCodeCoverage]
    public class ExceptionFilter : IExceptionFilter
    {
       
        public void OnException(ExceptionContext context)
        {
            ErrorDetailModel errorDetail;
            int errorCode;
            if (context.Exception is AggregateException agrException)
            {

                errorDetail = GetErrorDetail(agrException.InnerException, context.HttpContext.TraceIdentifier);
                errorCode = GetErrorCode(agrException.InnerException);
            }
            else
            {
                errorDetail = GetErrorDetail(context.Exception, context.HttpContext.TraceIdentifier);
                errorCode = GetErrorCode(context.Exception);
            }

            context.Result = new ObjectResult(errorDetail);
            context.HttpContext.Response.StatusCode = errorCode;
        }

        private ErrorDetailModel GetErrorDetail(System.Exception exception, string httpContextTraceIdentifier)
        {
            var errorDetail = new ErrorDetailModel
            {
                State = GetState(exception),
                Code = GetErrorCode(exception)
            };

            errorDetail.Detail = exception.Message;

            if (exception is BusinessException businessException && businessException.Errors != null)
            {

                foreach (var item in businessException.Errors)
                {

                    errorDetail.Errors.Add(new Error
                    {
                        Code = GetErrorCode(exception).ToString(), //todo:ver codigo interno
                        Title = item.Key,
                        Detail = string.Join(',', item.Value),
                        Source = exception.Source,
                        SpvTrackId = httpContextTraceIdentifier
                    });
                }
            }
            else
            {
                errorDetail.Errors.Add(new Error
                {
                    Code = GetErrorCode(exception).ToString(), //todo:ver codigo interno
                    Title = exception.Message,
                    Detail = exception.InnerException?.Message ?? string.Empty,
                    Source = exception.Source,
                    SpvTrackId = httpContextTraceIdentifier
                });
            }

            return errorDetail;
        }

        private int GetErrorCode(System.Exception exception)
        {
            var exceptionType = exception.GetType();

            switch (exceptionType.Name)
            {
                case nameof(BusinessException):
                    {
                        var data = (BusinessException)exception;

                        return (int)data.StatusCode;
                    }
                case nameof(System.Exception):
                    {
                        return 500;
                    }
                default:
                    return 500;
            }
        }

        private string GetState(System.Exception exception)
        {
            var exceptionType = exception.GetType();

            switch (exceptionType.Name)
            {
                case nameof(BusinessException):
                    {
                        return HttpStatusCode.UnprocessableEntity.ToString();
                    }
                case nameof(System.Exception):
                    {
                        return HttpStatusCode.InternalServerError.ToString();
                    }
                default:
                    return HttpStatusCode.InternalServerError.ToString();
            }
        }



    }
}
