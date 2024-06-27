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

            CreateMap<User, UserRanking>();
            CreateMap<UserRanking, User>().ReverseMap();

            CreateMap<User, UserLogin>();
            CreateMap<UserLogin, User>().ReverseMap();

            CreateMap<User, UserInfo>();
            CreateMap<UserInfo, User>().ReverseMap();

            CreateMap<Record, RecordDto>();
            CreateMap<RecordDto, Record>().ReverseMap();

            CreateMap<Item, ItemDto>();
            CreateMap<ItemDto, Item>().ReverseMap();

            CreateMap<Animal, AnimalDto>();
            CreateMap<AnimalDto, Animal>().ReverseMap();

            CreateMap<Plant, PlantDto>();
            CreateMap<PlantDto, Plant>().ReverseMap();
        }
    }
}