// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-20-2020
//
// Last Modified By : silvi
// Last Modified On : 12-29-2020
// ***********************************************************************
// <copyright file="PublisherRepository.cs" company="Transilvanya University of Brasov">
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
    /// Class PublisherRepository.
    /// Implements the <see cref="UnitBV_Biblioteq.Persistence.Repositories.Repository{UnitBV_Biblioteq.Core.DomainModel.Publisher}" />
    /// Implements the <see cref="UnitBV_Biblioteq.Core.Repositories.IPublisherRepository" />
    /// </summary>
    /// <seealso cref="UnitBV_Biblioteq.Persistence.Repositories.Repository{UnitBV_Biblioteq.Core.DomainModel.Publisher}" />
    /// <seealso cref="UnitBV_Biblioteq.Core.Repositories.IPublisherRepository" />
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublisherRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PublisherRepository(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(typeof(PublisherRepository));

        /// <summary>
        /// Gets the application database context.
        /// </summary>
        /// <value>The application database context.</value>
        private AppDbContext AppDbContext => Context as AppDbContext;

        /// <summary>
        /// Gets the publishers.
        /// </summary>
        /// <value>The publishers.</value>
        public IEnumerable<Publisher> Publishers => AppDbContext.Set<Publisher>();

        /// <summary>
        /// Edits the publisher.
        /// </summary>
        /// <param name="publisher">The publisher.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool EditPublisher(Publisher publisher)
        {
            try
            {
                if (publisher == null)
                {
                    Logger.Info($"Failed to edit null publisher.");
                    return false;
                }
                var existing = AppDbContext.Publishers.FirstOrDefault(a => a.Id == publisher.Id);
                if (existing != null)
                {
                    existing.Name = publisher.Name;
                    AppDbContext.SaveChanges();

                    Logger.Info($"Publisher with id={publisher.Id} was updated.");
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Info($"Failed to update publisher with id={publisher.Id}.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }
    }
}
