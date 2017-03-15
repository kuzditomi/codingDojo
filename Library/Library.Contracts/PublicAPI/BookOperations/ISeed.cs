namespace Library.Contracts.PublicAPI.BookOperations
{
    public interface ISeed
    {
        void GenerateData(int amount, IBorrow borrow);
    }
}