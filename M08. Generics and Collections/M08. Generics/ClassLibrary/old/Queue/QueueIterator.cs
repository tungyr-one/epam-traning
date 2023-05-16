using System;

namespace ClassLibrary.Queue
{
    public class QueueIterator<T> : IQueueIterator<T>
    {
        public QueueNode<T> Current { get; set; }
        public CustomNewQueue<T> InputQueue { get; set; }

        public QueueIterator(CustomNewQueue<T> obj)
        {
            InputQueue = obj;
            Current = obj.Start;
        }

        virtual public void First()
        {
            Current = InputQueue.Start;
        }

        virtual public bool IsEmpty()
        {
            return Current == null;
        }

        virtual public T Next()
        {
            if (IsEmpty())
            {
                throw new ArgumentOutOfRangeException();
            }

            T item = Current.Value;
            Current = Current.Next;

            return item;
        }
    }
}
