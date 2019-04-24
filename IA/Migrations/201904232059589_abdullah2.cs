namespace IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abdullah2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.users", "phone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.users", "phone", c => c.String(maxLength: 11));
        }
    }
}
