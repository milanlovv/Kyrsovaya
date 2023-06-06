using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository.Models;

[Microsoft.EntityFrameworkCore.Index("ClientId", Name = "IX_FK__Carts__ClientID__49C3F6B7")]
public partial class Cart
{
    [Key]
    [Column("CartID")]
    public int CartId { get; set; }

    [Column("ClientID")]
    public int ClientId { get; set; }

    [InverseProperty("Cart")]
    public virtual ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

    [ForeignKey("ClientId")]
    [InverseProperty("Carts")]
    public virtual Client1 Client { get; set; }
}
