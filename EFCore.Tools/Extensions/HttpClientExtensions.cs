using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Tools.Extensions
{
    public static class HttpClientExtensions
    {

        /// <summary>
        /// 执行HTTP GET请求。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url">请求地址</param>
        /// <param name="dictionary">请求参数</param>
        /// <returns>HTTP响应</returns>
        public static async Task<string> DoGetAsync(this HttpClient client, string url, IDictionary<string, string> dictionary)
        {
            if (url.Contains("?"))
            {
                url = url + "&" + BuildQuery(dictionary);
            }
            else
            {
                url = url + "?" + BuildQuery(dictionary);
            }

            using (var response = await client.GetAsync(url))
            using (var responseContent = response.Content)
            {
                return await responseContent.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// 执行HTTP POST请求。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url">请求地址</param>
        /// <param name="dictionary">请求参数</param>
        /// <returns>HTTP响应</returns>
        public static async Task<string> DoPostAsync(this HttpClient client, string url, IDictionary<string, string> dictionary)
        {
            using (var requestContent = new StringContent(BuildQuery(dictionary), Encoding.UTF8, "application/x-www-form-urlencoded"))
            using (var response = await client.PostAsync(url, requestContent))
            using (var responseContent = response.Content)
            {
                return await responseContent.ReadAsStringAsync();
            }
        }


        /// <summary>
        /// 执行HTTP POST请求。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url">请求地址</param>
        /// <param name="content">请求内容</param>
        /// <returns>HTTP响应</returns>
        public static async Task<string> DoPostAsync(this HttpClient client, string url, string content)
        {
            using (var requestContent = new StringContent(content, Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(url, requestContent))
            using (var responseContent = response.Content)
            {
                return await responseContent.ReadAsStringAsync();
            }
        }


        /// <summary>
        /// 组装请求参数。
        /// </summary>
        /// <param name="dictionary">Key-Value形式请求参数字典</param>
        /// <returns>URL编码后的请求数据</returns>
        public static string BuildQuery(IDictionary<string, string> dictionary)
        {
            if (dictionary == null || dictionary.Count == 0)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            var content = new StringBuilder();
            foreach (var iter in dictionary)
            {
                if (!string.IsNullOrEmpty(iter.Value))
                {
                    content.Append(iter.Key + "=" + WebUtility.UrlEncode(iter.Value) + "&");
                }
            }
            return content.ToString().Substring(0, content.Length - 1);
        }
    }
}
