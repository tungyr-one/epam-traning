using System;
using System.Collections;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class CustomQueue<T> : IEnumerable<T>, IQueue<T>
    {
        private LightLinkedList<T> _container = new();
        public int Count { get; private set; }

        public void Enqueue(T obj)
        {
            _container.Add(obj);
            Count++;
        }

        public T Dequeue()
        {
            if (Count == 0)
                throw new InvalidOperationException("There is no more elements in queue.");
            var result = _container.First;
            _container.RemoveFirst();
            Count--;
            return result;

        }

        public T Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException("Queue is empty.");
            return _container.First;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = _container.Head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
