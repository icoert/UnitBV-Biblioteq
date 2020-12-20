using System.Collections.Generic;
using UnitBV_Biblioteq.Core.DomainModel;

namespace UnitBV_Biblioteq.Core.Repositories
{
    public interface IDomainRepository : IRepository<Domain>
    {
        IEnumerable<Domain> Domains { get; }

        bool AddDomain(Domain domain);

        bool EditDomain(Domain domain);

        bool DeleteDomain(Domain domain);
    }
}
