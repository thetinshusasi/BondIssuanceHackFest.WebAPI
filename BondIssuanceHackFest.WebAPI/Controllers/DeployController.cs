using BondIssuanceHackFest.DLL.IRepositories;
using BondIssuanceHackFest.DLL.Models;
using BondIssuanceHackFest.Netherum.ContractAdapters;
using BondIssuanceHackFest.Netherum.QuorumAdapters;
using BondIssuanceHackFest.WebAPI.BondIssuance.Interfaces;
using BondIssuanceHackFest.WebAPI.Controllers.Nethereum.StandardTokenEIP20.ContractDefinition;
using Nethereum.Contracts.CQS;
using Nethereum.Quorum;
using Nethereum.RPC;
using Nethereum.RPC.Eth.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BondIssuanceHackFest.WebAPI.Controllers
{
    public class DeployController : ApiController
    {
        public static async Task<TransactionReceipt> asyncReceiptDeployTokenAsync(IContractDeploymentTransactionHandler<EIP20DeploymentBase> deploymentHandler, EIP20DeploymentBase deploymentMessage)
        {
            return await deploymentHandler.SendRequestAndWaitForReceiptAsync(deploymentMessage);
        }
        private readonly IBondRepository _bondRepository;
        private readonly IQuorumUserRepository _quorumUserRepository;
        private readonly IQuorumNodeRepository _quorumNodeRepository;
        private readonly DbContext _dbContext;
        private readonly IContractRepository _contractRepository;
        // GET api/values
        
        public static async Task<bool> asyncUnlockAccount(Web3Quorum web3Private, string account)
        {
            return await web3Private.Personal.UnlockAccount.SendRequestAsync(account, "", 30000);

        }

        public DeployController(DbContext dbContext, 
            IBondRepository bondRepository,
            IQuorumUserRepository quorumUserRepository,
            IQuorumNodeRepository quorumNodeRepository,
             IContractRepository contractRepository)
        {
            _bondRepository = bondRepository;
            _quorumUserRepository = quorumUserRepository;
            _quorumNodeRepository = quorumNodeRepository;
            _dbContext = dbContext;
            _contractRepository = contractRepository;

        }


        // GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        public IHttpActionResult Post()
        {
            var quorumConnector = new QuorumConnectionBuider(_quorumUserRepository, _quorumNodeRepository, _dbContext);
            quorumConnector.BuildQuorumUserAddress();
            var contractConnector = new ContractConnectionBuider(_contractRepository, _dbContext);
            contractConnector.DeploySols();
            return Ok();
        }

        // PUT api/values/5

        [HttpPost]
        [Route(Name ="DeployContract")]
        public IHttpActionResult DeployContracts()
        {
            var user = _quorumUserRepository.GetAll().Where(item=>item.UserType == UserType.Admin).FirstOrDefault();
            var admin = _quorumNodeRepository.GetAll().Where(item => item.Id == user.QuorumNodeId).FirstOrDefault();
            QuorumAccount accountOut = new QuorumAccount(user.AccountAddress);
            var web3QuorumOut = new Web3Quorum(accountOut, admin.Uri);
            var unlockedOut = asyncUnlockAccount(web3QuorumOut, user.AccountAddress).Result;

            var deploymentHandlerOut = web3QuorumOut.Eth.GetContractDeploymentHandler<EIP20DeploymentBase>();
            var deploymentMessageOut = new EIP20DeploymentBase
            {
                InitialAmount = 100000,
                TokenSymbol = "NwmT",
                TokenName = "NatwestToken",
                DecimalUnits = 8
            };
            deploymentMessageOut.GasPrice = 0;

            var transactionReceipt = asyncReceiptDeployTokenAsync(deploymentHandlerOut, deploymentMessageOut).Result;
            var contractAddress = transactionReceipt.ContractAddress;

            return Ok();
        }
       
    }
}
