using BondIssuanceHackFest.DLL.IRepositories;
using BondIssuanceHackFest.DLL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BondIssuanceHackFest.Netherum.ContractAdapters
{
    public class ContractConnectionBuider
    {
        private readonly IContractRepository _contractRepository;
        private readonly DbContext _dbContext;
       public ContractConnectionBuider(IContractRepository contractRepository, DbContext dbContext)
        {
            _contractRepository = contractRepository;
            _dbContext = dbContext;
        }

        public void DeploySols()
        {
            var dict = GetByteCodeStringForSols();
            foreach(var key in dict.Keys)
            {
                var entity = new Contract()
                {
                    Name = key,
                    Address = dict[key]

                };
                _contractRepository.Add(entity);
            }
            _dbContext.SaveChanges();
        }
        public Dictionary<string, string> GetByteCodeStringForSols()
        {
            Dictionary<string, string> byteCodes = new Dictionary<string, string>();
            var currDir = Directory.GetCurrentDirectory();
            DirectoryInfo d = new DirectoryInfo(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Contracts/bin"));
            FileInfo[] files = d.GetFiles("*.bin");
            if (files.Count() > 0)
            {
                foreach (var file in files)
                {
                    byte[] bytes = File.ReadAllBytes(file.FullName);
                    byteCodes.Add(file.Name,System.Text.Encoding.UTF8.GetString(bytes));
                }
            }
            return byteCodes;

        }
    }
}
