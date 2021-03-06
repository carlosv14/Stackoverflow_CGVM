﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Antlr.Runtime.Misc;
using AutoMapper;
using Stackoverflow_CGVM.Data;
using Stackoverflow_CGVM.Data.Migrations;
using Stackoverflow_CGVM.Domain.Entities;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
   [Authorize]
    public class AnswerController : Controller
    {
        private readonly IMappingEngine _mappingEngine;
        public AnswerController(IMappingEngine mappingEngine)
        {
            _mappingEngine = mappingEngine;
        }
       [AllowAnonymous]
        public ActionResult Index(Guid questionId)
        {
            
            IList<AnswerListModel> models = new ListStack<AnswerListModel>();
            var context = new StackoverflowContext();
            int cont = 1;
            foreach (Answer a in context.Answers.Include("Owner"))
            {
                var md = new MarkdownDeep.Markdown();

                if (a.QuestionId == questionId)
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
                    answer.Description = md.Transform(a.AnswerDescription);
                    models.Add(answer);
                }
            }
            models = models.OrderByDescending(x => x.Votes).ThenByDescending(x => x.CreationTime).ToList();
           AnswerListModel Canswer = models.FirstOrDefault(x => x.Best);
           if (Canswer != null)
           {
               int index = models.IndexOf(Canswer);
               models.Insert(0, Canswer);
               models.RemoveAt(index);
           }
           return PartialView(models);
        }

         
        public ActionResult CreateAnswer()
        {
           return PartialView(new AnswerCreateModel());
        }

        
        [HttpPost]
        public ActionResult CreateAnswer(AnswerCreateModel modelo, Guid questionId)
        {
            if (modelo.AnswerDescription != null)
            {
                System.Text.RegularExpressions.MatchCollection wordColl =
                    System.Text.RegularExpressions.Regex.Matches(modelo.AnswerDescription, @"[\S]+");
                System.Text.RegularExpressions.MatchCollection charColl =
                    System.Text.RegularExpressions.Regex.Matches(modelo.AnswerDescription, @".");
                if (wordColl.Count < 5 || charColl.Count < 50)
                {
                    TempData["AnswerBelow5word"] = "Answer must have at least 5 words and 50 characters";
                    var context = new StackoverflowContext();
                    context.Questions.Find(questionId).Vistas--;
                    context.SaveChanges();
                    return RedirectToAction("Detail", "Question", new {ID = questionId});
                }
            }
            if (ModelState.IsValid)
            {
                var context = new StackoverflowContext();
                var newAnswer = _mappingEngine.Map<AnswerCreateModel, Answer>(modelo);
                HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                    Guid ownerId = Guid.Parse(ticket.Name);
                    newAnswer.CreationDate = DateTime.Now;
                    newAnswer.ModificationDate = newAnswer.CreationDate;
                    newAnswer.Owner = context.Accounts.FirstOrDefault(x => x.Id == ownerId);
                    newAnswer.Votes = 0;
                    newAnswer.QuestionId = questionId;
                    newAnswer.isCorrect = false;
                    context.Answers.Add(newAnswer);
                    context.SaveChanges();
                }
                context.Questions.Find(questionId).Vistas--;
                context.Questions.Find(questionId).CantidadRespuestas++;
                context.SaveChanges();
                return RedirectToAction("Detail", "Question", new { ID = questionId });

            }

            return PartialView(modelo);
        }

        [AllowAnonymous]
        public ActionResult ViewAnswer( Guid id)
        {
            var context = new StackoverflowContext();
            var answer = context.Answers.Find(id);
            AnswerModel model = _mappingEngine.Map<Answer, AnswerModel>(answer);
            return View(model);
        }

         
        public ActionResult MeGusta(Guid Id)
        {
            var context = new StackoverflowContext();
            var voto= new Vote();
            var vistas = context.Questions.Find(Id).Vistas;
            Guid questionId = context.Answers.Find(Id).QuestionId;
             HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                Guid ownerID = Guid.Parse(ticket.Name);
               
                foreach (var vote in context.Votes)
                {
                    if (ownerID == vote.OwnerID && Id == vote.QorA_ID) {
                        ViewBag.Message = "Ya voto sobre esta respuesta";
                        vistas--;
                        return RedirectToAction("Detail","Question", new{Id = questionId});
                       
                    }
                }

                voto.OwnerID = ownerID;
                voto.QorA_ID = Id;
                context.Votes.Add(voto);
            }
            context.Answers.Find(Id).Votes ++;
            vistas--;   
            context.SaveChanges();
            return RedirectToAction("Detail","Question", new {Id = questionId});
        }

       
        public ActionResult NoMeGusta(Guid Id)
        {
            var context = new StackoverflowContext();
            var voto = new Vote();
            var vistas = context.Questions.Find(Id).Vistas;
            Guid questionId = context.Answers.Find(Id).QuestionId;
            HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                Guid ownerID = Guid.Parse(ticket.Name);
                foreach (var vote in context.Votes)
                {
                    if (ownerID == vote.OwnerID && Id == vote.QorA_ID) {
                        ViewBag.Message = "Ya voto sobre esta respuesta";
                        vistas--;
                        return RedirectToAction("Detail", "Question", new { Id = questionId });
                       
                    }
                 }
                voto.OwnerID = ownerID;
                voto.QorA_ID = Id;
                context.Votes.Add(voto);
            }
            context.Answers.Find(Id).Votes--;
            vistas--;
            context.SaveChanges();
            return RedirectToAction("Detail", "Question", new { Id = questionId });
        }
         
        public ActionResult Correcta(Guid id)
        {
            Guid ownerId = Guid.NewGuid();
             HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                 ownerId = Guid.Parse(ticket.Name);
            }
            var context = new StackoverflowContext();
            var questionId = context.Answers.Find(id).QuestionId;
            var Owner = context.Questions.Find(questionId).Owner.Id;
            foreach (Answer a in context.Answers)
            {
                if(ownerId!= Owner)
                {
                     context.Questions.Find(questionId).Vistas--;
                         context.SaveChanges();
                    return RedirectToAction("Detail", "Question", new {id = questionId});
                }
            }
            if (context.Answers.Find(id).isCorrect == false)
            {
                if (context.Questions.Find(questionId).hasCorrectAnswer == false)
                {
                    context.Answers.Find(id).isCorrect = true;
                    context.Questions.Find(questionId).hasCorrectAnswer = true;
                    context.Questions.Find(questionId).correctAnswer = id;
                }

            }
            else
            {
                if (context.Questions.Find(questionId).correctAnswer == id)
                {
                    context.Answers.Find(id).isCorrect = false;
                    context.Questions.Find(questionId).hasCorrectAnswer =false;
                    context.Questions.Find(questionId).correctAnswer = Guid.Empty;
                }
            }
            context.Questions.Find(questionId).Vistas--;
            context.SaveChanges();
            return RedirectToAction("Detail", "Question", new { id = questionId });
        }
    }

   
}