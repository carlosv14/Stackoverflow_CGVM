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
                

         Question question = Mapper.Map<CreateQuestionModel, Question>(model);
         var session = context.sessions.FirstOrDefault(x => x.Token == model.token);
         if(session== null)
             throw new HttpResponseException(0);
         var Owner = context.Accounts.Find(session.whosLoggedId);
            question.Owner = Owner;
            question.ModificationDate = question.CreationDate =
                DateTime.Now;

            context.Questions.Add(question);
         context.SaveChanges();   
         HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.Created, model);

            return response;
        }
    }
}
