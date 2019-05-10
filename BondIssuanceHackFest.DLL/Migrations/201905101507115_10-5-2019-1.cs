namespace BondIssuanceHackFest.DLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10520191 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Investors", "Wallet_Id", "dbo.Wallets");
            DropIndex("dbo.Investors", new[] { "Wallet_Id" });
            CreateIndex("dbo.Wallets", "InvestorId");
            AddForeignKey("dbo.Wallets", "InvestorId", "dbo.Investors", "Id", cascadeDelete: true);
            DropColumn("dbo.Investors", "Wallet_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Investors", "Wallet_Id", c => c.Int());
            DropForeignKey("dbo.Wallets", "InvestorId", "dbo.Investors");
            DropIndex("dbo.Wallets", new[] { "InvestorId" });
            CreateIndex("dbo.Investors", "Wallet_Id");
            AddForeignKey("dbo.Investors", "Wallet_Id", "dbo.Wallets", "Id");
        }
    }
}
