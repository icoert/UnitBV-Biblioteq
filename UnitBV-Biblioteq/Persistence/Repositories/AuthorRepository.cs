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
        public AppDbContext AppDbContext => Context as AppDbContext;

        public IEnumerable<Author> Authors => Context.Set<Author>();

        public bool AddAuthor(Author author)
        {
            try
            {
                Context.Set<Author>().Add(author);
                Context.SaveChanges();
                Logger.Info($"New author was added(id={author.Id}).");
            }
            catch (Exception ex)
            {
                Logger.Info("Failed to add author.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }

        public bool DeleteAuthor(Author author)
        {
            try
            {
                Context.Set<Author>().Remove(author);
                Context.SaveChanges();
                Logger.Info($"Author with id={author.Id} was deleted.");
            }
            catch (Exception ex)
            {
                Logger.Info($"Failed to delete author with id={author.Id}.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }

        public bool EditAuthor(Author author)
        {
            try
            {
                var existing = Context.Set<Author>().FirstOrDefault(a => a.Id == author.Id); 
                if (existing != null)
                {
                    existing.Firstname = author.Firstname;
                    existing.Lastname = author.Lastname;
                    Context.SaveChanges();
                    Logger.Info($"Author with id={author.Id} was updated.");
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Info($"Failed to update author with id={author.Id}.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }
    }
}
