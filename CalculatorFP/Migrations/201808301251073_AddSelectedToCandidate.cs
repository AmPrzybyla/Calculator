namespace CalculatorFP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSelectedToCandidate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidates", "IsSelected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Candidates", "IsSelected");
        }
    }
}
