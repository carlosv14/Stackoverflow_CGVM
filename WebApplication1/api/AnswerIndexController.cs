using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Stackoverflow_CGVM.Data;
using Stackoverflow_CGVM.Domain.Entities;
using WebApplication1.Models;

namespace WebApplication1.api
{
    public class AnswerIndexController : ApiController
    {

        public IEnumerable<AnswerListModel> GetAnswers(Guid id)
        {
            var context = new StackoverflowContext();
            List<AnswerListModel> models = new List<AnswerListModel>();
            int cont = 1;
            foreach (Answer a in context.Answers.Include("Owner"))
            {
                if (a.QuestionId == id)
                {
                    AnswerListModel answer = new AnswerListModel();
                    answer.name = "Respuesta" + cont++;
                    answer.Votes = a.Votes;
                    answer.CreationTime = a.CreationDate;
                    answer.OwnerName = a.Owner.Name;
                    answer.AnswerId = a.Id;
                    answer.QuestionId = a.QuestionId;
                    answer.OwnerId = a.Owner.Id;
                    answer.Best = a.isCorrect;
                    answer.Description = a.AnswerDescription;
                    models.Add(answer);
                }
            }

            return models;

        }
       
    }
}