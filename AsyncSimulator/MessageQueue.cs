using System;
using System.Collections.Concurrent;
using System.Threading;

namespace AsyncSimulator
{
    public class MessageQueue<T>
    {
        private readonly ConcurrentQueue<T> queue = new ConcurrentQueue<T>();
        public event EventHandler Changed;
        protected virtual void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }
        public virtual void Enqueue(T item)
        {
            queue.Enqueue(item);
            OnChanged();
        }
        public int Count { get { return queue.Count; } }

        public virtual T Dequeue()
        {
            T item;
            if (queue.IsEmpty) return default(T);

            while (!queue.TryDequeue(out item)) { Thread.Sleep(50); }
            OnChanged();
            return item;
        }
    }
}
