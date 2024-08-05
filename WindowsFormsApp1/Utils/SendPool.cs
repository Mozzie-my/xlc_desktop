using NLog;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using WindowsFormsApp1;
using WindowsFormsApp1.Model;
using WindowsFormsApp1.Service;
using WindowsFormsApp1.Utils; // 假设我们使用WinForms  
using WinFormsListBox = System.Windows.Forms.ListBox;
public class MessagePool
{
    private const int MessageCheckInterval = 100; // 检查新消息的间隔时间
    private const int MessageProcessingDelay = 300; // 处理消息的延迟时间
    private bool _isProcessing = false;
    private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
    private Queue<Msg> _messages = new Queue<Msg>();
    private TransService transService;
    private WinFormsListBox _targetControl;
    private WinFormsListBox _logListBox;
    private int sleepTime = 300;
    public bool IsWxFlag { get; set; } = false;
    public bool IsQqFlag { get; set; } = false;

    private Task _processingTask;

    public MessagePool(WinFormsListBox targetControl, WinFormsListBox logTextBox)
    {
        _targetControl = targetControl;
        _logListBox = logTextBox; // 日志框
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

    private async Task ProcessMessagesAsync()
    {
            Msg message = null;
            lock (_messages)
            {
                if (_messages.Count > 0)
                {
                    message = _messages.Dequeue();
                }
            }

            if (message != null)
            {
                await ProcessMessageAsync(message);
                UpdateControl();
                Console.WriteLine(message.content);
            }
    }

    private async Task ProcessMessageAsync(Msg message)
    {
        try
        {
            message.content = await transService.Trans(message.content, "复制本条消息打开桃宝APP弹窗");
            if (message.picUrl != null)
            {
                if (IsQqFlag)
                {
                    QQBroadSend.SendQQChatBack(MainForm.robotqq, message.content, MainForm.SendGroupList);
                }
                if (IsWxFlag)
                {
                    var qqLink = TextUtils.ExtractPicGuid(message.picUrl, MainForm.robotqq);
                    if (qqLink != null)
                    {
                        string picPattern = @"\[pic=(.*?)\]";
                        message.content = Regex.Replace(message.content, picPattern, "");
                    }

                    message.content = message.content.Replace("\r\n\r\n", "\r");
                    message.content = message.content.Replace("\r\n", "\r");
                    message.content = message.content.TrimEnd('\r', '\n');
                    WxBoardSend.SendMsg(MainForm.WxSendGroupList, message.content, qqLink);
                }

                UpdateControl();
            }
            else
            {
                // 记录错误日志
                _logListBox.Items.Add(" 消息返回为空：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + message);
            }
        }
        catch (Exception ex)
        {
            // 记录错误日志
            _logListBox.Items.Add(" 处理消息失败：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ex.Message);
            logger.Error(ex, "处理消息失败");
            _logListBox.TopIndex = _logListBox.Items.Count - 1;
            // 可以选择继续处理下一个消息或采取其他措施

        }
        await Task.Delay(MessageProcessingDelay); // 模拟处理消息过程，延迟300ms
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
    public async Task  StartProcessing()
    {
        int i = 0;
        _isProcessing = true;
        while (true)
        {
            if (!_isProcessing)
            {
                break;
            }
            if(i % 10 == 0)
            {
                logger.Info(i + "消息队列中消息数量：" + _messages.Count);
            }
            await Task.Run(() => ProcessMessagesAsync());
            await Task.Delay(1000);
        }
    }

    // 停止处理消息  
    public void StopProcessing()
    {

        _isProcessing = false;
    }
}