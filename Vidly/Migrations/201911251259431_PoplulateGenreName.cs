namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PoplulateGenreName : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Genres(Name)values('Comedy')");

            Sql("Insert into  Genres(Name)values('Science Fiction')");

            Sql("Insert into Genres(Name)values('Thriller')");

            Sql("Insert into Genres(Name)values('Horror')");

        }

        public override void Down()
        {
        }
    }
}
