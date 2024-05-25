using System;
using System.Collections.Generic;

namespace QuestionRepo.Models;

public partial class Record
{
    public int RecordId { get; set; }

    public int UserId { get; set; }

    public string UserAnswer { get; set; } = null!;

    public int QuestionId { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
