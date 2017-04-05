using Library.BookOperations;
using Library.Contracts;
using Library.Helpers;
using Library.Sql;
using Autofac;
using Library.Contracts.PublicAPI.BookOperations;
using Library.Contracts.PublicAPI.Helpers;
using Library.File;

namespace Library.IOC
{
    public class Resolver
    {
        public ContainerBuilder BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ScreenHelper>().As<IScreenHelper>();
            builder.RegisterType<MenuHelper>().As<IMenuHelper>();
            builder.RegisterType<SqlBookRepostiroy>().As<IBookRepository>();
            //builder.RegisterType<FileBookRepository>().As<IBookRepository>();
            //builder.RegisterGeneric(typeof(NumberInputReader)).As(typeof(IInputReader<int>));
            //builder.RegisterGeneric(typeof(StringInputReader)).As(typeof(IInputReader<string>));
            //builder.RegisterGeneric(typeof(BookDataInputReader)).As(typeof(IInputReader<string>));
            builder.RegisterType<SimpleStringInputReader>().As<ISimpeStringReader>();
            builder.RegisterType<SimpleIntInputReader>().As<ISimpleIntInputReader>();
            builder.RegisterType<Add>().As<IAdd>();
            builder.RegisterType<Borrow>().As<IBorrow>();
            builder.RegisterType<Fetch>().As<IFetch>();
            builder.RegisterType<TakeBack>().As<ITakeBack>();
            builder.RegisterType<Search>().As<ISearch>();
            builder.RegisterType<Seed>().As<ISeed>();
            builder.RegisterType<Load>().As<ILoad>();
            return builder;
        }
    }
}
