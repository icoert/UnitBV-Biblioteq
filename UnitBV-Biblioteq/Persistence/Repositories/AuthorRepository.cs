// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-20-2020
//
// Last Modified By : silvi
// Last Modified On : 12-29-2020
// ***********************************************************************
// <copyright file="AuthorRepository.cs" company="Transilvanya University of Brasov">
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
    /// Class AuthorRepository.
    /// Implements the <see cref="UnitBV_Biblioteq.Persistence.Repositories.Repository{UnitBV_Biblioteq.Core.DomainModel.Author}" />
    /// Implements the <see cref="UnitBV_Biblioteq.Core.Repositories.IAuthorsRepository" />
    /// </summary>
    /// <seealso cref="UnitBV_Biblioteq.Persistence.Repositories.Repository{UnitBV_Biblioteq.Core.DomainModel.Author}" />
    /// <seealso cref="UnitBV_Biblioteq.Core.Repositories.IAuthorsRepository" />
    public class AuthorRepository : Repository<Author>, IAuthorsRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public AuthorRepository(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(typeof(AuthorRepository));
        /// <summary>
        /// Gets the application database context.
        /// </summary>
        /// <value>The application database context.</value>
        private AppDbContext AppDbContext => Context as AppDbContext;

        /// <summary>
        /// Gets the authors.
        /// </summary>
        /// <value>The authors.</value>
        public IEnumerable<Author> Authors => AppDbContext.Set<Author>();

        /// <summary>
        /// Edits the author.
        /// </summary>
        /// <param name="author">The author.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool EditAuthor(Author author)
        {
            try
            {
                if (author == null)
                {
                    Logger.Info("Failed to edit null author.");
                    return false;
                }

                var existing = AppDbContext.Authors.FirstOrDefault(a => a.Id == author.Id);
                if (existing != null)
                {
                    existing.Firstname = author.Firstname;
                    existing.Lastname = author.Lastname;

                    AppDbContext.SaveChanges();

                    Logger.Info($"Author with id={author.Id} was updated.");
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Info(author != null
                    ? $"Failed to update author with id {author.Id}."
                    : $"Failed to update author.");
                Logger.Error(ex.Message, ex);

                return false;
            }

            return true;
        }
    }
}
