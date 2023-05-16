using System;
using System.Collections;
using System.Collections.Generic;


namespace ClassLibrary.Stack
{
    public class CustomNewStack<T> : IEnumerable<T>
    {
        public StackNode<T> Start { get; set; }
        public StackNode<T> End { get; set; }
        public StackNode<T> Current { get; set; }

        // returns the Start without removing it
        public T Peek()
        {
            if (Start == null)
                throw new Exception("the stack is empty");
            return Start.Value;
        }

        // returns the Start and removes it
        public T Pop()
        {
            if (Start == null)
                throw new Exception("the stack is empty");

            Current = Start;
            Start = Start.Next;
            return Current.Value;
        }

        public void Push(T item)
        {
            var node = new StackNode<T>(item);

            if (Start != null)
                node.Next = Start;
            else
            {
                node.Next = null;
                End = node;
            }

            Start = node;
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
            throw new NotImplementedException();
        }
    }
}
