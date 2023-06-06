using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository.Models;

[Microsoft.EntityFrameworkCore.Index("ClientId", Name = "IX_FK__Favourite__Clien__5070F446")]
public partial class Favourite
{
    [Key]
    [Column("FavouriteID")]
    public int FavouriteId { get; set; }

    [Column("ClientID")]
    public int ClientId { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("Favourites")]
    public virtual Client1 Client { get; set; }

    [InverseProperty("Favourite")]
    public virtual ICollection<FavouriteProduct> FavouriteProducts { get; set; } = new List<FavouriteProduct>();
}
