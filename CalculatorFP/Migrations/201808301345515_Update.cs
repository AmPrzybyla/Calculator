namespace CalculatorFP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Votes", "Candidate_Id", "dbo.Candidates");
            DropIndex("dbo.Votes", new[] { "Candidate_Id" });
            RenameColumn(table: "dbo.Votes", name: "Candidate_Id", newName: "CandidateId");
            AlterColumn("dbo.Votes", "CandidateId", c => c.Int(nullable: false));
            CreateIndex("dbo.Votes", "CandidateId");
            AddForeignKey("dbo.Votes", "CandidateId", "dbo.Candidates", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "CandidateId", "dbo.Candidates");
            DropIndex("dbo.Votes", new[] { "CandidateId" });
            AlterColumn("dbo.Votes", "CandidateId", c => c.Int());
            RenameColumn(table: "dbo.Votes", name: "CandidateId", newName: "Candidate_Id");
            CreateIndex("dbo.Votes", "Candidate_Id");
            AddForeignKey("dbo.Votes", "Candidate_Id", "dbo.Candidates", "Id");
        }
    }
}
