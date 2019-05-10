using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BondIssuanceHackFest.DLL.DataModels
{
    enum InvesterType
    {
        FundManager,
        HedgeFund,
        Retails,
        Institutional


    }
    class Investor
    {
        public int Id { get; set; }

        public InvesterType InvesterType { get; set; }

        public Wallet Wallet { get; set; }

        public IList<Bond> Bonds { get; set; }


    }
}
