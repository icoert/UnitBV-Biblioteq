using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitBV_Biblioteq.Core.Domain;

namespace UnitBV_Biblioteq.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
            :base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookEdition> BookEditions { get; set; }
        public DbSet<BookBorrow> BookBorrows { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public static AppDbContext Create()
        {
            return new AppDbContext();;
        }
    }
}
