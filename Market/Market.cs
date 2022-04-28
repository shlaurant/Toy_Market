﻿using System;
using System.Collections.Generic;

namespace Market
{
    public class Market
    {
        private Dictionary<Good, List<Order>> buyOrders;
        private Dictionary<Good, List<Order>> sellOrders;

        public Market()
        {
            buyOrders = new Dictionary<Good, List<Order>>();
            sellOrders = new Dictionary<Good, List<Order>>();
        }

        public void OrderBuy(Order order)
        {
            AddOrder(buyOrders, order);
        }

        public void OrderSell(Order order)
        {
            AddOrder(sellOrders, order);
        }

        public void Trade()
        {
            var buyKeys = buyOrders.Keys;
            var sellKeys = sellOrders.Keys;
        }

        private void AddOrder(Dictionary<Good, List<Order>> dic, Order order)
        {
            if (dic.ContainsKey(order.Good))
            {
                dic[order.Good].Add(order);
            }
            else
            {
                dic.Add(order.Good, new List<Order> {order});
            }
        }

        private class GoodMarket
        {
            private readonly Good good;
            private readonly LinkedList<Order> bids = new();
            private readonly LinkedList<Order> offers = new();
            private int currentPrice;

            public void Bid(Order bid)
            {
                ResolveBid(bid);
                RemoveResolvedOffers();
                if (!bid.IsResolved())
                {
                    AddBidToBook(bid);
                }
            }

            public void Offer(Order offer)
            {
                var curNode = bids.First;
                while (curNode != null && offer.Price <= curNode.Value.Price)
                {
                    offer.Offer(curNode.Value);
                    currentPrice = curNode.Value.Price;
                    if (offer.IsResolved())
                    {
                        break;
                    }
                    else
                    {
                        curNode = curNode.Next;
                    }
                }
            }

            private void AddBidToBook(Order bid)
            {
                var curNode = bids.First;
                while (curNode != null)
                {
                    if (bid.Price > curNode.Value.Price)
                    {
                        bids.AddBefore(curNode, bid);
                        break;
                    }
                    else
                    {
                        curNode = curNode.Next;
                    }
                }

                if (curNode == null)
                {
                    bids.AddLast(bid);
                }
            }

            private void RemoveResolvedOffers()
            {
                while (offers.First != null && offers.First.Value.IsResolved())
                {
                    offers.RemoveFirst();
                }
            }

            private void ResolveBid(Order bid)
            {
                var curNode = offers.First;
                while (curNode != null && bid.Price >= curNode.Value.Price)
                {
                    bid.Bid(curNode.Value);
                    currentPrice = curNode.Value.Price;
                    if (bid.IsResolved())
                    {
                        break;
                    }
                    else
                    {
                        curNode = curNode.Next;
                    }
                }
            }
        }
    }

    public class Order
    {
        public readonly Good Good;
        public readonly int Price;
        public readonly int Amount;
        private readonly ITrader trader;

        private int amountLeft;

        public Order(Good good, int price, int amount, ITrader trader)
        {
            Good = good;
            Price = price;
            Amount = amount;
            this.trader = trader;
            amountLeft = Amount;
        }

        /// <summary>
        /// When the bid is made after an offer. Matched at a offer price
        /// </summary>
        public void Bid(Order offer)
        {
            if (offer.Price > Price)
            {
                throw new ArgumentException(
                    $"Bid price should be higher than offer price. Current bid: {Price}, offer: {offer.Price}");
            }

            var amountTraded = AmountTraded(offer);
            amountLeft -= amountTraded;
            offer.amountLeft -= amountTraded;
            trader.OnBidMatched(this, offer.Price, amountTraded);
            offer.trader.OnOfferMatched(offer, offer.Price, amountTraded);
        }

        /// <summary>
        /// When the offer is made after an bid. Matched at a bid price
        /// </summary>
        public void Offer(Order bid)
        {
            if (Price > bid.Price)
            {
                throw new ArgumentException(
                    $"Bid price should be higher than offer price. Current bid: {bid.Price}, offer: {Price}");
            }

            var amountTraded = AmountTraded(bid);
            amountLeft -= amountTraded;
            bid.amountLeft -= amountTraded;
            trader.OnOfferMatched(this, bid.Price, amountTraded);
            bid.trader.OnBidMatched(this, bid.Price, amountTraded);
        }

        public bool IsResolved()
        {
            return amountLeft <= 0;
        }

        private int AmountTraded(Order other)
        {
            return Math.Min(amountLeft, other.amountLeft);
        }
    }

    public interface ITrader
    {
        void OnBidMatched(Order order, int price, int amount);
        void OnOfferMatched(Order offer, int price, int amount);
    }
}