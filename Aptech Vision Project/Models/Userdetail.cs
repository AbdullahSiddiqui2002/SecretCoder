using System;
using System.Collections.Generic;

namespace Aptech_Vision_Project.Models;

public partial class Userdetail
{
    public int Id { get; set; }

    public int Userid { get; set; }

    public string Gender { get; set; } = null!;

    public DateTime Dob { get; set; }

    public string City { get; set; } = null!;

    public string Address { get; set; } = null!;

    public long Mobile { get; set; }

    public string Qualification { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
