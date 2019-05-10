using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BondIssuanceHackFest.DLL.DataModels
{
  public  class Wallet
    {
        public int Id { get; set; }

        public Investor Investor { get; set; }

        public int Balance { get; set; }

    }
}
