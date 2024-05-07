using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models;

public class Admin
{
    [Key]
    public int AdminId { get; set; }

    public string AdminEmail { get; set; } = null!;

    public string AdminLogin { get; set; } = null!;

    public string AdminPassword { get; set; } = null!;
}
