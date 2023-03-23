namespace TicketsUNO27.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateusers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Password");
        }
    }
}
