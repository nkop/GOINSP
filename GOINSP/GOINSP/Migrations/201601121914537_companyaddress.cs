namespace GOINSP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class companyaddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "BedrijfsAdres", c => c.String());
            AddColumn("dbo.Companies", "BedrijfsPostcode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "BedrijfsPostcode");
            DropColumn("dbo.Companies", "BedrijfsAdres");
        }
    }
}
