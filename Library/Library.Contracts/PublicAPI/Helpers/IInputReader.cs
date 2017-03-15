namespace Library.Contracts.PublicAPI.Helpers
{
    public interface IInputReader<T>
    {
        T Read();
    }
}
