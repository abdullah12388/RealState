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
                        pPM = c.Int(),
                        pCustomer = c.Int(nullable: false),
                        pTeam = c.Int(),
                        users_Id = c.Int(),
                        users_Id1 = c.Int(),
                        Team_Id = c.Int(),
                        Customer_Id = c.Int(),
                        Pm_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.users", t => t.users_Id)
                .ForeignKey("dbo.users", t => t.users_Id1)
                .ForeignKey("dbo.Team", t => t.Team_Id)
                .ForeignKey("dbo.users", t => t.Customer_Id)
                .ForeignKey("dbo.users", t => t.Pm_Id)
                .Index(t => t.users_Id)
                .Index(t => t.users_Id1)
                .Index(t => t.Team_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.Pm_Id);
            
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
                        phone = c.String(),
                        jobDescription = c.String(),
                        qualification = c.String(),
                        experience = c.String(),
                        userTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.userTypes", t => t.userTypeId)
                .Index(t => t.userTypeId);
            
            CreateTable(
                "dbo.Report",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        rdesc = c.String(),
                        rUser = c.Int(),
                        user_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.users", t => t.user_Id)
                .Index(t => t.user_Id);
            
            CreateTable(
                "dbo.Req_proj",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        rStatue = c.Int(),
                        rProject = c.Int(nullable: false),
                        rPM = c.Int(nullable: false),
                        Project_Id = c.Int(),
                        user_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.projects", t => t.Project_Id)
                .ForeignKey("dbo.users", t => t.user_Id)
                .Index(t => t.Project_Id)
                .Index(t => t.user_Id);
            
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
                        pmId = c.Int(nullable: false),
                        user_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.users", t => t.user_Id)
                .Index(t => t.user_Id);
            
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
                        Project_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.projects", t => t.Project_Id)
                .ForeignKey("dbo.Team", t => t.teamId, cascadeDelete: true)
                .Index(t => t.teamId)
                .Index(t => t.Project_Id);
            
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
            DropForeignKey("dbo.projects", "Pm_Id", "dbo.users");
            DropForeignKey("dbo.projects", "Customer_Id", "dbo.users");
            DropForeignKey("dbo.users", "userTypeId", "dbo.userTypes");
            DropForeignKey("dbo.teamMembers", "userId", "dbo.users");
            DropForeignKey("dbo.Team", "user_Id", "dbo.users");
            DropForeignKey("dbo.teamMembers", "teamId", "dbo.Team");
            DropForeignKey("dbo.Schedule", "teamId", "dbo.Team");
            DropForeignKey("dbo.Schedule", "Project_Id", "dbo.projects");
            DropForeignKey("dbo.projects", "Team_Id", "dbo.Team");
            DropForeignKey("dbo.Req_proj", "user_Id", "dbo.users");
            DropForeignKey("dbo.Req_proj", "Project_Id", "dbo.projects");
            DropForeignKey("dbo.Report", "user_Id", "dbo.users");
            DropForeignKey("dbo.projects", "users_Id1", "dbo.users");
            DropForeignKey("dbo.projects", "users_Id", "dbo.users");
            DropIndex("dbo.Schedule", new[] { "Project_Id" });
            DropIndex("dbo.Schedule", new[] { "teamId" });
            DropIndex("dbo.Team", new[] { "user_Id" });
            DropIndex("dbo.teamMembers", new[] { "userId" });
            DropIndex("dbo.teamMembers", new[] { "teamId" });
            DropIndex("dbo.Req_proj", new[] { "user_Id" });
            DropIndex("dbo.Req_proj", new[] { "Project_Id" });
            DropIndex("dbo.Report", new[] { "user_Id" });
            DropIndex("dbo.users", new[] { "userTypeId" });
            DropIndex("dbo.projects", new[] { "Pm_Id" });
            DropIndex("dbo.projects", new[] { "Customer_Id" });
            DropIndex("dbo.projects", new[] { "Team_Id" });
            DropIndex("dbo.projects", new[] { "users_Id1" });
            DropIndex("dbo.projects", new[] { "users_Id" });
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
