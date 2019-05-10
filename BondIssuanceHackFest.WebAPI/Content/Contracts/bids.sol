pragma solidity >=0.4.22 <0.6.0;

contract Bids {
    int bond_id;
    int lots_to_purchase;
    int bid_id;
    
    constructor(int b_id, int bi_id, int lots_purchase) public {
        bond_id = b_id;
        lots_to_purchase = lots_purchase;
        bid_id = bi_id;
    }
}