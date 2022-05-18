﻿using System.Collections.Generic;
using Market;
using NUnit.Framework;

namespace Tests.GoodMarket
{
    public class ForBookWithOrders
    {
        protected TestMarketFac marketFac = new TestMarketFac("oil");
        protected Market.GoodMarket market;

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

        public class DefaultTests : ForBookWithOrders
        {
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
                var offer = marketFac.AddOrder(Order.OrderType.Offer, 11, 5);
                Assert.True(market.Offers[1].Equals(offer));
            }
        }

        public class WhenImmediateBidResolve : ForBookWithOrders
        {
            private Order bid;

            [SetUp]
            public new void Setup()
            {
                base.Setup();
                bid = marketFac.AddOrder(Order.OrderType.Bid, 11, 5);
            }

            [Test]
            public void MatchBidWithOneOffer()
            {
                Assert.AreEqual(5, market.Offers[0].AmountLeft);
            }

            [Test]
            public void MatchBidAndResolve()
            {
                Assert.AreNotEqual(bid, market.Bids[0]);
            }

            [Test]
            public void ChangeCurrentPrice()
            {
                Assert.AreEqual(market.Offers[0].Price, market.CurrentPrice);
            }
        }

        public class WhenBidAndOfferFullyMatched : ForBookWithOrders
        {
            private Order bid;
            private Order firstOffer;

            [SetUp]
            public new void Setup()
            {
                base.Setup();
                firstOffer = market.Offers[0];
                bid = marketFac.AddOrder(Order.OrderType.Bid, 11, 10);
            }

            [Test]
            public void RemoveFullMatchedOffer()
            {
                Assert.AreNotEqual(firstOffer, market.Offers[0]);
            }

            [Test]
            public void DoNotAddBidToBook()
            {
                Assert.AreNotEqual(bid, market.Bids[0]);
            }
        }

        public class WhenBidLeft : ForBookWithOrders
        {
            private Order bid;
            private Order secondOffer;

            [SetUp]
            public new void Setup()
            {
                base.Setup();
                secondOffer = market.Offers[1];
                bid = marketFac.AddOrder(Order.OrderType.Bid, 11, 20);
            }

            [Test]
            public void ResolveFirstOffer()
            {
                Assert.AreEqual(secondOffer, market.Offers[0]);
            }
        }

        // public class WhenBidMatchesSeveralOffers : ForBookWithOrders
        // {
        //     private Order bid;
        //     private List<Order> originalOffers;
        //
        //     [SetUp]
        //     public new void Setup()
        //     {
        //         base.Setup();
        //         
        //     }
        // }
        //2or more offer
        //vice versa
    }
}