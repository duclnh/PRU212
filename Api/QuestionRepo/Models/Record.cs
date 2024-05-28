using System;
using System.Collections.Generic;

namespace QuestionRepo.Models;

public partial class Record
{
    public Guid RecordId { get; set; }

    public Guid UserId { get; set; }

    public string UserAnswer { get; set; } = null!;

    public Guid QuestionId { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
