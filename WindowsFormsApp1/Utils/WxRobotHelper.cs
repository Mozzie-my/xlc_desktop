using HSM.SDK.Comm;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WindowsFormsApp1.Model;
using WindowsFormsApp1.Model.Wx;
using WindowsFormsApp1.Properties;
using Wsm.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace WindowsFormsApp1.Utils
{
    public static class WxRobotHelper
    {


        public static string SendMsg(string wxid, string msg)
        {
            var body = new WxCommQuery()
            {
                type = "Q0001",
                data = new { 
                    wxid = wxid,
                    msg = msg
                }

            };
            var res = sendPost(body);

            return res.msg;
        }
        public static string SendImg(string wxid, string img)
        {
            var body = new WxCommQuery()
            {
                type = "Q0010",
                data = new { 
                    wxid = wxid,
                    path = img
                }

            };
            var res = sendPost(body);

            return res.msg;
        }
        public static List<WxGroupInfo> GetGroupList()
        {
            var body = new WxCommQuery()
            {
                type = "Q0006",
            };
            var res = sendPost(body);
            if (res != null)
            {
                return JsonConvert.DeserializeObject<List<WxGroupInfo>>(res.result.ToString());
            }
            return null;
        }
  

        private static WxResultComm sendPost(Object body)
        {

            string url = AppSetting.Configuration["WXROBOT:URL"];
            var b = JsonConvert.SerializeObject(body);
            var postres = RequestHelper.StrPostAsync(b, url);
            var res = JsonConvert.DeserializeObject<WxResultComm>(postres.Result);
            if (res.code != 200)
            {
                throw new Exception(res.msg);

            }
            return res;

        }
        
    }
}
