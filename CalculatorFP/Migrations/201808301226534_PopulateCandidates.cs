namespace CalculatorFP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCandidates : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Candidates ON");

            Sql("INSERT INTO Candidates (Id, Name, PartyTypeId) Values(1, 'Mieszko I', 1)");
            Sql("INSERT INTO Candidates (Id, Name, PartyTypeId) Values(2, 'Boles�aw Chrobry', 1)");
            Sql("INSERT INTO Candidates (Id, Name, PartyTypeId) Values(3, 'W�adys�aw �okietek', 1)");
            Sql("INSERT INTO Candidates (Id, Name, PartyTypeId) Values(4, 'Kazimierz Wielki', 1)");

            Sql("INSERT INTO Candidates (Id, Name, PartyTypeId) Values(5, 'W�adys�aw Jagie��o', 2)");
            Sql("INSERT INTO Candidates (Id, Name, PartyTypeId) Values(6, 'W�adys�aw Warne�czyk', 2)");
            Sql("INSERT INTO Candidates (Id, Name, PartyTypeId) Values(7, 'Zygmunt Stary', 2)");

            Sql("INSERT INTO Candidates (Id, Name, PartyTypeId) Values(8, 'Henryk Walezy', 3)");
            Sql("INSERT INTO Candidates (Id, Name, PartyTypeId) Values(9, 'Anna Jagiellonka', 3)");
            Sql("INSERT INTO Candidates (Id, Name, PartyTypeId) Values(10, 'Stefan Batory', 3)");

            Sql("INSERT INTO Candidates (Id, Name, PartyTypeId) Values(11, 'Zygmunt Waza', 4)");
            Sql("INSERT INTO Candidates (Id, Name, PartyTypeId) Values(12, 'W�adys�aw Waza', 4)");
            Sql("INSERT INTO Candidates (Id, Name, PartyTypeId) Values(13, 'Jan Kazimierz', 4)");

            Sql("SET IDENTITY_INSERT Candidates OFF");

        }

        public override void Down()
        {
        }
    }
}
