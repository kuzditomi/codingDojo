using Library.BookOperations;
using Library.Helpers;
using Library.Menu;
using Autofac;
using Library.IOC;

namespace Library
{
    class Program
    {
        private static readonly Resolver Resolver = new Resolver();
        private static IContainer Container { get; set; }


        static void Main(string[] args)
        {
            Container = Resolver.BuildContainer().Build();
            var add = Container.Resolve<IAdd>();
            var search = Container.Resolve<ISearch>();
            var borrow = Container.Resolve<IBorrow>();
            var takeBack = Container.Resolve<ITakeBack>();
            var fetch = Container.Resolve<IFetch>();
            var seed = Container.Resolve<ISeed>();
            var load = Container.Resolve<ILoad>();
            var menuHelper = Container.Resolve<IMenuHelper>();
            MainMenu menuItem;

            do
            {
                menuItem = menuHelper.DoMainMenuSelection();

                switch (menuItem)
                {
                    case MainMenu.Add:
                        add.AddNewBook();
                        break;
                    case MainMenu.Search:
                        search.SingleBook();
                        break;
                    case MainMenu.Borrow:
                        borrow.PerformBorrowingProcess();
                        break;
                    case MainMenu.Return:
                        takeBack.ReturnBook();
                        break;
                    case MainMenu.List:
                        fetch.ListAllBooks();
                        break;
                    case MainMenu.Expiring:
                        fetch.ListExpiringBooks();
                        break;
                    case MainMenu.Seed:
                        seed.GenerateData(100, borrow);
                        break;
                    case MainMenu.LazyLoad:
                        load.LazyLoad();
                        break;
                    case MainMenu.EagerLoad:
                        load.EagerLoad();
                        break;
                }
            } while (menuItem != MainMenu.Exit);
        }
    }
}