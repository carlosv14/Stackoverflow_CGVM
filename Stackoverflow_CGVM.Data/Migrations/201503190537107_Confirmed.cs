namespace Stackoverflow_CGVM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Confirmed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "confirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "confirmed");
        }
    }
}
