namespace CalculatorFP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPersonalNumberToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PersonalNumber", c => c.String(nullable: false, maxLength: 11));
            AddColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Surname", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Surname");
            DropColumn("dbo.AspNetUsers", "Name");
            DropColumn("dbo.AspNetUsers", "PersonalNumber");
        }
    }
}
