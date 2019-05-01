namespace IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abdullah : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        project_Id = c.Int(nullable: false),
                        comment = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.projects", t => t.project_Id, cascadeDelete: true)
                .Index(t => t.project_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "project_Id", "dbo.projects");
            DropIndex("dbo.Comments", new[] { "project_Id" });
            DropTable("dbo.Comments");
        }
    }
}
