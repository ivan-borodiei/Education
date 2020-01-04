namespace EFCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhoneType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PersonPhones", "PhoneType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PersonPhones", "PhoneType");
        }
    }
}
