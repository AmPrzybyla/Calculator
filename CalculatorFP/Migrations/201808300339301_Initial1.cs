namespace CalculatorFP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Candidates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PartyTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PartyTypes", t => t.PartyTypeId, cascadeDelete: true)
                .Index(t => t.PartyTypeId);
            
            CreateTable(
                "dbo.PartyTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Disalloweds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pesel = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Candidates", "PartyTypeId", "dbo.PartyTypes");
            DropIndex("dbo.Candidates", new[] { "PartyTypeId" });
            DropTable("dbo.Disalloweds");
            DropTable("dbo.PartyTypes");
            DropTable("dbo.Candidates");
        }
    }
}
