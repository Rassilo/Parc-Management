namespace ParcProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArticleOrigins", "SortNo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ArticleOrigins", "SortNo");
        }
    }
}
