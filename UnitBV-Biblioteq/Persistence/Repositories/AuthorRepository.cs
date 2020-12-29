using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using UnitBV_Biblioteq.Core.DomainModel;
using UnitBV_Biblioteq.Core.Repositories;

namespace UnitBV_Biblioteq.Persistence.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorsRepository
    {
        public AuthorRepository(AppDbContext context) : base(context)
        {
            
        }

        private static readonly ILog Logger = LogManager.GetLogger(typeof(AuthorRepository));
        private AppDbContext AppDbContext => Context as AppDbContext;

        public IEnumerable<Author> Authors => AppDbContext.Set<Author>();

        public bool EditAuthor(Author author)
        {
            try
            {
                if (author == null)
                {
                    Logger.Info("Failed to edit null author.");
                    return false;
                }
                var existing = AppDbContext.Authors.FirstOrDefault(a => a.Id == author.Id); 
                if (existing != null)
                {
                    existing.Firstname = author.Firstname;
                    existing.Lastname = author.Lastname;

                    AppDbContext.SaveChanges();

                    Logger.Info($"Author with id={author.Id} was updated.");
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Info(author != null
                    ? $"Failed to update author with id {author.Id}."
                    : $"Failed to update author.");
                Logger.Error(ex.Message, ex);

                return false;
            }

            return true;
        }
    }
}
