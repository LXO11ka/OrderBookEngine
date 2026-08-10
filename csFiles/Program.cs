namespace OrderBookEngine;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== ORDER BOOK ENGINE TEST ===");
        Console.WriteLine();

        OrderBook book = new OrderBook();

        // 1. Add sell orders (Asks)
        Order sell1 = new Order(1, OrderSide.Sell, 10.00m, 100);
        Order sell2 = new Order(2, OrderSide.Sell, 10.50m, 50);

        Console.WriteLine("Adding Sell Order 1: 100 shares @ 10.00 EUR");
        book.AddOrder(sell1);

        Console.WriteLine("Adding Sell Order 2: 50 shares @ 10.50 EUR");
        book.AddOrder(sell2);

        // Display book state before any trades
        OrderBookPrinter.PrintBook(book);

        // 2. Add buy order (Bid) that triggers a match
        Console.WriteLine("--- Incoming Buy Order ---");
        Order buy1 = new Order(3, OrderSide.Buy, 10.20m, 120);

        Console.WriteLine("Adding Buy Order: 120 shares @ 10.20 EUR...");


        TradeResult result = book.AddOrder(buy1);


        // Display execution result
        Console.WriteLine();
        Console.WriteLine("Trades executed in this step: " + result.ExecutedTrades.Count);
        Console.WriteLine("Buy Order Status: " + buy1.Status);
        Console.WriteLine("Remaining Buy Quantity: " + buy1.CurrentQuantity);

 


        // Display final order book state and trade history
        OrderBookPrinter.PrintBook(book);
        OrderBookPrinter.PrintTrades(book.Trades);
    }
}

public enum OrderSide
{
    Buy,
    Sell
}

public enum OrderStatus
{
    New,
    PartiallyFilled,
    Filled,
    Cancelled
}
