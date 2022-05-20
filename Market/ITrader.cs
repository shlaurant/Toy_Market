namespace Market
{
    public interface ITrader
    {
        void OnBidMatched(Order order, int price, int amount);
        void OnOfferMatched(Order offer, int price, int amount);
    }
}