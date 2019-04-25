namespace IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abdullah : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Req_Team",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        rStatue = c.Int(),
                        rTL = c.Int(nullable: false),
                        rPM = c.Int(),
                        users_Id = c.Int(),
                        users_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.users", t => t.rTL, cascadeDelete: true)
                .ForeignKey("dbo.users", t => t.rPM)
                .ForeignKey("dbo.users", t => t.users_Id)
                .ForeignKey("dbo.users", t => t.users_Id1)
                .Index(t => t.rTL)
                .Index(t => t.rPM)
                .Index(t => t.users_Id)
                .Index(t => t.users_Id1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Req_Team", "users_Id1", "dbo.users");
            DropForeignKey("dbo.Req_Team", "users_Id", "dbo.users");
            DropForeignKey("dbo.Req_Team", "rPM", "dbo.users");
            DropForeignKey("dbo.Req_Team", "rTL", "dbo.users");
            DropIndex("dbo.Req_Team", new[] { "users_Id1" });
            DropIndex("dbo.Req_Team", new[] { "users_Id" });
            DropIndex("dbo.Req_Team", new[] { "rPM" });
            DropIndex("dbo.Req_Team", new[] { "rTL" });
            DropTable("dbo.Req_Team");
        }
    }
}
