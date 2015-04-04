namespace Stackoverflow_CGVM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "CantidadRespuestas", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "Vistas", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "Vistas");
            DropColumn("dbo.Questions", "CantidadRespuestas");
        }
    }
}
