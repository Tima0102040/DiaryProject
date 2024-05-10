﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Project.Models;

public partial class Class
{
    [Display(Name="ID")]
    public int ClassId { get; set; }
    
    [Display(Name="Class Name")]
    public string ClassName { get; set; } = null!;

    public int? TeacherId { get; set; }

    public virtual ICollection<ClassSubject> ClassSubjects { get; set; } = new List<ClassSubject>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual Teacher? Teacher { get; set; }
}
