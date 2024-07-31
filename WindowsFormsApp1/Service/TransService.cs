using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Model;
using WindowsFormsApp1.Utils;
using Wsm.Utils;

namespace WindowsFormsApp1.Service
{


    internal class TransService
    {
        string url;

        public TransService()
        {
            url = AppSetting.Configuration["TransUrl"];
        }
        public String trans(string data,string tail)
        {
            TbTransDataChild res = new TbTransDataChild();
            string sendurl = url + "/MsgHandle/transform";

            var reqdata = new { 
                content=data,
                tailDIY=tail
            };


            var resdata = JsonConvert.DeserializeObject<CommonResult>(RequestHelper.Post(JsonConvert.SerializeObject( reqdata), sendurl));
            if (resdata.code == "200")
            {
                return resdata.data;
            }
            return null;
        }

    }
}
