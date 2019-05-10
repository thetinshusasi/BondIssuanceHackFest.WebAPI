pragma solidity >=0.4.22 <0.6.0;
contract Models {
    struct Wallet{
        address addr;
        uint balance;
    }
    struct Bond {
        address addr;
        string name;
        uint min_price;
        uint max_price;
        uint coupon_freq;
        int stage;
        uint lot_size;
        uint lots_to_allocate;
        Investor[1000] investors;
        uint investors_cnt;
        string currency;
        uint tenure;
    }
    struct Bid {
        address addr;
        uint bid_price;
        address bond_id;
    }
    struct Investor {
        address addr;
        string name;
        string investor_type;
        Wallet wallet;
        Bid[1000] bids;
    }
    struct BookRunner {
        address addr;
    }
}
