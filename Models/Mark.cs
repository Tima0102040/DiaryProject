using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class Mark
{
    public int MarkId { get; set; }

    public int MarkValue { get; set; }

    public int LessonId { get; set; }

    public int StudentId { get; set; }

    public virtual Lesson Lesson { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
