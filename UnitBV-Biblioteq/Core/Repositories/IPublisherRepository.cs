// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-20-2020
//
// Last Modified By : silvi
// Last Modified On : 12-29-2020
// ***********************************************************************
// <copyright file="IPublisherRepository.cs" company="Transilvanya University of Brasov">
//     Copyright © Silviu-Daniel Vijiala 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace UnitBV_Biblioteq.Core.Repositories
{
    using System.Collections.Generic;
    using UnitBV_Biblioteq.Core.DomainModel;

    /// <summary>
    /// Interface IPublisherRepository
    /// Implements the <see cref="UnitBV_Biblioteq.Core.Repositories.IRepository{UnitBV_Biblioteq.Core.DomainModel.Publisher}" />
    /// </summary>
    /// <seealso cref="UnitBV_Biblioteq.Core.Repositories.IRepository{UnitBV_Biblioteq.Core.DomainModel.Publisher}" />
    public interface IPublisherRepository : IRepository<Publisher>
    {
        /// <summary>
        /// Gets the publishers.
        /// </summary>
        /// <value>The publishers.</value>
        IEnumerable<Publisher> Publishers { get; }

        /// <summary>
        /// Edits the publisher.
        /// </summary>
        /// <param name="publisher">The publisher.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool EditPublisher(Publisher publisher);
    }
}
