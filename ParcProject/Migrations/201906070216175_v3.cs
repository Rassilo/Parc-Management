namespace ParcProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("Images");
            AlterColumn("Images", "docId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("Images", "docId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("Images");
            AlterColumn("Images", "docId", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
            AddPrimaryKey("Images", "docId");
        }
    }
}
