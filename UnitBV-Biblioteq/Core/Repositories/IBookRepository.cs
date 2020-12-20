using System.Collections.Generic;
using UnitBV_Biblioteq.Core.DomainModel;

namespace UnitBV_Biblioteq.Core.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<Book> Books { get; }

        bool AddBook(Book book);

        bool EditBook(Book book);

        bool DeleteBook(Book book);
    }
}
