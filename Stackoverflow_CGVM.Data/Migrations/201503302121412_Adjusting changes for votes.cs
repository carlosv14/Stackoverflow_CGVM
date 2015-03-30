namespace Stackoverflow_CGVM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adjustingchangesforvotes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OwnerID = c.Guid(nullable: false),
                        QorA_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Votes");
        }
    }
}
