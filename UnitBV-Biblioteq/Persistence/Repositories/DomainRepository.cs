using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using UnitBV_Biblioteq.Core.DomainModel;
using UnitBV_Biblioteq.Core.Repositories;

namespace UnitBV_Biblioteq.Persistence.Repositories
{
    public class DomainRepository : Repository<Domain>, IDomainRepository
    {
        public DomainRepository(AppDbContext context) : base(context)
        {
            
        }

        private static readonly ILog Logger = LogManager.GetLogger(typeof(DomainRepository));
        private AppDbContext AppDbContext => Context as AppDbContext;


        public IEnumerable<Domain> Domains => AppDbContext.Set<Domain>();

        public bool EditDomain(Domain domain)
        {
            try
            {
                if (domain == null)
                {
                    return false;
                }
                var existing = AppDbContext.Domains.FirstOrDefault(a => a.Id == domain.Id);
                if (existing != null)
                {
                    existing.Name = domain.Name;
                    existing.Parent = domain.Parent;
                    Logger.Info($"Domain with id={domain.Id} was updated.");
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Info($"Failed to update domain with id={domain.Id}.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }
    }
}
