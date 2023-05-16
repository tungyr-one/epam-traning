namespace ClassLibrary
{
    public interface ILightLinkedList<T>
    {
        int Count { get; }
        T First { get; }
        Node<T> Head { get; }
        T Last { get; }
        void Add(T data);
        void RemoveFirst();
        void RemoveLast();
    }
}
