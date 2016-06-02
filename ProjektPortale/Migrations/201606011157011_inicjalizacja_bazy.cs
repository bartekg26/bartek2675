namespace ProjektPortale.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicjalizacja_bazy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Produkts",
                c => new
                    {
                        idProduct = c.Int(nullable: false, identity: true),
                        nazwaProduktu = c.String(nullable: false),
                        opisProduktu = c.String(nullable: false),
                        cena = c.Int(nullable: false),
                        sciezkaObrazka = c.String(),
                    })
                .PrimaryKey(t => t.idProduct);
            
            CreateTable(
                "dbo.ApplicationUserProdukts",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Produkt_idProduct = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Produkt_idProduct })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Produkts", t => t.Produkt_idProduct, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Produkt_idProduct);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserProdukts", "Produkt_idProduct", "dbo.Produkts");
            DropForeignKey("dbo.ApplicationUserProdukts", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserProdukts", new[] { "Produkt_idProduct" });
            DropIndex("dbo.ApplicationUserProdukts", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserProdukts");
            DropTable("dbo.Produkts");
        }
    }
}
