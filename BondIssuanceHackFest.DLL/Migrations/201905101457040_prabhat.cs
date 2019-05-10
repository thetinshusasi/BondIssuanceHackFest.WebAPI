namespace BondIssuanceHackFest.DLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prabhat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bids",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NoOfLots = c.Int(nullable: false),
                        Investor_Id = c.Int(),
                        Bond_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Investors", t => t.Investor_Id)
                .ForeignKey("dbo.Bonds", t => t.Bond_Id)
                .Index(t => t.Investor_Id)
                .Index(t => t.Bond_Id);
            
            CreateTable(
                "dbo.Investors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvesterType = c.String(),
                        Bond_Id = c.Int(),
                        Wallet_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bonds", t => t.Bond_Id)
                .ForeignKey("dbo.Wallets", t => t.Wallet_Id)
                .Index(t => t.Bond_Id)
                .Index(t => t.Wallet_Id);
            
            CreateTable(
                "dbo.Wallets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvestorId = c.Int(nullable: false),
                        Balance = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Bonds", "MaturityDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Bonds", "LotSize", c => c.Int(nullable: false));
            AddColumn("dbo.Bonds", "MinPrice", c => c.Int(nullable: false));
            AddColumn("dbo.Bonds", "MaxPrice", c => c.Int(nullable: false));
            AddColumn("dbo.Bonds", "CouponFrequency", c => c.Int(nullable: false));
            AddColumn("dbo.Bonds", "LotsToAllocate", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bids", "Bond_Id", "dbo.Bonds");
            DropForeignKey("dbo.Investors", "Wallet_Id", "dbo.Wallets");
            DropForeignKey("dbo.Investors", "Bond_Id", "dbo.Bonds");
            DropForeignKey("dbo.Bids", "Investor_Id", "dbo.Investors");
            DropIndex("dbo.Investors", new[] { "Wallet_Id" });
            DropIndex("dbo.Investors", new[] { "Bond_Id" });
            DropIndex("dbo.Bids", new[] { "Bond_Id" });
            DropIndex("dbo.Bids", new[] { "Investor_Id" });
            DropColumn("dbo.Bonds", "LotsToAllocate");
            DropColumn("dbo.Bonds", "CouponFrequency");
            DropColumn("dbo.Bonds", "MaxPrice");
            DropColumn("dbo.Bonds", "MinPrice");
            DropColumn("dbo.Bonds", "LotSize");
            DropColumn("dbo.Bonds", "MaturityDate");
            DropTable("dbo.Wallets");
            DropTable("dbo.Investors");
            DropTable("dbo.Bids");
        }
    }
}
