using System.Data.Entity;
using UnitBV_Biblioteq.Core.DomainModel;

namespace UnitBV_Biblioteq.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
            :base("DefaultConnection")
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Domain> Domains { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<BookEdition> BookEditions { get; set; }
        public virtual DbSet<BookBorrow> BookBorrows { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}
