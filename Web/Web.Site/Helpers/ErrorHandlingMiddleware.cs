﻿using System;
using System.Net;
using System.Threading.Tasks;
using Core.Dominio;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Web.Site.Helpers
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                bool isAjaxCall = string.Equals("XMLHttpRequest", context.Request.Headers["x-requested-with"], StringComparison.OrdinalIgnoreCase);

                if (isAjaxCall)
                    await HandleExceptionAsync(context, ex);
                else
                    throw;
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            //var result = new BaseResponseDTO<string>()
            //{
            //    ErrorCode = (int)HttpStatusCode.InternalServerError,
            //    ErrorMessage = ex.Message,
            //    Succeed = false,
            //};

            var jsonResult = JsonConvert.SerializeObject(new { message = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(jsonResult);

        }
    }

}
