

using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceFabric.Utils.Shared
{
    public class ApiActionResult : IActionResult
    {
        private readonly HttpRequest _requestMessage;
        private readonly HttpStatusCode _code;
        private readonly object _message;
        private readonly string _info;


        public ApiActionResult(HttpRequest request, HttpStatusCode statusCode,
            object message, string additionalInfo = null)
        {
            _requestMessage = request;
            _code = statusCode;
            _message = message;
            _info = additionalInfo;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var apiMessage = new ApiResponseMessage<object>
            {
                Code = _code,
                Message = _message,
                Info = _info
            };

            var json = new JsonResult(apiMessage);

            context.HttpContext.Response.StatusCode = (int)_code;

            await json.ExecuteResultAsync(context);
        }
    }
}
