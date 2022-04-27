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
    }

    public class Order
    {
        public readonly Good Good;
        public readonly double Price;
        public readonly int Count;

        public Order(Good good, double price, int count)
        {
            Good = good;
            Price = price;
            Count = count;
        }
    }
}