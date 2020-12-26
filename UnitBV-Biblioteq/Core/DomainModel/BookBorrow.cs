using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitBV_Biblioteq.Core.DomainModel
{
    public class BookBorrow
    {
        public int Id { get; set; }
        public virtual User Reader { get; set; }
        public virtual User Employee { get; set; }
        public virtual List<BookEdition> Books{ get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime LastReBorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
        public int ReBorrows { get; set; }

        public int BooksInDomain(DomainModel.Domain domain)
        {
            return this.Books.Count(book => book.Book.IsInDomain(domain));
        }
    }
}
