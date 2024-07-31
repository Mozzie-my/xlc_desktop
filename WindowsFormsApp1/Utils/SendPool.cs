using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;
using WindowsFormsApp1.Model;
using WindowsFormsApp1.Service;
using WindowsFormsApp1.Utils; // 假设我们使用WinForms  

public class MessagePool
{
    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
    private Queue<Msg> _messages = new Queue<Msg>();
    private TransService transService;
    private ListBox _targetControl;
    private int sleepTime = 300;
    public bool IsWxFlag { get; set; } = false;
    public bool IsQqFlag { get; set; } = false;

    public MessagePool(ListBox targetControl)
    {
        _targetControl = targetControl;
        transService = new TransService();

    }

    public void AddMessage(Msg message)
    {
        lock (_messages)
        {
            _messages.Enqueue(message);

            // 如果控件是UI控件，并且需要更新UI，需要确保在UI线程上执行  
            if (_targetControl.InvokeRequired)
            {
                _targetControl.Invoke(new MethodInvoker(delegate { UpdateControl(); }));
            }
            else
            {
                DisplayMessage(message);
            }
        }
    }

    // 处理消息的方法  
    private void ProcessMessages(CancellationToken cancellationToken)
    {
        while (true)
        {
            Msg message;
            //lock (_messages)  
            //{  
            if (_messages.Count > 0)
            {
                message = _messages.Dequeue();
                message.content = transService.trans(message.content, "复制本条消息打开桃宝APP弹窗");
                if (IsQqFlag)
                {
                    QQBroadSend.SendQQChatBack(MainForm.robotqq, message.content, MainForm.SendGroupList);
                }
                if (IsWxFlag)
                {
                    // 加入到发送池内
                    //// 替换表情
                    var qqLink = TextUtils.ExtractPicGuid(message.picUrl, MainForm.robotqq);
                    if (qqLink != null)
                    {
                        string picPattern = @"\[pic=(.*?)\]";
                        if (message.content != null)
                        {

                            message.content = Regex.Replace(message.content, picPattern, "");
                        }
                    }
                    // 如果有两个\r\n则替换为一个，文末去掉空行
                    message.content = message.content.Replace("\r\n\r\n", "\r");
                    message.content = message.content.Replace("\r\n", "\r");
                    message.content = message.content.TrimEnd('\r', '\n');
                    WxBoardSend.SendMsg(MainForm.WxSendGroupList, message.content, qqLink);
                }

                UpdateControl();
                Console.WriteLine(message.content);
            }
            else
            {
                Thread.Sleep(100); // 等待新消息  
                continue;
            }

            Thread.Sleep(sleepTime); // 模拟处理消息过程，延迟300ms
                                     //}  

            // 假设有一个方法将消息显示到控件上  
            //DisplayMessage(message);  
        }
    }

    private void DisplayMessage(Msg message)
    {
        // 这里假设_targetControl是一个支持显示文本的控件，如ListBox, TextBox等  
        // 需要根据实际情况调整  
        if (_targetControl.InvokeRequired)
        {
            _targetControl.Invoke(new MethodInvoker(delegate { _targetControl.Text += $"{message.content}\n"; }));
        }
        else
        {
            _targetControl.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 以加入到消息队列中：" + message.content);
        }

    }

    private void UpdateControl()
    {
        // 这里可以添加一些额外的UI更新逻辑，比如滚动到最新消息  
        _targetControl.Items.RemoveAt(0);
    }

    // 启动处理消息  
    public void StartProcessing()
    {
        Task.Run(() => ProcessMessages(_cancellationTokenSource.Token));
    }

    // 停止处理消息  
    public void StopProcessing()
    {
        _cancellationTokenSource.Cancel();
        // 可选：等待 ProcessMessages 任务完成（如果需要的话）  
        // 注意：在这个例子中，我们不会等待它完成，因为 ProcessMessages 可能会无限循环  
        // 并且我们依赖于 CancellationToken 来优雅地退出循环  
    }
}