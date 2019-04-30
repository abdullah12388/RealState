namespace IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abdullah1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Schedule", "Start", c => c.String());
            AlterColumn("dbo.Schedule", "End", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Schedule", "End", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Schedule", "Start", c => c.DateTime(nullable: false));
        }
    }
}
