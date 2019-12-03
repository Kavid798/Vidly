namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PoplulateGenreNames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "AvailableStocks", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "AvailableStocks");
        }
    }
}
