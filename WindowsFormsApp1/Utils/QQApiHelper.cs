using HSM.SDK.Comm;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WindowsFormsApp1.Model;
using WindowsFormsApp1.Properties;
using Wsm.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace WindowsFormsApp1.Utils
{
    public static class QQApiHelper
    {


        public static string SendQQMsg(string bot, string groupId, string msg)
        {
            var body = CreateMyqqReqBody("Api_SendMsg");
            body.SetParams(bot, 1, "", groupId, msg);

            return sendPost(body);
        }
        public static List<Model.Group> GetGroupList(string bot)
        {
            var body = CreateMyqqReqBody("Api_GetGroupList");
            body.SetParams(bot);
            var res = sendPost(body);
            var groupList = JsonConvert.DeserializeObject<QQGroupListView>(res).ret.data.group;
            return groupList;
        }
        /// <summary>
        /// 取框架在线qq列表
        /// </summary>
        /// <param name="bot"></param>
        /// <returns></returns>
        public static string GetOnlineQQlist()
        {
            var body = CreateMyqqReqBody("Api_GetOnlineQQlist");
            body.SetParams();
            var list =  sendPost(body);
            return list != null ? Regex.Match(JsonConvert.DeserializeObject<QQPicLink>(list).ret, @"\d+").Value : null;
        }
        /// <summary>
        /// 根据pic guid转link
        /// </summary>
        /// <param name="bot"></param>
        /// <param name="picGuid"></param>
        /// <returns></returns>
        public static string GetPicLink(string bot,string picGuid)
        {
            var body = CreateMyqqReqBody("Api_GetPicLink");
            body.SetParams(bot, 1,"2222" ,picGuid);
            var picjson = sendPost(body);
            var picLink = picjson!=null ?JsonConvert.DeserializeObject<QQPicLink>(picjson).ret : null;
            return picLink;
        }
        private static ApiBody CreateMyqqReqBody(string function)
        {
            return new ApiBody() { Function = function };
        }
        private static string sendPost(ApiBody body)
        {

            string url = AppSetting.Configuration["QQROBOT:URL"];
            var postres = RequestHelper.StrPostAsync(JsonConvert.SerializeObject(body), url);
            var res = JsonConvert.DeserializeObject<QQResultComm>(postres.Result);
            if (res.code != 200)
            {
                throw new Exception(res.msg);

            }
            return res.data.ToString();

        }
        /// <summary>
        /// POST Api请求体
        /// </summary>
        class ApiBody
        {
            [JsonProperty("function")] public string Function { get; set; }
            [JsonProperty("token")] public string Token { get; set; }
            [JsonProperty("params")] public Dictionary<string, string> Params { get; set; }

            public void SetParams(params object[] args)
            {
                Params = new Dictionary<string, string>();
                for (var index = 0; index < args.Length; index++)
                {
                    var arg = args[index];
                    Params.Add($"c{index + 1}", arg?.ToString() ?? "");
                }
            }
        }
    }
}
