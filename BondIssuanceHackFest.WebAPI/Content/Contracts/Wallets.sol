pragma solidity >=0.4.22 <0.6.0;
contract Wallets {
    address investor;
    mapping (address => uint) balances;
    
    constructor() public {
        investor = msg.sender;
    }
    
    function add_coupon(address owner, uint coupon_amt) public {
        balances[owner] += coupon_amt; // anyone can add coupons to my wallets
    }
    function add_balance(address owner, uint bal) public {
        if (msg.sender != investor) return; // only i can addd palance myself, other pay me
        balances[owner] += bal;
    }
    function pay(address receiver, uint amount) public {
        if(balances[msg.sender] < amount) return;
        balances[msg.sender] -= amount;
        balances[receiver] += amount;
    }
    
    function query_balance(address addr) public view returns (uint balance) {
        return balances[addr];
    }
}