using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BondIssuanceHackFest.Netherum.QuorumAdapters
{
    public class StandardTokenDeployment : ContractDeploymentMessage
    {
        public StandardTokenDeployment(string byteCode) : base(byteCode)
        {

        }
    }
}
