using System.Collections.Generic;
using UnitBV_Biblioteq.Core.DomainModel;

namespace UnitBV_Biblioteq.Core.Repositories
{
    public interface IAuthorsRepository : IRepository<Author>
    {
        IEnumerable<Author> Authors { get; }
        bool AddAuthor(Author author);
        bool EditAuthor(Author author);
        bool DeleteAuthor(Author author);
    }
}
