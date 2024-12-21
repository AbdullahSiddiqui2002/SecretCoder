using System;
using System.Collections.Generic;

namespace Aptech_Vision_Project.Models;

public partial class User
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Userdetail> Userdetails { get; set; } = new List<Userdetail>();

    public virtual ICollection<Userimage> Userimages { get; set; } = new List<Userimage>();
}
