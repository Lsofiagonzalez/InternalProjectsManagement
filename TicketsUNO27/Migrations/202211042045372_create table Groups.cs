namespace TicketsUNO27.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createtableGroups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GroupName = c.String(),
                        ProjectId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.Guid(),
                        UpdatedBy = c.Guid(),
                        CreatedByName = c.String(),
                        UpdatedByName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            AddColumn("dbo.Rols", "ParentId", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Groups", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Groups", new[] { "ProjectId" });
            DropColumn("dbo.Rols", "ParentId");
            DropTable("dbo.Groups");
        }
    }
}
