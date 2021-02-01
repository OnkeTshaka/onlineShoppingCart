namespace ReturnGym.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        Product_ProductID = c.Int(),
                    })
                .PrimaryKey(t => t.CategoryID)
                .ForeignKey("dbo.Product", t => t.Product_ProductID)
                .Index(t => t.Product_ProductID);
            
            CreateTable(
                "dbo.Club",
                c => new
                    {
                        ClubID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ClubID);
            
            CreateTable(
                "dbo.Trainer",
                c => new
                    {
                        TrainerID = c.Int(nullable: false, identity: true),
                        firstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.TrainerID);
            
            CreateTable(
                "dbo.Member",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Username = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        IDNum = c.String(),
                        MobileNumber = c.String(nullable: false),
                        JoinDate = c.String(nullable: false),
                        Profile = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        OrderDetailId = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        CountItems = c.Int(nullable: false),
                        IsSelected = c.Boolean(nullable: false),
                        Member_ID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderDetailId)
                .ForeignKey("dbo.Member", t => t.Member_ID)
                .ForeignKey("dbo.Order", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID)
                .Index(t => t.Member_ID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        CustomerPhone = c.String(),
                        CustomerEmail = c.String(),
                        CustomerAddress = c.String(),
                        Refcode = c.String(),
                        OrderDate = c.DateTime(nullable: false),
                        PaymentType = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.OrderID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Picture = c.Binary(),
                        Price = c.Double(nullable: false),
                        Time = c.String(),
                        Quantity = c.Int(nullable: false),
                        CatName = c.String(),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.ReturnItem",
                c => new
                    {
                        ReturnItemID = c.Int(nullable: false, identity: true),
                        PurchaseRef = c.String(),
                        OTP = c.Int(),
                        Quantity = c.Int(nullable: false),
                        Description = c.String(),
                        Reason = c.String(),
                        ClientName = c.String(),
                        ClientEmail = c.String(),
                        ReturnDate = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        Tax = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                        Status = c.Boolean(nullable: false),
                        OrderID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReturnItemID)
                .ForeignKey("dbo.Order", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Address = c.String(),
                        IDNum = c.String(),
                        LastName = c.String(),
                        ProfilePicture = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TrainerClub",
                c => new
                    {
                        ClubID = c.Int(nullable: false),
                        TrainerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClubID, t.TrainerID })
                .ForeignKey("dbo.Club", t => t.ClubID, cascadeDelete: true)
                .ForeignKey("dbo.Trainer", t => t.TrainerID, cascadeDelete: true)
                .Index(t => t.ClubID)
                .Index(t => t.TrainerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ReturnItem", "OrderID", "dbo.Order");
            DropForeignKey("dbo.OrderDetail", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Category", "Product_ProductID", "dbo.Product");
            DropForeignKey("dbo.OrderDetail", "OrderID", "dbo.Order");
            DropForeignKey("dbo.OrderDetail", "Member_ID", "dbo.Member");
            DropForeignKey("dbo.TrainerClub", "TrainerID", "dbo.Trainer");
            DropForeignKey("dbo.TrainerClub", "ClubID", "dbo.Club");
            DropIndex("dbo.TrainerClub", new[] { "TrainerID" });
            DropIndex("dbo.TrainerClub", new[] { "ClubID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ReturnItem", new[] { "OrderID" });
            DropIndex("dbo.OrderDetail", new[] { "Member_ID" });
            DropIndex("dbo.OrderDetail", new[] { "ProductID" });
            DropIndex("dbo.OrderDetail", new[] { "OrderID" });
            DropIndex("dbo.Category", new[] { "Product_ProductID" });
            DropTable("dbo.TrainerClub");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ReturnItem");
            DropTable("dbo.Product");
            DropTable("dbo.Order");
            DropTable("dbo.OrderDetail");
            DropTable("dbo.Member");
            DropTable("dbo.Trainer");
            DropTable("dbo.Club");
            DropTable("dbo.Category");
        }
    }
}
