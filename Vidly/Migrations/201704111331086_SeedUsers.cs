namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9f1dda59-b8b5-46d9-8d09-e28ceb04960e', N'admin@vidly.com', 0, N'AAUE5BO755pM4eSmOc3Ufr4f1XQCw9F8oV3WqImgY8U1mEyfx1MKfhfqgqew3h/vMQ==', N'b9d19b86-de73-4b76-ab36-f6d60722e53b', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'e3573d78-1885-4a33-8d68-f207340d3a75', N'guest@vidly.com', 0, N'ANxFhMx4D9yBWeYavqsAux+6ALNVeR3FPzxe05X4jwOhKPhf2+H/fxN0p6FFKdRPIg==', N'0effcf90-6056-4199-a6be-11feccc8d3b8', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'929827b2-a410-454e-bab1-cc44f578dac8', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9f1dda59-b8b5-46d9-8d09-e28ceb04960e', N'929827b2-a410-454e-bab1-cc44f578dac8')
");
        }
        
        public override void Down()
        {
        }
    }
}
