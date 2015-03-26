using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Stackoverflow_CGVM.Data;
using Stackoverflow_CGVM.Domain.Entities;

namespace WebApplication1.api
{
    public class QuestionDetailController : ApiController
    {
        public UnitOfWork _unitOfWork = new UnitOfWork();
        public string GetQuestion(Guid id)
        {
            Question pregunta = _unitOfWork.QuestionRepository.GetById(id);
           return pregunta.Description.ToString();
        }
    }
}
