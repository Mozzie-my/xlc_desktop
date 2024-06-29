using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WindowsInput.Native;
using WindowsInput;

namespace WindowsFormsApp1.Utils
{
    class QQBroadSend
    {



        public static string ChatWindowName = "";
        const int GWL_STYLE = -16;
        #region bVk参数 常量定义

        public const byte vbKeyLButton = 0x1;    // 鼠标左键
        public const byte vbKeyRButton = 0x2;    // 鼠标右键
        public const byte vbKeyCancel = 0x3;     // CANCEL 键
        public const byte vbKeyMButton = 0x4;    // 鼠标中键
        public const byte vbKeyBack = 0x8;       // BACKSPACE 键
        public const byte vbKeyTab = 0x9;        // TAB 键
        public const byte vbKeyClear = 0xC;      // CLEAR 键
        public const byte vbKeyReturn = 0xD;     // ENTER 键
        public const byte vbKeyShift = 0x10;     // SHIFT 键
        public const byte vbKeyControl = 0x11;   // CTRL 键
        public const byte vbKeyAlt = 18;         // Alt 键  (键码18)
        public const byte vbKeyMenu = 0x12;      // MENU 键
        public const byte vbKeyPause = 0x13;     // PAUSE 键
        public const byte vbKeyCapital = 0x14;   // CAPS LOCK 键
        public const byte vbKeyEscape = 0x1B;    // ESC 键
        public const byte vbKeySpace = 0x20;     // SPACEBAR 键
        public const byte vbKeyPageUp = 0x21;    // PAGE UP 键
        public const byte vbKeyEnd = 0x23;       // End 键
        public const byte vbKeyHome = 0x24;      // HOME 键
        public const byte vbKeyLeft = 0x25;      // LEFT ARROW 键
        public const byte vbKeyUp = 0x26;        // UP ARROW 键
        public const byte vbKeyRight = 0x27;     // RIGHT ARROW 键
        public const byte vbKeyDown = 0x28;      // DOWN ARROW 键
        public const byte vbKeySelect = 0x29;    // Select 键
        public const byte vbKeyPrint = 0x2A;     // PRINT SCREEN 键
        public const byte vbKeyExecute = 0x2B;   // EXECUTE 键
        public const byte vbKeySnapshot = 0x2C;  // SNAPSHOT 键
        public const byte vbKeyDelete = 0x2E;    // Delete 键
        public const byte vbKeyHelp = 0x2F;      // HELP 键
        public const byte vbKeyNumlock = 0x90;   // NUM LOCK 键

        //常用键 字母键A到Z
        public const byte vbKeyA = 65;
        public const byte vbKeyB = 66;
        public const byte vbKeyC = 67;
        public const byte vbKeyD = 68;
        public const byte vbKeyE = 69;
        public const byte vbKeyF = 70;
        public const byte vbKeyG = 71;
        public const byte vbKeyH = 72;
        public const byte vbKeyI = 73;
        public const byte vbKeyJ = 74;
        public const byte vbKeyK = 75;
        public const byte vbKeyL = 76;
        public const byte vbKeyM = 77;
        public const byte vbKeyN = 78;
        public const byte vbKeyO = 79;
        public const byte vbKeyP = 80;
        public const byte vbKeyQ = 81;
        public const byte vbKeyR = 82;
        public const byte vbKeyS = 83;
        public const byte vbKeyT = 84;
        public const byte vbKeyU = 85;
        public const byte vbKeyV = 86;
        public const byte vbKeyW = 87;
        public const byte vbKeyX = 88;
        public const byte vbKeyY = 89;
        public const byte vbKeyZ = 90;

        //数字键盘0到9
        public const byte vbKey0 = 48;    // 0 键
        public const byte vbKey1 = 49;    // 1 键
        public const byte vbKey2 = 50;    // 2 键
        public const byte vbKey3 = 51;    // 3 键
        public const byte vbKey4 = 52;    // 4 键
        public const byte vbKey5 = 53;    // 5 键
        public const byte vbKey6 = 54;    // 6 键
        public const byte vbKey7 = 55;    // 7 键
        public const byte vbKey8 = 56;    // 8 键
        public const byte vbKey9 = 57;    // 9 键


