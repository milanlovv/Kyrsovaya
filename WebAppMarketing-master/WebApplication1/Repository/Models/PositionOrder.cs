using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository.Models;

[PrimaryKey("PositionId", "OrderId")]
[Table("PositionOrder")]
[Microsoft.EntityFrameworkCore.Index("OrderId", Name = "IX_FK_PositionOrder_Order")]
public partial class PositionOrder
{
    [Key]
    [Column("PositionID")]
    public int PositionId { get; set; }

    [Key]
    [Column("OrderID")]
    public int OrderId { get; set; }

    public int Amount { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("PositionOrders")]
    public virtual Order Order { get; set; }

    [ForeignKey("PositionId")]
    [InverseProperty("PositionOrders")]
    public virtual Position Position { get; set; }
}
