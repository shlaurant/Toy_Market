using Market;

namespace Tests.GoodMarket
{
    public class TestMarketFac
    {
        private readonly Good good;
        private Market.GoodMarket market;

        public Market.GoodMarket Market => market;

        public TestMarketFac(string goodName)
        {
            good = new Good(goodName);
            Reset();
        }

        public void Reset()
        {
            market = new Market.GoodMarket(good);
        }

        public void AddOrder(Order.OrderType type, int price, int amount)
        {
            market.TakeOrder(new Order(good, price, amount, type, null));
        }
    }
}