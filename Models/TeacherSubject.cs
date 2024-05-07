using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class TeacherSubject
{
    public int TeacherSubjectId { get; set; }

    public int SubjectId { get; set; }

    public int TeacherId { get; set; }

    public virtual Subject Subject { get; set; } = null!;

    public virtual Teacher Teacher { get; set; } = null!;
}
