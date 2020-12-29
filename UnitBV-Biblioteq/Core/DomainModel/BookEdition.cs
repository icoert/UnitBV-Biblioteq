// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-19-2020
//
// Last Modified By : silvi
// Last Modified On : 12-29-2020
// ***********************************************************************
// <copyright file="BookEdition.cs" company="Transilvanya University of Brasov">
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
    /// Class BookEdition.
    /// </summary>
    public class BookEdition
        {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the publisher.
        /// </summary>
        /// <value>The publisher.</value>
        [Required]
        public virtual Publisher Publisher { get; set; }

        /// <summary>
        /// Gets or sets the book.
        /// </summary>
        /// <value>The book.</value>
        [Required]
        public virtual Book Book { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>The year.</value>
        [MaxLength(4)]
        [MinLength(4)]
        public string Year { get; set; }

        /// <summary>
        /// Gets or sets the pages.
        /// </summary>
        /// <value>The pages.</value>
        [IntegerValidator(MinValue = 1)]
        public int Pages { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public BookType Type { get; set; }

        /// <summary>
        /// Gets or sets the copies.
        /// </summary>
        /// <value>The copies.</value>
        [IntegerValidator(MinValue = 0)]
        public int Copies { get; set; }

        /// <summary>
        /// Gets or sets the copies library.
        /// </summary>
        /// <value>The copies library.</value>
        [IntegerValidator(MinValue = 0)]
        public int CopiesLibrary { get; set; }

        /// <summary>
        /// Gets or sets the book borrows.
        /// </summary>
        /// <value>The book borrows.</value>
        public virtual List<BookBorrow> BookBorrows { get; set; }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns><c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public bool IsValid()
            {
                if (!int.TryParse(this.Year, out _))
                {
                    return false;
                }

                if (this.Copies < 0 || this.CopiesLibrary <  0 || this.Pages < 1)
                {
                    return false;
                }

                return this.CopiesLibrary <= this.Copies;
            }

    }
}
