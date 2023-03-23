namespace TicketsUNO27.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatetableUsers_Groups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users_Groups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        GroupId = c.Guid(nullable: false),
                        RolId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        CreatedBy = c.Guid(),
                        UpdatedBy = c.Guid(),
                        CreatedByName = c.String(),
                        UpdatedByName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: false)
                .ForeignKey("dbo.Rols", t => t.RolId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.GroupId)
                .Index(t => t.RolId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users_Groups", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users_Groups", "RolId", "dbo.Rols");
            DropForeignKey("dbo.Users_Groups", "GroupId", "dbo.Groups");
            DropIndex("dbo.Users_Groups", new[] { "RolId" });
            DropIndex("dbo.Users_Groups", new[] { "GroupId" });
            DropIndex("dbo.Users_Groups", new[] { "UserId" });
            DropTable("dbo.Users_Groups");
        }
    }
}
