using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using UnitBV_Biblioteq.Core.DomainModel;
using UnitBV_Biblioteq.Core.Repositories;

namespace UnitBV_Biblioteq.Persistence.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context)
        {
            
        }

        private static readonly ILog Logger = LogManager.GetLogger(typeof(BookRepository));
        private AppDbContext AppDbContext => Context as AppDbContext;

        public IEnumerable<Book> Books => AppDbContext.Set<Book>();

        public bool EditBook(Book book)
        {
            try
            {
                if (book == null)
                {
                    return false;
                }
                var existing = AppDbContext.Books.FirstOrDefault(a => a.Id == book.Id);
                if (existing != null)
                {
                    if (string.IsNullOrEmpty(book.Title) || string.IsNullOrWhiteSpace(book.Title))
                    {
                        Logger.Info($"Failed to edit book.");
                        return false;
                    }

                    if (book.Authors == null || book.Authors.Count == 0 || book.Domains == null || book.Domains.Count == 0)
                    {
                        Logger.Info($"Failed to edit book.");
                        return false;
                    }

                    if (!book.DomainStructure())
                    {
                        Logger.Info($"Failed to edit book.");
                        return false;
                    }

                    existing.Title = book.Title;
                    existing.Domains = book.Domains;
                    existing.Authors = book.Authors;
                    
                    AppDbContext.SaveChanges();

                    Logger.Info($"Book with id={book.Id} was updated.");
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Info($"Failed to update book with id={book.Id}.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }

        public bool IsValid(Book book)
        {
            if (string.IsNullOrEmpty(book.Title) || string.IsNullOrWhiteSpace(book.Title))
            {
                Logger.Info($"Failed to add book.");
                return false;
            }

            if (book.Authors == null || book.Authors.Count == 0 || book.Domains == null || book.Domains.Count == 0)
            {
                Logger.Info($"Failed to add book.");
                return false;
            }

            if (!book.DomainStructure())
            {
                Logger.Info($"Failed to add book.");
                return false;
            }

            return true;
        }
    }
}
