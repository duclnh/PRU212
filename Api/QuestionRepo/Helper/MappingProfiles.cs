using System;
using AutoMapper;
using QuestionRepo.Dto;
using QuestionRepo.Models;

namespace QuestionRepo.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Question, QuestionDto>();
            CreateMap<QuestionDto, Question>().ReverseMap();

            CreateMap<User, UserGet>();
            CreateMap<UserGet, User>().ReverseMap();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>().ReverseMap();

            CreateMap<Record, RecordDto>();
            CreateMap<RecordDto, Record>().ReverseMap();
        }
    }
}