// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-26-2020
//
// Last Modified By : silvi
// Last Modified On : 12-26-2020
// ***********************************************************************
// <copyright file="202012261552319_UpdateUserTableRequiredProps.cs" company="Transilvanya University of Brasov">
//     Copyright © Silviu-Daniel Vijiala 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace UnitBV_Biblioteq.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Class UpdateUserTableRequiredProps. This class cannot be inherited.
    /// Implements the <see cref="System.Data.Entity.Migrations.DbMigration" />
    /// Implements the <see cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class UpdateUserTableRequiredProps : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            AlterColumn("dbo.Users", "Firstname", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Lastname", c => c.String(nullable: false));
        }

        /// <summary>
        /// Downs this instance.
        /// </summary>
        public override void Down()
        {
            AlterColumn("dbo.Users", "Lastname", c => c.String());
            AlterColumn("dbo.Users", "Firstname", c => c.String());
        }
    }
}
