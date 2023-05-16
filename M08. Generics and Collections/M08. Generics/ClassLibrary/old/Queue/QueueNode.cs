namespace ClassLibrary.Queue
{
    public class QueueNode<T>
    {
        public QueueNode(T value)
        {
            this.Value = value;
        }

        public QueueNode<T> Next { get; set; }
        public T Value { get; private set; }
    }
}
