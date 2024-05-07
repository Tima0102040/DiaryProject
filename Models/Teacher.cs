using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public string TecherFullName { get; set; } = null!;

    public string TeacherEmail { get; set; } = null!;

    public string TeacherLogin { get; set; } = null!;

    public string TeacherPassword { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
}
