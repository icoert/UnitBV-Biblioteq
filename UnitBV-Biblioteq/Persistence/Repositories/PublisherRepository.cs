using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using UnitBV_Biblioteq.Core.DomainModel;
using UnitBV_Biblioteq.Core.Repositories;

namespace UnitBV_Biblioteq.Persistence.Repositories
{
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(AppDbContext context) : base(context)
        {
            
        }

        private static readonly ILog Logger = LogManager.GetLogger(typeof(PublisherRepository));
        public AppDbContext AppDbContext => Context as AppDbContext;

        public IEnumerable<Publisher> Publishers => Context.Set<Publisher>();

        public bool AddPublisher(Publisher publisher)
        {
            try
            {
                Context.Set<Publisher>().Add(publisher);
                Context.SaveChanges();
                Logger.Info($"New publisher was added(id={publisher.Id}).");
            }
            catch (Exception ex)
            {
                Logger.Info("Failed to add publisher.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }

        public bool DeletePublisher(Publisher publisher)
        {
            try
            {
                Context.Set<Publisher>().Remove(publisher);
                Context.SaveChanges();
                Logger.Info($"Publisher was deleted (id={publisher.Id}).");
            }
            catch (Exception ex)
            {
                Logger.Info("Failed to delete publisher.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }

        public bool EditPublisher(Publisher publisher)
        {
            try
            {
                var existing = Context.Set<Publisher>().FirstOrDefault(a => a.Id == publisher.Id);
                if (existing != null)
                {
                    existing.Name = publisher.Name;
                    Context.SaveChanges();
                    Logger.Info($"Publisher with id={publisher.Id} was updated.");
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Info($"Failed to update publisher with id={publisher.Id}.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }
    }
}
