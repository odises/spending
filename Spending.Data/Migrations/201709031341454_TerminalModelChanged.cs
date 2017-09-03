namespace Spending.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TerminalModelChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Terminals", "SerialNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Terminals", "SerialNumber", c => c.Int(nullable: false));
        }
    }
}
