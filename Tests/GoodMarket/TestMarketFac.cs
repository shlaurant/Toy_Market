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

        public Order AddOrder(Order.OrderType type, int price, int amount)
        {
            var orderAdded = new Order(good, price, amount, type, null);
            market.TakeOrder(orderAdded);
            return orderAdded;
        }
    }
}