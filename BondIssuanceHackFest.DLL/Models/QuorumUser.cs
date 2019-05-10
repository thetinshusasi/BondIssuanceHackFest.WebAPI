using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BondIssuanceHackFest.DLL.Models
{
    public enum UserType
    {
        Admin,
        Investor,
        Issuer


    }
    public class QuorumUser
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string AccountAddress { get; set; }
        public int  QuorumNodeId { get; set; }

        public UserType UserType { get; set; }




    }
}
