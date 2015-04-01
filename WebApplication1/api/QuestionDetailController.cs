using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Stackoverflow_CGVM.Data;
using Stackoverflow_CGVM.Domain.Entities;
using WebApplication1.Models;

namespace WebApplication1.api
{
    public class QuestionDetailController : ApiController
    {
        public UnitOfWork _unitOfWork = new UnitOfWork();
        public DetailModel GetQuestion(Guid id)
        {
            Question pregunta = _unitOfWork.QuestionRepository.GetById(id);
            return AutoMapper.Mapper.Map<Question, DetailModel>(pregunta);
        }
    }
}
