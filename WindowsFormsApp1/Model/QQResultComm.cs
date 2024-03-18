using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Model
{
    public class QQResultComm
    {
        public bool success { get; set; }
        public int code { get; set; }
        public string msg { get; set; }
        public object data { get; set; }
    }

}
