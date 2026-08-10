using System;
using System.Collections.Generic;
using System.Text;

namespace OrderBookEngine;

internal class Order
{
    public int Id { get ; init; }

    public OrderSide Side { get; set; }

    public decimal Price { get; set; }

    public int InitialQuantity  { get; set; }

    public int CurrentQuantity { get; set; }

    public DateTime Timestamp { get; set; }

    public OrderStatus Status { get; set; }

    public Order (int id, OrderSide side, decimal price, int quantity)
    {
        Id = id;
        Side = side;
        Price = price;
        InitialQuantity = quantity;
        CurrentQuantity = quantity;
        Timestamp = DateTime.UtcNow;
        Status = OrderStatus.New;
    }

    public void Fill (int quantity)
    {
        CurrentQuantity -= quantity;

        if (CurrentQuantity == 0)
        {
            Status = OrderStatus.Filled;
        }
        else if (CurrentQuantity > 0)
        {
            Status = OrderStatus.PartiallyFilled;
        }
    }
}
