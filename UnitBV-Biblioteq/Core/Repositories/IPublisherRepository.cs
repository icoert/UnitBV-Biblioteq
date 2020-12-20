using System.Collections.Generic;
using UnitBV_Biblioteq.Core.DomainModel;

namespace UnitBV_Biblioteq.Core.Repositories
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        IEnumerable<Publisher> Publishers { get; }

        bool AddPublisher(Publisher publisher);

        bool EditPublisher(Publisher publisher);

        bool DeletePublisher(Publisher publisher);
    }
}
