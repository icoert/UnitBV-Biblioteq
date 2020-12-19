using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitBV_Biblioteq.Core.Domain
{
    public class BookBorrow
    {
        public int Id { get; set; }
        public virtual User Reader { get; set; }
        public virtual User Employee { get; set; }
        public virtual  List<BookEdition> Books{ get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime LastBorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
        public int NrOfBorrows { get; set; }

        public int BooksInDomain(Domain domain)
        {
            return this.Books.Count(book => book.Book.IsInDomain(domain));
        }
    }
}
