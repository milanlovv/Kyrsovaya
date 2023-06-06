using System;
using System.Collections.Generic;

namespace WebApplication7.Models;

public partial class PossibleAddress
{
    public int AddressId { get; set; }

    public string Location { get; set; } = null!;
}
