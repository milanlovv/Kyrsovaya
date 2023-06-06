using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository.Models;

[Table("Product_Orders")]
[Microsoft.EntityFrameworkCore.Index("OrderId", Name = "IX_FK__Product_O__Order__2C3393D0")]
public partial class ProductOrder
{
    [Key]
    [Column("ProductID")]
    public int ProductId { get; set; }

    [Column("OrderID")]
    public int OrderId { get; set; }

    public int Amount { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("ProductOrders")]
    public virtual Order1 Order { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductOrder")]
    public virtual Product Product { get; set; }
}
