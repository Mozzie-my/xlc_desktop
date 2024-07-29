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

                if (!string.IsNullOrEmpty(content))
                {

                    WxRobotHelper.SendMsg(user, content);
                }
                if (!string.IsNullOrEmpty(img))
                {
                    // 延时1秒，防止图片发送失败
                    System.Threading.Thread.Sleep(500);
                    WxRobotHelper.SendImg(user, img);
                }

                System.Threading.Thread.Sleep(500);
            });
        }
    }
}
