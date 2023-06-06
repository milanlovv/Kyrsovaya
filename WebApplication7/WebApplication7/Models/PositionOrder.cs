using System;
using System.Collections.Generic;

namespace WebApplication7.Models;

public partial class PositionOrder
{
    public int PositionId { get; set; }

    public int OrderId { get; set; }

    public int Amount { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Position Position { get; set; } = null!;
}
