namespace Stackoverflow_CGVM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Profile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "RegistrationDate", c => c.String());
            AddColumn("dbo.Accounts", "LastSeen", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "LastSeen");
            DropColumn("dbo.Accounts", "RegistrationDate");
        }
    }
}
