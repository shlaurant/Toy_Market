using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Market
{
    public class GoodMarket
    {
        private readonly Good good;
        private readonly LinkedList<Order> bids = new();
        private readonly LinkedList<Order> offers = new();
        private int currentPrice;

        public ImmutableList<Order> Bids => bids.ToImmutableList();
        public ImmutableList<Order> Offers => offers.ToImmutableList();

        public GoodMarket(Good good)
        {
            this.good = good;
        }

        public void TakeOrder(Order order)
        {
            if (!order.Good.Equals(good))
            {
                throw new ArgumentException(
                    $"Received an order of {order.Good} while this is {good} market");
            }

            if (order.Type.Equals(Order.OrderType.Bid))
            {
                AddOrderToBook(order, bids, IsHigher);
            }
            else
            {
                AddOrderToBook(order, offers, IsLower);
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
    }
}