namespace Spending.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredUserId : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers]
           ([Id]
           ,[Email]
           ,[EmailConfirmed]
           ,[PasswordHash]
           ,[SecurityStamp]
           ,[PhoneNumber]
           ,[PhoneNumberConfirmed]
           ,[TwoFactorEnabled]
           ,[LockoutEndDateUtc]
           ,[LockoutEnabled]
           ,[AccessFailedCount]
           ,[UserName])
            VALUES
           ('9f8968a2-1fd0-4691-80b1-3fbc29f1b340'
           , 'r.odises@gmail.com'
           , 0
           , 'AFIMlnwl25D/MpQUlxZb8TfDc5rZ3Q9rg9fkXA1BUpuw2iX/IqPtZlLa6xtD6/IQQQ=='
           , 'd670d69f-24c7-40ca-8154-4f750e9655ee'
           , NULL
           , 0
           , 0
           , NULL
           , 1
           , 0
           , 'r.odises@gmail.com')");


            Sql(@"UPDATE[dbo].[Transactions]
                  SET[UserId] = '9f8968a2-1fd0-4691-80b1-3fbc29f1b340'");

            DropForeignKey("dbo.Transactions", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Transactions", new[] { "UserId" });
            AlterColumn("dbo.Transactions", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Transactions", "UserId");
            AddForeignKey("dbo.Transactions", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Transactions", new[] { "UserId" });
            AlterColumn("dbo.Transactions", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Transactions", "UserId");
            AddForeignKey("dbo.Transactions", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
