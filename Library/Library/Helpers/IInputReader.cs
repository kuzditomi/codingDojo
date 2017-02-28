namespace Library.Helpers
{
    public interface IInputReader<T>
    {
        T Read(int boundary);
    }
}
