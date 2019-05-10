using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BondIssuanceHackFest.DLL.DataModels
{
    public class Bond
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime MaturityDate { get; set; }

        public int LotSize { get; set; }

        public int MinPrice { get; set; }

        public int MaxPrice { get; set; }

        public int CouponFrequency { get; set; }

        public int LotsToAllocate { get; set; }

        public IList<Investor> Inverstors { get; set; }



    }
}
