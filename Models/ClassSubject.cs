using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class ClassSubject
{
    public int ClassSubjectId { get; set; }

    public int SubjectId { get; set; }

    public int ClassId { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual Subject Subject { get; set; } = null!;
}
