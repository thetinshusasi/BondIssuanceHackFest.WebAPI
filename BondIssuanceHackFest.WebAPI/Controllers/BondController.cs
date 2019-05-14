using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Threading.Tasks;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts.CQS;
using Nethereum.Quorum;
using Nethereum.RPC;
using Nethereum.RPC.Eth.DTOs;

namespace BondIssuanceHackFest.WebAPI.Controllers
{
    public class BondController : ApiController
    {
        List<Bid> bids = new List<Bid>();

        public static async Task<Nethereum.RPC.Eth.DTOs.TransactionReceipt> asyncReceiptDeployTokenBidAsync(IContractDeploymentTransactionHandler<BookRunner> deploymentHandler, BookRunner deploymentMessage)
        {
            return await deploymentHandler.SendRequestAndWaitForReceiptAsync(deploymentMessage);
        }
        public static async Task<bool> asyncUnlockAccount(Web3Quorum web3Private, string account)
        {
            return await web3Private.Personal.UnlockAccount.SendRequestAsync(account, "", 30000);

        }

        public IHttpActionResult Post(int userid, int nooflots, int tolerance)
        {
            Bid bid = new Bid() { UserId = userid, NoOfLots = nooflots, Tolerace = tolerance };
            bids.Add(bid);

            var bidTransactionReceipt = asyncReceiptDeployTokenBidAsync(bidDeploymentHandlerOut, bidDeploymentMessageOut).Result;
            var bidContractAddress = transactionReceipt.ContractAddress;


            accountOut = new QuorumAccount(accountAddress[1]);
            web3QuorumOut = new Web3Quorum(accountOut, uri[1]);
            var unlockedOut = asyncUnlockAccount(web3QuorumOut, accountAddress[1]).Result;
            var bidTrasnferHandlerOut = web3QuorumOut.Eth.GetContractTransactionHandler<ExecuteBid>();
            var bidTransfer = new ExecuteBid()
            {
                Bond_Id = "0xaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                Bid_Min_Price = 970,
                Bid_Max_Price = 980,
                Lots_Bid = 1

            };
            bidTransfer.Gas = 0;

            var bidTransactionReceipt1 = asyncExecuteBid(bidContractAddress, bidTransfer, bidTrasnferHandlerOut).Result;
            var bidTrasanctionHas3 = transactionReceipt2.TransactionHash;


            accountOut = new QuorumAccount(accountAddress[2]);
            web3QuorumOut = new Web3Quorum(accountOut, uri[2]);
            unlockedOut = asyncUnlockAccount(web3QuorumOut, accountAddress[2]).Result;
            bidTrasnferHandlerOut = web3QuorumOut.Eth.GetContractTransactionHandler<ExecuteBid>();
            bidTransfer = new ExecuteBid()
            {
                Bond_Id = "0xaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                Bid_Min_Price = 970,
                Bid_Max_Price = 980,
                Lots_Bid = 10

            };
            bidTransfer.Gas = 0;
            bidTransactionReceipt1 = asyncExecuteBid(bidContractAddress, bidTransfer, bidTrasnferHandlerOut).Result;
            bidTrasanctionHas3 = transactionReceipt2.TransactionHash;

            accountOut = new QuorumAccount(accountAddress[1]);
            web3QuorumOut = new Web3Quorum(accountOut, uri[1]);
            unlockedOut = asyncUnlockAccount(web3QuorumOut, accountAddress[1]).Result;

            var bidValueFromNodeHandlerOut = web3QuorumOut.Eth.GetContractQueryHandler<BidValueForNodeFunction>();

            var bidOfnvFunctionMessage = new BidValueForNodeFunction()
            {
                InvestoAddress = accountAddress[1]
            };



            return Ok();
        }

        public IHttpActionResult Put(int baseprice, int tolerance)
        {
            Bond obj = new Bond();
            obj.BasePrice = baseprice ;
            obj.Tolerance = tolerance;

            return Ok();
        }

        public int Get(int Userid)
        {
           return  bids.Count(x => x.UserId == Userid);
        }

        public IHttpActionResult Delete(int Userid)
        {
            Bid bid= bids.SingleOrDefault(x => x.UserId == Userid);
            if (bid != null)
            {
                bids.Remove(bid);
                return Ok();
            }
            return NotFound();
        }
    }


    public class Bid
    {
        public int UserId { get; set; }

        public int NoOfLots { get; set; }

        public int Tolerace { get; set; }

        public int BasePrice { get; set; }

    }

    public class Bond
    {
        public int BasePrice { get; set; }

        public  int Tolerance { get; set; }
    }
}