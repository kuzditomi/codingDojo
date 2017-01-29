using System.Collections.Generic;
using Library.Contracts.Models;

namespace Library.Contracts
{
    public interface IBookSerializer
    {
        void Save(IEnumerable<Book> book);
        IEnumerable<Book> Load();
    }
}
