using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ServiceFabric.Utils.Shared;

namespace ServiceFabric.Utils.Shared.Extensions
{
    /// <summary>
    /// Extension methods for HttpContent
    /// </summary>
    public static class HttpContentExtensions
    {
        /// <summary>
        /// Reads <see cref="HttpContent"/> as an <see cref="ApiResponseMessage{TMessageType}"/> 
        /// with <see cref="ApiResponseMessage{TMessageType}.Message"/> set to <typeparam name="TExpectedMessageType"/> 
        /// as an asynchronous operation
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