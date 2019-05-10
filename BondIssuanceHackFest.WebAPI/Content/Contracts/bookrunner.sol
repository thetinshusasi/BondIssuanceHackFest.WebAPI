pragma solidity >=0.4.22 <0.6.0;
// issue bonds basis types
// get balance of each investor

function allocate(int hfund_cnt, int fm_cnt, int retail_cnt) public view {
        // counts depict how many to allocate depending on the bid, 
        // if bid is for 2 lots and hfund_cnt count is more than double then add more hfunds
        // else just distribute
        for (uint i=0; i<get_investors_cnt(); i++) {
			  //Investor inves =  investors[i];
			  //string memory investor_type = inves.get_investor_type();
			  //logic here
			}
        //loop on investors
        //check their typeo
        // deduct balance
        // add bond to their wallet
    }
function make_bid () public {
        
    }