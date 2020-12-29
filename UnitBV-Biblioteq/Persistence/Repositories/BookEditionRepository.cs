// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-20-2020
//
// Last Modified By : silvi
// Last Modified On : 12-29-2020
// ***********************************************************************
// <copyright file="BookEditionRepository.cs" company="Transilvanya University of Brasov">
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
    /// Class BookEditionRepository.
    /// Implements the <see cref="UnitBV_Biblioteq.Persistence.Repositories.Repository{UnitBV_Biblioteq.Core.DomainModel.BookEdition}" />
    /// Implements the <see cref="UnitBV_Biblioteq.Core.Repositories.IBookEditionRepository" />
    /// </summary>
    public class BookEditionRepository : Repository<BookEdition>, IBookEditionRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookEditionRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BookEditionRepository(AppDbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(typeof(BookEditionRepository));
        /// <summary>
        /// Gets the application database context.
        /// </summary>
        /// <value>The application database context.</value>
        private AppDbContext AppDbContext => Context as AppDbContext;

        /// <summary>
        /// Gets the book editions.
        /// </summary>
        /// <value>The book editions.</value>
        public IEnumerable<BookEdition> BookEditions => AppDbContext.Set<BookEdition>();

        /// <summary>
        /// Adds the specified book edition.
        /// </summary>
        /// <param name="bookEdition">The book edition.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public new bool Add(BookEdition bookEdition)
        {
            try
            {
                if (bookEdition == null)
                {
                    Logger.Info("Failed to add null book edition.");
                    return false;
                }
                if (!bookEdition.IsValid())
                {
                    Logger.Info("Failed to add book edition.");
                    return false;
                }

                AppDbContext.BookEditions.Add(bookEdition);
                AppDbContext.SaveChanges();
                Logger.Info($"New book edition was added(id={bookEdition.Id}).");
            }
            catch (Exception ex)
            {
                Logger.Info("Failed to add book edition.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Edits the book edition.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool EditBookEdition(BookEdition edition)
        {
            try
            {
                if (edition == null)
                {
                    Logger.Info("Failed to edit null book edition.");
                    return false;
                }
                var existing = AppDbContext.Set<BookEdition>().FirstOrDefault(a => a.Id == edition.Id);
                if (existing != null)
                {
                    if (!edition.IsValid())
                    {
                        Logger.Info($"Failed to update book edition with id={edition.Id}.");
                        return false;
                    }

                    existing.Copies = edition.Copies;
                    existing.CopiesLibrary = edition.CopiesLibrary;
                    existing.Pages = edition.Pages;
                    existing.Type = edition.Type;
                    existing.Year = edition.Year;
                    existing.Publisher = edition.Publisher;
                    existing.Book = edition.Book;
                    
                    AppDbContext.SaveChanges();

                    Logger.Info($"Book edition with id={edition.Id} was updated.");
                }
                else
                {
                    Logger.Info($"Failed to update book edition with id={edition.Id}.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Info($"Failed to update book edition with id={edition.Id}.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }
    }
}
