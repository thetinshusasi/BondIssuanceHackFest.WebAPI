using BondIssuanceHackFest.DLL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BondIssuanceHackFest.DLL.IRepositories;

namespace BondIssuanceHackFest.DLL.Repositories
{
    public class BondRepository : Repository<Bond>, IBondRepository
    {
        public BondRepository(DbContext context) : base(context)
        {
        }
    }
}
