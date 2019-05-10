namespace BondIssuanceHackFest.DLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _110520193 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contracts", "ByteCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contracts", "ByteCode");
        }
    }
}
