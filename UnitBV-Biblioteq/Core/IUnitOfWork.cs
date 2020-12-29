// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-19-2020
//
// Last Modified By : silvi
// Last Modified On : 12-29-2020
// ***********************************************************************
// <copyright file="IUnitOfWork.cs" company="Transilvanya University of Brasov">
//     Copyright © Silviu-Daniel Vijiala 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace UnitBV_Biblioteq.Core
{
    using System;
    using UnitBV_Biblioteq.Core.Repositories;

    /// <summary>
    /// Interface IUnitOfWork
    /// Implements the <see cref="System.IDisposable" />
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the authors.
        /// </summary>
        /// <value>The authors.</value>
        IAuthorsRepository Authors { get; }

        /// <summary>
        /// Gets the book borrows.
        /// </summary>
        /// <value>The book borrows.</value>
        IBookBorrowRepository BookBorrows { get; }

        /// <summary>
        /// Gets the book editions.
        /// </summary>
        /// <value>The book editions.</value>
        IBookEditionRepository BookEditions { get; }

        /// <summary>
        /// Gets the books.
        /// </summary>
        /// <value>The books.</value>
        IBookRepository Books { get; }

        /// <summary>
        /// Gets the domains.
        /// </summary>
        /// <value>The domains.</value>
        IDomainRepository Domains { get; }

        /// <summary>
        /// Gets the publishers.
        /// </summary>
        /// <value>The publishers.</value>
        IPublisherRepository Publishers { get; }

        /// <summary>
        /// Completes this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool Complete();
    }
}
