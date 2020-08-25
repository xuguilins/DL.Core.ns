using DL.Core.utility.Extendsition;
using DL.Core.utility.Extensiton;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DL.Core.utility.Web
{
    /// <summary>
    /// 简单请求接口,
    /// 需开发者自主在startup注册,注册方式【services.AddHttpClient<IHttpServerClient, HttpServerClient>()】
    /// </summary>
    public class HttpServerClient : IHttpServerClient
    {
        //  private static readonly System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

        public HttpClient HttpClient { get; set; }

        public HttpServerClient(HttpClient httpclient)
        {
            httpclient.DefaultRequestHeaders.Add("Accept", "application/json");
            HttpClient = httpclient;
        }

        /// <summary>
        /// 设置token
        /// </summary>
        /// <param name="token"></param>
        public void SetBearerToken(string token)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        #region [GetAndPost-Api-Params is String]

        /// <summary>
        /// Get请求网络Api接口(Query)
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="pairs">请求参数</param>
        /// <returns></returns>
        public async Task<string> GetApiAsync(string url, Dictionary<string, string> pairs = null)
        {
            var result = await Get(url, pairs);
            return await result.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Get请求网络Api接口(Query)
        /// </summary>
        /// <typeparam name="T">返回类型,通常是class</typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="pairs">请求参数</param>
        /// <returns></returns>
        public async Task<T> GetApiAsync<T>(string url, Dictionary<string, string> pairs = null) where T : class
        {
            var result = await Get(url, pairs);
            var data = await result.Content.ReadAsStringAsync();
            return await data.FromJson<T>().toTask();
        }

        /// <summary>
        /// Get请求网络Api接口(Query)
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="pairs">请求参数</param>
        /// <returns></returns>
        public string GetApi(string url, Dictionary<string, string> pairs = null)
        {
            var result = Get(url, pairs).Result;
            var data = result.Content.ReadAsStringAsync().Result;
            return data;
        }

        /// <summary>
        /// Get请求网络Api接口(Query)
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="pairs">请求参数</param>
        /// <returns></returns>
        public T GetApi<T>(string url, Dictionary<string, string> pairs = null) where T : class
        {
            var result = Get(url, pairs).Result;
            var data = result.Content.ReadAsStringAsync().Result;
            return data.FromJson<T>();
        }

        /// <summary>
        /// Post请求网络Api接口
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="pairs">请求参数</param>
        /// <returns></returns>
        public async Task<string> PostApiAsync(string url, Dictionary<string, object> pairs = null)
        {
            var data = await Post(url, pairs);
            var result = await data.Content.ReadAsStringAsync();
            return await result.toTask();
        }

        /// <summary>
        /// Post请求网络Api接口
        /// </summary>
        /// <typeparam name="T">返回参数</typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="pairs">请求参数</param>
        /// <returns></returns>
        public async Task<T> PostApiAsync<T>(string url, Dictionary<string, object> pairs = null) where T : class
        {
            var data = await Post(url, pairs);
            var result = await data.Content.ReadAsStringAsync();
            return await result.FromJson<T>().toTask();
        }

        /// <summary>
        /// Post请求网络Api接口
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="pairs">请求参数</param>
        /// <returns></returns>
        public string PostApi(string url, Dictionary<string, object> pairs = null)
        {
            var data = Post(url, pairs).Result;
            var result = data.Content.ReadAsStringAsync().Result;
            return result;
        }

        /// <summary>
        /// Post请求网络Api接口
        /// </summary>
        /// <typeparam name="T">返回参数</typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="pairs">请求参数</param>
        /// <returns></returns>
        public T PostApi<T>(string url, Dictionary<string, object> pairs = null) where T : class
        {
            var data = Post(url, pairs).Result;
            var result = data.Content.ReadAsStringAsync().Result;
            return result.FromJson<T>();
        }

        #endregion [GetAndPost-Api-Params is String]

        #region [private Method]

        private async Task<HttpResponseMessage> Get(string url, Dictionary<string, string> pairs = null)
        {
            StringBuilder sb = new StringBuilder();
            if (pairs != null)
            {
                sb.Append("?");
                int index = 0;
                foreach (var item in pairs)
                {
                    sb.Append(item.Key + $"={item.Value}");
                    if (index != pairs.Count - 1)
                        sb.Append("&");
                    index++;
                }
            }
            url += sb.ToString();
            var result = await HttpClient.GetAsync(url);
            return result;
        }

        private async Task<HttpResponseMessage> Post(string url, Dictionary<string, object> pairs = null)
        {
            if (pairs != null)
            {
                StringContent content = new StringContent(pairs.ToJson(), Encoding.UTF8, "application/json");
                var data = await HttpClient.PostAsync(url, content);
                return await data.toTask();
            }
            else
            {
                var data = await HttpClient.PostAsync(url, null);
                return await data.toTask();
            }
        }

        public void Dispose()
        {
            if (HttpClient != null)
            {
                HttpClient.Dispose();
            }
        }

        #endregion [private Method]
    }
}