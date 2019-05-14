using BondIssuanceHackFest.DLL.DataModels;
using BondIssuanceHackFest.DLL.IRepositories;
using BondIssuanceHackFest.DLL.Models;
using BondIssuanceHackFest.Netherum.ContractAdapters;
using BondIssuanceHackFest.Netherum.QuorumAdapters;
using BondIssuanceHackFest.WebAPI.BondIssuance.Interfaces;
using BondIssuanceHackFest.WebAPI.Controllers.Nethereum.StandardTokenEIP20.ContractDefinition;
using Nethereum.Contracts.ContractHandlers;
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
using System.Web.Http.Cors;

namespace BondIssuanceHackFest.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class DeployController : ApiController
    {
        public static async Task<TransactionReceipt> asyncReceiptDeployTokenAsync(IContractDeploymentTransactionHandler<EIP20DeploymentBase> deploymentHandler, EIP20DeploymentBase deploymentMessage)
        {
            return await deploymentHandler.SendRequestAndWaitForReceiptAsync(deploymentMessage);
        }
       
        // GET api/values
        
        public static async Task<bool> asyncUnlockAccount(Web3Quorum web3Private, string account)
        {
            return await web3Private.Personal.UnlockAccount.SendRequestAsync(account, "", 30000);

        }
        public static async Task<TransactionReceipt> asyncTransferAmount(string contractAddress, TransferFunction transfer, IContractTransactionHandler<TransferFunction> deploymentHandler)
        {
            return await deploymentHandler.SendRequestAndWaitForReceiptAsync(contractAddress, transfer);
        }

        private readonly IBondRepository _bondRepository;
        private readonly IQuorumUserRepository _quorumUserRepository;
        private readonly IQuorumNodeRepository _quorumNodeRepository;
        private readonly DbContext _dbContext;
        private readonly IContractRepository _contractRepository;
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
        public async Task<IHttpActionResult> Post()
        {
            var quorumConnector = new QuorumConnectionBuider(_quorumUserRepository, _quorumNodeRepository, _dbContext);
           await quorumConnector.BuildQuorumUserAddress();
            var contractConnector = new ContractConnectionBuider(_contractRepository, _dbContext);
            contractConnector.DeploySols();
            return Ok();
        }

        // PUT api/values/5

        [HttpPost]
        [Route("api/DeployContract")]
        public async Task<IHttpActionResult> DeployContracts()
        {
            var user = _quorumUserRepository.GetAll().Where(item=>item.Id ==7).FirstOrDefault();
            var admin = _quorumNodeRepository.GetAll().Where(item=>item.Id == 1).FirstOrDefault();
                //.Where(item => item.Id == user.QuorumNodeId).FirstOrDefault();
            QuorumAccount accountOut = new QuorumAccount(user.AccountAddress);
            var web3QuorumOut = new Web3Quorum(accountOut, admin.Uri);
            var unlockedOut = await asyncUnlockAccount(web3QuorumOut, user.AccountAddress);

            var deploymentHandlerOut = web3QuorumOut.Eth.GetContractDeploymentHandler<EIP20DeploymentBase>();
            var deploymentMessageOut = new EIP20DeploymentBase
            {
                InitialAmount = 9000000,
                TokenSymbol = "NwmT",
                TokenName = "NatwestToken",
                DecimalUnits = 8
            };
            deploymentMessageOut.GasPrice = 0;

            var transactionReceipt = await asyncReceiptDeployTokenAsync(deploymentHandlerOut, deploymentMessageOut);
            var contractAddress = transactionReceipt.ContractAddress;

            QuorumAccount accountOut2 = new QuorumAccount(user.AccountAddress);
            var web3QuorumOut2 = new Web3Quorum(accountOut2, admin.Uri);
            var unlockedOut2 = await asyncUnlockAccount(web3QuorumOut2, user.AccountAddress);

            var deploymentHandlerOut2 = web3QuorumOut2.Eth.GetContractTransactionHandler<TransferFunction>();

            var user8 = _quorumUserRepository.GetAll().Where(item => item.Id == 8).FirstOrDefault();
            var admin2 = _quorumNodeRepository.GetAll().Where(item => item.Id == 2).FirstOrDefault();
            var transfer = new TransferFunction()
            {
                To = user8.AccountAddress,
                Value = 300000
            };
            transfer.GasPrice = 0;
            var transactionReceipt2 = await asyncTransferAmount(contractAddress, transfer, deploymentHandlerOut2);
            var transactionHash2 = transactionReceipt2.TransactionHash;

            var user9 = _quorumUserRepository.GetAll().Where(item => item.Id == 9).FirstOrDefault();
            var admin3 = _quorumNodeRepository.GetAll().Where(item => item.Id == 3).FirstOrDefault();

            var transfer2 = new TransferFunction()
            {
                To = user9.AccountAddress,
                Value = 300000
            };
            transfer.GasPrice = 0;
            var transactionReceipt3 =await asyncTransferAmount(contractAddress, transfer2, deploymentHandlerOut2);
            var transactionHash3 = transactionReceipt3.TransactionHash;

            var user10 = _quorumUserRepository.GetAll().Where(item => item.Id == 10).FirstOrDefault();
            var admin4 = _quorumNodeRepository.GetAll().Where(item => item.Id == 4).FirstOrDefault();
            var transfer3 = new TransferFunction()
            {
                To = user10.AccountAddress,
                Value = 300000
            };
            transfer.GasPrice = 0;
            //transfer.Gas = 3000000;
            var transactionReceipt4 = await asyncTransferAmount(contractAddress, transfer, deploymentHandlerOut2);
            //await deploymentHandlerOut2.SendRequestAndWaitForReceiptAsync("0x882138260be6fe8d6014d5419ace001c400cce2c", transfer);
            var transactionHash = transactionReceipt2.TransactionHash;


            //////////////////////////////////////////////////////////////////////
            var bidDeploymentHandlerOut = web3QuorumOut.Eth.GetContractDeploymentHandler<BookRunner>();
            var bidDeploymentMessageOut = new BookRunner
            {
                TokenAddress = contractAddress,
                Investor = user.AccountAddress,
                Name = "Tesla Bond",
                ISIN = "HFU34HR34",
                DecimalUnits = 8,
                Lead_Book_Runner = user.AccountAddress,
                Inv_type = "Runner",
                Bond_Name = "Tesla Bond",
                Bond_Min_Price = 950,
                Bond_Max_Price = 1000,
                Bond_Total_Lots = 1000
            };
            deploymentMessageOut.GasPrice = 0;

            var bidTransactionReceipt = await asyncReceiptDeployTokenAsync(deploymentHandlerOut, deploymentMessageOut);
            var bidContractAddress = transactionReceipt.ContractAddress;
            SqlContext d = new SqlContext();
           var contract= d.Contracts.FirstOrDefault(x => x.Name.Equals( "BookRunner",StringComparison.CurrentCultureIgnoreCase));
            if (contract != null)
            {
                contract.Address = bidContractAddress;

            }
            else
            {
                d.Contracts.Add(new Contract()
                {
                    Name= "BookRunner",
                    Address= bidContractAddress,
                    ByteCode= BookRunner.BYTECODE
                });
            }



            return Ok();
        }

        
       
    }
}
