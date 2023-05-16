using System;
using System.Collections;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class CustomStack<T> : IEnumerable<T>, IStack<T>
    {
        private LightLinkedList<T> _container = new();
        public int Count { get; private set; }

        public void Push(T obj)
        {           
            _container.Add(obj);
            Count++;
        }

        public T Pop()        
        {
            if (Count == 0)
                throw new InvalidOperationException("There is no more elements in stack.");
            var result = _container.Last;
            _container.RemoveLast();
            Count--;
            return result;
        }        

        public T Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException("Stack is empty.");
            return _container.Last;
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
