// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-20-2020
//
// Last Modified By : silvi
// Last Modified On : 12-29-2020
// ***********************************************************************
// <copyright file="IBookEditionRepository.cs" company="Transilvanya University of Brasov">
//     Copyright © Silviu-Daniel Vijiala 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace UnitBV_Biblioteq.Core.Repositories
{
    using System.Collections.Generic;
    using UnitBV_Biblioteq.Core.DomainModel;

    /// <summary>
    /// Interface IBookEditionRepository
    /// Implements the <see cref="UnitBV_Biblioteq.Core.Repositories.IRepository{UnitBV_Biblioteq.Core.DomainModel.BookEdition}" />
    /// </summary>
    /// <seealso cref="UnitBV_Biblioteq.Core.Repositories.IRepository{UnitBV_Biblioteq.Core.DomainModel.BookEdition}" />
    public interface IBookEditionRepository : IRepository<BookEdition>
    {
        /// <summary>
        /// Gets the book editions.
        /// </summary>
        /// <value>The book editions.</value>
        IEnumerable<BookEdition> BookEditions { get; }

        /// <summary>
        /// Edits the book edition.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool EditBookEdition(BookEdition edition);

    }
}
