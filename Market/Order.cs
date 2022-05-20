using System;
using System.Collections.Generic;
using System.Linq;

namespace Market
{
    public class Order
    {
        public enum OrderType
        {
            Bid,
            Offer
        }

        public readonly Good Good;
        public readonly int Price;
        public readonly int Amount;
        public readonly OrderType Type;
        public readonly ITrader trader;

        public int AmountLeft => Amount - transactions.Sum(trs => trs.Amount);

        private readonly List<Transaction> transactions = new List<Transaction>();

        public Order(Good good, int price, int amount, OrderType type,
            ITrader trader)
        {
            Good = good;
            Price = price;
            Amount = amount;
            this.trader = trader;
            Type = type;
        }

        public static void Match(Order first, Order second)
        {
            var tradedAmount = Math.Min(first.AmountLeft, second.AmountLeft);
            first.AddTransaction(second, first.Price, tradedAmount);
            second.AddTransaction(first, first.Price, tradedAmount);
        }

        private void AddTransaction(Order other, int strikePrice, int amount)
        {
            transactions.Add(new Transaction(other, strikePrice, amount));
        }

        public override string ToString()
        {
            return
                $"{nameof(Good)}: {Good}, {nameof(Price)}: {Price}, {nameof(Amount)}: {Amount}, {nameof(Type)}: {Type}";
        }
    }
}