using BondIssuanceHackFest.WebAPI.BondIssuance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BondIssuanceHackFest.WebAPI.Models
{
    public class Test : ITest
    {
        public string FullName

        {
            get
            {
                return "Tinshu";

            }
            set
            {
                this.FullName = value;
            }
        }
    }
}