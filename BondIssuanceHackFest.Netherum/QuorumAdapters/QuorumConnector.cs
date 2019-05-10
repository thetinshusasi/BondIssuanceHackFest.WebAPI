using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts.CQS;
using Nethereum.Util;
using Nethereum.Web3.Accounts;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Contracts;
using Nethereum.Contracts.Extensions;
using System.Numerics;
using System.IO;

namespace BondIssuanceHackFest.Netherum.QuorumAdapters
{
    public class QuorumConnector
    {
        
        public List<string> ConvertSolsToByteCodeStrings()
        {
            List<string> byteCodes = new List<string>();
            var currDir = Directory.GetCurrentDirectory();
            DirectoryInfo d = new DirectoryInfo(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Contracts/bin"));
            FileInfo[] files = d.GetFiles("*.bin");
            if (files.Count() > 0)
            {
                foreach(var file in files)
                {
                    byte[] bytes = File.ReadAllBytes(file.FullName);
                    byteCodes.Add(System.Text.Encoding.UTF8.GetString(bytes));
                }
            }
            return byteCodes;

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
