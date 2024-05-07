using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string StudentFullName { get; set; } = null!;

    public string StudentEmail { get; set; } = null!;

    public DateTime StudentBirthDate { get; set; }

    public string StudentLogin { get; set; } = null!;

    public string StudentPassword { get; set; } = null!;

    public int ClassId { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual ICollection<Mark> Marks { get; set; } = new List<Mark>();
}
