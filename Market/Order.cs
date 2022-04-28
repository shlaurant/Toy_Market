using System;

namespace Market
{
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
}