using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models;

public partial class Student
{
    [Display(Name="ID")]
    public int StudentId { get; set; }
    
    [Display(Name="Full Name")]
    public string StudentFullName { get; set; } = null!;

    [Display(Name="Email")]
    public string StudentEmail { get; set; } = null!;
    
    [Display(Name="Birth Date")]
    [DataType(DataType.Date)]
    [Column(TypeName="date")]
    public DateTime StudentBirthDate { get; set; }

    [Display(Name="Login")]
    public string StudentLogin { get; set; } = null!;

    public string StudentPassword { get; set; } = null!;

    public int ClassId { get; set; }

    public virtual Class? Class { get; set; } = null!;

    public virtual ICollection<Mark> Marks { get; set; } = new List<Mark>();
}
