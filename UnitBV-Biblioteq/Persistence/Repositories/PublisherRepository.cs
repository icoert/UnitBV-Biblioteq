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
        private AppDbContext AppDbContext => Context as AppDbContext;

        public IEnumerable<Publisher> Publishers => AppDbContext.Set<Publisher>();

        public bool EditPublisher(Publisher publisher)
        {
            try
            {
                if (publisher == null)
                {
                    return false;
                }
                var existing = AppDbContext.Publishers.FirstOrDefault(a => a.Id == publisher.Id);
                if (existing != null)
                {
                    existing.Name = publisher.Name;
                    
                    AppDbContext.SaveChanges();

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
