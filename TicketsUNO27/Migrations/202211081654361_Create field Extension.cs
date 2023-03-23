namespace TicketsUNO27.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatefieldExtension : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "Extension", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "Extension");
        }
    }
}
