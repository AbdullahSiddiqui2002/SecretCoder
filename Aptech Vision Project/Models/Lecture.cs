using System;
using System.Collections.Generic;

namespace Aptech_Vision_Project.Models;

public partial class Lecture
{
    public int Id { get; set; }

    public string Lecture1 { get; set; } = null!;

    public int Courseid { get; set; }

    public virtual Course Course { get; set; } = null!;
}
