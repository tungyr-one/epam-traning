using System;
using System.Collections;
using System.Collections.Generic;

namespace ClassLibrary.Queue
{
    public class CustomNewQueue<T> : IQueueNumerable<T>, IEnumerable<T>
    {
        public CustomNewQueue()
        {
            this.Count = 0;
            Current = Start;
        }

        public int Count { get; private set; }
        public QueueNode<T> Start { get; set; }
        public QueueNode<T> End { get; set; }
        public QueueNode<T> Current { get; set; }

        public T Dequeue()
        {
            if (this.Count <= 0)
            {
                throw new InvalidOperationException("Queue is empty.");
            }

            var elementToReturn = this.Start;
            this.Start = this.Start.Next;
            this.Count--;

            return elementToReturn.Value;
        }

        public void Enqueue(T item)
        {
            var newItem = new QueueNode<T>(item);

            if (this.Count == 0)
            {
                this.Start = this.End = newItem;
            }
            else
            {
                this.End.Next = newItem;
                this.End = newItem;
            }

            this.Count++;
        }

        public T Peak()
        {
            if (this.Count <= 0)
            {
                throw new InvalidOperationException("Queue is empty.");
            }

            Current = Start;
            return Current.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Current = Start;
            while (Current != null)
            {                
                T item = Current.Value;
                Current = Current.Next;

                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}




