using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository.Models;

[Table("Order")]
[Microsoft.EntityFrameworkCore.Index("ClientId", Name = "IX_FK_Order_Client")]
public partial class Order
{
    [Key]
    [Column("OrderID")]
    public int OrderId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal Sum { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }

    [Required]
    [StringLength(30)]
    public string Status { get; set; }

    [Required]
    public string Address { get; set; }

    [Column("ClientID")]
    public int ClientId { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("Orders")]
    public virtual Client Client { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<PositionOrder> PositionOrders { get; set; } = new List<PositionOrder>();
}
