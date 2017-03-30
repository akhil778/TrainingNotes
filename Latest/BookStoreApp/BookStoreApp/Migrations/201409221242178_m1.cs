namespace BookStoreApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AuthorId = c.Int(nullable: false, identity: true),
                        AuthorName = c.String(),
                        DateOfBirth = c.DateTime(),
                        State = c.String(),
                        City = c.String(),
                        Phone = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.AuthorId);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Title = c.String(),
                        AuthorId = c.Int(nullable: false),
                        PublisherId = c.Int(nullable: false),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                        ISBN = c.String(),
                        PublicationDate = c.DateTime(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Publishers", t => t.PublisherId, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.CategoryId)
                .Index(t => t.PublisherId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        PublisherId = c.Int(nullable: false, identity: true),
                        PublisherName = c.String(),
                        DateOfBirth = c.DateTime(),
                        State = c.String(),
                        City = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.PublisherId);
            
            CreateTable(
                "dbo.BookReviews",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        BookName = c.String(),
                        Description = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        CartId = c.String(),
                        BookId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        ShoppingCartViewModel_id = c.Int(),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.ShoppingCartViewModels", t => t.ShoppingCartViewModel_id)
                .Index(t => t.BookId)
                .Index(t => t.ShoppingCartViewModel_id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderDetailId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        Username = c.String(),
                        FirstName = c.String(nullable: false, maxLength: 160),
                        LastName = c.String(nullable: false, maxLength: 160),
                        Address = c.String(nullable: false, maxLength: 70),
                        City = c.String(nullable: false, maxLength: 40),
                        State = c.String(nullable: false, maxLength: 40),
                        PostalCode = c.String(nullable: false, maxLength: 10),
                        Country = c.String(nullable: false, maxLength: 40),
                        Phone = c.String(nullable: false, maxLength: 24),
                        Email = c.String(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.ShoppingCartViewModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CartTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "ShoppingCartViewModel_id", "dbo.ShoppingCartViewModels");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "BookId", "dbo.Books");
            DropForeignKey("dbo.Carts", "BookId", "dbo.Books");
            DropForeignKey("dbo.Books", "PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.Books", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Authors");
            DropIndex("dbo.Carts", new[] { "ShoppingCartViewModel_id" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.OrderDetails", new[] { "BookId" });
            DropIndex("dbo.Carts", new[] { "BookId" });
            DropIndex("dbo.Books", new[] { "PublisherId" });
            DropIndex("dbo.Books", new[] { "CategoryId" });
            DropIndex("dbo.Books", new[] { "AuthorId" });
            DropTable("dbo.ShoppingCartViewModels");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Carts");
            DropTable("dbo.BookReviews");
            DropTable("dbo.Publishers");
            DropTable("dbo.Categories");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
