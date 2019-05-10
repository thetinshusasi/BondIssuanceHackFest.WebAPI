using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BondIssuanceHackFest.DLL.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ByteCode { get; set; }

    }
}
