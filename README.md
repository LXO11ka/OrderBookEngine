# C# Limit Order Book Engine

A clean and simple implementation of a stock exchange limit order book engine written in C#. 

This project simulates how financial exchanges (like NASDAQ or Binance) match buy and sell orders in real-time using **Price-Time Priority**.

## Features

- **Price-Time Priority Matching:** Orders are matched based on the best price first. If prices are identical, older orders are executed first (FIFO).
- **Partial Fills:** Handles partial order executions (`PartiallyFilled`) and tracks remaining quantities.
- **Order States:** Manages order lifecycles (`New`, `PartiallyFilled`, `Filled`, `Cancelled`).
- **Terminal Visualizer:** Includes an `OrderBookPrinter` module to display live market depth and executed trades in the console.

## Architecture & Data Structures

- **Bids (Buy Orders):** Stored in a `SortedDictionary<decimal, List<Order>>` sorted in **descending** order (highest bid first).
- **Asks (Sell Orders):** Stored in a `SortedDictionary<decimal, List<Order>>` sorted in **ascending** order (lowest ask first).
- **Time Priority:** Each price level holds a `List<Order>` to maintain time priority processing.

## How to Run

1. Open `OrderBookEngine.sln` in **Visual Studio**.
2. Press **F5** or click **Start** to run the console test application.

## Example Output

```text
================ ORDER BOOK ================
 Price (€)   | Quantity    | Side
--------------------------------------------
 10.50       | 50          | ASK (Sell)
--------------------------------------------
 10.20       | 20          | BID (Buy)
============================================

================ TRADE HISTORY =============
 ID | Price (€)   | Quantity    | Timestamp
--------------------------------------------
 1  | 10.00       | 100         | 14:30:15
============================================
