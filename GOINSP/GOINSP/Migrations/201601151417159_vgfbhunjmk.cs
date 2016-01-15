namespace GOINSP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vgfbhunjmk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inspection", "directory", c => c.Guid(nullable: false));
            DropColumn("dbo.Inspection", "image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Inspection", "image", c => c.Binary());
            DropColumn("dbo.Inspection", "directory");
        }
    }
}
