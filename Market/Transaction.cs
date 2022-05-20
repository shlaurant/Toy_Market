namespace Market
{
    public class Transaction
    {
        public readonly Order Other;
        public readonly int StrikePrice;
        public readonly int Amount;

        public Transaction(Order other, int strikePrice, int amount)
        {
            Other = other;
            StrikePrice = strikePrice;
            Amount = amount;
        }
    }
}