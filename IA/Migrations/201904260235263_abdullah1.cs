namespace IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abdullah1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Req_Team", "tId", c => c.Int());
            CreateIndex("dbo.Req_Team", "tId");
            AddForeignKey("dbo.Req_Team", "tId", "dbo.Team", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Req_Team", "tId", "dbo.Team");
            DropIndex("dbo.Req_Team", new[] { "tId" });
            DropColumn("dbo.Req_Team", "tId");
        }
    }
}
