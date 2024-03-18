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
    internal class TaobaoService
    {
        string url;
        public TaobaoService() {

            url = AppSetting.Configuration["TransUrl"];
        }
        public TbTransDataChild trans(string data)
        {
            TbTransDataChild res = new TbTransDataChild();
            string sendurl = url + "/Tb/trans?token=" + data;
            var  resdata = JsonConvert.DeserializeObject<TbTransData>(RequestHelper.Get(sendurl));
            if (resdata.data.Where(x => x.code == 1).FirstOrDefault() != null)
            {
                res = resdata.data.Where(x => x.code == 1).FirstOrDefault().data;
                return res;
            }
            return null;
        }
    }
}
