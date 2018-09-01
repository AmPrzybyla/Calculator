namespace CalculatorFP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CandidateIdInVoteCanBeNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Votes", "CandidateId", "dbo.Candidates");
            DropIndex("dbo.Votes", new[] { "CandidateId" });
            AlterColumn("dbo.Votes", "CandidateId", c => c.Int());
            CreateIndex("dbo.Votes", "CandidateId");
            AddForeignKey("dbo.Votes", "CandidateId", "dbo.Candidates", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "CandidateId", "dbo.Candidates");
            DropIndex("dbo.Votes", new[] { "CandidateId" });
            AlterColumn("dbo.Votes", "CandidateId", c => c.Int(nullable: false));
            CreateIndex("dbo.Votes", "CandidateId");
            AddForeignKey("dbo.Votes", "CandidateId", "dbo.Candidates", "Id", cascadeDelete: true);
        }
    }
}