        public const byte vbKeyNumpad0 = 0x60;    //0 键
        public const byte vbKeyNumpad1 = 0x61;    //1 键
        public const byte vbKeyNumpad2 = 0x62;    //2 键
        public const byte vbKeyNumpad3 = 0x63;    //3 键
        public const byte vbKeyNumpad4 = 0x64;    //4 键
        public const byte vbKeyNumpad5 = 0x65;    //5 键
        public const byte vbKeyNumpad6 = 0x66;    //6 键
        public const byte vbKeyNumpad7 = 0x67;    //7 键
        public const byte vbKeyNumpad8 = 0x68;    //8 键
        public const byte vbKeyNumpad9 = 0x69;    //9 键
        public const byte vbKeyMultiply = 0x6A;   // MULTIPLICATIONSIGN(*)键
        public const byte vbKeyAdd = 0x6B;        // PLUS SIGN(+) 键
        public const byte vbKeySeparator = 0x6C;  // ENTER 键
        public const byte vbKeySubtract = 0x6D;   // MINUS SIGN(-) 键
        public const byte vbKeyDecimal = 0x6E;    // DECIMAL POINT(.) 键
        public const byte vbKeyDivide = 0x6F;     // DIVISION SIGN(/) 键


        //F1到F12按键
        public const byte vbKeyF1 = 0x70;   //F1 键
        public const byte vbKeyF2 = 0x71;   //F2 键
        public const byte vbKeyF3 = 0x72;   //F3 键
        public const byte vbKeyF4 = 0x73;   //F4 键
        public const byte vbKeyF5 = 0x74;   //F5 键
        public const byte vbKeyF6 = 0x75;   //F6 键
        public const byte vbKeyF7 = 0x76;   //F7 键
        public const byte vbKeyF8 = 0x77;   //F8 键
        public const byte vbKeyF9 = 0x78;   //F9 键
        public const byte vbKeyF10 = 0x79;  //F10 键
        public const byte vbKeyF11 = 0x7A;  //F11 键
        public const byte vbKeyF12 = 0x7B;  //F12 键

