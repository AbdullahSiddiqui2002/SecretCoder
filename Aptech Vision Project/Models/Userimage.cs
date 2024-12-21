using System;
using System.Collections.Generic;

namespace Aptech_Vision_Project.Models;

public partial class Userimage
{
    public int Id { get; set; }

    public int Userid { get; set; }

    public string Image { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
