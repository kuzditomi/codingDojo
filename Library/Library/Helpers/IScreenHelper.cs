using System;
using System.Collections.Generic;
using Library.Contracts.Models;

namespace Library.Helpers
{
    public interface IScreenHelper
    {
        Book GetNewBookDetails();
        void Reset();
        void PrintBookDetails(Book book, string reader);
        void PrintSearchResult(IEnumerable<Book> result);
        Reader GetNewReader();
        int GetBookId();
        int GetDueDate();
        string ReadInputString(string property);
        void PrintBookAddedMessage(Book book);
        void PrintBookBorrowedMessage(Book book, Reader reader);
        void PrintListOfBooks();
        void GetLimit();
        void PrintLazyLoading();
        void PrintEagerLoading();
        void PrintElapsedTime(TimeSpan elapsed);
        void PrintSuccessfulSeeding();
    }
}
