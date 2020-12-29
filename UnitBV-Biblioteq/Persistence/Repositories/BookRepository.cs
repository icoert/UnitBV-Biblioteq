// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-20-2020
//
// Last Modified By : silvi
// Last Modified On : 12-29-2020
// ***********************************************************************
// <copyright file="BookRepository.cs" company="Transilvanya University of Brasov">
//     Copyright © Silviu-Daniel Vijiala 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace UnitBV_Biblioteq.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using log4net;
    using UnitBV_Biblioteq.Core.DomainModel;
    using UnitBV_Biblioteq.Core.Repositories;
    /// <summary>
    /// Class BookRepository.
    /// Implements the <see cref="UnitBV_Biblioteq.Persistence.Repositories.Repository{UnitBV_Biblioteq.Core.DomainModel.Book}" />
    /// Implements the <see cref="UnitBV_Biblioteq.Core.Repositories.IBookRepository" />
    /// </summary>
    /// <seealso cref="UnitBV_Biblioteq.Persistence.Repositories.Repository{UnitBV_Biblioteq.Core.DomainModel.Book}" />
    /// <seealso cref="UnitBV_Biblioteq.Core.Repositories.IBookRepository" />
    public class BookRepository : Repository<Book>, IBookRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BookRepository(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(typeof(BookRepository));
        /// <summary>
        /// Gets the application database context.
        /// </summary>
        /// <value>The application database context.</value>
        private AppDbContext AppDbContext => Context as AppDbContext;

        /// <summary>
        /// Gets the books.
        /// </summary>
        /// <value>The books.</value>
        public IEnumerable<Book> Books => AppDbContext.Set<Book>();

        /// <summary>
        /// Adds the specified book.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public new bool Add(Book book)
        {
            try
            {
                if (book == null)
                {
                    Logger.Info("Failed to add null book.");
                    return false;
                }
                if (!this.IsValid(book))
                {
                    Logger.Info("Failed to add book.");
                    return false;
                }

                AppDbContext.Books.Add(book);
                AppDbContext.SaveChanges();
                Logger.Info($"New book was added(id={book.Id}).");
            }
            catch (Exception ex)
            {
                Logger.Info("Failed to add book.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Edits the book.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool EditBook(Book book)
        {
            try
            {
                if (book == null)
                {
                        Logger.Info($"Failed to edit null book.");
                        return false;
                }

                var existing = AppDbContext.Books.FirstOrDefault(a => a.Id == book.Id);
                if (existing != null)
                {
                    if (string.IsNullOrEmpty(book.Title) || string.IsNullOrWhiteSpace(book.Title))
                    {
                        Logger.Info($"Failed to edit book.");
                        return false;
                    }

                    if (book.Authors == null || book.Authors.Count == 0 || book.Domains == null || book.Domains.Count == 0)
                    {
                        Logger.Info($"Failed to edit book.");
                        return false;
                    }

                    if (!book.DomainStructure())
                    {
                        Logger.Info($"Failed to edit book.");
                        return false;
                    }

                    existing.Title = book.Title;
                    existing.Domains = book.Domains;
                    existing.Authors = book.Authors;

                    AppDbContext.SaveChanges();

                    Logger.Info($"Book with id={book.Id} was updated.");
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Info($"Failed to update book with id={book.Id}.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns><c>true</c> if the specified book is valid; otherwise, <c>false</c>.</returns>
        public bool IsValid(Book book)
        {
            if (string.IsNullOrEmpty(book.Title) || string.IsNullOrWhiteSpace(book.Title))
            {
                return false;
            }

            if (book.Authors == null || book.Authors.Count == 0 || book.Domains == null || book.Domains.Count == 0)
            {
                return false;
            }

            if (!book.DomainStructure())
            {
                return false;
            }

            return true;
        }
    }
}
