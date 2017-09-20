using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ServiceFabric.Utils.Shared.Core
{
    public class ApiResponseMessage<TMessageType>
    {
        public HttpStatusCode Code { get; set; }
        public TMessageType Message { get; set; }   
        public string Info { get; set; }
    }
}
