using System;
using Nethereum.Web3;
using Nethereum.Quorum;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts.CQS;
using Nethereum.Util;
using Nethereum.Web3.Accounts;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Contracts;
using Nethereum.Contracts.Extensions;
using System.Numerics;
using System.Threading.Tasks;
using System.Net.WebSockets;
using Nethereum.Web3.Accounts.Managed;
using Nethereum.Contracts.ContractHandlers;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using BondIssuanceHackFest.DLL.IRepositories;
using System.Linq;
using System.Data.Entity;
using BondIssuanceHackFest.DLL.DataModels;

namespace BondIssuanceHackFest.Netherum.QuorumAdapters
{
    public class QuorumConnectionBuider
    {
        private readonly IQuorumUserRepository _quorumUserRepository;
        private readonly IQuorumNodeRepository _quorumNodeRepository;
        private readonly DbContext _dbContext;

        public QuorumConnectionBuider(IQuorumUserRepository quorumUserRepository,
            IQuorumNodeRepository quorumNodeRepository,
            DbContext dbContext)
        {
            _quorumUserRepository = quorumUserRepository;
            _quorumNodeRepository = quorumNodeRepository;
            _dbContext = dbContext;
        }

        public object SqlDbContext { get; private set; }

        public static async Task<string> asyncCreateAccount(Web3Quorum web3Private)
        {
            return await web3Private.Personal.NewAccount.SendRequestAsync("");
        }

        SqlContext db = new SqlContext();

        public async Task<int> BuildQuorumUserAddress()
        {
           var users = _quorumUserRepository.GetAll();
            var nodes = _quorumNodeRepository.GetAll();
           foreach(var user in users)
            {
                var node = nodes.ToList().Where(item => item.Id == user.QuorumNodeId).FirstOrDefault();
                if(node != null && node.Uri !=null)
                {
                    var web3Quorum = new Web3Quorum(node.Uri);
                    var accountAddress1 = await asyncCreateAccount(web3Quorum);
                    user.AccountAddress = accountAddress1;
                }
                //_quorumUserRepository.Add(user);
                //db.QuorumUsers.Add(user);

            }
            //int i=_dbContext.SaveChanges();
            int i = 0; //db.SaveChanges();
            return i;
        }


    }



    //class Program
    //{
    //    public static async Task<Nethereum.RPC.Eth.DTOs.TransactionReceipt> asyncReceiptAsync(IContractDeploymentTransactionHandler<StandardTokenDeployment> deploymentHandler, StandardTokenDeployment deploymentMessage)
    //    {
    //        return await deploymentHandler.SendRequestAndWaitForReceiptAsync(deploymentMessage);
    //    }
    //    static void Main(string[] args)
    //    {
    //        var url = "http://172.18.0.2:30303";
    //        var privateKey = "0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7";
    //        var account = new Account(privateKey);
    //        var web3 = new Web3(account, url);
    //        var deploymentMessage = new StandardTokenDeployment
    //        {
    //            TotalSupply = 100000
    //        };
    //        var deploymentHandler = web3.Eth.GetContractDeploymentHandler<StandardTokenDeployment>();
    //        var transactionReceipt = asyncReceiptAsync(deploymentHandler, deploymentMessage).Result;
    //        var contractAddress = transactionReceipt.ContractAddress;
    //        var balanceOfFunctionMessage = new BalanceOfFunction()
    //        {
    //            Owner = account.Address,
    //        };

    //        var balanceHandler = web3.Eth.GetContractQueryHandler<BalanceOfFunction>();
    //        //var balance = await balanceHandler.QueryAsync<BigInteger>(contractAddress, balanceOfFunctionMessage);

    //        //var balance = await balanceHandler.QueryDeserializingToObjectAsync<BalanceOfOutputDTO>(balanceOfFunctionMessage, contractAddress);

    //        Console.WriteLine("Hello World!");
    //    }
    //}
}
