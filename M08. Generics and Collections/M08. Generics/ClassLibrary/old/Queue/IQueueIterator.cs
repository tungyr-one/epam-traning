namespace ClassLibrary.Queue
{
    internal interface IQueueIterator<T>
    {
        void First();
        bool IsEmpty();
        T Next();
    }
}
