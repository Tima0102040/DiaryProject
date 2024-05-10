using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models;

public partial class Teacher
{
    [Display(Name="ID")]
    public int TeacherId { get; set; }

    [Display(Name="Teacher Name")]
    public string TeacherFullName { get; set; } = null!;

    [Display(Name="Email")]
    public string TeacherEmail { get; set; } = null!;

    [Display(Name="Login")]
    public string TeacherLogin { get; set; } = null!;

    public string TeacherPassword { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
}
