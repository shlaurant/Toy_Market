using System;
using System.Collections.Generic;

namespace Market
{
    public class Market
    {
        private Dictionary<Good, double> catalog;

        public void Order(Order order)
        {
            throw new NotImplementedException();
        }

        public void Trade()
        {
            throw new NotImplementedException();
        }
    }

    public class Order
    {
        public enum Type
        {
            Buy,
            Sell
        }
    }
}