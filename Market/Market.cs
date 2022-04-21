using System;

namespace Market
{
    public class Market
    {
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