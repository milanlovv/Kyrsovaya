using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository.Models;

[Table("Cart_Products")]
[Microsoft.EntityFrameworkCore.Index("CartId", Name = "IX_FK__Cart_Prod__CartI__4D94879B")]
[Microsoft.EntityFrameworkCore.Index("ProductId", Name = "IX_FK__Cart_Prod__Produ__4CA06362")]
public partial class CartProduct
{
    [Key]
    [Column("CartProductID")]
    public int CartProductId { get; set; }

    [Column("ProductID")]
    public int ProductId { get; set; }

    [Column("CartID")]
    public int CartId { get; set; }

    public int Amount { get; set; }

    [ForeignKey("CartId")]
    [InverseProperty("CartProducts")]
    public virtual Cart Cart { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("CartProducts")]
    public virtual Product Product { get; set; }
}
