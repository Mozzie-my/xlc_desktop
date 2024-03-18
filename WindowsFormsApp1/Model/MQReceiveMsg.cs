using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Model
{

    public class MQReceiveMsg
    {
        public string MQ_robot { get; set; }
        public int MQ_type { get; set; }
        public int MQ_type_sub { get; set; }
        public string MQ_fromID { get; set; }
        public string MQ_fromQQ { get; set; }
        public string MQ_passiveQQ { get; set; }
        public string MQ_msg { get; set; }
        public string MQ_msgSeq { get; set; }
        public string MQ_msgID { get; set; }
        public string MQ_msgData { get; set; }
        public string MQ_timestamp { get; set; }
    }

}
