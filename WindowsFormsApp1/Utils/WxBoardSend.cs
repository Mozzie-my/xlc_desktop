using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsApp1.Utils
{
    public class WxBoardSend
    {
        public static void SendMsg(List<string> toUser, string content,String img)
        {
            toUser.ForEach(user =>
            {
                content = TextUtils.removeEmojisWithHex(content);
                WxRobotHelper.SendMsg(user, content);
                if (!string.IsNullOrEmpty(img))
                {
                    WxRobotHelper.SendImg(user, img);
                }
            });
        }
    }
}
