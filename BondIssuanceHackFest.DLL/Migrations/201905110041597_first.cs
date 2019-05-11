namespace BondIssuanceHackFest.DLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
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
                "dbo.Bonds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MaturityDate = c.DateTime(nullable: false),
                        LotSize = c.Int(nullable: false),
                        MinPrice = c.Int(nullable: false),
                        MaxPrice = c.Int(nullable: false),
                        CouponFrequency = c.Int(nullable: false),
                        LotsToAllocate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Investors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvesterType = c.String(),
                        Bond_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bonds", t => t.Bond_Id)
                .Index(t => t.Bond_Id);
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        ByteCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QuorumNodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Uri = c.String(),
                        ConstellationPublicKey = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QuorumUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AccountAddress = c.String(),
                        QuorumNodeId = c.Int(nullable: false),
                        UserType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Wallets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvestorId = c.Int(nullable: false),
                        Balance = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Investors", t => t.InvestorId, cascadeDelete: true)
                .Index(t => t.InvestorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wallets", "InvestorId", "dbo.Investors");
            DropForeignKey("dbo.Bids", "Bond_Id", "dbo.Bonds");
            DropForeignKey("dbo.Investors", "Bond_Id", "dbo.Bonds");
            DropForeignKey("dbo.Bids", "Investor_Id", "dbo.Investors");
            DropIndex("dbo.Wallets", new[] { "InvestorId" });
            DropIndex("dbo.Investors", new[] { "Bond_Id" });
            DropIndex("dbo.Bids", new[] { "Bond_Id" });
            DropIndex("dbo.Bids", new[] { "Investor_Id" });
            DropTable("dbo.Wallets");
            DropTable("dbo.QuorumUsers");
            DropTable("dbo.QuorumNodes");
            DropTable("dbo.Contracts");
            DropTable("dbo.Investors");
            DropTable("dbo.Bonds");
            DropTable("dbo.Bids");
        }
    }
}
