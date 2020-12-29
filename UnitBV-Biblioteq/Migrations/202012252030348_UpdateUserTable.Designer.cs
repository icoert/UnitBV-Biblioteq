// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-25-2020
//
// Last Modified By : silvi
// Last Modified On : 12-25-2020
// ***********************************************************************
// <copyright file="202012252030348_UpdateUserTable.designer.cs" company="Transilvanya University of Brasov">
//     Copyright © Silviu-Daniel Vijiala 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace UnitBV_Biblioteq.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;

    /// <summary>
    /// Class UpdateUserTable. This class cannot be inherited.
    /// Implements the <see cref="System.Data.Entity.Migrations.DbMigration" />
    /// Implements the <see cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    [GeneratedCode("EntityFramework.Migrations", "6.4.4")]
    public sealed partial class UpdateUserTable : IMigrationMetadata
    {
        /// <summary>
        /// The resources
        /// </summary>
        private readonly ResourceManager Resources = new ResourceManager(typeof(UpdateUserTable));

        /// <summary>
        /// Gets the unique identifier for the migration.
        /// </summary>
        /// <value>The identifier.</value>
        string IMigrationMetadata.Id
        {
            get { return "202012252030348_UpdateUserTable"; }
        }

        /// <summary>
        /// Gets the state of the model before this migration is run.
        /// </summary>
        /// <value>The source.</value>
        string IMigrationMetadata.Source
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the target.
        /// </summary>
        /// <value>The target.</value>
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
