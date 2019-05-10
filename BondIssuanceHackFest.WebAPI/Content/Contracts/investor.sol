pragma solidity >=0.4.22 <0.6.0;

import {Wallets as Wallets} from "./wallets.sol";

interface Investor {
    function get_investor_type() external view returns (string memory invs_type);
}

contract I is Investor {
    address investor;
    Wallets wallet;
    string public investor_type;
    
    constructor(string memory invs_type) public {
        investor = msg.sender;
        investor_type = invs_type;
    }
    
    function get_investor_type() public view returns (string memory invs_type) {
        return investor_type;
    }
    //make bid 
}
