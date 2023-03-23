namespace TicketsUNO27.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createtablehistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Histories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HistoryName = c.String(),
                        HistoryDescription = c.String(),
                        ProjectsId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.Guid(),
                        UpdatedBy = c.Guid(),
                        CreatedByName = c.String(),
                        UpdatedByName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectsId, cascadeDelete: true)
                .Index(t => t.ProjectsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Histories", "ProjectsId", "dbo.Projects");
            DropIndex("dbo.Histories", new[] { "ProjectsId" });
            DropTable("dbo.Histories");
        }
    }
}
