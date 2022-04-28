using Market;
using NUnit.Framework;

namespace Tests
{
    public class GoodMarketTest
    {
        private GoodMarket testMarket;
        private Good good = new Good("Oil");
        private Order bid0;

        [SetUp]
        public void Setup()
        {
            bid0 = new Order(good, 10, 10, Order.OrderType.Bid, null);
            testMarket = new GoodMarket(good);
        }

        [Test]
        public void Construct()
        {
        }

        [Test]
        public void AddFirstBid()
        {
            testMarket.TakeOrder(bid0);
            Assert.True(testMarket.Bids[0].Equals(bid0));
        }
    }
}