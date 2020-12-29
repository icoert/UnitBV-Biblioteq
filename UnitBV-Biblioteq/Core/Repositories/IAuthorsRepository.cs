// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-20-2020
//
// Last Modified By : silvi
// Last Modified On : 12-29-2020
// ***********************************************************************
// <copyright file="IAuthorsRepository.cs" company="Transilvanya University of Brasov">
//     Copyright © Silviu-Daniel Vijiala 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using UnitBV_Biblioteq.Core.DomainModel;

namespace UnitBV_Biblioteq.Core.Repositories
{
    /// <summary>
    /// Interface IAuthorsRepository
    /// Implements the <see cref="UnitBV_Biblioteq.Core.Repositories.IRepository{UnitBV_Biblioteq.Core.DomainModel.Author}" />
    /// </summary>
    /// <seealso cref="UnitBV_Biblioteq.Core.Repositories.IRepository{UnitBV_Biblioteq.Core.DomainModel.Author}" />
    public interface IAuthorsRepository : IRepository<Author>
    {
        /// <summary>
        /// Gets the authors.
        /// </summary>
        /// <value>The authors.</value>
        IEnumerable<Author> Authors { get; }
        /// <summary>
        /// Edits the author.
        /// </summary>
        /// <param name="author">The author.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool EditAuthor(Author author);
    }
}
