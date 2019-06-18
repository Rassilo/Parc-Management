namespace ParcProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OriginesOENs", "brandName");
            DropColumn("dbo.OriginesOENs", "sortNumber");
            DropColumn("dbo.OriginesOENs", "articleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OriginesOENs", "articleId", c => c.Int(nullable: false));
            AddColumn("dbo.OriginesOENs", "sortNumber", c => c.Int());
            AddColumn("dbo.OriginesOENs", "brandName", c => c.String(unicode: false));
        }
    }
}
