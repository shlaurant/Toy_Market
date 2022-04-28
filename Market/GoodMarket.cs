using System.Collections.Generic;

namespace Market
{
    public class GoodMarket
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