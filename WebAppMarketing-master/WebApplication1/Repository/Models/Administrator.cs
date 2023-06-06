using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository.Models;

[Table("Administrator")]
public partial class Administrator
{
    [Key]
    [Column("AdministratorID")]
    public int AdministratorId { get; set; }

    [Required]
    [StringLength(20)]
    public string Login { get; set; }

    [Required]
    public string Password { get; set; }
}
