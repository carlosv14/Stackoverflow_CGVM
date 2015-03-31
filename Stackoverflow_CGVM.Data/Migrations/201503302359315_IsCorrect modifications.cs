namespace Stackoverflow_CGVM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsCorrectmodifications : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "hasCorrectAnswer", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "hasCorrectAnswer");
        }
    }
}
