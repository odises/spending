namespace Spending.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TerminalsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Terminals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerialNumber = c.Int(nullable: false),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Terminals");
        }
    }
}
