namespace CalculatorFP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameToVoteTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Votes", "Name", c => c.String());
            AddColumn("dbo.Votes", "Surname", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Votes", "Surname");
            DropColumn("dbo.Votes", "Name");
        }
    }
}
