// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-19-2020
//
// Last Modified By : silvi
// Last Modified On : 12-19-2020
// ***********************************************************************
// <copyright file="202012192027064_InitialMigration.cs" company="Transilvanya University of Brasov">
//     Copyright © Silviu-Daniel Vijiala 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace UnitBV_Biblioteq.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Class InitialMigration. This class cannot be inherited.
    /// Implements the <see cref="System.Data.Entity.Migrations.DbMigration" />
    /// Implements the <see cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    /// <seealso cref="System.Data.Entity.Migrations.Infrastructure.IMigrationMetadata" />
    public partial class InitialMigration : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Domains",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Parent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Domains", t => t.Parent_Id)
                .Index(t => t.Parent_Id);
            
            CreateTable(
                "dbo.BookBorrows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Reader_Firstname = c.String(),
                        Reader_Lastname = c.String(),
                        Reader_Address = c.String(),
                        Reader_UserType = c.Int(nullable: false),
                        Employee_Firstname = c.String(),
                        Employee_Lastname = c.String(),
                        Employee_Address = c.String(),
                        Employee_UserType = c.Int(nullable: false),
                        BorrowDate = c.DateTime(nullable: false),
                        LastBorrowDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(),
                        IsReturned = c.Boolean(nullable: false),
                        NrOfBorrows = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookEditions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.String(maxLength: 4),
                        Pages = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Copies = c.Int(nullable: false),
                        CopiesLibrary = c.Int(nullable: false),
                        Book_Id = c.Int(nullable: false),
                        Publisher_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .ForeignKey("dbo.Publishers", t => t.Publisher_Id, cascadeDelete: true)
                .Index(t => t.Book_Id)
                .Index(t => t.Publisher_Id);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookAuthors",
                c => new
                    {
                        Book_Id = c.Int(nullable: false),
                        Author_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Book_Id, t.Author_Id })
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .ForeignKey("dbo.Authors", t => t.Author_Id, cascadeDelete: true)
                .Index(t => t.Book_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.DomainBooks",
                c => new
                    {
                        Domain_Id = c.Int(nullable: false),
                        Book_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Domain_Id, t.Book_Id })
                .ForeignKey("dbo.Domains", t => t.Domain_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .Index(t => t.Domain_Id)
                .Index(t => t.Book_Id);
            
            CreateTable(
                "dbo.BookEditionBookBorrows",
                c => new
                    {
                        BookEdition_Id = c.Int(nullable: false),
                        BookBorrow_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookEdition_Id, t.BookBorrow_Id })
                .ForeignKey("dbo.BookEditions", t => t.BookEdition_Id, cascadeDelete: true)
                .ForeignKey("dbo.BookBorrows", t => t.BookBorrow_Id, cascadeDelete: true)
                .Index(t => t.BookEdition_Id)
                .Index(t => t.BookBorrow_Id);
            
        }

        /// <summary>
        /// Downs this instance.
        /// </summary>
        public override void Down()
        {
            DropForeignKey("dbo.BookEditions", "Publisher_Id", "dbo.Publishers");
            DropForeignKey("dbo.BookEditionBookBorrows", "BookBorrow_Id", "dbo.BookBorrows");
            DropForeignKey("dbo.BookEditionBookBorrows", "BookEdition_Id", "dbo.BookEditions");
            DropForeignKey("dbo.BookEditions", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.Domains", "Parent_Id", "dbo.Domains");
            DropForeignKey("dbo.DomainBooks", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.DomainBooks", "Domain_Id", "dbo.Domains");
            DropForeignKey("dbo.BookAuthors", "Author_Id", "dbo.Authors");
            DropForeignKey("dbo.BookAuthors", "Book_Id", "dbo.Books");
            DropIndex("dbo.BookEditionBookBorrows", new[] { "BookBorrow_Id" });
            DropIndex("dbo.BookEditionBookBorrows", new[] { "BookEdition_Id" });
            DropIndex("dbo.DomainBooks", new[] { "Book_Id" });
            DropIndex("dbo.DomainBooks", new[] { "Domain_Id" });
            DropIndex("dbo.BookAuthors", new[] { "Author_Id" });
            DropIndex("dbo.BookAuthors", new[] { "Book_Id" });
            DropIndex("dbo.BookEditions", new[] { "Publisher_Id" });
            DropIndex("dbo.BookEditions", new[] { "Book_Id" });
            DropIndex("dbo.Domains", new[] { "Parent_Id" });
            DropTable("dbo.BookEditionBookBorrows");
            DropTable("dbo.DomainBooks");
            DropTable("dbo.BookAuthors");
            DropTable("dbo.Publishers");
            DropTable("dbo.BookEditions");
            DropTable("dbo.BookBorrows");
            DropTable("dbo.Domains");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
