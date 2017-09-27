using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServiceFabric.Utils.Shared.Extensions
{
    public static class HttpContentExtensions
    {
        /// <summary>
        /// Reads <see cref="HttpContent"/> as an <see cref="ApiResponseMessage{TMessageType}"/> 
        /// with <see cref="ApiResponseMessage{TMessageType}.Message"/> set to <typeparam name="TExpectedMessageType"/> 
        /// as a synchronous operation
        /// </summary>
        /// <typeparam name="TExpectedMessageType">Expected type of the <see cref="ApiResponseMessage{TMessageType}.Message"/> 
        /// property in <see cref="HttpContent"/></typeparam>
        /// <param name="content"><see cref="HttpContent"/> to read from and deserialize</param>
        /// <returns><see cref="ApiResponseMessage{TMessageType}"/> with the <see cref="ApiResponseMessage{TMessageType}.Message"/> 
        /// property set to <typeparam name="TExpectedMessageType"/></returns>
        public static async Task<ApiResponseMessage<TExpectedMessageType>> ReadAsApiResponseMessageAsync<TExpectedMessageType>(this HttpContent content)
        {
            var json = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponseMessage<TExpectedMessageType>>(json);
        }
    }
}
