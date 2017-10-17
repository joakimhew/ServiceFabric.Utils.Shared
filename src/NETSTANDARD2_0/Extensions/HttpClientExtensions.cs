#if NETSTANDARD2_0

using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServiceFabric.Utils.Shared.Extensions
{
    public static class HttpClientExtensions
    {
        /// <summary>
        ///  Sends a POST request as an asynchronous operation to the specified Uri with the
        ///  given value serialized as JSON.</summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value that will be placed in the request's entity body.</param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(
            this HttpClient client, Uri requestUri, T value)
        {
            return await PostAsJsonAsync<T>(client, requestUri, value, Encoding.UTF8);
        }

        /// <summary>
        ///  Sends a POST request as an asynchronous operation to the specified Uri with the
        ///  given value serialized as JSON.</summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value that will be placed in the request's entity body.</param>
        /// <param name="encoding">The encoding to use for the JSON string</param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(
            this HttpClient client, Uri requestUri, T value, Encoding encoding)
        {
            string json = JsonConvert.SerializeObject(value);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            request.Content = new StringContent(json, encoding);

            return await client.SendAsync(request);
        }

        /// <summary>
        ///  Sends a PUT request as an asynchronous operation to the specified Uri with the
        ///  given value serialized as JSON.</summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value that will be placed in the request's entity body.</param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> PutAsJsonAsync<T>(
            this HttpClient client, Uri requestUri, T value)
        {
            return await PutAsJsonAsync<T>(client, requestUri, value, Encoding.UTF8);
        }

        /// <summary>
        ///  Sends a PUT request as an asynchronous operation to the specified Uri with the
        ///  given value serialized as JSON.</summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value that will be placed in the request's entity body.</param>
        /// <param name="encoding">The encoding to use for the JSON string</param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> PutAsJsonAsync<T>(
            this HttpClient client, Uri requestUri, T value, Encoding encoding)
        {
            string json = JsonConvert.SerializeObject(value);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, requestUri);
            request.Content = new StringContent(json, encoding);

            return await client.SendAsync(request);
        }
    }
}

#endif