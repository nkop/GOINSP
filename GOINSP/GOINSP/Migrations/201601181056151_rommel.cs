namespace GOINSP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rommel : DbMigration
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
                        BedrijfsPortalKey = c.String(),
                        BedrijfsEmail = c.String(),
                        BedrijfsAdres = c.String(),
                        BedrijfsPostcode = c.String(),
                        BedrijfsNummer = c.String(),
                        BedrijfsGemeente = c.String(),
                        BedrijfsWijk = c.String(),
                        BedrijfsGemeenteCode = c.String(),
                        BedrijfsLat = c.Decimal(nullable: false, precision: 15, scale: 13),
                        BedrijfsLon = c.Decimal(nullable: false, precision: 15, scale: 13),
                    })
                .PrimaryKey(t => t.companyid);
            
            CreateTable(
                "dbo.QuestionBases",
                c => new
                    {
                        QuestionID = c.Guid(nullable: false, identity: true),
                        ListNumber = c.Int(nullable: false),
                        Visible = c.Boolean(nullable: false),
                        VisibleConditions = c.String(),
                        Question = c.String(),
                        DropDownQuestionID = c.Guid(identity: true),
                        Answers = c.String(),
                        SelectedAnswer = c.String(),
                        InspectorDropDownQuestionID = c.Guid(identity: true),
                        Answer = c.String(),
                        RadioAnswerID = c.Guid(identity: true),
                        Text = c.String(),
                        Checked = c.Boolean(),
                        GroupName = c.String(),
                        RadioQuestionID = c.Guid(identity: true),
                        SelectedAnswer1 = c.String(),
                        AlternativeAnswer = c.String(),
                        AlternativeAnswerVisibility = c.Boolean(),
                        SimpleDateQuestionID = c.Guid(identity: true),
                        Answer1 = c.DateTime(),
                        SimpleIntegerQuestionID = c.Guid(identity: true),
                        Answer2 = c.Int(),
                        SimpleTextBlockQuestionID = c.Guid(identity: true),
                        Answer3 = c.String(),
                        SimpleTextQuestionID = c.Guid(identity: true),
                        Answer4 = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        RadioQuestion_QuestionID = c.Guid(),
                        DropDownQuestion_QuestionID = c.Guid(),
                        Questionnaire_QuestionnaireID = c.Guid(),
                    })
                .PrimaryKey(t => t.QuestionID)
                .ForeignKey("dbo.QuestionBases", t => t.RadioQuestion_QuestionID)
                .ForeignKey("dbo.QuestionBases", t => t.DropDownQuestion_QuestionID)
                .ForeignKey("dbo.Questionnaires", t => t.Questionnaire_QuestionnaireID)
                .Index(t => t.RadioQuestion_QuestionID)
                .Index(t => t.DropDownQuestion_QuestionID)
                .Index(t => t.Questionnaire_QuestionnaireID);
            
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
                        description = c.String(),
                        directory = c.Guid(nullable: false),
                        company_companyid = c.Guid(),
                        inspectiontype_id = c.Guid(),
                        inspector_id = c.Guid(),
                        questionnaire_QuestionnaireID = c.Guid(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Companies", t => t.company_companyid)
                .ForeignKey("dbo.InspectionTypes", t => t.inspectiontype_id)
                .ForeignKey("dbo.Accounts", t => t.inspector_id)
                .ForeignKey("dbo.Questionnaires", t => t.questionnaire_QuestionnaireID)
                .Index(t => t.company_companyid)
                .Index(t => t.inspectiontype_id)
                .Index(t => t.inspector_id)
                .Index(t => t.questionnaire_QuestionnaireID);
            
            CreateTable(
                "dbo.InspectionTypes",
                c => new
                    {
                        id = c.Guid(nullable: false, identity: true),
                        type = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Questionnaires",
                c => new
                    {
                        QuestionnaireID = c.Guid(nullable: false, identity: true),
                        IsTemplate = c.Boolean(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.QuestionnaireID);
            
            CreateTable(
                "dbo.PostCodeDatas",
                c => new
                    {
                        PostCodeID = c.Guid(nullable: false, identity: true),
                        postcode = c.String(),
                        postcode_id = c.Int(nullable: false),
                        pnum = c.Int(nullable: false),
                        pchar = c.String(),
                        minnumber = c.Int(nullable: false),
                        maxnumber = c.Int(nullable: false),
                        numbertype = c.String(),
                        street = c.String(),
                        city = c.String(),
                        city_id = c.Int(nullable: false),
                        municipality = c.String(),
                        municipality_id = c.String(),
                        province = c.String(),
                        province_code = c.String(),
                        lat = c.Decimal(nullable: false, precision: 15, scale: 13),
                        lon = c.Decimal(nullable: false, precision: 15, scale: 13),
                        rd_x = c.Decimal(nullable: false, precision: 31, scale: 20),
                        rd_y = c.Decimal(nullable: false, precision: 31, scale: 20),
                        street_number = c.Int(),
                        location_detail = c.String(),
                        changed_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PostCodeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inspection", "questionnaire_QuestionnaireID", "dbo.Questionnaires");
            DropForeignKey("dbo.QuestionBases", "Questionnaire_QuestionnaireID", "dbo.Questionnaires");
            DropForeignKey("dbo.Inspection", "inspector_id", "dbo.Accounts");
            DropForeignKey("dbo.Inspection", "inspectiontype_id", "dbo.InspectionTypes");
            DropForeignKey("dbo.Inspection", "company_companyid", "dbo.Companies");
            DropForeignKey("dbo.QuestionBases", "DropDownQuestion_QuestionID", "dbo.QuestionBases");
            DropForeignKey("dbo.QuestionBases", "RadioQuestion_QuestionID", "dbo.QuestionBases");
            DropIndex("dbo.Inspection", new[] { "questionnaire_QuestionnaireID" });
            DropIndex("dbo.Inspection", new[] { "inspector_id" });
            DropIndex("dbo.Inspection", new[] { "inspectiontype_id" });
            DropIndex("dbo.Inspection", new[] { "company_companyid" });
            DropIndex("dbo.QuestionBases", new[] { "Questionnaire_QuestionnaireID" });
            DropIndex("dbo.QuestionBases", new[] { "DropDownQuestion_QuestionID" });
            DropIndex("dbo.QuestionBases", new[] { "RadioQuestion_QuestionID" });
            DropTable("dbo.PostCodeDatas");
            DropTable("dbo.Questionnaires");
            DropTable("dbo.InspectionTypes");
            DropTable("dbo.Inspection");
            DropTable("dbo.TDatas");
            DropTable("dbo.RegioS");
            DropTable("dbo.QuestionBases");
            DropTable("dbo.Companies");
            DropTable("dbo.Accounts");
        }
    }
}
