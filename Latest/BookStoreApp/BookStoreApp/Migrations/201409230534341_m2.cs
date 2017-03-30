namespace BookStoreApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookReviews", "BookId", c => c.Int(nullable: false));
            AddColumn("dbo.BookReviews", "UserName", c => c.String());
            CreateIndex("dbo.BookReviews", "BookId");
            AddForeignKey("dbo.BookReviews", "BookId", "dbo.Books", "BookId", cascadeDelete: true);
            DropColumn("dbo.BookReviews", "BookName");
            DropColumn("dbo.BookReviews", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BookReviews", "Name", c => c.String());
            AddColumn("dbo.BookReviews", "BookName", c => c.String());
            DropForeignKey("dbo.BookReviews", "BookId", "dbo.Books");
            DropIndex("dbo.BookReviews", new[] { "BookId" });
            DropColumn("dbo.BookReviews", "UserName");
            DropColumn("dbo.BookReviews", "BookId");
        }
    }
}
