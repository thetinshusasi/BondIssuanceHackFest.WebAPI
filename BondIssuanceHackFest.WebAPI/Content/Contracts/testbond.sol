pragma solidity >=0.4.22 <0.6.0;
contract BondTest {
    address investor;
    mapping (address => uint) bids;
    constructor() public {
        investor = msg.sender;
        bids[investor] = 0;
    }
    function set_bid(address addr) public {
        if(bids[addr] != 0 && bids[addr] >= 0){
            bids[addr] += 1;  //msg.sender always gives the address of the one where genesis block was created
        }
        else{
            bids[addr] = 1;
        }
    }
    function get_bid(address addr) public view returns (uint) {
        return bids[addr];
    }
}