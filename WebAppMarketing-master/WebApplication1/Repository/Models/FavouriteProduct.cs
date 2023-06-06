using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository.Models;

[Table("Favourite_Products")]
[Microsoft.EntityFrameworkCore.Index("FavouriteId", Name = "IX_FK__Favourite__Favou__5441852A")]
[Microsoft.EntityFrameworkCore.Index("ProductId", Name = "IX_FK__Favourite__Produ__534D60F1")]
public partial class FavouriteProduct
{
    [Key]
    [Column("FavouriteProductID")]
    public int FavouriteProductId { get; set; }

    [Column("ProductID")]
    public int ProductId { get; set; }

    [Column("FavouriteID")]
    public int FavouriteId { get; set; }

    public int Amount { get; set; }

    [ForeignKey("FavouriteId")]
    [InverseProperty("FavouriteProducts")]
    public virtual Favourite Favourite { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("FavouriteProducts")]
    public virtual Product Product { get; set; }
}
