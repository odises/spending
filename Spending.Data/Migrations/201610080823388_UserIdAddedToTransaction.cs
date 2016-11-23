namespace Spending.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIdAddedToTransaction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Transactions", "UserId");
            AddForeignKey("dbo.Transactions", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Transactions", new[] { "UserId" });
            DropColumn("dbo.Transactions", "UserId");
        }
    }
}
