using System;
using System.Collections.Generic;

namespace WebApplication7.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? DateOfBirth { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
