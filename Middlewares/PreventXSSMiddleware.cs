using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class PreventXSSMiddleware
    {
        private readonly RequestDelegate _next;

        public PreventXSSMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // Check XSS in URL
            if (!string.IsNullOrWhiteSpace(httpContext.Request.Path.Value))
            {
                var url = httpContext.Request.Path.Value;

                if (IsDangerousString(url))
                {
                    await RespondWithAnErrorView(httpContext);
                }
            }

            // Check XSS in query string
            if (!string.IsNullOrWhiteSpace(httpContext.Request.QueryString.Value))
            {
                var queryString = WebUtility.UrlDecode(httpContext.Request.QueryString.Value);

                if (IsDangerousString(queryString))
                {
                    await RespondWithAnErrorView(httpContext);
                }
            }
            await _next(httpContext);
            
        }

        private async Task RespondWithAnErrorView(HttpContext context)
        {
            context.Request.Path = "/Home/Error";
            await _next(context);
        }

        public static bool IsDangerousString(string s)
        {
            char[] StartingChars = { '<', '&' };

            for (var i = 0; ;)
            {

                // Look for the start of one of our patterns 
                var n = s.IndexOfAny(StartingChars, i);

                // If not found, the string is safe
                if (n < 0) return false;

                // If it's the last char, it's safe 
                if (n == s.Length - 1) return false;

                switch (s[n])
                {
                    case '<':
                        // If the < is followed by a letter or '!', it's unsafe (looks like a tag or HTML comment)
                        if (IsAtoZ(s[n + 1]) || s[n + 1] == '!' || s[n + 1] == '/' || s[n + 1] == '?') return true;
                        break;
                    case '&':
                        // If the & is followed by a #, it's unsafe (e.g. S) 
                        if (s[n + 1] == '#') return true;
                        break;
                }

                // Continue searching
                i = n + 1;
            }
        }

        private static bool IsAtoZ(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UsePreventXSSMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PreventXSSMiddleware>();
        }
    }
}
