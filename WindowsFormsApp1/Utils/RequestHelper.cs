﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace WindowsFormsApp1.Utils
{
    /// <summary>
    /// 请求帮助类
    /// </summary>
    public class RequestHelper
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private const int RequestTimeout = 30000; // 请求超时时间，单位为毫秒

        public static async Task<string> PostAsync(string postData, string url)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.Timeout = RequestTimeout;
                req.ContentType = "application/json";

                byte[] data = Encoding.UTF8.GetBytes(postData);
                req.ContentLength = data.Length;

                using (Stream reqStream = await req.GetRequestStreamAsync())
                {
                    await reqStream.WriteAsync(data, 0, data.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)await req.GetResponseAsync())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string content = await reader.ReadToEndAsync();
                    logger.Info("Request completed successfully.");
                    logger.Debug("Response content: {0}", content);
                    return content;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while processing the request.");
                throw;
            }
        }
        public static async Task<string> StrPostAsync(string postData, string Url)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(Url),
                Content = new StringContent(postData)
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };
            using (var response = await client.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
                return body;
            }

        }
        public static string Get(string Url)
        {
            string content = String.Empty;
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(Url);
            req.Method = "GET";
            req.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            Stream stream = response.GetResponseStream();
            Encoding encode = Encoding.UTF8;
            StreamReader reader = new StreamReader(stream, encode);
            content = reader.ReadToEnd();
            stream.Close();
            reader.Close();
            return content;
        }
        public static string GetResponseByGet(string url)
        {
            string result = string.Empty;

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                //statusCode = response.StatusCode.ToString();

                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                }
            }
            return result;
        }
        /// <summary>
        /// http异步请求
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="reqMethod">请求方法 GET、POST</param>
        /// <param name="callback">回调函数</param>
        /// <param name="ob">回传对象</param>
        /// <param name="postData">post数据</param>
        public static void HttpAsyncRequest(string url, string reqMethod, AsyRequetCallback callback, object ob = null, string postData = "")
        {
            Stream requestStream = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = reqMethod;
                if (reqMethod.ToUpper() == "POST")
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(postData);
                    request.ContentLength = bytes.Length;
                    requestStream = request.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                }
                //开始调用异步请求 
                //AsyResultTag 是自定义类 用于传递调用时信息 其中HttpWebRequest 是必须传递对象。
                //因为回调需要用HttpWebRequest来获取HttpWebResponse 
                request.BeginGetResponse(new AsyncCallback(HttpCallback), new AsyResultTag() { obj = ob, callback = callback, req = request });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (requestStream != null)
                {
                    requestStream.Close();
                }
            }
        }


        /// <summary>
        /// http请求回调 由.net内部调用 参数必须为IAsyncResult
        /// </summary>
        /// <param name="asynchronousResult">http回调时回传对象</param>
        private static void HttpCallback(IAsyncResult asynchronousResult)
        {
            int statusCode = 0;
            string retString = "";
            AsyResultTag tag = new AsyResultTag();
            WebException webEx = null;
            try
            {
                //获取请求时传递的对象
                tag = asynchronousResult.AsyncState as AsyResultTag;
                HttpWebRequest req = tag.req;
                //获取异步返回的http结果
                HttpWebResponse response = req.EndGetResponse(asynchronousResult) as HttpWebResponse;
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                statusCode = ((int)response.StatusCode);

            }
            catch (WebException ex)
            {
                if ((HttpWebResponse)ex.Response != null)
                {
                    statusCode = ((int)((HttpWebResponse)ex.Response).StatusCode);
                }

                webEx = ex;
            }
            //调用外部回调 即最外层的回调
            tag.callback(tag.obj, retString, statusCode, webEx);

        }

        /// <summary>
        /// 异步请求回调委托
        /// </summary>
        /// <param name="asyObj">回传对象</param>
        /// <param name="resStr">http响应结果</param>
        /// <param name="statusCode">http状态码</param>
        /// <param name="webEx">异常</param>
        public delegate void AsyRequetCallback(object asyObj, string respStr, int statusCode, WebException webEx);

        /// <summary>
        /// 异步返回对象
        /// </summary>
        public class AsyResultTag
        {
            /// <summary>
            /// 回传对象
            /// </summary>
            public object obj { get; set; }
            /// <summary>
            /// 当前httpRequest请求实例
            /// </summary>
            public HttpWebRequest req { get; set; }
            /// <summary>
            /// 回调函数委托
            /// </summary>
            public AsyRequetCallback callback { get; set; }
        }

    }


}
