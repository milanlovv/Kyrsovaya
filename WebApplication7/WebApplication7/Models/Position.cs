using System;
using System.Collections.Generic;

namespace WebApplication7.Models;

public partial class Position
{
    public int PositionId { get; set; }

    public byte[] Image { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public bool IsHidden { get; set; }

    public virtual ICollection<PositionOrder> PositionOrders { get; set; } = new List<PositionOrder>();
}
