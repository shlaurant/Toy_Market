﻿using System;
using Market;
using NUnit.Framework;
using NUnit.Framework.Constraints;

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
            var offer = marketFac.AddOrder(Order.OrderType.Offer, 11, 5);
            Assert.True(market.Offers[1].Equals(offer));
        }

        [Test]
        public void MatchBidWithOneOffer()
        {
            marketFac.AddOrder(Order.OrderType.Bid, 11, 5);
            Assert.AreEqual(5, market.Offers[0].AmountLeft);
        }
        
        //offer also resolved
        //2or more offer
        //vice versa
    }
}