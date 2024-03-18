using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fleck;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;
using Newtonsoft.Json;
using WindowsFormsApp1;

namespace WindowsFormsApp1.Utils
{

    class WebSocketServers
    {


        private static IDictionary<string, IWebSocketConnection> dic_Sockets = new Dictionary<string, IWebSocketConnection>();



        public static async Task StartServer(MainForm form1)
        {
            MainForm _form1;

            //客户端url以及其对应的Socket对象字典
            //创建

            _form1 = form1;


            WebSocketServer server = new WebSocketServer("ws://127.0.0.1:8989");//监听所有的的地址
                                                                                //出错后进行重启
            server.RestartAfterListenError = true;


            //开始监听
            server.Start(socket =>
                {
                    socket.OnOpen = () =>   //连接建立事件
                    {
                        //获取客户端网页的url
                        string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                        dic_Sockets.Add(clientUrl, socket);
                        var msg = new
                        {
                            code = "99",
                            body = socket.ConnectionInfo.ClientPort.ToString(),
                        };

                        _form1.setWslog(JsonConvert.SerializeObject(msg));
                        socket.Send(JsonConvert.SerializeObject(msg));
                        Console.WriteLine(DateTime.Now.ToString() + "|服务器:和客户端网页:" + clientUrl + " 建立WebSock连接！");
                    };
                    socket.OnClose = () =>  //连接关闭事件
                    {
                        string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                        //如果存在这个客户端,那么对这个socket进行移除
                        if (dic_Sockets.ContainsKey(clientUrl))
                        {
                            //注:Fleck中有释放
                            //关闭对象连接 
                            //if (dic_Sockets[clientUrl] != null)
                            //{
                            //dic_Sockets[clientUrl].Close();
                            //}
                            dic_Sockets.Remove(clientUrl);
                        }
                        Console.WriteLine(DateTime.Now.ToString() + "|服务器:和客户端网页:" + clientUrl + " 断开WebSock连接！");
                    };
                    socket.OnMessage = message =>  //接受客户端网页消息事件
                    {
                        _form1.setWslog(message);
                        string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                        Console.WriteLine(DateTime.Now.ToString() + "|服务器:【收到】来客户端网页:" + clientUrl + "的信息：\n" + message);
                    };
                });

            Console.ReadKey();
            foreach (var item in dic_Sockets.Values)
            {
                if (item.IsAvailable == true)
                {
                    item.Send("服务器消息：" + DateTime.Now.ToString());
                }
            }
            Console.ReadKey();

            //关闭与客户端的所有的连接
            foreach (var item in dic_Sockets.Values)
            {
                if (item != null)
                {
                    item.Close();
                }
            }

            Console.ReadKey();

        }
        public static void SendMsg(string msg)
        {
            foreach (var socket in dic_Sockets.Values)
            {
                socket.Send(msg);
            }
        }
    }
}