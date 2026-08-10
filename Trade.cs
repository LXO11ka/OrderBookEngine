using System;
using System.Collections.Generic;
using System.Text;

namespace OrderBookEngine;

internal class Trade
{
    public int TradeId { get; set; }
    public int BuyOrderId { get; set; }

    public int SellOrderId { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public DateTime TimeStamp { get; set; }
}
