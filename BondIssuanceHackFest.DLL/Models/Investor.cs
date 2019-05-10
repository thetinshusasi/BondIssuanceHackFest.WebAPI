using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BondIssuanceHackFest.DLL.DataModels
{
   public enum InvesterType
    {
        FundManager,
        HedgeFund,
        Retails,
        Institutional


    }
    public class Investor
    {
        public int Id { get; set; }

        public string InvesterType { get; set; }


        public Bond Bond { get; set; }

        public  IList<Bid> Bids { get; set; }


    }
}
