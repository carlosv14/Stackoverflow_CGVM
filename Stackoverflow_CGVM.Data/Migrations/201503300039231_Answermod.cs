namespace Stackoverflow_CGVM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Answermod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "AnswerDescription", c => c.String());
            DropColumn("dbo.Answers", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Answers", "Description", c => c.String());
            DropColumn("dbo.Answers", "AnswerDescription");
        }
    }
}
