pragma solidity >=0.4.22 <0.6.0;

// hash for legal docs in bonds
contract BondAllocation {
    struct Bids{
        address addr;
        address bond_addr;
        uint min_price;
        uint max_price;
        uint lots_bid;
    }
    // bids  per investor
    mapping(address => Bids[]) private bids_mapping;

    // totals per address
    mapping(address => uint) private total_bids;
    address[] bids_address_list;
    uint investor_bids;
    Bids current_bid;
    // Investor A Address
    address[] investors;
    /**
    * @dev Contract constructor
    */
    constructor(address[] memory _investors) public{
            investors = _investors;
    }

  /*
   * Public functions
   */

  function bid(address bid_id, address bond_id, uint bid_min_price, uint bid_max_price, uint lots_bid) public {
    for (uint i = 0 ; i < investors.length ; i++) {
        if(msg.sender == investors[i]) { // Only allow the registered investors to bid
            current_bid = Bids(bid_id, bond_id, bid_min_price, bid_max_price, lots_bid);
            investor_bids = total_bids[msg.sender];
            if(investor_bids > 0){
                // loop and find if this bid exists,
                // if exists update else add new. If added new also update the counter
                int found = 0;
                for (uint j = 0 ; j < investor_bids ; j++){
                    if (bids_mapping[msg.sender][j].addr==bid_id){
                        //bid exists
                        bids_mapping[msg.sender][j] = current_bid;
                        found = 1;
                        break;
                    }
                    if (found == 0){
                        bids_mapping[msg.sender].push(current_bid);
                        total_bids[msg.sender] += 1;
                    }
                }
            }
            else {
                // append to the list
                // increment counter
                bids_mapping[msg.sender].push(current_bid);
                total_bids[msg.sender] += 1;
            }
        }
    }
}
  /**
  * @dev Get total bids for option
  */
    function get_total_investor_bids(address _investor) view public returns (uint total) {
        return total_bids[_investor];
    }
    function get_total_bids() view public returns (uint total) {
        return bids_address_list.length;
    }
}
//     require(msg.sender == investorA || msg.sender == investorB || msg.sender == investorC,"No Valid Investors");
//     // Only allow voting once
//     require(votes[msg.sender] == 0);
//     // Only allow 1 or 2 as options
//     require(_option == 1 || _option == 2);

//     // save vote
//     votes[msg.sender] = _option;

//     // update totals
//     totals[_option] = totals[_option] + 1;
//   }

// }