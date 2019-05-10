pragma solidity >=0.4.22 <0.6.0;

import {Investor as Investor} from "./investor.sol";

contract Bond {
    int bond_id;
    string name;
    uint min_price;
    uint max_price;
    uint coupon_freq;
    int stage;
    uint lot_size;
    uint lots_to_allocate;
    Investor[50] investors;
    uint investors_cnt = 0;
    string currency;
    uint tenure;
    //bids;
    // allocations

    constructor(int ins_id, string memory ins_name, uint ins_min_price,
    uint ins_max_price, uint ins_coupon_freq, uint l_size, uint l_to_allocate,
    string memory curr, uint tenure) public {
        bond_id = ins_id;
        name = ins_name;
        min_price = ins_min_price;
        max_price = ins_max_price;
        coupon_freq = ins_coupon_freq;
        lot_size = l_size;
        lots_to_allocate = l_to_allocate;
        currency = curr;
        stage = 0; // -1: no longer tradeable , 0: not yet traded, 1 : in market
    }
    function price_change (uint min_price_new, uint max_price_new) public {
        // uptic or lowtic
        min_price = min_price_new;
        max_price = max_price_new;
        // remove the previous bids list
    }
    function update_lots_to_allocate (uint l_to_allocate) public {
        lots_to_allocate = l_to_allocate;
    }
    function add_investor (Investor investor) public {
        investors[investors_cnt] = investor;
		// max limit 50
        investors_cnt += 1;
    }
    function get_investors_cnt() public view returns (uint cnt) {
        return investors_cnt;
    }
}