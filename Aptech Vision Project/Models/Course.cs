using System;
using System.Collections.Generic;

namespace Aptech_Vision_Project.Models;

public partial class Course
{
    public int Id { get; set; }

    public string Coursename { get; set; } = null!;

    public string Courseshortdescription { get; set; } = null!;

    public string Courselongdescription { get; set; } = null!;

    public string Courseimage { get; set; } = null!;

    public int Courselevelid { get; set; }

    public double Courseduration { get; set; }

    public double Courserating { get; set; }

    public int Studentsenrolled { get; set; }

    public double Coursefee { get; set; }

    public string Language { get; set; } = null!;

    public int Lectures { get; set; }

    public double Deadline { get; set; }

    public string Certificate { get; set; } = null!;

    public int Courseinstructor { get; set; }

    public virtual Instructor CourseinstructorNavigation { get; set; } = null!;

    public virtual Courselevel Courselevel { get; set; } = null!;

    public virtual ICollection<Lecture> LecturesNavigation { get; set; } = new List<Lecture>();
}
