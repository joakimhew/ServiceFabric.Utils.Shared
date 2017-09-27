using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ServiceFabric.Utils.Shared
{
    public class ApiResponseMessage<TMessageType>
    {

        public ApiResponseMessage()
        {
        }

        public ApiResponseMessage(HttpStatusCode code, TMessageType message, string info)
        {
            Code = code;
            Message = message;
            Info = info;
        }

        public HttpStatusCode Code { get; set; }
        public TMessageType Message { get; set; }   
        public string Info { get; set; }
    }
}
