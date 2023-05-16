
namespace ClassLibrary.Stack
{
    public class StackNode<T>
    {
        public StackNode(T value)
        {
            this.Value = value;
        }

        public StackNode<T> Next { get; set; }
        public T Value { get; private set; }
        
    }
}
