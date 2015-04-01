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
    public class QuestionIndexController :ApiController
    {
        public IEnumerable<QuestionListModel> Get()
        {
            var context = new StackoverflowContext();
            List<QuestionListModel> models = new List<QuestionListModel>();
            foreach (Question q in context.Questions.Include("Owner"))
            {
                QuestionListModel question1 = new QuestionListModel();
                question1.Title = q.Title;
                question1.Votes = q.Votes;
                question1.CreationTime = q.CreationDate;
                question1.OwnerName = q.Owner.Name;
                question1.Description = q.Description;
                if (question1.Description.Length > 25)
                    question1.Description = question1.Description.Remove(25);
                question1.QuestionId = q.Id;
                question1.OwnerId = q.Owner.Id;
                models.Add(question1);
            }

            return models;
 
        }
    }
}