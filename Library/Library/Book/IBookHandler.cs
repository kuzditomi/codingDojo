namespace Library.Book
{
    public interface IBookHandler
    {
        void AddNewBook();
        void BorrowBook();
        void GenerateBooks();
        void GetBooksList();
        void ReturnBook();
        void SearchForBook();
    }
}