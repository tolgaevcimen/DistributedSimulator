using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace AsyncSimulator
{
    public class MessageQueue<T> where T : Message
    {
        private readonly ConcurrentQueue<T> queue = new ConcurrentQueue<T>();
        public event EventHandler MessageAdded;
        protected virtual void OnMessageAdded()
        {
            MessageAdded?.Invoke(this, EventArgs.Empty);
        }
        public virtual void Enqueue(T item)
        {
            if (queue.Any(m => m.ToString() == item.ToString()))
            {
                Trace.WriteLine(String.Format("item({0}) already exists", item));
                return;
            }
            
            queue.Enqueue(item);
            OnMessageAdded();
        }
        public int Count { get { return queue.Count; } }

        public virtual T Dequeue()
        {
            T item;
            if (queue.IsEmpty) return default(T);

            while (!queue.TryDequeue(out item))
            {
                Console.WriteLine("couldn't dequeue, will wait 50 ms");
                Thread.Sleep(50);
            }
            return item;
        }

        public void ForEach(Action<Message> method)
        {
            foreach (var message in queue)
            {
                method(message);
            }
        }
    }
}
