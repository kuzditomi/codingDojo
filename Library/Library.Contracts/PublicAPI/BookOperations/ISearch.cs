namespace Library.Contracts.PublicAPI.BookOperations
{
    public interface ISearch
    {
        void SearchBooks();
        void SearchForTitle();
        void SearchForAuthor();
        void SearchForReader();
        void SearchForYear();
        void SearchBeforeYear();
        void SearchAfterYear();
    }
}