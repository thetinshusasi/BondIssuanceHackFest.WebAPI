using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BondIssuanceHackFest.Netherum.ContractAdapters
{
    public class ContractConnectionBuider
    {

        public void DeploySols()
        {

        }
        public List<string> GetByteCodeStringForSols()
        {
            List<string> byteCodes = new List<string>();
            var currDir = Directory.GetCurrentDirectory();
            DirectoryInfo d = new DirectoryInfo(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Contracts/bin"));
            FileInfo[] files = d.GetFiles("*.bin");
            if (files.Count() > 0)
            {
                foreach (var file in files)
                {
                    byte[] bytes = File.ReadAllBytes(file.FullName);
                    byteCodes.Add(System.Text.Encoding.UTF8.GetString(bytes));
                }
            }
            return byteCodes;

        }
    }
}
