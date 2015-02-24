namespace Stackoverflow_CGVM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Marcar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "isCorrect", c => c.Boolean(nullable: false));
            AddColumn("dbo.Questions", "correctAnswer", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "correctAnswer");
            DropColumn("dbo.Answers", "isCorrect");
        }
    }
}
