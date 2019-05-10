using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace BondIssuanceHackFest.WebAPI.Controllers
{
    public class BondController : ApiController
    {
        List<Bid> bids = new List<Bid>();

        public IHttpActionResult Post(int userid, int nooflots, int tolerance)
        {
            Bid bid = new Bid() { UserId = userid, NoOfLots = nooflots, Tolerace = tolerance };
            bids.Add(bid);
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