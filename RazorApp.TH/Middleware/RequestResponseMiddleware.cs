using Microsoft.AspNetCore.Http;
using RazorApp.TH.Services.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.TH.Middleware
{
    public class RequestResponseMiddleware 
    {
        private readonly RequestDelegate _next;

        public RequestResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        private void CheckSession(HttpContext context)
        {
           
        }
        public async Task Invoke(HttpContext context)
        {

            var request = context.Request;
            var url = request.Path.ToString().ToLower();
            Console.WriteLine($"Url: {url}");
            if(!AllowsAnonymousAccess(url))
            {
                Console.WriteLine("Checking Session");
                var sessionFound = !string.IsNullOrEmpty(context.Session.GetString("cliente")) &&
                              !string.IsNullOrEmpty(context.Session.GetString("usuario")) && 
                              !string.IsNullOrEmpty(context.Session.GetString("senha"));

                Console.WriteLine(!sessionFound ? "Session not found - redirecting..." : "Session found!");
                if(!sessionFound)
                {
                    if (Statics.IsDev)
                    {
                        context.Response.Redirect("/login");
                        return;
                    }
                    context.Response.Redirect(Statics.UrlLoginSipWeb);

                    return;                    
                }
            }

            CheckSession(context);
            await _next(context);






        }

        private bool AllowsAnonymousAccess(string url)
        {
            return !string.IsNullOrEmpty(Statics.AllowsAnonymousAccess.FirstOrDefault(e => e.ToLower().Contains(url)));
        }
        public async Task Invoke_BACKUP(HttpContext context)
        {
            //First, get the incoming request
            var request = await FormatRequest(context.Request);

            //Copy a pointer to the original response body stream
            var originalBodyStream = context.Response.Body;

            //Create a new memory stream...
            using(var responseBody = new MemoryStream())
            {
                //...and use that for the temporary response body
                context.Response.Body = responseBody;
                Console.WriteLine("Req.Content-type: " + context.Request.ContentType);
                //Continue down the Middleware pipeline, eventually returning to this class
                await _next(context);

                //Format the response from the 
                Console.WriteLine("Res.Content-type: " + context.Response.ContentType);
                var response = await FormatResponse(context.Response);

                //TODO: Save log to chosen datastore

                //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

    
        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;

            //This line allows us to set the reader for the request back at the beginning of its stream.
            request.EnableBuffering();

            //We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            //...Then we copy the entire request stream into the new buffer.
            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            //We convert the byte[] into a string using UTF8 encoding...
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            //..and finally, assign the read body back to the request body, which is allowed because of EnableRewind()
            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //...and copy it into a string
            string text = await new StreamReader(response.Body).ReadToEndAsync();

            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);

            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            return $"{response.StatusCode}: {text}";
        }
    }
}
