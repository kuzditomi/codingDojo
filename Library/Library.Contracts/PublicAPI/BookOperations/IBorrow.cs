using Library.Contracts.Models;

namespace Library.Contracts.PublicAPI.BookOperations
{
    public interface IBorrow
    {
        void PerformBorrowingProcess();
        Book BorrowSingleBook(int id, Reader reader, int daysToBorrow);
    }
}