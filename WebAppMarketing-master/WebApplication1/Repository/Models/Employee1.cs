using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository.Models;

[Table("Employees")]
public partial class Employee1
{
    [Key]
    [Column("EmployeeID")]
    public int EmployeeId { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string EmployeeLogin { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string EmployeePassword { get; set; }

    [Required]
    [Column("EmployeeFIO")]
    [StringLength(50)]
    [Unicode(false)]
    public string EmployeeFio { get; set; }
}
