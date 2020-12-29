// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-20-2020
//
// Last Modified By : silvi
// Last Modified On : 12-29-2020
// ***********************************************************************
// <copyright file="DomainRepository.cs" company="Transilvanya University of Brasov">
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
    /// Class DomainRepository.
    /// Implements the <see cref="UnitBV_Biblioteq.Persistence.Repositories.Repository{UnitBV_Biblioteq.Core.DomainModel.Domain}" />
    /// Implements the <see cref="UnitBV_Biblioteq.Core.Repositories.IDomainRepository" />
    /// </summary>
    /// <seealso cref="UnitBV_Biblioteq.Persistence.Repositories.Repository{UnitBV_Biblioteq.Core.DomainModel.Domain}" />
    /// <seealso cref="UnitBV_Biblioteq.Core.Repositories.IDomainRepository" />
    public class DomainRepository : Repository<Domain>, IDomainRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DomainRepository(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(typeof(DomainRepository));

        /// <summary>
        /// Gets the application database context.
        /// </summary>
        /// <value>The application database context.</value>
        private AppDbContext AppDbContext => Context as AppDbContext;
        /// <summary>
        /// Gets the domains.
        /// </summary>
        /// <value>The domains.</value>
        public IEnumerable<Domain> Domains => AppDbContext.Set<Domain>();

        /// <summary>
        /// Edits the domain.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool EditDomain(Domain domain)
        {
            try
            {
                if (domain == null)
                {
                    return false;
                }
                var existing = AppDbContext.Domains.FirstOrDefault(a => a.Id == domain.Id);
                if (existing != null)
                {
                    existing.Name = domain.Name;
                    existing.Parent = domain.Parent;
                    
                    AppDbContext.SaveChanges();

                    Logger.Info($"Domain with id={domain.Id} was updated.");
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Info($"Failed to update domain with id={domain.Id}.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }
    }
}
