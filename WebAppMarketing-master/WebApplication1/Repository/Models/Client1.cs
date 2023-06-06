using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository.Models;

[Table("Clients")]
public partial class Client1
{
    [Key]
    [Column("ClientID")]
    public int ClientId { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string ClientLogin { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string ClientPassword { get; set; }

    [Required]
    [Column("ClientFIO")]
    [StringLength(50)]
    [Unicode(false)]
    public string ClientFio { get; set; }

    [InverseProperty("Client")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [InverseProperty("Client")]
    public virtual ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();

    [InverseProperty("Client")]
    public virtual ICollection<Order1> Order1s { get; set; } = new List<Order1>();
}
