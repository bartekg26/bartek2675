namespace ProjektPortale.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicjalizacja_bazy1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produkts", "Produkty_UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Produkts", "Produkty_UserId");
            AddForeignKey("dbo.Produkts", "Produkty_UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Produkts", "Produkty_UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Produkts", new[] { "Produkty_UserId" });
            DropColumn("dbo.Produkts", "Produkty_UserId");
        }
    }
}
