using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Model.Wx
{
    public class WxGroupInfo
    {
        public string wxid { get; set; }
        public string wxNum { get; set; }
        public string nick { get; set; }
        public string remark { get; set; }
        public string nickBrief { get; set; }
        public string nickWhole { get; set; }
        public string remarkBrief { get; set; }
        public string remarkWhole { get; set; }
        public string enBrief { get; set; }
        public string enWhole { get; set; }
        public string v3 { get; set; }
        public string sign { get; set; }
        public string country { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string momentsBackgroudImgUrl { get; set; }
        public string avatarMinUrl { get; set; }
        public string avatarMaxUrl { get; set; }
        public string sex { get; set; }
        public int groupMemberNum { get; set; }
        public string groupManger { get; set; }
    }

}
