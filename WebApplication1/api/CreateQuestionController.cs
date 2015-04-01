using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Stackoverflow_CGVM.Data;
using Stackoverflow_CGVM.Domain.Entities;
using WebApplication1.Models;

namespace WebApplication1.api
{
    public class CreateQuestionController :ApiController
    {
    
     public HttpResponseMessage PostQuestion(CreateQuestionModel model)
     {
         var context = new StackoverflowContext();
            if (!this.ModelState.IsValid)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest));

            Question question = Mapper.Map<CreateQuestionModel, Question>(model);
            var creatorId = Guid.Parse("402d1c43-55e7-4fc7-98b6-dcd05d42f07c");
            question.Owner = context.Accounts.Find(creatorId);
            question.ModificationDate = question.CreationDate =
                DateTime.Now;

            context.Questions.Add(question);
         context.SaveChanges();   
         HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.Created, model);

            return response;
        }
    }
}
