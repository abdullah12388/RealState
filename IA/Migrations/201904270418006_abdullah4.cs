namespace IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abdullah4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.users", "qualification");
            DropColumn("dbo.users", "experience");
        }
        
        public override void Down()
        {
            AddColumn("dbo.users", "experience", c => c.String());
            AddColumn("dbo.users", "qualification", c => c.String());
        }
    }
}
