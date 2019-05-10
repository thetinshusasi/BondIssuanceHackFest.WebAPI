using BondIssuanceHackFest.DLL.IRepositories;
using BondIssuanceHackFest.Netherum.ContractAdapters;
using BondIssuanceHackFest.Netherum.QuorumAdapters;
using BondIssuanceHackFest.WebAPI.BondIssuance.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BondIssuanceHackFest.WebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly ITest _test;
        private readonly IBondRepository _bondRepository;
        private readonly IQuorumUserRepository _quorumUserRepository;
        private readonly IQuorumNodeRepository _quorumNodeRepository;
        private readonly DbContext _dbContext;
        // GET api/values


        public ValuesController(DbContext dbContext, IBondRepository bondRepository, IQuorumUserRepository quorumUserRepository)
        {
            _bondRepository = bondRepository;
            _quorumUserRepository = quorumUserRepository;
            _quorumUserRepository = quorumUserRepository;
            _dbContext = dbContext;
        }
        public IEnumerable<string> Get()
        {
            var data = new string[] { "value1", "value2", _test.FullName };
            var quorumConnector = new QuorumConnectionBuider(_quorumUserRepository, _quorumNodeRepository, _dbContext);
            quorumConnector.BuildQuorumUserAddress();
            var contractConnector = new ContractConnectionBuider();
            contractConnector.DeploySols();
            var d = _bondRepository.GetAll();
            return data;
        }

        // GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
