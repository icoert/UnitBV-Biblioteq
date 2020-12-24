using System.Collections.Generic;
using UnitBV_Biblioteq.Core.DomainModel;

namespace UnitBV_Biblioteq.Core.Repositories
{
    public interface IDomainRepository : IRepository<Domain>
    {
        IEnumerable<Domain> Domains { get; }

        bool EditDomain(Domain domain);
    }
}
