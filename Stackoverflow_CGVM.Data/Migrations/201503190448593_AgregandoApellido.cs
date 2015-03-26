namespace Stackoverflow_CGVM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregandoApellido : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "LastName");
        }
    }
}
