namespace IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abdullah : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User_Type",
                c => new
                    {
                        T_ID = c.Int(nullable: false, identity: true),
                        type = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.T_ID);
            
            CreateTable(
                "dbo.users",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        F_Name = c.String(nullable: false, maxLength: 50),
                        L_Name = c.String(nullable: false, maxLength: 50),
                        mail = c.String(nullable: false, maxLength: 50),
                        password = c.String(nullable: false, maxLength: 50),
                        address = c.String(maxLength: 50),
                        photo = c.String(nullable: false, maxLength: 50),
                        T_ID = c.Int(nullable: false),
                        phone = c.String(maxLength: 11),
                        J_description = c.String(),
                        qualification = c.String(),
                        experience = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User_Type", t => t.T_ID, cascadeDelete: true)
                .Index(t => t.T_ID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        P_ID = c.Int(nullable: false, identity: true),
                        p_name = c.String(nullable: false, maxLength: 50),
                        p_salary = c.Decimal(nullable: false, storeType: "money"),
                        p_desc = c.String(nullable: false, unicode: false, storeType: "text"),
                        p_area = c.String(nullable: false, maxLength: 50),
                        p_pm = c.Int(),
                        p_customer = c.Int(nullable: false),
                        p_comment = c.String(unicode: false, storeType: "text"),
                        p_status = c.Int(nullable: false),
                        p_team = c.Int(),
                        Team_T_ID = c.Int(),
                        user_Id = c.Int(),
                        user1_Id = c.Int(),
                        user_Id1 = c.Int(),
                        user_Id2 = c.Int(),
                    })
                .PrimaryKey(t => t.P_ID)
                .ForeignKey("dbo.Team", t => t.Team_T_ID)
                .ForeignKey("dbo.users", t => t.user_Id)
                .ForeignKey("dbo.users", t => t.user1_Id)
                .ForeignKey("dbo.users", t => t.user_Id1)
                .ForeignKey("dbo.users", t => t.user_Id2)
                .Index(t => t.Team_T_ID)
                .Index(t => t.user_Id)
                .Index(t => t.user1_Id)
                .Index(t => t.user_Id1)
                .Index(t => t.user_Id2);
            
            CreateTable(
                "dbo.Req_proj",
                c => new
                    {
                        R_ID = c.Int(nullable: false, identity: true),
                        R_PM = c.Int(nullable: false),
                        R_PROJ = c.Int(nullable: false),
                        R_statue = c.Int(),
                        Project_P_ID = c.Int(),
                        user_Id = c.Int(),
                    })
                .PrimaryKey(t => t.R_ID)
                .ForeignKey("dbo.Projects", t => t.Project_P_ID)
                .ForeignKey("dbo.users", t => t.user_Id)
                .Index(t => t.Project_P_ID)
                .Index(t => t.user_Id);
            
            CreateTable(
                "dbo.Schedule",
                c => new
                    {
                        S_ID = c.Int(nullable: false, identity: true),
                        P_ID = c.Int(nullable: false),
                        Start = c.DateTime(nullable: false, storeType: "date"),
                        End = c.DateTime(nullable: false, storeType: "date"),
                        Progress = c.Int(nullable: false),
                        team_ID = c.Int(nullable: false),
                        Team_T_ID = c.Int(),
                    })
                .PrimaryKey(t => t.S_ID)
                .ForeignKey("dbo.Projects", t => t.P_ID, cascadeDelete: true)
                .ForeignKey("dbo.Team", t => t.Team_T_ID)
                .Index(t => t.P_ID)
                .Index(t => t.Team_T_ID);
            
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        T_ID = c.Int(nullable: false, identity: true),
                        PM_ID = c.Int(nullable: false),
                        user_Id = c.Int(),
                    })
                .PrimaryKey(t => t.T_ID)
                .ForeignKey("dbo.users", t => t.user_Id)
                .Index(t => t.user_Id);
            
            CreateTable(
                "dbo.Team_member",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        T_ID = c.Int(nullable: false),
                        Mem_ID = c.Int(nullable: false),
                        Statue = c.Int(),
                        user_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Team", t => t.T_ID, cascadeDelete: true)
                .ForeignKey("dbo.users", t => t.user_Id)
                .Index(t => t.T_ID)
                .Index(t => t.user_Id);
            
            CreateTable(
                "dbo.Report",
                c => new
                    {
                        R_ID = c.Int(nullable: false, identity: true),
                        R_user = c.Int(),
                        desc = c.String(nullable: false, unicode: false, storeType: "text"),
                        user_Id = c.Int(),
                    })
                .PrimaryKey(t => t.R_ID)
                .ForeignKey("dbo.users", t => t.user_Id)
                .Index(t => t.user_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.users", "T_ID", "dbo.User_Type");
            DropForeignKey("dbo.Report", "user_Id", "dbo.users");
            DropForeignKey("dbo.Projects", "user_Id2", "dbo.users");
            DropForeignKey("dbo.Projects", "user_Id1", "dbo.users");
            DropForeignKey("dbo.Projects", "user1_Id", "dbo.users");
            DropForeignKey("dbo.Projects", "user_Id", "dbo.users");
            DropForeignKey("dbo.Team", "user_Id", "dbo.users");
            DropForeignKey("dbo.Team_member", "user_Id", "dbo.users");
            DropForeignKey("dbo.Team_member", "T_ID", "dbo.Team");
            DropForeignKey("dbo.Schedule", "Team_T_ID", "dbo.Team");
            DropForeignKey("dbo.Projects", "Team_T_ID", "dbo.Team");
            DropForeignKey("dbo.Schedule", "P_ID", "dbo.Projects");
            DropForeignKey("dbo.Req_proj", "user_Id", "dbo.users");
            DropForeignKey("dbo.Req_proj", "Project_P_ID", "dbo.Projects");
            DropIndex("dbo.Report", new[] { "user_Id" });
            DropIndex("dbo.Team_member", new[] { "user_Id" });
            DropIndex("dbo.Team_member", new[] { "T_ID" });
            DropIndex("dbo.Team", new[] { "user_Id" });
            DropIndex("dbo.Schedule", new[] { "Team_T_ID" });
            DropIndex("dbo.Schedule", new[] { "P_ID" });
            DropIndex("dbo.Req_proj", new[] { "user_Id" });
            DropIndex("dbo.Req_proj", new[] { "Project_P_ID" });
            DropIndex("dbo.Projects", new[] { "user_Id2" });
            DropIndex("dbo.Projects", new[] { "user_Id1" });
            DropIndex("dbo.Projects", new[] { "user1_Id" });
            DropIndex("dbo.Projects", new[] { "user_Id" });
            DropIndex("dbo.Projects", new[] { "Team_T_ID" });
            DropIndex("dbo.users", new[] { "T_ID" });
            DropTable("dbo.Report");
            DropTable("dbo.Team_member");
            DropTable("dbo.Team");
            DropTable("dbo.Schedule");
            DropTable("dbo.Req_proj");
            DropTable("dbo.Projects");
            DropTable("dbo.users");
            DropTable("dbo.User_Type");
        }
    }
}
