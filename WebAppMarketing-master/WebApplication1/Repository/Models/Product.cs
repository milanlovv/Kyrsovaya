using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository.Models;

public partial class Product
{
    [Key]
    [Column("ProductID")]
    public int ProductId { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Description { get; set; }

    public byte[] ProductImage { get; set; }

    public int ProductCost { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

    [InverseProperty("Product")]
    public virtual ICollection<FavouriteProduct> FavouriteProducts { get; set; } = new List<FavouriteProduct>();

    [InverseProperty("Product")]
    public virtual ProductOrder ProductOrder { get; set; }
}
