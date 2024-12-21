using System;
using System.Collections.Generic;

namespace Aptech_Vision_Project.Models;

public partial class Instructor
{
    public int Id { get; set; }

    public string Instructorname { get; set; } = null!;

    public string Instructorrole { get; set; } = null!;

    public string Instructoremail { get; set; } = null!;

    public double Instructorrating { get; set; }

    public int Instructorreviews { get; set; }

    public int Enrolledstudents { get; set; }

    public int Coursesteach { get; set; }

    public string Instructordescription { get; set; } = null!;

    public string Instructorimage { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
