using System;
using UnitBV_Biblioteq.Core.Repositories;

namespace UnitBV_Biblioteq.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthorsRepository Authors { get; }
        IBookBorrowRepository BookBorrows { get; }
        IBookEditionRepository BookEditions { get; }
        IBookRepository Books { get; }
        IDomainRepository Domains { get; }
        IPublisherRepository Publishers { get; }
        int Complete();
    }
}
