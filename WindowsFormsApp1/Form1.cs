using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using WindowsFormsApp1.Model;
using WindowsFormsApp1.Service;
using WindowsFormsApp1.Utils;
using Wsm.Utils;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        QueueHelper<MQReceiveMsg> queueHelper = QueueHelper<MQReceiveMsg>.Instance;

        private HttpListenerServer httpListenerServer;
        private TaobaoService taobaoService;
        private TransService transService;
        IniFileHelper _ini = new IniFileHelper();
        string MonGroupID="";
        // 屏蔽关键词
        string blockingWords = "";
        string robotqq = "";
        List<string> blockingWordsList = new List<string>();

        //  发生qq群号
        string SendGroup = "";
        List<string> SendGroupList = new List<string>();

        // 发送微信群
        string WxSendGroup = "";
        List<string> WxSendGroupList = new List<string>();

        public MainForm()
        {

            InitializeHttpListener();

            InitializeComponent();

            taobaoService = new TaobaoService();
            transService = new TransService();


            queueHelper.StartMonitoring();

            StringBuilder sb = new StringBuilder(300);
            if(_ini.GetIniString("MQ", "MonGroup", "", sb, sb.Capacity))
            {
                MonGroupID =sb.ToString();
                MontextBox.Text = MonGroupID;
            }
            sb.Clear();
            if(_ini.GetIniString("WX", "SendGroup", "", sb, sb.Capacity))
            {
                WxSendGroup = sb.ToString();
                WxSendtextBox.Text = SendGroup;
                WxSendGroupList = new List<string>(WxSendGroup.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries));
            }
            sb.Clear();
            if (_ini.GetIniString("xlc", "blockingWords", "", sb, sb.Capacity))
            {
                blockingWords = sb.ToString();
                blockingWordsBox.Text = blockingWords;
                blockingWordsList = new List<string>(blockingWords.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries));

            }
            sb.Clear();
            if (_ini.GetIniString("xlc", "SendGroup", "", sb, sb.Capacity))
            {
                SendGroup = sb.ToString();
                SendtextBox.Text = SendGroup;
                SendGroupList = new List<string>(SendGroup.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries));

            }
            Control.CheckForIllegalCrossThreadCalls = false; //加载时 取消跨线程检查
        }
        private void InitializeHttpListener()
        {
            string url = "http://localhost:88/";
            httpListenerServer = new HttpListenerServer(url);
            httpListenerServer.PostDataReceived += OnPostDataReceived;
            StartHttpListener();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            wslog.Items.Add("test1");
            wslog.Items.Add("test2");
        }
        // 设置日志
        public void setWslog(string s)
        {
            wslog.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss      ") + s);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            wslog.Items.Add("发送：");
        }
        public bool IsBlockingWords(string msg)
        {
            foreach (var keyword in blockingWordsList)
            {
                // 如果输入文本包含屏蔽词，返回 true  
                if (msg.Contains(keyword.ToLowerInvariant()))
                {
                    return true;
                }
            }
            return false;
        }

        private void wslog_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void StartHttpListener()
        {
            Task.Run(() => httpListenerServer.StartListening());
        }
        /// <summary>
        /// 接收机器人消息
        /// </summary>
        /// <param name="data"></param>
        private void OnPostDataReceived(string data)
        {
            // 使用 Control.Invoke 将结果传递给 UI 线程
            BeginInvoke(new Action(() =>
            {
               var msg = JsonConvert.DeserializeObject<MQReceiveMsg>(data);
                if (MonGroupID.IndexOf(msg.MQ_fromID) != -1)
                {
                    var text = HttpUtility.UrlDecode(msg.MQ_msg);
                    if (IsBlockingWords(text))
                    {
                        wslog.Items.Add("已屏蔽关键词：收信人：" + msg.MQ_robot + "发送人：" + msg.MQ_fromQQ + "内容：" + text);
                    }
                    else
                    {
                        text = transService.trans(text);
                        //var link = TextUtils.ExtractLink(text);
                        //var tbcode = TextUtils.ExtractTaobaoCode(text);
                        //if (!String.IsNullOrEmpty(link) || !String.IsNullOrEmpty(tbcode))
                        //{
                        //    var res = taobaoService.trans(text);
                        //    if (res != null)
                        //    {
                        //        text = TextUtils.ReplaceLinkAndTaobaoCode(text, res.short_link, res.new_data);
                        //    }
                        //}
                        QQBroadSend.SendQQChatBack(robotqq, text, SendGroupList);

                        ////// 替换表情
                        //text = TextUtils.ReplaceEmojisWithHex(text);
                        //var picGuid = TextUtils.ExtractPicGuid(text);
                        //var qqLink = "";
                        //if (picGuid != null)
                        //{
                        //    text = text.Replace(picGuid, "");
                        //    qqLink = QQApiHelper.GetPicLink("488244998", picGuid);
                        //}
                        //QQBroadSend.SendQQChat(text, qqLink);
                        wslog.Items.Add("已发送收信人：" + msg.MQ_robot + "发送人：" + msg.MQ_fromQQ + "内容：" + text);

                    }

                    //queueHelper.Enqueue(msg);

                }
            }));
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            robotqq = QQApiHelper.GetOnlineQQlist();
            if (String.IsNullOrEmpty(robotqq))
            {
                DialogResult result = MessageBox.Show("未登录qq机器人", "提示", MessageBoxButtons.OK);
                return;
            }
            var c = QQApiHelper.GetGroupList(robotqq);
            c.ForEach(x =>
            {
                int index = MonGroupGrid.Rows.Add();
                if (MonGroupID.IndexOf(x.groupcode.ToString()) != -1)
                {

                    MonGroupGrid.Rows[index].Cells[0].Value = true;
                }
                MonGroupGrid.Rows[index].Cells[1].Value = x.groupcode;
                MonGroupGrid.Rows[index].Cells[2].Value = x.groupname;
                MonGroupGrid.Rows[index].Cells[3].Value = x.total_member;
                Console.WriteLine(x.groupcode);
            });


        }
        private void TabGroupList_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MonGroupList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MonGroupGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = MonGroupGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            string newval;
            if ((bool)MonGroupGrid.Rows[e.RowIndex].Cells[0].EditedFormattedValue)
            {
                if (MonGroupID.IndexOf(id) == -1)
                {
                    if (MonGroupID != "")
                    {
                        newval = MonGroupID + "#" + id;
                    }
                    else
                    {
                        newval = id;
                    }
                }
                else
                {
                    newval = MonGroupID;
                }
            }
            else
            {
                List<string> c = MonGroupID.Split('#').ToList<string>();
                List<string>  LIS=  c.Where(x => x.Trim()!=id).ToList();
                newval = string.Join("#", LIS);
            }
            _ini.WriteIniString("MQ", "MonGroup", newval);
            MonGroupID = newval;
            MontextBox.Text = newval;
            Console.WriteLine(MonGroupGrid.Rows[e.RowIndex].Cells[1].Value);


        }

        private void transBtn_Click(object sender, EventArgs e)
        {

            var text = orginRTB.Text;
            // 链接提取
            //string linkPattern = @"https?://\S+";
            //Match linkMatch = Regex.Match(text, linkPattern);
            //if (linkMatch.Success)
            //{
            //    string link = linkMatch.Value;
            //    Console.WriteLine("链接: " + link);
            //    orginRTB.Text = link;
            //}

            var t=  TextUtils.ExtractLink(text);
            if (t != null)
            {

                if (!String.IsNullOrEmpty(t))
                {
                    var res = taobaoService.trans(orginRTB.Text);
                    if (res != null)
                    {
                        var c = TextUtils.ReplaceLinkAndTaobaoCode(text, res.short_link, res.new_data);
                        transRTB.Text = c;
                    }
                }
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            string url = "http://49.233.142.188:8001/medical-web/industryMain.do?method=displayH5&ciphertext=50eca557d69ddb52dc23fcae30ce99d8be5db97508b4276e57809ddf7ce848636a08b3038173e97fb7c86a0f6d5f5d5abb7de8e316e11c72abdd6b64a36faf6b04e9dfe0819944e0d3a886943303dd27d1b5b13176a364606c9cbd231c28fe5c523fb227a07a51a72096f0c9a558e95c29a3ac5c7fa9ab30f74389ef14b790d9d4715b107e39d4a07e870e1ae728ec20e5d07252c30ce3bc2496fb4232da2ef689df6991e0457f62713c92fb04eb5f196fcb800ba88e8e6a9333fdafe1e90486974d0e70e20253c7b48cfacdfcf7372bac18b0eed047101ad56f6b67122c86be9a4fe5b22fcbbbff8be7a40ddfa70cf5f93ef04b93143a359b835bccff7a59b8ac1efb9daaf6271bfdf8ba46d7e1a623b88cb698fcb107c3b1201a936cdd7db1478716b8e48ed3c3ce99b995f7ac973fdeea58f6ae0401c2077849d732ed31a4"; // 替换为你要访问的页面的URL

            try
            {
                string pageContent = GetPageContent(url);

                if (!string.IsNullOrEmpty(pageContent))
                {
                    // 根据实际情况修改正则表达式以匹配你的全局变量
                    string regexPattern = @"var\s+ciphertext\s*=\s*""([^""]*)"";";
                    string Appidregex = @"var\s+appId\s*=\s*""([^""]*)"";";

                    string globalVariableValue = ExtractGlobalVariableValue(pageContent, regexPattern);
                    string ss = ExtractGlobalVariableValue(pageContent, Appidregex);

                    if (!string.IsNullOrEmpty(globalVariableValue))
                    {
                        Console.WriteLine($"Global Variable Value: {globalVariableValue}");
                    }
                    else
                    {
                        Console.WriteLine("Global variable not found on the page.");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to retrieve page content.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        static string GetPageContent(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                return client.GetStringAsync(url).Result;
            }
        }

        static string ExtractGlobalVariableValue(string pageContent, string regexPattern)
        {
            Regex regex = new Regex(regexPattern);
            Match match = regex.Match(pageContent);

            if (match.Success && match.Groups.Count > 1)
            {
                return match.Groups[1].Value;
            }

            return null;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void blockingWordsBox_Leave(object sender, EventArgs e)
        {
            _ini.WriteIniString("xlc", "blockingWords", blockingWordsBox.Text);
            blockingWords = blockingWordsBox.Text;
            Console.WriteLine("写入新的屏蔽词："+ blockingWordsBox.Text);
            blockingWordsList = new List<string>(blockingWords.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries));

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _ini.WriteIniString("xlc", "SendGroup", SendtextBox.Text);
            SendGroup = SendtextBox.Text;
            blockingWordsList = new List<string>(SendGroup.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries));
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            QQBroadSend.MoveWindowToTopLeft();
        }

        private void getWxGroupListBtn_Click(object sender, EventArgs e)
        {
            var list = WxRobotHelper.GetGroupList();

            //把群列表显示到表格中
            wxGroupList.Rows.Clear();
            foreach (var item in list)
            {
                int index = wxGroupList.Rows.Add();
                if (WxSendGroup.IndexOf(item.wxid) != -1)
                {
                    wxGroupList.Rows[index].Cells[0].Value = true;
                }
                wxGroupList.Rows[index].Cells[1].Value = item.wxid;
                wxGroupList.Rows[index].Cells[2].Value = item.nick;
                wxGroupList.Rows[index].Cells[3].Value = item.groupMemberNum.ToString();
            }

            
        }

        private void wxGroupList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string wxid = wxGroupList.Rows[e.RowIndex].Cells[1].Value.ToString();
            string newval;
            if ((bool)wxGroupList.Rows[e.RowIndex].Cells[0].EditedFormattedValue)
            {
                if (WxSendGroup.IndexOf(wxid) == -1)
                {
                    if (WxSendGroup != "")
                    {
                        newval = WxSendGroup + "#" + wxid;
                    }
                    else
                    {
                        newval = wxid;
                    }
                }
                else
                {
                    newval = WxSendGroup;
                }
            }
            else
            {
                List<string> c = WxSendGroup.Split('#').ToList<string>();
                List<string> LIS = c.Where(x => x.Trim() != wxid).ToList();
                newval = string.Join("#", LIS);
            }
            _ini.WriteIniString("WX", "SendGroup", newval);
            WxSendGroup = newval;
            WxSendtextBox.Text = newval;
            WxSendGroupList = new List<string>(WxSendGroup.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries));
        }

        private void WxSendtextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
