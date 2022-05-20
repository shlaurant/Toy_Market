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

        public int AmountLeft => Amount - transactions.Sum(trs => trs.Amount);

        private readonly List<Transaction> transactions = new();

        public Order(Good good, int price, int amount, OrderType type)
        {
            Good = good;
            Price = price;
            Amount = amount;
            Type = type;
        }

        public static Transaction Match(Order first, Order second)
        {
            if (first.Type.Equals(second.Type))
            {
                throw new ArgumentException(
                    "Transaction cannot be made with orders of same type");
            }

            var (bid, offer) = SeparateType(first, second);
            var result = new Transaction(bid, offer, first.Price,
                Math.Min(first.AmountLeft, second.AmountLeft));

            first.transactions.Add(result);
            second.transactions.Add(result);
            
            return result;
        }

        private static (Order bid, Order offer) SeparateType(Order first, Order second)
        {
            Order bid;
            Order offer;

            if (first.Type.Equals(OrderType.Bid))
            {
                bid = first;
                offer = second;
            }
            else
            {
                bid = second;
                offer = first;
            }

            return (bid, offer);
        }

        public override string ToString()
        {
            return
                $"{nameof(Good)}: {Good}, {nameof(Price)}: {Price}, {nameof(Amount)}: {Amount}, {nameof(Type)}: {Type}";
        }
    }
}