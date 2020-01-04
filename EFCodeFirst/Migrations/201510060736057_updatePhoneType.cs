namespace EFCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatePhoneType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PersonPhones", "PhoneType", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PersonPhones", "PhoneType", c => c.Int(nullable: false));
        }
    }
}
