using System.Collections.Generic;
using UnitBV_Biblioteq.Core.DomainModel;

namespace UnitBV_Biblioteq.Core.Repositories
{
    public interface IBookBorrowRepository : IRepository<BookBorrow>
    {
        IEnumerable<BookBorrow> BookBorrows { get; }
        bool AddBookBorrow(BookBorrow borrow);
        bool EditBookBorrow(BookBorrow borrow);
        bool DeleteBookBorrow(BookBorrow borrow);
        bool ReBorrowBook(BookBorrow borrow);
        bool ReturnBooks(BookBorrow borrow);
    }
}
