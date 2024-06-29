using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Model
{
    internal class CommonResult
    {


        public string code { get; set; }
        public string msg { get; set; }
        public string data { get; set; }

        public CommonResult(string code, string msg, string data)
        {
            this.code = code;
            this.msg = msg;
            this.data = data;
        }
    }
}
