// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-20-2020
//
// Last Modified By : silvi
// Last Modified On : 12-29-2020
// ***********************************************************************
// <copyright file="IBookBorrowRepository.cs" company="Transilvanya University of Brasov">
//     Copyright © Silviu-Daniel Vijiala 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace UnitBV_Biblioteq.Core.Repositories
{
    using System.Collections.Generic;
    using UnitBV_Biblioteq.Core.DomainModel;

    /// <summary>
    /// Interface IBookBorrowRepository
    /// Implements the <see cref="UnitBV_Biblioteq.Core.Repositories.IRepository{UnitBV_Biblioteq.Core.DomainModel.BookBorrow}" />
    /// </summary>
    /// <seealso cref="UnitBV_Biblioteq.Core.Repositories.IRepository{UnitBV_Biblioteq.Core.DomainModel.BookBorrow}" />
    public interface IBookBorrowRepository : IRepository<BookBorrow>
    {
        /// <summary>
        /// Gets the book borrows.
        /// </summary>
        /// <value>The book borrows.</value>
        IEnumerable<BookBorrow> BookBorrows { get; }

        /// <summary>
        /// Edits the book borrow.
        /// </summary>
        /// <param name="borrow">The borrow.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool EditBookBorrow(BookBorrow borrow);

        /// <summary>
        /// Res the borrow book.
        /// </summary>
        /// <param name="borrow">The borrow.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool ReBorrowBook(BookBorrow borrow);

        /// <summary>
        /// Returns the books.
        /// </summary>
        /// <param name="borrow">The borrow.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool ReturnBooks(BookBorrow borrow);
    }
}
