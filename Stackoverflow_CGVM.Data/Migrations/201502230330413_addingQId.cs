namespace Stackoverflow_CGVM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingQId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "QuestionId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Answers", "QuestionId");
        }
    }
}
