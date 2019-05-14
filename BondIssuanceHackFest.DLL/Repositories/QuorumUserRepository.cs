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
   public class QuorumUserRepository : Repository<QuorumUser>, IQuorumUserRepository
    {
        public QuorumUserRepository(DbContext context) : base(context)
        {
        }
    }
}
