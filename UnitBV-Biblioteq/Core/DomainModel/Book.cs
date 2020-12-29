// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-19-2020
//
// Last Modified By : silvi
// Last Modified On : 12-29-2020
// ***********************************************************************
// <copyright file="Book.cs" company="Transilvanya University of Brasov">
//     Copyright © Silviu-Daniel Vijiala 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace UnitBV_Biblioteq.Core.DomainModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;

    /// <summary>
    /// Class Book.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the domains.
        /// </summary>
        /// <value>The domains.</value>
        public List<DomainModel.Domain> Domains { get; set; }

        /// <summary>
        /// Gets or sets the authors.
        /// </summary>
        /// <value>The authors.</value>
        public List<Author> Authors { get; set; }

        /// <summary>
        /// Domains the structure.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool DomainStructure()
        {
            var maxDomains = int.Parse(ConfigurationManager.AppSettings["MaxDomainsForBook"]);
            if (this.Domains.Count > maxDomains)
            {
                return false;
            }

            foreach (var domain in this.Domains)
            {
                var parent = domain.Parent;
                while (parent != null)
                {
                    // check parent domain in the list
                    if (this.Domains.Contains(parent))
                    {
                        return false;
                    }

                    parent = parent.Parent;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether [is in domain] [the specified domain].
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns><c>true</c> if [is in domain] [the specified domain]; otherwise, <c>false</c>.</returns>
        public bool IsInDomain(DomainModel.Domain domain)
        {
            foreach (var dom in this.Domains)
            {
                var parent = dom;
                while (parent != null)
                {
                    if (parent == domain)
                    {
                        return true;
                    }

                    parent = parent.Parent;
                }
            }

            return false;
        }
    }
}
