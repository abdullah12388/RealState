namespace IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abdullah : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        pName = c.String(),
                        pSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        pDescription = c.String(),
                        pArea = c.String(),
                        pStatus = c.Int(nullable: false),
                        pmId = c.Int(),
                        customerId = c.Int(nullable: false),
                        pTeam = c.Int(),
                        users_Id = c.Int(),
                        users_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.users", t => t.users_Id)
                .ForeignKey("dbo.users", t => t.users_Id1)
                .ForeignKey("dbo.users", t => t.customerId, cascadeDelete: true)
                .ForeignKey("dbo.users", t => t.pmId)
                .ForeignKey("dbo.Team", t => t.pTeam)
                .Index(t => t.pmId)
                .Index(t => t.customerId)
                .Index(t => t.pTeam)
                .Index(t => t.users_Id)
                .Index(t => t.users_Id1);
            
            CreateTable(
                "dbo.users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        firstName = c.String(nullable: false, maxLength: 50),
                        lastName = c.String(nullable: false, maxLength: 50),
                        email = c.String(nullable: false, maxLength: 50),
                        password = c.String(nullable: false, maxLength: 50),
                        address = c.String(nullable: false, maxLength: 50),
                        phone = c.String(nullable: false),
                        jobDescription = c.String(),
                        qualification = c.String(),
                        experience = c.String(),
                        photo = c.String(),
                        userTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.userTypes", t => t.userTypeId, cascadeDelete: true)
                .Index(t => t.userTypeId);
            
            CreateTable(
                "dbo.Report",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        rdesc = c.String(),
                        rUser = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.users", t => t.rUser)
                .Index(t => t.rUser);
            
            CreateTable(
                "dbo.Req_proj",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        rStatue = c.Int(),
                        rProject = c.Int(nullable: false),
                        rPM = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.projects", t => t.rProject, cascadeDelete: true)
                .ForeignKey("dbo.users", t => t.rPM)
                .Index(t => t.rProject)
                .Index(t => t.rPM);
            
            CreateTable(
                "dbo.teamMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        teamId = c.Int(nullable: false),
                        userId = c.Int(nullable: false),
                        Statue = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Team", t => t.teamId, cascadeDelete: true)
                .ForeignKey("dbo.users", t => t.userId, cascadeDelete: true)
                .Index(t => t.teamId)
                .Index(t => t.userId);
            
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        pmId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.users", t => t.pmId)
                .Index(t => t.pmId);
            
            CreateTable(
                "dbo.Schedule",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        Progress = c.Int(nullable: false),
                        pId = c.Int(nullable: false),
                        teamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.projects", t => t.pId, cascadeDelete: true)
                .ForeignKey("dbo.Team", t => t.teamId, cascadeDelete: true)
                .Index(t => t.pId)
                .Index(t => t.teamId);
            
            CreateTable(
                "dbo.userTypes",
                c => new
                    {
                        userTypeId = c.Int(nullable: false, identity: true),
                        type = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.userTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.projects", "pTeam", "dbo.Team");
            DropForeignKey("dbo.projects", "pmId", "dbo.users");
            DropForeignKey("dbo.projects", "customerId", "dbo.users");
            DropForeignKey("dbo.users", "userTypeId", "dbo.userTypes");
            DropForeignKey("dbo.teamMembers", "userId", "dbo.users");
            DropForeignKey("dbo.teamMembers", "teamId", "dbo.Team");
            DropForeignKey("dbo.Team", "pmId", "dbo.users");
            DropForeignKey("dbo.Schedule", "teamId", "dbo.Team");
            DropForeignKey("dbo.Schedule", "pId", "dbo.projects");
            DropForeignKey("dbo.Req_proj", "rPM", "dbo.users");
            DropForeignKey("dbo.Req_proj", "rProject", "dbo.projects");
            DropForeignKey("dbo.Report", "rUser", "dbo.users");
            DropForeignKey("dbo.projects", "users_Id1", "dbo.users");
            DropForeignKey("dbo.projects", "users_Id", "dbo.users");
            DropIndex("dbo.Schedule", new[] { "teamId" });
            DropIndex("dbo.Schedule", new[] { "pId" });
            DropIndex("dbo.Team", new[] { "pmId" });
            DropIndex("dbo.teamMembers", new[] { "userId" });
            DropIndex("dbo.teamMembers", new[] { "teamId" });
            DropIndex("dbo.Req_proj", new[] { "rPM" });
            DropIndex("dbo.Req_proj", new[] { "rProject" });
            DropIndex("dbo.Report", new[] { "rUser" });
            DropIndex("dbo.users", new[] { "userTypeId" });
            DropIndex("dbo.projects", new[] { "users_Id1" });
            DropIndex("dbo.projects", new[] { "users_Id" });
            DropIndex("dbo.projects", new[] { "pTeam" });
            DropIndex("dbo.projects", new[] { "customerId" });
            DropIndex("dbo.projects", new[] { "pmId" });
            DropTable("dbo.userTypes");
            DropTable("dbo.Schedule");
            DropTable("dbo.Team");
            DropTable("dbo.teamMembers");
            DropTable("dbo.Req_proj");
            DropTable("dbo.Report");
            DropTable("dbo.users");
            DropTable("dbo.projects");
        }
    }
}
