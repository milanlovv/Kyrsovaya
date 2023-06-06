using System;
using System.Collections.Generic;

namespace WebApplication7.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string PhoneNumber { get; set; } = null!;
}
