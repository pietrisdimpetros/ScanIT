namespace ScanIT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserProperties : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        Country = c.String(nullable: false),
                        Region = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        StreetNumber = c.String(nullable: false),
                        ZipCode = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ( [Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], 
                                                  [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], 
                                                  [LastName], [DateOfBirth], [Country], [Region], [City], [Street], [StreetNumber], [ZipCode] ) 
                                                  VALUES (N'9e7525ce-9426-450f-83cb-f53cb49fabd4', N'UserNRoles@usernroles.com', 0, N'AEtjksZmeAhZ86pax6te7H3uXlWD6gCVfcOnaovpBySaNv/Nw9k+LCdMKDuiBlPLmA==',
                                                          N'72276c48-c692-48dc-8f4e-0b63d1cf8287', NULL, 0, 0, NULL, 1, 0, N'UserNRoles@usernroles.com','Admin','Admin','1900-01-01','Greece','Attiki','Athens',
                                                          'Panemistimiou','23A',55555)
                INSERT INTO [dbo].[AspNetUsers] ( [Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], 
                                                  [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], 
                                                  [LastName], [DateOfBirth], [Country], [Region], [City], [Street], [StreetNumber], [ZipCode] ) 
                                                  VALUES (N'd76c9d60-34f4-48c5-9c49-6559a4822346', N'ApplicationManager@usernroles.com', 0, N'AGvUhkYc9FjSQcCq6U31P3AsQMXUjdzylZeduASH5L6Fk1s4r46ISqWw+Va/O0yR/Q==', 
                                                          N'cc8c0820-0bf1-4b63-b551-408bda21b88c', NULL, 0, 0, NULL, 1, 0, N'ApplicationManager@usernroles.com','Manager','Manager','1900-01-01','Greece','Attiki','Athens',
                                                          'Panemistimiou','23A',55555)
                INSERT INTO [dbo].[AspNetUsers] ( [Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], 
                                                  [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], 
                                                  [LastName], [DateOfBirth], [Country], [Region], [City], [Street], [StreetNumber], [ZipCode] ) 
                                                  VALUES (N'0f429f0a-62af-4995-acb2-afe5d5b5f345', N'FirstValidatedUser@usernroles.com', 0, N'AI3JK0lRdQlBnN6cbZUtDgDktEaK6DI/Ck2hVGSD5rszgweduKNi2Mb/QVMrFzLAUQ==', 
                                                          N'dbf30e88-f17d-488b-8ca8-47aece4ffa03', NULL, 0, 0, NULL, 1, 0, N'FirstValidatedUser@usernroles.com','User','User','1900-01-01','Greece','Attiki','Athens',
                                                          'Panemistimiou','23A',55555)


                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'827a30bb-ae7d-4866-a26e-d9f03004dba2', N'Administrator')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'71efcf05-9d51-41e9-8b6a-00d90a90ad5d', N'Manager')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'610bf9e4-da1d-49a7-9cec-8522d4a3324c', N'Validated User')
                

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9e7525ce-9426-450f-83cb-f53cb49fabd4', N'827a30bb-ae7d-4866-a26e-d9f03004dba2')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd76c9d60-34f4-48c5-9c49-6559a4822346', N'71efcf05-9d51-41e9-8b6a-00d90a90ad5d')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0f429f0a-62af-4995-acb2-afe5d5b5f345', N'610bf9e4-da1d-49a7-9cec-8522d4a3324c')

            ");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
