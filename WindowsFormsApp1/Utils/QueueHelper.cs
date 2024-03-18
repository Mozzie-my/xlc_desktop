using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.Utils
{
    public class QueueHelper<T> where T : class, new()
    {
        public static QueueHelper<T> Instance = new QueueHelper<T>();
        private ConcurrentQueue<T> queue = new ConcurrentQueue<T>();

        public void Enqueue(T t)
        {
            queue.Enqueue(t);

            // 添加元素后，触发监控操作
            OnQueueUpdated();
        }

        public T Dequeue()
        {
            if (queue.Count > 0)
            {
                T obj = new T();

                if (queue.TryDequeue(out obj))
                    return obj;
                else
                    return null;
            }
            else
            {
                return null;
            }
        }

        // 事件通知机制，用于通知队列更新
        public event EventHandler QueueUpdated;

        // 触发队列更新事件
        protected virtual void OnQueueUpdated()
        {
            QueueUpdated?.Invoke(this, EventArgs.Empty);
        }

        // 启动监控线程
        public void StartMonitoring()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    // 每隔一段时间检查一次队列是否有更新
                    Thread.Sleep(1000);

                    if (queue.Count > 0)
                    {

                        // 执行出队操作
                        var dequeuedItem = Dequeue();

                        if (dequeuedItem != null)
                        {
                            // 在实际应用中，你可以在这里调用你的处理逻辑，使用 dequeuedItem 对象
                            Console.WriteLine("Dequeued item: " + dequeuedItem.ToString());
                        }
                    }
                }
            });
        }
    }   
}
