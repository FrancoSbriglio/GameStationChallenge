using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog;
using ServiceStack.Text;

namespace GamesStationChallenge.Middleware
{
    /// <summary>
    ///     RequestResponseLoggingMiddleware
    /// </summary>
    public class RequestResponseLoggingMiddleware
    {
        private const int ReadChunkBufferLength = 4096;
        private readonly RequestDelegate _next;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        /// <summary>
        ///     RequestResponseLoggingMiddleware
        /// </summary>
        /// <param name="next"></param>
        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        /// <summary>
        ///     Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            var versionList = new List<string>
            {
                "/v1/",
                "/v1.0/"
            };

            if (versionList.Any(f => context.Request.Path.ToString().Contains(f)))
            {
                var model = new RequestProfilerModel
                {
                    RequestTime = new DateTimeOffset(),
                    Request = await FormatRequest(context)
                };

                var originalBody = context.Response.Body;
                Log.Logger.Debug("Api-Request: {@request}", model.Request);

                using (var newResponseBody = _recyclableMemoryStreamManager.GetStream())
                {
                    context.Response.Body = newResponseBody;
                    await _next(context);
                    newResponseBody.Seek(0, SeekOrigin.Begin);
                    await newResponseBody.CopyToAsync(originalBody);
                    newResponseBody.Seek(0, SeekOrigin.Begin);
                    model.Response = FormatResponse(context, newResponseBody);
                    model.ResponseTime = new DateTimeOffset();
                    Log.Logger.Debug("Api-Response: {@response}", model.Response);
                }
            }
            else
            {
                await _next(context);
            }
        }

        private string FormatResponse(HttpContext context, MemoryStream newResponseBody)
        {
            var request = context.Request;
            var response = context.Response;

            return $"Http Response Information: {Environment.NewLine}" +
                   $"Schema: {request.Scheme} {Environment.NewLine}" +
                   $"Host: {request.Host} {Environment.NewLine}" +
                   $"Path: {request.Path} {Environment.NewLine}" +
                   $"QueryString: {request.QueryString} {Environment.NewLine}" +
                   $"Code: {response.StatusCode} {Environment.NewLine}" +
                   $"Response Body: {ReadStreamInChunks(newResponseBody)}";
        }

        private async Task<string> FormatRequest(HttpContext context)
        {
            var request = context.Request;

            StringBuilder headers =new StringBuilder();
            foreach (var header in context.Request.Headers)
            {
                headers.Append($"{header.Key}:{header.Value};");
            }

            return $"Http Request Information: {Environment.NewLine}" +
                   $"Schema: {request.Scheme} {Environment.NewLine}" +
                   $"Host: {request.Host} {Environment.NewLine}" +
                   $"Path: {request.Path} {Environment.NewLine}" +
                   $"QueryString: {request.QueryString} {Environment.NewLine}" +
                   $"Headers: {headers} {Environment.NewLine}" +
                   $"Request Body: {await GetRequestBody(request)}";
        }

        private async Task<string> GetRequestBody(HttpRequest request)
        {
            request.EnableBuffering();
            using (var requestStream = _recyclableMemoryStreamManager.GetStream())
            {
                await request.Body.CopyToAsync(requestStream);
                request.Body.Seek(0, SeekOrigin.Begin);
                return ReadStreamInChunks(requestStream);
            }
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            string result;
            using (var textWriter = new StringWriter())
            using (var reader = new StreamReader(stream))
            {
                var readChunk = new char[ReadChunkBufferLength];
                int readChunkLength;
                do
                {
                    readChunkLength = reader.ReadBlock(readChunk, 0, ReadChunkBufferLength);
                    textWriter.Write(readChunk, 0, readChunkLength);
                } while (readChunkLength > 0);

                result = textWriter.ToString();
            }

            return result;
        }
    }
}