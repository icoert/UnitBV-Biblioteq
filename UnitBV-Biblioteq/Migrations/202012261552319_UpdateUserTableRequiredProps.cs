namespace UnitBV_Biblioteq.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserTableRequiredProps : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Firstname", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Lastname", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Lastname", c => c.String());
            AlterColumn("dbo.Users", "Firstname", c => c.String());
        }
    }
}
