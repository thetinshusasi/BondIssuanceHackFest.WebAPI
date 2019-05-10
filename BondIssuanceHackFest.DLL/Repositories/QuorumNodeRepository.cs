using BondIssuanceHackFest.DLL.IRepositories;
using BondIssuanceHackFest.DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BondIssuanceHackFest.DLL.Repositories
{
    public class QuorumNodeRepository : Repository<QuorumNode>, IQuorumNodeRepository
    {
        public QuorumNodeRepository(DbContext context) : base(context)
        {
        }
    }
}
