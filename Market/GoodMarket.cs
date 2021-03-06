using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Market
{
    /// <summary>
    /// A market which handles one specific good. Can be used to construct a general market
    /// </summary>
    public class GoodMarket
    {
        private readonly Good good;
        private readonly LinkedList<Order> bids = new();
        private readonly LinkedList<Order> offers = new();

        /// <summary>
        /// Returns a book of bids. High priority first.
        /// </summary>
        public ImmutableList<Order> Bids => bids.ToImmutableList();
        
        /// <summary>
        /// Returns a book of offers. High priority first.
        /// </summary>
        public ImmutableList<Order> Offers => offers.ToImmutableList();
        
        /// <summary>
        /// Returns latest strike price
        /// </summary>
        public int CurrentPrice { get; private set; }

        public GoodMarket(Good good)
        {
            this.good = good;
        }

        /// <summary>
        /// Take an order and try to resolve or book it.
        /// </summary>
        /// <param name="order">An order to take</param>
        /// <exception cref="ArgumentException">When an order has wrong good info.</exception>
        /// <exception cref="ArgumentOutOfRangeException">When a type of an order is not recognizable.</exception>
        public void TakeOrder(Order order)
        {
            if (!order.Good.Equals(good))
            {
                throw new ArgumentException(
                    $"Received an order of {order.Good} while this is {good} market");
            }

            switch (order.Type)
            {
                case Order.OrderType.Bid:
                {
                    HandleBid(order);
                    break;
                }
                case Order.OrderType.Offer:
                {
                    HandleOffer(order);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(order.Type.ToString(), $"No operation exist for {order.Type}");
            }
        }

        private void HandleOffer(Order order)
        {
            TryMatchOrder(order, bids, IsLower);
            RemoveResolvedOrders(bids);
            if (order.AmountLeft > 0)
            {
                AddOrderToBook(order, offers, IsLower);
            }
        }

        private void HandleBid(Order order)
        {
            TryMatchOrder(order, offers, IsHigher);
            RemoveResolvedOrders(offers);
            if (order.AmountLeft > 0)
            {
                AddOrderToBook(order, bids, IsHigher);
            }
        }

        private void RemoveResolvedOrders(LinkedList<Order> book)
        {
            book.Where(offer => offer.AmountLeft == 0).ToList()
                .ForEach(resolved => book.Remove(resolved));
        }

        private void TryMatchOrder(Order order, LinkedList<Order> book, Predicate<(Order,Order)> predicate)
        {
            var curNode = book.First;
            while (curNode != null && order.AmountLeft > 0)
            {
                if (predicate.Invoke((curNode.Value, order)))
                {
                    break;
                }

                Order.Match(curNode.Value, order);
                CurrentPrice = curNode.Value.Price;
                curNode = curNode.Next;
            }
        }

        private void AddOrderToBook(Order order, LinkedList<Order> book,
            Predicate<(Order, Order)> predicate)
        {
            var curNode = book.First;
            while (curNode != null)
            {
                if (predicate.Invoke((order, curNode.Value)))
                {
                    book.AddBefore(curNode, order);
                    break;
                }
                else
                {
                    curNode = curNode.Next;
                }
            }

            if (curNode == null)
            {
                book.AddLast(order);
            }
        }

        private bool IsHigher((Order, Order) orders)
        {
            return orders.Item1.Price > orders.Item2.Price;
        }

        private bool IsLower((Order, Order) orders)
        {
            return orders.Item1.Price < orders.Item2.Price;
        }

        private string StringFormOf(LinkedList<Order> orders)
        {
            var result = new StringBuilder();
            var node = orders.First;
            while (node != null)
            {
                result.AppendLine(node.Value.ToString());
                node = node.Next;
            }

            return result.ToString();
        }

        public override string ToString()
        {
            return
                $"{nameof(good)}: {good},\n\n{nameof(bids)}:\n{StringFormOf(bids)}\n{nameof(offers)}:\n{StringFormOf(offers)}\n{nameof(CurrentPrice)}: {CurrentPrice}";
        }
    }
}