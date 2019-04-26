namespace IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abdullah2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Req_Team", "users_Id", "dbo.users");
            DropForeignKey("dbo.Req_Team", "users_Id1", "dbo.users");
            DropIndex("dbo.Req_Team", new[] { "users_Id" });
            DropIndex("dbo.Req_Team", new[] { "users_Id1" });
            DropColumn("dbo.Req_Team", "users_Id");
            DropColumn("dbo.Req_Team", "users_Id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Req_Team", "users_Id1", c => c.Int());
            AddColumn("dbo.Req_Team", "users_Id", c => c.Int());
            CreateIndex("dbo.Req_Team", "users_Id1");
            CreateIndex("dbo.Req_Team", "users_Id");
            AddForeignKey("dbo.Req_Team", "users_Id1", "dbo.users", "Id");
            AddForeignKey("dbo.Req_Team", "users_Id", "dbo.users", "Id");
        }
    }
}
