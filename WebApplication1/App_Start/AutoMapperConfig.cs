using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Stackoverflow_CGVM.Domain.Entities;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace WebApplication1.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMaps()
        {
            Mapper.CreateMap<AccountRegisterModel, Account>().ReverseMap();
            Mapper.CreateMap<LoginModel, Account>().ReverseMap();
            Mapper.CreateMap<CreateQuestionModel, Question>().ReverseMap();
            Mapper.CreateMap<AnswerModel, Answer>().ReverseMap();
            Mapper.CreateMap<AnswerCreateModel, Answer>();
            Mapper.CreateMap<DetailModel, Question>().ReverseMap();
        }
    }
}