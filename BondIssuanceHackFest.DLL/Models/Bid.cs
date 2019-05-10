using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BondIssuanceHackFest.DLL.DataModels
{
   public class Bid
    {
        public int Id { get; set; }

        public Bond Bond { get; set; }

        public int NoOfLots { get; set; }

        public Investor Investor { get; set; }

    }
}
