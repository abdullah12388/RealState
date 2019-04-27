namespace IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Omar2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Experiences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        E_Name = c.String(),
                        year = c.String(),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.users", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId);
            
            CreateTable(
                "dbo.Qualifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Q_Name = c.String(),
                        rate = c.String(),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.users", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Qualifications", "userId", "dbo.users");
            DropForeignKey("dbo.Experiences", "userId", "dbo.users");
            DropIndex("dbo.Qualifications", new[] { "userId" });
            DropIndex("dbo.Experiences", new[] { "userId" });
            DropTable("dbo.Qualifications");
            DropTable("dbo.Experiences");
        }
    }
}
