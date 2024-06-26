﻿namespace QuestionRepo.Dto
{
    public class QuestionDto
    {
        public Guid QuestionId { get; set; }
        public string Question1 { get; set; } = null!;


        public string OptionA { get; set; } = null!;


        public string OptionB { get; set; } = null!;


        public string? OptionC { get; set; }


        public string? OptionD { get; set; }

        public string Answer { get; set; } = null!;
    }
}
