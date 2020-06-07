namespace HockeyPlayerDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClubName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "ClubName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "ClubName");
        }
    }
}
