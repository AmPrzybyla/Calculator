namespace CalculatorFP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVotesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PerdonalNumber = c.String(),
                        SpoiledVote = c.Boolean(nullable: false),
                        IsVoted = c.Boolean(nullable: false),
                        Candidate_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Candidates", t => t.Candidate_Id)
                .Index(t => t.Candidate_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "Candidate_Id", "dbo.Candidates");
            DropIndex("dbo.Votes", new[] { "Candidate_Id" });
            DropTable("dbo.Votes");
        }
    }
}
