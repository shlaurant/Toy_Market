using System.Collections.Generic;

namespace Market
{
    public partial class Market
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
    }

    public interface ITrader
    {
        void OnBidMatched(Order order, int price, int amount);
        void OnOfferMatched(Order offer, int price, int amount);
    }
}