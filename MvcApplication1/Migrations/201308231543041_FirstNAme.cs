namespace MvcApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstNAme : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "FirstName", c => c.String(nullable: false));
            AddColumn("dbo.Employees", "LastName", c => c.String(nullable: false));
            AddColumn("dbo.Employees", "Extension", c => c.String());
            DropColumn("dbo.Employees", "Name");
            DropColumn("dbo.Employees", "Extention");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Extention", c => c.String());
            AddColumn("dbo.Employees", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Employees", "Extension");
            DropColumn("dbo.Employees", "LastName");
            DropColumn("dbo.Employees", "FirstName");
        }
    }
}
