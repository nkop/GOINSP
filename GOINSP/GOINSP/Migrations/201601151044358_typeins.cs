namespace GOINSP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class typeins : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InspectionTypes",
                c => new
                    {
                        id = c.Guid(nullable: false, identity: true),
                        type = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Inspection", "inspectiontype_id", c => c.Guid());
            CreateIndex("dbo.Inspection", "inspectiontype_id");
            AddForeignKey("dbo.Inspection", "inspectiontype_id", "dbo.InspectionTypes", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inspection", "inspectiontype_id", "dbo.InspectionTypes");
            DropIndex("dbo.Inspection", new[] { "inspectiontype_id" });
            DropColumn("dbo.Inspection", "inspectiontype_id");
            DropTable("dbo.InspectionTypes");
        }
    }
}
