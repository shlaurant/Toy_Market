using System;
using Market;
using NUnit.Framework;

namespace Tests
{
    public class WhenThereIsNoMatch
    {
        private GoodMarket testMarket;
        private Good good = new Good("Oil");
        private Good wrongGood = new Good("Cotton");
        private Order bid0;
        private Order bid1;
        private Order bid2;
        private Order bid3;
        private Order wrongBid;

        [SetUp]
        public void Setup()
        {
            bid0 = new Order(good, 10, 10, Order.OrderType.Bid, null);
            bid1 = new Order(good, 11, 10, Order.OrderType.Bid, null);
            bid2 = new Order(good, 8, 10, Order.OrderType.Bid, null);
            bid3 = new Order(good, 10, 10, Order.OrderType.Bid, null);
            wrongBid = new Order(wrongGood, 10, 10, Order.OrderType.Bid, null);
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

        [Test]
        public void RejectBidWithWrongGood()
        {
            Assert.Throws<ArgumentException>(delegate
            {
                testMarket.TakeOrder(wrongBid);
            });
        }

        [Test]
        public void AddHighestBid()
        {
            testMarket.TakeOrder(bid0);
            testMarket.TakeOrder(bid1);
            Assert.True(testMarket.Bids[0].Equals(bid1));
        }

        [Test]
        public void AddLowestBid()
        {
            testMarket.TakeOrder(bid0);
            testMarket.TakeOrder(bid2);
            Assert.True(testMarket.Bids[1].Equals(bid2));
        }

        [Test]
        public void AddMiddleBid()
        {
            testMarket.TakeOrder(bid1);
            testMarket.TakeOrder(bid2);
            testMarket.TakeOrder(bid0);
            Assert.True(testMarket.Bids[1].Equals(bid0));
        }

        [Test]
        public void AddWithFifo()
        {
            testMarket.TakeOrder(bid1);
            testMarket.TakeOrder(bid2);
            testMarket.TakeOrder(bid0);
            testMarket.TakeOrder(bid3);
            Assert.True(testMarket.Bids[2].Equals(bid3));
        }
    }
}