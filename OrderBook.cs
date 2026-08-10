using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace OrderBookEngine;

// This class has two main tasks:
// 1. manage: Hold open orders ordered in storage
// 2. match: If a new order is coming, immediately check if a deal is possible
internal class OrderBook
{
    public SortedDictionary<decimal, List<Order>> Bids { get; set; }
    public SortedDictionary<decimal, List<Order>> Asks { get; set; }
    public List<Trade> Trades { get; set; }

    public OrderBook()
    {
        // Sort Bids descending (highest price first)
        Bids = new SortedDictionary<decimal, List<Order>>(Comparer<decimal>.Create((a, b) => b.CompareTo(a)));

        // Sort Asks ascending (lowest price first)
        Asks = new SortedDictionary<decimal, List<Order>>();

        Trades = new List<Trade>();
    }

    public TradeResult AddOrder(Order newOrder)
    {
        var executedTrades = new List<Trade>();

        if (newOrder.CurrentQuantity > 0)
        {
            // Try to execute the order immediately
            executedTrades = MatchOrder(newOrder);

            // Put remaining quantity into the order book
            if (newOrder.CurrentQuantity > 0)
            {
                InsertIntoBook(newOrder);
            }
        }

        TradeResult result = new TradeResult();
        result.Order = newOrder;
        result.ExecutedTrades = executedTrades;

        return result;
    }

    private List<Trade> MatchOrder(Order incomingOrder)
    {
        var possibleTrades = new List<Trade>();
        SortedDictionary<decimal, List<Order>> oppositeBook;

        // Find the correct book for the matching process
        if (incomingOrder.Side == OrderSide.Buy)
        {
            oppositeBook = Asks;
        }
        else
        {
            oppositeBook = Bids;
        }

        while (incomingOrder.CurrentQuantity > 0 && oppositeBook.Count > 0)
        {
            var bestPriceLevel = oppositeBook.First();
            decimal bestPrice = bestPriceLevel.Key;
            List<Order> ordersAtPrice = bestPriceLevel.Value;

            // Check if the price is good enough for a trade
            bool isPriceMatching = false;

            if (incomingOrder.Side == OrderSide.Buy && incomingOrder.Price >= bestPrice)
            {
                isPriceMatching = true;
            }
            if (incomingOrder.Side == OrderSide.Sell && incomingOrder.Price <= bestPrice)
            {
                isPriceMatching = true;
            }

            if (isPriceMatching == false)
            {
                break;
            }

            // Loop through all orders at this specific price level
            for (int i = 0; i < ordersAtPrice.Count; i++)
            {
                Order restingOrder = ordersAtPrice[i];

                int tradeQuantity = Math.Min(incomingOrder.CurrentQuantity, restingOrder.CurrentQuantity);

                incomingOrder.Fill(tradeQuantity);
                restingOrder.Fill(tradeQuantity);

                Trade newTrade = new Trade();
                newTrade.TradeId = Trades.Count + 1;
                newTrade.Price = bestPrice;
                newTrade.Quantity = tradeQuantity;
                newTrade.TimeStamp = DateTime.UtcNow;

                if (incomingOrder.Side == OrderSide.Buy)
                {
                    newTrade.BuyOrderId = incomingOrder.Id;
                    newTrade.SellOrderId = restingOrder.Id;
                }
                else
                {
                    newTrade.BuyOrderId = restingOrder.Id;
                    newTrade.SellOrderId = incomingOrder.Id;
                }

                possibleTrades.Add(newTrade);
                Trades.Add(newTrade);

                if (restingOrder.CurrentQuantity == 0)
                {
                    ordersAtPrice.RemoveAt(i);
                    i--; // Adjust index because list size decreased
                }

                if (incomingOrder.CurrentQuantity == 0)
                {
                    break;
                }
            }

            // Clean up empty price levels
            if (ordersAtPrice.Count == 0)
            {
                oppositeBook.Remove(bestPrice);
            }
        }

        return possibleTrades;
    }

    private void InsertIntoBook(Order order)
    {
        SortedDictionary<decimal, List<Order>> targetBook;

        if (order.Side == OrderSide.Buy)
        {
            targetBook = Bids;
        }
        else
        {
            targetBook = Asks;
        }

        if (targetBook.ContainsKey(order.Price) == false)
        {
            targetBook[order.Price] = new List<Order>();
        }

        targetBook[order.Price].Add(order);
    }
}