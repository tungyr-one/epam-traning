namespace ClassLibrary.Queue
{
    public interface IQueueNumerable<T>
    {
        public int Count { get; }
        public QueueNode<T> End { get; set; }
        public QueueNode<T> Start { get; set; }
        public void Enqueue(T item);
        public T Dequeue();
        public T Peak();
    }
}
