using System;
using System.Collections.Generic;

namespace WebApplication7.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public decimal Sum { get; set; }

    public int ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<PositionOrder> PositionOrders { get; set; } = new List<PositionOrder>();
}
