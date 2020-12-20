using System.Collections.Generic;
using UnitBV_Biblioteq.Core.DomainModel;

namespace UnitBV_Biblioteq.Core.Repositories
{
    public interface IBookEditionRepository : IRepository<BookEdition>
    {
        IEnumerable<BookEdition> BookEditions { get; }

        bool AddBookEdition(BookEdition edition);

        bool EditBookEdition(BookEdition edition);

        bool DeleteBookEdition(BookEdition edition);
    }
}
