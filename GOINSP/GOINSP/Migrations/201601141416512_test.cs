namespace GOINSP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "BedrijfsNummer", c => c.String());
            AddColumn("dbo.Companies", "BedrijfsGemeente", c => c.String());
            AddColumn("dbo.Companies", "BedrijfsWijk", c => c.String());
            AddColumn("dbo.Companies", "BedrijfsGemeenteCode", c => c.String());
            AddColumn("dbo.Companies", "BedrijfsLat", c => c.Decimal(nullable: false, precision: 15, scale: 13));
            AddColumn("dbo.Companies", "BedrijfsLon", c => c.Decimal(nullable: false, precision: 15, scale: 13));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "BedrijfsLon");
            DropColumn("dbo.Companies", "BedrijfsLat");
            DropColumn("dbo.Companies", "BedrijfsGemeenteCode");
            DropColumn("dbo.Companies", "BedrijfsWijk");
            DropColumn("dbo.Companies", "BedrijfsGemeente");
            DropColumn("dbo.Companies", "BedrijfsNummer");
        }
    }
}
