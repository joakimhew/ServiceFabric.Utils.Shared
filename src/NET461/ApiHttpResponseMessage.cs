#if NET461

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;

namespace ServiceFabric.Utils.Shared
{
    public class ApiHttpResponseMessage : HttpResponseMessage
    {
        /// <summary>
        /// Creates a new instance of <see cref="ApiHttpResponseMessage"/>
        /// </summary>
        /// <param name="statusCode">statusCode of the JSON object</param>
        /// <param name="message">message property of the JSON object</param>
        /// <param name="additionalInfo">additionalInfo property of the JSON object</param>
        public ApiHttpResponseMessage(HttpStatusCode statusCode, object message, object additionalInfo = null)
        {
            var body = new
            {
                code = statusCode,
                message,
                info = additionalInfo
            };

            base.Content = new ObjectContent<object>(body, new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
            });

            base.StatusCode = statusCode;
        }
    }
}


#endif