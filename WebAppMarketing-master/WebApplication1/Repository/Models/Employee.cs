﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository.Models;

[Table("Employee")]
public partial class Employee
{
    [Key]
    [Column("EmployeeID")]
    public int EmployeeId { get; set; }

    [Required]
    [StringLength(20)]
    public string Login { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [StringLength(20)]
    public string LastName { get; set; }

    [Required]
    [StringLength(20)]
    public string FirstName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [StringLength(10)]
    public string PhoneNumber { get; set; }
}
