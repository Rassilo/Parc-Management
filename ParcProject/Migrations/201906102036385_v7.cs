namespace ParcProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v7 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ArticleAttributes", "attrIsConditional");
            DropColumn("dbo.ArticleAttributes", "attrIsInterval");
            DropColumn("dbo.ArticleAttributes", "attrIsLinked");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ArticleAttributes", "attrIsLinked", c => c.Boolean());
            AddColumn("dbo.ArticleAttributes", "attrIsInterval", c => c.Boolean());
            AddColumn("dbo.ArticleAttributes", "attrIsConditional", c => c.Boolean());
        }
    }
}
