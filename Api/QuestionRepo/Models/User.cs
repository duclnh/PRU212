﻿using System;
using System.Collections.Generic;

namespace QuestionRepo.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Record> Records { get; set; } = new List<Record>();
}
