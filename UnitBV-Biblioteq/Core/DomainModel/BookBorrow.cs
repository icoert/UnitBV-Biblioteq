// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-19-2020
//
// Last Modified By : silvi
// Last Modified On : 12-29-2020
// ***********************************************************************
// <copyright file="BookBorrow.cs" company="Transilvanya University of Brasov">
//     Copyright © Silviu-Daniel Vijiala 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace UnitBV_Biblioteq.Core.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class BookBorrow.
    /// </summary>
    public class BookBorrow
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the reader.
        /// </summary>
        /// <value>The reader.</value>
        public virtual User Reader { get; set; }
        /// <summary>
        /// Gets or sets the employee.
        /// </summary>
        /// <value>The employee.</value>
        public virtual User Employee { get; set; }
        /// <summary>
        /// Gets or sets the books.
        /// </summary>
        /// <value>The books.</value>
        public virtual List<BookEdition> Books{ get; set; }
        /// <summary>
        /// Gets or sets the borrow date.
        /// </summary>
        /// <value>The borrow date.</value>
        public DateTime BorrowDate { get; set; }
        /// <summary>
        /// Gets or sets the last re borrow date.
        /// </summary>
        /// <value>The last re borrow date.</value>
        public DateTime LastReBorrowDate { get; set; }
        /// <summary>
        /// Gets or sets the return date.
        /// </summary>
        /// <value>The return date.</value>
        public DateTime? ReturnDate { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is returned.
        /// </summary>
        /// <value><c>true</c> if this instance is returned; otherwise, <c>false</c>.</value>
        public bool IsReturned { get; set; }
        /// <summary>
        /// Gets or sets the re borrows.
        /// </summary>
        /// <value>The re borrows.</value>
        public int ReBorrows { get; set; }

        /// <summary>
        /// Bookses the in domain.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns>System.Int32.</returns>
        public int BooksInDomain(DomainModel.Domain domain)
        {
            return this.Books.Count(book => book.Book.IsInDomain(domain));
        }
    }
}
