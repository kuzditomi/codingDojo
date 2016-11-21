﻿namespace Library.Book
{
    public interface IBookHandler
    {
        void AddNewBook();
        void BorrowBook();
        void GenerateBooks();
        void GetBooksList();
        void GetExpiringBooks();
        void ReturnBook();
        void SearchForBook();
    }
}