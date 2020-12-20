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
        public AppDbContext AppDbContext => Context as AppDbContext;


        public IEnumerable<Domain> Domains => Context.Set<Domain>();

        public bool AddDomain(Domain domain)
        {
            try
            {
                Context.Set<Domain>().Add(domain);
                Context.SaveChanges();
                Logger.Info($"New domain was added(id={domain.Id}).");
            }
            catch (Exception ex)
            {
                Logger.Info("Failed to add domain.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }

        public bool DeleteDomain(Domain domain)
        {
            try
            {
                Context.Set<Domain>().Remove(domain);
                Context.SaveChanges();
                Logger.Info($"Domain was deleted (id={domain.Id}).");
            }
            catch (Exception ex)
            {
                Logger.Info("Failed to delete domain.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }

        public bool EditDomain(Domain domain)
        {
            try
            {
                var existing = Context.Set<Domain>().FirstOrDefault(a => a.Id == domain.Id);
                if (existing != null)
                {
                    existing.Name = domain.Name;
                    existing.Parent = domain.Parent;
                    Context.SaveChanges();
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
