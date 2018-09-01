namespace CalculatorFP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulatePartyTypes : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT PartyTypes ON");
            Sql("INSERT INTO PartyTypes (Id, Name) Values(1, 'Piastowie')");
            Sql("INSERT INTO PartyTypes (Id, Name) Values(2, 'Dynastia Jagiellonów')");
            Sql("INSERT INTO PartyTypes (Id, Name) Values(3, 'Elekcyjni Dla Polski')");
            Sql("INSERT INTO PartyTypes (Id, Name) Values(4, 'Wazowie')");
            Sql("SET IDENTITY_INSERT PartyTypes OFF");
        }
        
        public override void Down()
        {
        }
    }
}
