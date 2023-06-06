using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository.Models;

[Table("Administrators")]
public partial class Administrator1
{
    [Key]
    [Column("AdministratorID")]
    public int AdministratorId { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string AdministratorLogin { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string AdministratorPassword { get; set; }

    [Required]
    [Column("AdministratorFIO")]
    [StringLength(50)]
    [Unicode(false)]
    public string AdministratorFio { get; set; }
}
