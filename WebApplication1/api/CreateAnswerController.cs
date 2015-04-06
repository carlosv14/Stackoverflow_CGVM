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
    public class CreateAnswerController :ApiController
    {
        
     public HttpResponseMessage PostQuestion(AnswerCreateModel model)
     {
         var context = new StackoverflowContext();
                
         Answer answer = Mapper.Map<AnswerCreateModel, Answer>(model);
         var session = context.sessions.FirstOrDefault(x => x.Token == model.token);
         if(session== null)
             throw new HttpResponseException(0);
            var Owner = context.Accounts.Find(session.whosLoggedId);
            answer.Owner = Owner;
            answer.CreationDate = DateTime.Now;
            answer.ModificationDate = answer.CreationDate;
            answer.Votes = 0;
            answer.isCorrect = false;
            context.Answers.Add(answer);
            context.Questions.Find(model.QuestionId).CantidadRespuestas++;
            context.SaveChanges();

         HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.Created, model);

            return response;
        }
    
    }
    
}


            
              
                   
              
              