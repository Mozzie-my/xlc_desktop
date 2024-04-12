using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Model
{

    public class WXFriendList
    {
        public int code { get; set; }
        public WXData data { get; set; }
        public string msg { get; set; }
    }

    public class WXData
    {
        public List<WXContact> contacts { get; set; }
        public int total { get; set; }
    }

    public class WXContact
    {
        public string Alias { get; set; }
        public string BigHeadImgUrl { get; set; }
        public string ChatRoomNotify { get; set; }
        public string ChatRoomType { get; set; }
        public string DelFlag { get; set; }
        public string DomainList { get; set; }
        public string EncryptUserName { get; set; }
        public string ExtraBuf { get; set; }
        public string HeadImgMd5 { get; set; }
        public string LabelIDList { get; set; }
        public string NickName { get; set; }
        public string PYInitial { get; set; }
        public string QuanPin { get; set; }
        public string Remark { get; set; }
        public string RemarkPYInitial { get; set; }
        public string RemarkQuanPin { get; set; }
        public string Reserved1 { get; set; }
        public string Reserved10 { get; set; }
        public string Reserved11 { get; set; }
        public string Reserved2 { get; set; }
        public string Reserved3 { get; set; }
        public string Reserved4 { get; set; }
        public string Reserved5 { get; set; }
        public string Reserved6 { get; set; }
        public string Reserved7 { get; set; }
        public string Reserved8 { get; set; }
        public string Reserved9 { get; set; }
        public string SmallHeadImgUrl { get; set; }
        public string Type { get; set; }
        public string UserName { get; set; }
        public string VerifyFlag { get; set; }
        public string profilePicture { get; set; }
        public string profilePictureSmall { get; set; }
    }

}
