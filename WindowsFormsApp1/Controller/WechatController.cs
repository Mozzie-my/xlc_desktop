
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MainView.Controller
{
    [ApiController]
    [Route("http://127.0.0.1:7777/DaenWxHook/server/")]
    public class WechatController : ControllerBase
    {
        public WechatController()
        {
           Console.WriteLine("WechatController init");
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Wechat()
        {
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                string requestBody = await reader.ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<dynamic>(requestBody);

                DateTime date = DateTime.FromBinary((long)data.timestamp / 1000);

                if (data.type == "D0003")
                {
                    var msgData = data.data;
                    string msg = msgData.msg;
                    string fromWxid = msgData.fromWxid;
                    string finalFromWxid = msgData.finalFromWxid;

                    if (msgData.msgType == 1)
                    {
                        if (fromWxid.Contains("@chatroom"))
                        {
                            if (wxid2nick_group[fromWxid].Contains("暴富"))
                            {
                                ReceiveGroupMsg(fromWxid, finalFromWxid, date, msg);
                            }
                        }
                        else
                        {
                            ReceiveFriendMsg(fromWxid, date, msg);
                        }
                    }
                }

                return Ok(new { code = 200, msg = "ok", timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() });
            }
        }

        private void ReceiveGroupMsg(string fromWxid, string finalFromWxid, DateTime date, string msg)
        {
            // 处理群消息的逻辑
        }

        private void ReceiveFriendMsg(string fromWxid, DateTime date, string msg)
        {
            // 处理好友消息的逻辑
        }

        private Dictionary<string, string> wxid2nick_group = new Dictionary<string, string>
    {
        { "@chatroom1", "暴富群" },
        { "@chatroom2", "其他群" }
    };
    }
}
