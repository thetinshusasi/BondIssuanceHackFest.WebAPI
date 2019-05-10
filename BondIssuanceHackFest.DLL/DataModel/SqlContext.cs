using BondIssuanceHackFest.DLL.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BondIssuanceHackFest.DLL.DataModels
{
    public class SqlContext : DbContext
    {
        public SqlContext():base("name=BondIssuanceConnectionString")
        {

        }
        
        public DbSet<Bond> Bonds { get; set; }

        public DbSet<Bid> Bids { get; set; }

        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<Investor> Investors { get; set; }
    }
}
