using System;
using System.Collections.Generic;

namespace QuestionRepo.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Money { get; set; } = 0;

    public float PositionX { get; set; } = 0;

    public float PositionY { get; set; } = 0;

    public float PositionZ { get; set; } = 0;

    public string? Sence { get; set; }

    public virtual ICollection<Record> Records { get; set; } = new List<Record>();

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<Plant> Plants { get; set; } = new List<Plant>();

    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();
}
