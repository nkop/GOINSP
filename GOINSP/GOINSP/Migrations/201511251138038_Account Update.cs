namespace GOINSP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "AccountRights", c => c.Int(nullable: false));
            AddColumn("dbo.Accounts", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "Email");
            DropColumn("dbo.Accounts", "AccountRights");
        }
    }
}
