namespace CalculatorFP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePeerdonalToPersonal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Votes", "PersonalNumber", c => c.String());
            DropColumn("dbo.Votes", "PerdonalNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Votes", "PerdonalNumber", c => c.String());
            DropColumn("dbo.Votes", "PersonalNumber");
        }
    }
}
