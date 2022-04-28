using Market;
using NUnit.Framework;

namespace Tests.GoodMarket
{
    public class WhenThereCanBeMatch
    {
        private TestMarketFac marketFac = new TestMarketFac("oil");
        private Market.GoodMarket market;

        [SetUp]
        public void Setup()
        {
            marketFac.Reset();

            marketFac.AddOrder(Order.OrderType.Bid, 10, 10);
            marketFac.AddOrder(Order.OrderType.Bid, 9, 10);
            marketFac.AddOrder(Order.OrderType.Bid, 8, 10);

            marketFac.AddOrder(Order.OrderType.Offer, 11, 10);
            marketFac.AddOrder(Order.OrderType.Offer, 12, 10);
            marketFac.AddOrder(Order.OrderType.Offer, 13, 10);

            market = marketFac.Market;
        }

        [Test]
        public void Construct()
        {
            Assert.Pass($"{market}");
        }

        [Test]
        public void AddBidToBookWithNoMatch()
        {
            var bid = marketFac.AddOrder(Order.OrderType.Bid, 10, 5);
            Assert.True(market.Bids[1].Equals(bid));
        }

        [Test]
        public void AddOfferToBookWithNoMatch()
        {
            Assert.Fail();
        }
    }
}