namespace GOINSP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        id = c.Guid(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        AccountRights = c.Int(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        companyid = c.Guid(nullable: false, identity: true),
                        BedrijfsNaam = c.String(),
                        BedrijfsEmail = c.String(),
                    })
                .PrimaryKey(t => t.companyid);
            
            CreateTable(
                "dbo.RegioS",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Key);
            
            CreateTable(
                "dbo.TDatas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RegioS = c.String(),
                        Perioden = c.String(),
                        TotaalHuishoudelijkAfval_1 = c.Int(),
                        HuishoudelijkRestafval_2 = c.Int(),
                        GrofHuishoudelijkRestafval_3 = c.Int(),
                        Verbouwingsrestafval_4 = c.Int(),
                        GFTAfval_5 = c.Int(),
                        OudPapierEnKarton_6 = c.Int(),
                        Verpakkingsglas_7 = c.Int(),
                        Textiel_8 = c.Int(),
                        KleinChemischAfvalKCA_9 = c.Int(),
                        MetalenVerpakkingenBlik_10 = c.Int(),
                        Drankenkartons_11 = c.Int(),
                        KunststofVerpakkingen_12 = c.Int(),
                        OverigeKunststoffen_13 = c.Int(),
                        Luiers_14 = c.Int(),
                        WitEnBruingoed_15 = c.Int(),
                        GrofTuinafval_16 = c.Int(),
                        BruikbaarHuisraad_17 = c.Int(),
                        Vloerbedekking_18 = c.Int(),
                        Vlakglas_19 = c.Int(),
                        Metalen_20 = c.Int(),
                        HoutafvalAEnBHout_21 = c.Int(),
                        HoutafvalCHout_22 = c.Int(),
                        SchoonPuin_23 = c.Int(),
                        BitumenhoudendeDakbedekking_24 = c.Int(),
                        AsbesthoudendAfval_25 = c.Int(),
                        Autobanden_26 = c.Int(),
                        SchoneGrond_27 = c.Int(),
                        OverigeAfvalstoffen_28 = c.Int(),
                        Gemeentecode_29 = c.String(),
                        Provincie_30 = c.String(),
                        Stedelijkheid_31 = c.String(),
                        InwonersPer1Januari_32 = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Inspection",
                c => new
                    {
                        id = c.Guid(nullable: false, identity: true),
                        name = c.String(),
                        date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        longtitude = c.Double(nullable: false),
                        latitude = c.Double(nullable: false),
                        address = c.String(),
                        zipcode = c.String(),
                        inspectorid = c.Guid(nullable: false),
                        companyid = c.Guid(nullable: false),
                        description = c.String(),
                        image = c.Binary(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Companies", t => t.companyid, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.inspectorid, cascadeDelete: true)
                .Index(t => t.inspectorid)
                .Index(t => t.companyid);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationID = c.Guid(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.LocationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inspection", "inspectorid", "dbo.Accounts");
            DropForeignKey("dbo.Inspection", "companyid", "dbo.Companies");
            DropIndex("dbo.Inspection", new[] { "companyid" });
            DropIndex("dbo.Inspection", new[] { "inspectorid" });
            DropTable("dbo.Locations");
            DropTable("dbo.Inspection");
            DropTable("dbo.TDatas");
            DropTable("dbo.RegioS");
            DropTable("dbo.Companies");
            DropTable("dbo.Accounts");
        }
    }
}
