using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace SMSService_Demo2
{
    /// <summary>
    /// This class handles the input from any request.
    /// It expects that a request will come in as "text/plain" or "application/octet-stream".
    /// It will allow all input values, formatting them into a string of key/value pairs (delimited with a ":")
    /// and with "\r\n" as line endings.
    /// 
    /// An successful request in Postman will have the following characteristics:
    ///     Request Type: POST
    ///     Body: raw
    ///     Media Type: Text/Plain
    ///     Example Post Data: 
    ///         number:111-222-3333
    ///         message:This is a test
    ///         service:SecondService
    ///         * note the line feed after end of line
    ///      
    /// The idea is that a request can comprise any values, while the application will look for specific 
    /// key/values (ie: service:SecondService in the example above).
    /// 
    /// This formatter is configured to run in the Startup.cs (ConfigureServices function).
    /// 
    /// </summary>
    public class RawRequestBodyFormatter : InputFormatter
    {
        public RawRequestBodyFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
        }

        public override bool CanRead(InputFormatterContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var contentType = context.HttpContext.Request.ContentType;
            if (string.IsNullOrEmpty(contentType) || contentType == "text/plain" ||
                contentType == "application/octet-stream")
                return true;

            return false;
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;
            var contentType = context.HttpContext.Request.ContentType;


            if (string.IsNullOrEmpty(contentType) || contentType == "text/plain")
            {
                using (var reader = new StreamReader(request.Body))
                {
                    var content = await reader.ReadToEndAsync();
                    return await InputFormatterResult.SuccessAsync(content);
                }
            }
            if (contentType == "application/octet-stream")
            {
                using (var ms = new MemoryStream(2048))
                {
                    await request.Body.CopyToAsync(ms);
                    var content = ms.ToArray();
                    return await InputFormatterResult.SuccessAsync(content);
                }
            }

            return await InputFormatterResult.FailureAsync();
        }
    }
}
