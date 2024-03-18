using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Utils
{
    class HttpListenerServer
    {
        public event Action<string> PostDataReceived;

        private HttpListener listener;

        public HttpListenerServer(string url)
        {
            listener = new HttpListener();
            listener.Prefixes.Add(url);
        }

        public async Task StartListening()
        {
            listener.Start();
            Console.WriteLine("Listening for POST requests...");

            while (true)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                ProcessRequestAsync(context);
            }
        }

        private async Task ProcessRequestAsync(HttpListenerContext context)
        {
            try
            {
                if (context.Request.HttpMethod == "POST")
                {
                    using (StreamReader reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
                    {
                        string requestData = await reader.ReadToEndAsync();
                        Console.WriteLine($"Received POST data: {requestData}");

                        // 将接收到的 POST 数据传递给事件
                        PostDataReceived?.Invoke(requestData);
                    }
                }
                else
                {
                    Console.WriteLine($"Received {context.Request.HttpMethod} request, but only POST is supported.");
                }

                string responseString = "Request processed successfully.";
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);

                context.Response.ContentLength64 = buffer.Length;
                using (Stream output = context.Response.OutputStream)
                {
                    await output.WriteAsync(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing request: {ex.Message}");
            }
            finally
            {
                context.Response.Close();
            }
        }

        public void StopListening()
        {
            listener.Stop();
            listener.Close();
        }
    }

}
