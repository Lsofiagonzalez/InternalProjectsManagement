namespace TicketsUNO27.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateProfilePhotoField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ProfilePhoto", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ProfilePhoto");
        }
    }
}
