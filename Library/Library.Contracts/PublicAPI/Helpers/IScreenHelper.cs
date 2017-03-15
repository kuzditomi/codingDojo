using System;
using System.Collections.Generic;
using Library.Contracts.Models;

namespace Library.Contracts.PublicAPI.Helpers
{
    public interface IScreenHelper
    {
        Book GetNewBookDetails();
        void Reset();
        void PrintBookDetails(Book book, string readerName);
        void PrintSearchResult(IEnumerable<Book> result);
        Reader GetNewReader();
        int GetBookId();
        int GetBorrowDays();
        string ReadInputString(string property);
        void PrintBookAddedMessage(Book book);
        void PrintBookBorrowedMessage(Book book, Reader reader);
        void PrintBookListingText();
        void GetLimit();
        void PrintLazyLoading();
        void PrintEagerLoading();
        void PrintElapsedTime(TimeSpan elapsed);
        void PrintSuccessfulSeeding();
    }
}
