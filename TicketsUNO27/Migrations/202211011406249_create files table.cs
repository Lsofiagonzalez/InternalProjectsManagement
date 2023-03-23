namespace TicketsUNO27.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createtablefiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FileName = c.String(),
                        DisplayName = c.String(),
                        FileDescription = c.String(),
                        Url = c.String(),
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
            DropForeignKey("dbo.Files", "ProjectsId", "dbo.Projects");
            DropIndex("dbo.Files", new[] { "ProjectsId" });
            DropTable("dbo.Files");
        }
    }
}
