namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@" INSERT INTO[dbo].[AspNetUsers]
        ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName])
VALUES(N'229d073c-c591-4475-ba03-ae75419356ec', N'kavithadanny@gmail.com', 0, N'AHSENGyeYCN7DBiT3s6iZlph0kAcVAdnICUuXnyxv8fBkV3a7F0VBVKjOuKzKCV6Pg==', N'ef3b73d0-42fc-4e78-93c5-b31fca1c9353', NULL, 0, 0, NULL, 1, 0, N'kavithadanny@gmail.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'ad34fdb8-c229-4ac3-9476-32ac83438212', N'CanManageMovies')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'229d073c-c591-4475-ba03-ae75419356ec', N'ad34fdb8-c229-4ac3-9476-32ac83438212')
");

        }

    public override void Down()
        {
        }
    }
}
