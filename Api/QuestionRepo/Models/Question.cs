using System;
using System.Collections.Generic;
using QuestionRepo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

namespace QuestionRepo.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public string Question1 { get; set; } = null!;


    public string OptionA { get; set; } = null!;


    public string OptionB { get; set; } = null!;


    public string? OptionC { get; set; }


    public string? OptionD { get; set; }

    public string Answer { get; set; } = null!;

    public virtual ICollection<Record> Records { get; set; } = new List<Record>();
}