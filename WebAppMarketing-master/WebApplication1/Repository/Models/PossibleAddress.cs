using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository.Models;

[Table("PossibleAddress")]
public partial class PossibleAddress
{
    [Key]
    [Column("AddressID")]
    public int AddressId { get; set; }

    [Required]
    [StringLength(100)]
    public string Location { get; set; }
}
