// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-20-2020
//
// Last Modified By : silvi
// Last Modified On : 12-29-2020
// ***********************************************************************
// <copyright file="IBookRepository.cs" company="Transilvanya University of Brasov">
//     Copyright © Silviu-Daniel Vijiala 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace UnitBV_Biblioteq.Core.Repositories
{
    using System.Collections.Generic;
    using UnitBV_Biblioteq.Core.DomainModel;

    /// <summary>
    /// Interface IBookRepository
    /// Implements the <see cref="UnitBV_Biblioteq.Core.Repositories.IRepository{UnitBV_Biblioteq.Core.DomainModel.Book}" />
    /// </summary>
    /// <seealso cref="UnitBV_Biblioteq.Core.Repositories.IRepository{UnitBV_Biblioteq.Core.DomainModel.Book}" />
    public interface IBookRepository : IRepository<Book>
    {
        /// <summary>
        /// Gets the books.
        /// </summary>
        /// <value>The books.</value>
        IEnumerable<Book> Books { get; }

        /// <summary>
        /// Edits the book.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool EditBook(Book book);

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns><c>true</c> if the specified book is valid; otherwise, <c>false</c>.</returns>
        bool IsValid(Book book);
    }
}
