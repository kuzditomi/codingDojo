namespace Library.Sql
{
    class ModelConverter
    {
        public Models.Book ConverToSqlBook(Contracts.Models.Book book)
        {
            Models.Reader sqlReader = new Models.Reader();
            if (book?.Reader != null)
            {
                sqlReader = ConverToSqlReader(book.Reader);
            }

            var sqlBook = new Models.Book
            {
                Title = book.Title,
                Author = book.Author,
                Year = book.Year,
                Available = book.Available,
                BookId = book.Id,
                DueDate = book.DueDate,
                Reader = sqlReader,
                ReaderId = sqlReader.ReaderId
            };
            return sqlBook;
        }

        public Contracts.Models.Book ConverToContractBook(Models.Book book)
        {
            Contracts.Models.Reader contractReader = new Contracts.Models.Reader();
            if (book?.Reader != null)
            {
                contractReader = ConverToContractReader(book.Reader);
            }

            var contractBook = new Contracts.Models.Book
            {
                Title = book.Title,
                Author = book.Author,
                Year = book.Year,
                Available = book.Available,
                Id = book.BookId,
                DueDate = book.DueDate,
                Reader = contractReader
            };
            return contractBook;
        }

        public Models.Reader ConverToSqlReader(Contracts.Models.Reader reader)
        {
            return new Models.Reader { ReaderId = reader.Id, Name = reader.Name };
        }

        public Contracts.Models.Reader ConverToContractReader(Models.Reader reader)
        {
            return new Contracts.Models.Reader { Id = reader.ReaderId, Name = reader.Name };
        }
    }
}
