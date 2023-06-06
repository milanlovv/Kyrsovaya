using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository.Models;

[Table("Orders")]
[Microsoft.EntityFrameworkCore.Index("ClientId", Name = "IX_FK__Orders__ClientID__267ABA7A")]
public partial class Order1
{
    [Key]
    [Column("OrderID")]
    public int OrderId { get; set; }

    [Column("ClientID")]
    public int ClientId { get; set; }

    public int OrderCost { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Status { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("Order1s")]
    public virtual Client1 Client { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
}
