namespace EFCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhones : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersonPhones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Person_Id = c.Int(),
                        Phone_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .ForeignKey("dbo.Phones", t => t.Phone_Id)
                .Index(t => t.Person_Id)
                .Index(t => t.Phone_Id);
            
            CreateTable(
                "dbo.Phones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonPhones", "Phone_Id", "dbo.Phones");
            DropForeignKey("dbo.PersonPhones", "Person_Id", "dbo.People");
            DropIndex("dbo.PersonPhones", new[] { "Phone_Id" });
            DropIndex("dbo.PersonPhones", new[] { "Person_Id" });
            DropTable("dbo.Phones");
            DropTable("dbo.PersonPhones");
        }
    }
}
