// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-20-2020
//
// Last Modified By : silvi
// Last Modified On : 12-20-2020
// ***********************************************************************
// <copyright file="202012200041280_UpdateTables.cs" company="Transilvanya University of Brasov">
//     Copyright © Silviu-Daniel Vijiala 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace UnitBV_Biblioteq.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Class UpdateTables. This class cannot be inherited.
    /// Implements the <see cref="System.Data.Entity.Migrations.DbMigration" />
    /// Implements the <see cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class UpdateTables : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Address = c.String(),
                        UserType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.BookBorrows", "LastReBorrowDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.BookBorrows", "ReBorrows", c => c.Int(nullable: false));
            AddColumn("dbo.BookBorrows", "Employee_Id", c => c.Int());
            AddColumn("dbo.BookBorrows", "Reader_Id", c => c.Int());
            CreateIndex("dbo.BookBorrows", "Employee_Id");
            CreateIndex("dbo.BookBorrows", "Reader_Id");
            AddForeignKey("dbo.BookBorrows", "Employee_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.BookBorrows", "Reader_Id", "dbo.Users", "Id");
            DropColumn("dbo.BookBorrows", "Reader_Firstname");
            DropColumn("dbo.BookBorrows", "Reader_Lastname");
            DropColumn("dbo.BookBorrows", "Reader_Address");
            DropColumn("dbo.BookBorrows", "Reader_UserType");
            DropColumn("dbo.BookBorrows", "Employee_Firstname");
            DropColumn("dbo.BookBorrows", "Employee_Lastname");
            DropColumn("dbo.BookBorrows", "Employee_Address");
            DropColumn("dbo.BookBorrows", "Employee_UserType");
            DropColumn("dbo.BookBorrows", "LastBorrowDate");
            DropColumn("dbo.BookBorrows", "NrOfBorrows");
        }

        /// <summary>
        /// Downs this instance.
        /// </summary>
        public override void Down()
        {
            AddColumn("dbo.BookBorrows", "NrOfBorrows", c => c.Int(nullable: false));
            AddColumn("dbo.BookBorrows", "LastBorrowDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.BookBorrows", "Employee_UserType", c => c.Int(nullable: false));
            AddColumn("dbo.BookBorrows", "Employee_Address", c => c.String());
            AddColumn("dbo.BookBorrows", "Employee_Lastname", c => c.String());
            AddColumn("dbo.BookBorrows", "Employee_Firstname", c => c.String());
            AddColumn("dbo.BookBorrows", "Reader_UserType", c => c.Int(nullable: false));
            AddColumn("dbo.BookBorrows", "Reader_Address", c => c.String());
            AddColumn("dbo.BookBorrows", "Reader_Lastname", c => c.String());
            AddColumn("dbo.BookBorrows", "Reader_Firstname", c => c.String());
            DropForeignKey("dbo.BookBorrows", "Reader_Id", "dbo.Users");
            DropForeignKey("dbo.BookBorrows", "Employee_Id", "dbo.Users");
            DropIndex("dbo.BookBorrows", new[] { "Reader_Id" });
            DropIndex("dbo.BookBorrows", new[] { "Employee_Id" });
            DropColumn("dbo.BookBorrows", "Reader_Id");
            DropColumn("dbo.BookBorrows", "Employee_Id");
            DropColumn("dbo.BookBorrows", "ReBorrows");
            DropColumn("dbo.BookBorrows", "LastReBorrowDate");
            DropTable("dbo.Users");
        }
    }
}
