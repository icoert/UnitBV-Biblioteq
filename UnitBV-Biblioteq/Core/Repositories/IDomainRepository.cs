// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-20-2020
//
// Last Modified By : silvi
// Last Modified On : 12-29-2020
// ***********************************************************************
// <copyright file="IDomainRepository.cs" company="Transilvanya University of Brasov">
//     Copyright © Silviu-Daniel Vijiala 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace UnitBV_Biblioteq.Core.Repositories
{
    using System.Collections.Generic;
    using UnitBV_Biblioteq.Core.DomainModel;

    /// <summary>
    /// Interface IDomainRepository
    /// Implements the <see cref="UnitBV_Biblioteq.Core.Repositories.IRepository{UnitBV_Biblioteq.Core.DomainModel.Domain}" />
    /// </summary>
    /// <seealso cref="UnitBV_Biblioteq.Core.Repositories.IRepository{UnitBV_Biblioteq.Core.DomainModel.Domain}" />
    public interface IDomainRepository : IRepository<Domain>
    {
        /// <summary>
        /// Gets the domains.
        /// </summary>
        /// <value>The domains.</value>
        IEnumerable<Domain> Domains { get; }

        /// <summary>
        /// Edits the domain.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool EditDomain(Domain domain);
    }
}
