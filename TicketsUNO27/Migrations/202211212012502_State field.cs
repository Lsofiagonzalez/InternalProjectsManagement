namespace TicketsUNO27.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Statefield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "State", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "State", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "State");
            DropColumn("dbo.Projects", "State");
        }
    }
}
