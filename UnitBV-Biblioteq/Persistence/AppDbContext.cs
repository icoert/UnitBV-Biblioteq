// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-19-2020
//
// Last Modified By : silvi
// Last Modified On : 12-29-2020
// ***********************************************************************
// <copyright file="AppDbContext.cs" company="Transilvanya University of Brasov">
//     Copyright © Silviu-Daniel Vijiala 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace UnitBV_Biblioteq.Persistence
{
    using System.Data.Entity;
    using UnitBV_Biblioteq.Core.DomainModel;

    /// <summary>
    /// Class AppDbContext.
    /// Implements the <see cref="System.Data.Entity.DbContext" />
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext" /> class.
        /// </summary>
        public AppDbContext()
            : base("DefaultConnection")
        {
        }

        /// <summary>
        /// Gets or sets the books.
        /// </summary>
        /// <value>The books.</value>
        public virtual DbSet<Book> Books { get; set; }

        /// <summary>
        /// Gets or sets the domains.
        /// </summary>
        /// <value>The domains.</value>
        public virtual DbSet<Domain> Domains { get; set; }

        /// <summary>
        /// Gets or sets the authors.
        /// </summary>
        /// <value>The authors.</value>
        public virtual DbSet<Author> Authors { get; set; }

        /// <summary>
        /// Gets or sets the book editions.
        /// </summary>
        /// <value>The book editions.</value>
        public virtual DbSet<BookEdition> BookEditions { get; set; }

        /// <summary>
        /// Gets or sets the book borrows.
        /// </summary>
        /// <value>The book borrows.</value>
        public virtual DbSet<BookBorrow> BookBorrows { get; set; }

        /// <summary>
        /// Gets or sets the publishers.
        /// </summary>
        /// <value>The publishers.</value>
        public virtual DbSet<Publisher> Publishers { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>The users.</value>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>AppDbContext.</returns>
        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}
