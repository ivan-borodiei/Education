namespace EFCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAppNo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Policies", "AppNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Policies", "AppNo");
        }
    }
}
