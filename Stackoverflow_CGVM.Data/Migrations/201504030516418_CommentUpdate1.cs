namespace Stackoverflow_CGVM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentUpdate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "CommentDescription", c => c.String());
            DropColumn("dbo.Comments", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Description", c => c.String());
            DropColumn("dbo.Comments", "CommentDescription");
        }
    }
}
