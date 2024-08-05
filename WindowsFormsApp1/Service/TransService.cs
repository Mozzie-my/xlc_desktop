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
        public async Task<String> Trans(string data,string tail)
        {
           // TbTransDataChild res = new TbTransDataChild();
            string sendurl = url + "/MsgHandle/transform";

            var reqdata = new { 
                content=data,
                tailDIY=tail
            };
            var res = await RequestHelper.PostAsync(JsonConvert.SerializeObject(reqdata), sendurl);

            var resdata = JsonConvert.DeserializeObject<CommonResult>(res);
            if (resdata.code == "200")
            {
                return resdata.data;
            }
            return null;
        }

    }
}
