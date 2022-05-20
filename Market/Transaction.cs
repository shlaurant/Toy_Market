namespace Market
{
    public class Transaction
    {
        public readonly Order Bid;
        public readonly Order Offer;
        public readonly int StrikePrice;
        public readonly int Amount;

        public Transaction(Order bid, Order offer, int strikePrice, int amount)
        {
            Bid = bid;
            Offer = offer;
            StrikePrice = strikePrice;
            Amount = amount;
        }

        public override string ToString()
        {
            return
                $"{nameof(Bid)}: {Bid}, {nameof(Offer)}: {Offer}, {nameof(StrikePrice)}: {StrikePrice}, {nameof(Amount)}: {Amount}";
        }
    }
}