namespace IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abdullah : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.projects", "progress", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.projects", "progress");
        }
    }
}
