namespace Library.Contracts.PublicAPI.BookOperations
{
    public interface ILoad
    {
        void LazyLoad();
        void EagerLoad();
    }
}