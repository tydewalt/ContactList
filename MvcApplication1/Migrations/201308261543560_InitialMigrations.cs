namespace MvcApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigrations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Location", c => c.String(nullable: false, maxLength: 3));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Location", c => c.String(maxLength: 3));
        }
    }
}
