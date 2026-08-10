using System;
using System.Collections.Generic;

namespace OrderBookEngine;

internal class OrderBookPrinter
{
    public static void PrintBook(OrderBook book)
    {
        Console.WriteLine();
        Console.WriteLine("================ ORDER BOOK ================");
        Console.WriteLine(" Price (€)   | Quantity    | Side");
        Console.WriteLine("--------------------------------------------");

        // Print Asks (Sells)
        foreach (KeyValuePair<decimal, List<Order>> level in book.Asks)
        {
            decimal price = level.Key;
            int totalQuantity = 0;

            foreach (Order order in level.Value)
            {
                totalQuantity = totalQuantity + order.CurrentQuantity;
            }

            Console.WriteLine(" " + price.ToString("F2").PadRight(11) + " | " + totalQuantity.ToString().PadRight(11) + " | ASK (Sell)");
        }

        Console.WriteLine("--------------------------------------------");

        // Print Bids (Buys)
        foreach (KeyValuePair<decimal, List<Order>> level in book.Bids)
        {
            decimal price = level.Key;
            int totalQuantity = 0;

            foreach (Order order in level.Value)
            {
                totalQuantity = totalQuantity + order.CurrentQuantity;
            }

            Console.WriteLine(" " + price.ToString("F2").PadRight(11) + " | " + totalQuantity.ToString().PadRight(11) + " | BID (Buy)");
        }

        Console.WriteLine("============================================");
        Console.WriteLine();
    }

    public static void PrintTrades(List<Trade> trades)
    {
        Console.WriteLine("================ TRADE HISTORY =============");
        Console.WriteLine(" ID | Price (€)   | Quantity    | Timestamp");
        Console.WriteLine("--------------------------------------------");

        foreach (Trade trade in trades)
        {
            Console.WriteLine(" " + trade.TradeId.ToString().PadRight(2) + " | " + trade.Price.ToString("F2").PadRight(11) + " | " + trade.Quantity.ToString().PadRight(11) + " | " + trade.TimeStamp.ToString("HH:mm:ss"));
        }

        Console.WriteLine("============================================");
        Console.WriteLine();
    }
}
