namespace Sword.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Unused : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "UserName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "UserName");
        }
    }
}
