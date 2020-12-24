using System;
using UnitBV_Biblioteq.Core;
using UnitBV_Biblioteq.Core.Repositories;
using UnitBV_Biblioteq.Persistence.Repositories;

namespace UnitBV_Biblioteq.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Authors = new AuthorRepository(_context);
            BookBorrows = new BookBorrowRepository(_context);
            BookEditions = new BookEditionRepository(_context);
            Books = new BookRepository(_context);
            Domains = new DomainRepository(_context);
            Publishers = new PublisherRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IAuthorsRepository Authors { get; private set; }
        public IBookBorrowRepository BookBorrows { get; set; }
        public IBookEditionRepository BookEditions { get; set; }
        public IBookRepository Books { get; set; }
        public IDomainRepository Domains { get; set; }
        public IPublisherRepository Publishers { get; set; }

        public bool Complete()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
