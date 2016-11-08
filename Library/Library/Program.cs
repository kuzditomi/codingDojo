namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            var bookHandler = new BookHandler();
            var menuItem = 9;

            do
            {
                switch (menuItem)
                {
                    case 9:
                        menuItem = MenuHandler.DoMainMenuSelection();
                        break;
                    case 1:
                        bookHandler.AddNewBook();
                        menuItem = 9;
                        break;
                    case 2:
                        bookHandler.SearchForBook();
                        menuItem = 9;
                        break;
                    case 3:
                        bookHandler.BorrowBook();
                        menuItem = 9;
                        break;
                    case 4:
                        bookHandler.ReturnBook();
                        menuItem = 9;
                        break;
                    case 5:
                        bookHandler.GetBooksList();
                        menuItem = 9;
                        break;
                    case 6:
                        bookHandler.GenerateBooks();
                        menuItem = 9;
                        break;
                }
            } while (menuItem != 0);
        }
    }
}
