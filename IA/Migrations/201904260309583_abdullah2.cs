namespace IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abdullah2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.projects", "projects_Id", c => c.Int());
            AddColumn("dbo.Req_Team", "proId", c => c.Int());
            CreateIndex("dbo.projects", "projects_Id");
            CreateIndex("dbo.Req_Team", "proId");
            AddForeignKey("dbo.Req_Team", "proId", "dbo.projects", "Id");
            AddForeignKey("dbo.projects", "projects_Id", "dbo.projects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.projects", "projects_Id", "dbo.projects");
            DropForeignKey("dbo.Req_Team", "proId", "dbo.projects");
            DropIndex("dbo.Req_Team", new[] { "proId" });
            DropIndex("dbo.projects", new[] { "projects_Id" });
            DropColumn("dbo.Req_Team", "proId");
            DropColumn("dbo.projects", "projects_Id");
        }
    }
}
