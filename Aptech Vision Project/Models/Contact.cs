﻿using System;
using System.Collections.Generic;

namespace Aptech_Vision_Project.Models;

public partial class Contact
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string Message { get; set; } = null!;
}
