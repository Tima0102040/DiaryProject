using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class Lesson
{
    public int LessonId { get; set; }

    public string LessonTopic { get; set; } = null!;

    public DateTime LessonDate { get; set; }

    public int ClassSubjectId { get; set; }

    public virtual ClassSubject ClassSubject { get; set; } = null!;

    public virtual ICollection<Mark> Marks { get; set; } = new List<Mark>();
}
