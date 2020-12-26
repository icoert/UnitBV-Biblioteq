using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using UnitBV_Biblioteq.Core.DomainModel;
using UnitBV_Biblioteq.Core.Repositories;

namespace UnitBV_Biblioteq.Persistence.Repositories
{
    public class BookEditionRepository : Repository<BookEdition>, IBookEditionRepository
    {
        public BookEditionRepository(AppDbContext context) : base(context)
        {
            
        }

        private static readonly ILog Logger = LogManager.GetLogger(typeof(BookEditionRepository));
        private AppDbContext AppDbContext => Context as AppDbContext;

        public IEnumerable<BookEdition> BookEditions => AppDbContext.Set<BookEdition>();

        public bool EditBookEdition(BookEdition edition)
        {
            try
            {
                if (edition == null)
                {
                    return false;
                }
                var existing = AppDbContext.Set<BookEdition>().FirstOrDefault(a => a.Id == edition.Id);
                if (existing != null)
                {
                    if (!edition.IsValid())
                    {
                        Logger.Info($"Failed to update book edition with id={edition.Id}.");
                        return false;
                    }

                    existing.Copies = edition.Copies;
                    existing.CopiesLibrary = edition.CopiesLibrary;
                    existing.Pages = edition.Pages;
                    existing.Type = edition.Type;
                    existing.Year = edition.Year;
                    existing.Publisher = edition.Publisher;
                    existing.Book = edition.Book;
                    
                    AppDbContext.SaveChanges();

                    Logger.Info($"Book edition with id={edition.Id} was updated.");
                }
                else
                {
                    Logger.Info($"Failed to update book edition with id={edition.Id}.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Info($"Failed to update book edition with id={edition.Id}.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }
    }
}
