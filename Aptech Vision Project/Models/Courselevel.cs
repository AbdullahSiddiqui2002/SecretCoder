using System;
using System.Collections.Generic;

namespace Aptech_Vision_Project.Models;

public partial class Courselevel
{
    public int Id { get; set; }

    public string Courselevel1 { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