        #endregion
        //找窗体
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        //拖动窗体
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        #region 引用win32api方法
        /// <summary>
        /// 设置此窗体为活动窗体
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        /// <summary>
        /// 导入模拟键盘的方法
        /// </summary>
        /// <param name="bVk" >按键的虚拟键值</param>
        /// <param name= "bScan" >扫描码，一般不用设置，用0代替就行</param>
        /// <param name= "dwFlags" >选项标志：0：表示按下，2：表示松开</param>
        /// <param name= "dwExtraInfo">一般设置为0</param>
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        /// <summary>
        /// 获取窗口标题
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="lpString">标题</param>
        /// <param name="nMaxCount">最大值</param>
        /// <returns></returns>
        [DllImport("user32", SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        /// <summary>
        /// 获取类的名字
        /// </summary>
        /// <param name="hWnd">句柄</param>
        /// <param name="lpString">类名</param>
        /// <param name="nMaxCount">最大值</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        /// <summary>
        /// 枚举窗口回调处理
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public delegate bool EnumWindowsCallback(IntPtr hwnd, int lParam);
        /// <summary>
        /// 枚举窗口
        /// </summary>
        /// <param name="callPtr"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern int EnumWindows(EnumWindowsCallback callPtr, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);


        [DllImport("user32.dll")]
        static extern void PostMessage(IntPtr hwnd, uint msg, int w, string l);
        [DllImport("user32.dll")]
        static extern void PostMessage(IntPtr hwnd, uint msg, int w, int l);
        [DllImport("user32.dll ", EntryPoint = "SendMessage ")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, string lParam);
        #endregion
        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        // 导入SetWindowPos函数  
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        // 窗口位置标志  
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOZORDER = 0x0004;


        public static void PressKey(Keys key, bool up)
        {
            const int KEYEVENTF_EXTENDEDKEY = 0x1;
            const int KEYEVENTF_KEYUP = 0x2;
            if (up)
            {

                keybd_event((byte)key, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);

            }
            else
            {

                keybd_event((byte)key, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);

            }
        }
        /// <summary>
        /// 获取窗口句柄
        /// </summary>
        /// <param name="className"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        static List<IntPtr> GetWindowsByClassNameAndStyle(string className, string style)
        {
            List<IntPtr> result = new List<IntPtr>();

            EnumWindows((hWnd, lParam) =>
            {
                StringBuilder windowClass = new StringBuilder(256);
                GetClassName(hWnd, windowClass, windowClass.Capacity);

                int windowStyle = GetWindowLong(hWnd, GWL_STYLE);

                if (windowClass.ToString() == className && windowStyle.ToString("X") == style)
                {
                    result.Add(hWnd);
                }

                return true;
            }, 0);

            return result;
        }

        /// <summary>
        /// 发送QQ聊天
        /// </summary>
        /// <param name="msg"></param>
        public static void SendQQChat(string msg, string picLink)
        {
            int times = 300;
            Image picdata = null;   
            if (!String.IsNullOrEmpty(picLink))
            {
                picdata = CopyImageAndTextToClipboard(picLink, msg);
            }

            Thread.Sleep(times);
            // 获取窗口句柄
            var c = GetWindowsByClassNameAndStyle("TXGuiFoundation", "960F0000");
            c.ForEach(hwnd =>
            {
                Clipboard.SetText(msg);
                SetForegroundWindow(hwnd);
                //StringBuilder windowTitle = new StringBuilder(256);
                //GetWindowText(hwnd, windowTitle, windowTitle.Capacity);
    
                SendKeys.SendWait("^V");
                //Thread.Sleep(times);
                //设置剪切板数据
                //Clipboard.SetText(msg);
                //PostMessage(hwnd, 194, 0, msg);//向QQ输入框粘贴字符，this.textBox1.Text是要发送的文字信息
                //SendCtrlV(hwnd);
                if (picdata != null)
                {
                    Thread.Sleep(times);
                    Clipboard.SetImage(picdata);
                    //SendCtrlV(hwnd);
                    SendKeys.SendWait("^V");

                }

                SendKeys.Send("{ENTER}");
                Thread.Sleep(times);
                //SendCtrlEnter(hwnd);
            });

            // 如果有图片先去下载下图片

        }

        // 将窗口移动到屏幕的左上角  
        public static void MoveWindowToTopLeft()
        {
            // 窗口的新位置（屏幕的左上角）  
            int x = 0;
            int y = 0;
            var c = GetWindowsByClassNameAndStyle("TXGuiFoundation", "960F0000");
            c.ForEach((hwnd) =>
            {
                // 调用SetWindowPos，不改变窗口大小，不改变Z顺序  
                SetWindowPos(hwnd, IntPtr.Zero, x, y, 0, 0, SWP_NOSIZE | SWP_NOZORDER);

            });
        }

        /// <summary>
        /// 发送QQ聊天bacjk
        /// </summary>
        /// <param name="msg"></param>
        public static void SendQQChatBack(string robotqq,string msg,List<string> groupid)
        {
            int times = 300;
            groupid.ForEach(hwnd =>
            {
                QQApiHelper.SendQQMsg(robotqq, hwnd, msg);
                Thread.Sleep(times);
            });

        }

        /// <summary>
        /// 给指定窗口发送Ctrl+V 粘贴命令
        /// </summary>
        /// <param name="hwnd"></param>
        public static void SendCtrlV(IntPtr hwnd)
        {




            PressKey(Keys.ControlKey, false);
            PressKey(Keys.V, false);
            PressKey(Keys.V, true);
            PressKey(Keys.ControlKey, true);

            //var inputSimulator = new InputSimulator();

            //inputSimulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
            //inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_V); // 使用正确的枚举值 VirtualKeyCode.V 来表示 'V' 键。  
            //inputSimulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
        }

        /// <summary>
        /// 给指定窗口发送Ctrl+Enter 粘贴命令
        /// </summary>
        /// <param name="hwnd"></param>
        public static void SendCtrlEnter(IntPtr hwnd)
        {
            //模拟按下ctrl键
            //keybd_event(vbKeyControl, 0, 0, 0);
            //模拟按下回车
            keybd_event(vbKeyReturn, 0, 0, 0);
            //松开按键ctrl
            //keybd_event(vbKeyControl, 0, 2, 0);
            //松开按键回车
            keybd_event(vbKeyReturn, 0, 2, 0);
        }


        public static Image CopyImageAndTextToClipboard(string imageUrl, string text)
        {
            // 下载图片  
            byte[] imageBytes = DownloadImage(imageUrl);
            if (imageBytes == null)
            {
                throw new Exception("无法下载图片");
            }

            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                // 将字节流转换为Bitmap对象  
                Image image = Image.FromStream(ms);

                // 创建一个包含文字和图片的DataObject  
                //DataObject dataObject = new DataObject();
                //dataObject.SetText(text);
                //dataObject.SetData(DataFormats.Bitmap, image);

                // 将DataObject放入剪贴板  
                return image;
            }
        }

        private static byte[] DownloadImage(string imageUrl)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    return webClient.DownloadData(imageUrl);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("下载图片时出错: " + ex.Message);
                return null;
            }
        }
    }
}
