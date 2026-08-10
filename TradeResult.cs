using System;
using System.Collections.Generic;
using System.Text;

namespace OrderBookEngine;

internal class TradeResult
{
    public Order? Order { get; set; }

    public List<Trade> ExecutedTrades { get; set; } = new List<Trade>();

    public bool IsFullyFilled => Order?.Status == OrderStatus.Filled;
}
