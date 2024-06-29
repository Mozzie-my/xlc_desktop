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
    public static class WXApiHelper
    {
        private static string wxrobotUrl = "";
        static WXApiHelper()
        {
            wxrobotUrl = AppSetting.Configuration["WXROBOT:URL"];
        }
        public static string SendWxMsg(string bot, string groupId, string msg)
        {

            return null;
        }
        public static List<WXContact>GetFriendList()
        {
            var url = wxrobotUrl + "/api/dbcontacts";
            var flist= JsonConvert.DeserializeObject<WXFriendList>( RequestHelper.Get(url));
            return flist.code==200?flist.data.contacts:null ;
        }


    }
}
