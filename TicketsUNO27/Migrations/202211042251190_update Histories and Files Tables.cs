namespace TicketsUNO27.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateHistoriesandFilesTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "UserId", "dbo.Users");
            DropForeignKey("dbo.Files", "ProjectsId", "dbo.Projects");
            DropIndex("dbo.Files", new[] { "ProjectsId" });
            DropIndex("dbo.Projects", new[] { "UserId" });
            AddColumn("dbo.Files", "HistoriesId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Files", "HistoriesId");
            AddForeignKey("dbo.Files", "HistoriesId", "dbo.Histories", "Id", cascadeDelete: true);
            DropColumn("dbo.Files", "ProjectsId");
            DropColumn("dbo.Projects", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "UserId", c => c.Guid(nullable: false));
            AddColumn("dbo.Files", "ProjectsId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.Files", "HistoriesId", "dbo.Histories");
            DropIndex("dbo.Files", new[] { "HistoriesId" });
            DropColumn("dbo.Files", "HistoriesId");
            CreateIndex("dbo.Projects", "UserId");
            CreateIndex("dbo.Files", "ProjectsId");
            AddForeignKey("dbo.Files", "ProjectsId", "dbo.Projects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Projects", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
