namespace IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abdullah1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.projects", "users_Id", "dbo.users");
            DropForeignKey("dbo.projects", "users_Id1", "dbo.users");
            DropIndex("dbo.projects", new[] { "users_Id" });
            DropIndex("dbo.projects", new[] { "users_Id1" });
            DropColumn("dbo.projects", "users_Id");
            DropColumn("dbo.projects", "users_Id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.projects", "users_Id1", c => c.Int());
            AddColumn("dbo.projects", "users_Id", c => c.Int());
            CreateIndex("dbo.projects", "users_Id1");
            CreateIndex("dbo.projects", "users_Id");
            AddForeignKey("dbo.projects", "users_Id1", "dbo.users", "Id");
            AddForeignKey("dbo.projects", "users_Id", "dbo.users", "Id");
        }
    }
}
