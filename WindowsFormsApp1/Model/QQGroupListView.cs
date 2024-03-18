using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Model
{

    public class QQGroupListView
    {
        public Ret ret { get; set; }
    }

    public class Ret
    {
        public int code { get; set; }
        public int subcode { get; set; }
        public string message { get; set; }
        public int _default { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public List<Group> group { get; set; }
    }

    public class Group
    {
        public int groupcode { get; set; }
        public string groupname { get; set; }
        public int total_member { get; set; }
        public int notfriends { get; set; }
    }

}
