using BondIssuanceHackFest.Netherum.QuorumAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BondIssuanceHackFest.Netherum.ContractExt
{
  public  class BallotStandardTokenDeployement : StandardTokenDeployment
    {
        private readonly string _byteCode;
        public BallotStandardTokenDeployement(string byteCode): base(byteCode)
        {
            _byteCode = byteCode;
        }
    }
}
