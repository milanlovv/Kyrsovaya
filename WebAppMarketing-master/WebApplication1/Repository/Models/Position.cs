using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository.Models;

[Table("Position")]
public partial class Position
{
    [Key]
    [Column("PositionID")]
    public int PositionId { get; set; }

    [Required]
    public byte[] Image { get; set; }

    [Required]
    [StringLength(30)]
    public string Name { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal Price { get; set; }

    public bool IsHidden { get; set; }

    [InverseProperty("Position")]
    public virtual ICollection<PositionOrder> PositionOrders { get; set; } = new List<PositionOrder>();
}
