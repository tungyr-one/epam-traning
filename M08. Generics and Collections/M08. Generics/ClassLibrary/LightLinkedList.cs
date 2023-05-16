using System;

namespace ClassLibrary
{
    public class LightLinkedList<T> : ILightLinkedList<T>
    { 
        public int Count { get; private set; }
        public T First { get; private set; }
        public Node<T> Head { get; private set; }
        public T Last { get; private set; }

        public LightLinkedList()
        {
        }

        public void Add(T data)
        {
            if (Head == null)
            {
                Head = new(data);
                First = Last = Head.Data;
            }
            else
            {
                Node<T> newNode = new(data);
                Node<T> current = Head;

                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = newNode;
                Last = current.Next.Data;
            }
            Count++;
        }

        public void RemoveFirst()
        {
            if (Count <= 0)
            {
                throw new InvalidOperationException("List is empty.");
            }
            
            if(Count == 1)
            {
                Head = null;
                First = Last = default;
                Count--;
            }
            else
            {
                Head = Head.Next;
                First = Head.Data;
                Count--;
            }
        }

        public void RemoveLast()
        {
            if (this.Count <= 0)
            {
                throw new InvalidOperationException("List is empty.");
            }

            if (Count == 1)
            {
                Head = null;
                First = Last = default;
                Count--;
            }
            else
            {
                Node<T> current = Head;
                Node<T> previous = null;

                while (current.Next != null)
                {
                    previous = current;
                    current = current.Next;
                }

                Last = previous.Data;
                previous.Next = null;
            }
            Count--;
        }
    }
}
