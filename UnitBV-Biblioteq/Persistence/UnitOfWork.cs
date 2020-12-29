// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-19-2020
//
// Last Modified By : silvi
// Last Modified On : 12-29-2020
// ***********************************************************************
// <copyright file="UnitOfWork.cs" company="Transilvanya University of Brasov">
//     Copyright © Silviu-Daniel Vijiala 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace UnitBV_Biblioteq.Persistence
{
    using System;
    using UnitBV_Biblioteq.Core;
    using UnitBV_Biblioteq.Core.Repositories;
    using UnitBV_Biblioteq.Persistence.Repositories;
    /// <summary>
    /// Class UnitOfWork.
    /// Implements the <see cref="UnitBV_Biblioteq.Core.IUnitOfWork" />
    /// </summary>
    /// <seealso cref="UnitBV_Biblioteq.Core.IUnitOfWork" />
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Authors = new AuthorRepository(_context);
            BookBorrows = new BookBorrowRepository(_context);
            BookEditions = new BookEditionRepository(_context);
            Books = new BookRepository(_context);
            Domains = new DomainRepository(_context);
            Publishers = new PublisherRepository(_context);
            Users = new UserRepository(_context);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }

        /// <summary>
        /// Gets the authors.
        /// </summary>
        /// <value>The authors.</value>
        public IAuthorsRepository Authors { get; private set; }

        /// <summary>
        /// Gets the book borrows.
        /// </summary>
        /// <value>The book borrows.</value>
        public IBookBorrowRepository BookBorrows { get; set; }
        /// <summary>
        /// Gets the book editions.
        /// </summary>
        /// <value>The book editions.</value>
        public IBookEditionRepository BookEditions { get; set; }

        /// <summary>
        /// Gets the books.
        /// </summary>
        /// <value>The books.</value>
        public IBookRepository Books { get; set; }

        /// <summary>
        /// Gets the domains.
        /// </summary>
        /// <value>The domains.</value>
        public IDomainRepository Domains { get; set; }

        /// <summary>
        /// Gets the publishers.
        /// </summary>
        /// <value>The publishers.</value>
        public IPublisherRepository Publishers { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>The users.</value>
        public IUserRepository Users { get; set; }

        /// <summary>
        /// Completes this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Complete()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
