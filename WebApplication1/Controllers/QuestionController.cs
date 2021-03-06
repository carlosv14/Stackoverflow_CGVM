﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Antlr.Runtime.Misc;
using AutoMapper;
using Stackoverflow_CGVM.Data;
using Stackoverflow_CGVM.Domain.Entities;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        
        private readonly IMappingEngine _mappingEngine;

       
        public QuestionController(IMappingEngine mappingEngine)
        {
            _mappingEngine = mappingEngine;
            
        }
        //
        // GET: /Question/
       
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (HttpContext.Session != null && HttpContext.Session["Multiplicador"] == null)
                HttpContext.Session["Multiplicador"] = 1;

            IList<QuestionListModel> models = new ListStack<QuestionListModel>();
            var context = new StackoverflowContext();
            ViewBag.cantidad = false;
            foreach (Question q in context.Questions.Include("Owner"))
            {
                QuestionListModel question1 = new QuestionListModel();
                question1.Title = q.Title;
                question1.Votes = q.Votes;
                question1.CreationTime = q.CreationDate;
                question1.OwnerName = q.Owner.Name;
                question1.Vistas = q.Vistas;
                question1.CantidadRespuestas = q.CantidadRespuestas;
                question1.Description = q.Description;
                if (question1.Description.Length > 25)
                   question1.Description= question1.Description.Remove(25);
                question1.QuestionId = q.Id;
                question1.OwnerId = q.Owner.Id;
                models.Add(question1);
            }
            models = models.OrderByDescending(x => x.CreationTime).ToList();
            ViewBag.order = "Votes";
             if ((HttpContext.Session != null && HttpContext.Session["Order"] == null)||(int)HttpContext.Session["Order"] ==4)
            {
                HttpContext.Session["Order"] = 0;
                ViewBag.order = "Votes";
                models = models.OrderByDescending(x => x.CreationTime).ToList();
              
            }
             else if ((int)HttpContext.Session["Order"]==1)
            {
                models = models.OrderByDescending(x => x.Votes).ToList();
                ViewBag.order = "Answers";
            }
             else if ((int)HttpContext.Session["Order"] == 2)
            {
                models = models.OrderByDescending(x => x.CantidadRespuestas).ToList();
                ViewBag.order = "Views";
            }
             else if ((int)HttpContext.Session["Order"] == 3)
             {
                 models = models.OrderByDescending(x => x.Vistas).ToList();
                 ViewBag.order = "Dates";
               
             }

             if (models.Count > (int)HttpContext.Session["Multiplicador"] * 25)
                 ViewBag.cantidad = true;
             models = models.Take((int)HttpContext.Session["Multiplicador"] * 25).ToList();
            return View(models);
        }

        public ActionResult CreateQuestion()
        {
            return View(new CreateQuestionModel());
        }
        
        [HttpPost]
        public ActionResult CreateQuestion(CreateQuestionModel modelo)
        {

            if (ModelState.IsValid)
            {
                 var context = new StackoverflowContext();
                 var newQuestion = _mappingEngine.Map<CreateQuestionModel, Question>(modelo);
                HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                    Guid ownerId = Guid.Parse(ticket.Name);
                    newQuestion.Votes =0;
                    newQuestion.Vistas = 0;
                    newQuestion.CantidadRespuestas = 0;
                    newQuestion.Owner = context.Accounts.FirstOrDefault(x => x.Id == ownerId);
                    newQuestion.ModificationDate = DateTime.Now;
                    newQuestion.CreationDate = DateTime.Now;
                    newQuestion.hasCorrectAnswer = false;
                    newQuestion.correctAnswer = Guid.NewGuid();
                    context.Questions.Add(newQuestion);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");

            }
            return View(modelo);
        }
         [AllowAnonymous]
        public ActionResult Detail(Guid Id )
        {
            var md = new MarkdownDeep.Markdown();
             DetailModel model = new DetailModel();
             var question = _mappingEngine.Map<DetailModel, Question>(model);    
            var context = new StackoverflowContext();
             context.Questions.Find(Id).Vistas++;
             context.SaveChanges();
            model.Title = context.Questions.FirstOrDefault(x => x.Id == Id).Title;
            model.Description = md.Transform(context.Questions.FirstOrDefault(x => x.Id == Id).Description);
            model.Votes = context.Questions.FirstOrDefault(x => x.Id == Id).Votes;
            model.Id = context.Questions.FirstOrDefault(x => x.Id == Id).Id;
            model.CreationDate = context.Questions.FirstOrDefault(x => x.Id == Id).CreationDate;
            model.Vistas = context.Questions.FirstOrDefault(x => x.Id == Id).Vistas;

             return View(model);
        }

      
        public ActionResult MeGusta(Guid Id)
        {
            var context = new StackoverflowContext();
            var voto = new Vote();
            var vistas = context.Questions.Find(Id).Vistas;
            HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                Guid ownerID = Guid.Parse(ticket.Name);
                foreach (var vote in context.Votes)
                {
                    if (ownerID == vote.OwnerID && Id == vote.QorA_ID) {
                        ViewBag.Message = "Ya voto sobre esta pregunta";
                            vistas--;

                        return RedirectToAction("Detail", "Question", new { Id = Id});

                       
                    }
                }
                voto.OwnerID = ownerID;
                voto.QorA_ID = Id;
                context.Votes.Add(voto);
            }

            context.Questions.Find(Id).Votes++;
            vistas--;
            context.SaveChanges();
            return RedirectToAction("Detail", new {Id = Id});
        }
        public ActionResult NoMeGusta(Guid Id)
        {
            var context = new StackoverflowContext();
            var voto = new Vote();
            var vistas = context.Questions.Find(Id).Vistas;
            HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                Guid ownerID = Guid.Parse(ticket.Name);

                foreach (var vote in context.Votes)
                {
                    if (ownerID == vote.OwnerID && Id == vote.QorA_ID)
                    {
                        ViewBag.Message = "Ya voto sobre esta pregunta";
                        vistas--;
                        return RedirectToAction("Detail", "Question", new {Id = Id});
                       
                    }
                }

                voto.OwnerID = ownerID;
                voto.QorA_ID = Id;
                context.Votes.Add(voto);
            }
            context.Questions.Find(Id).Votes--;
            vistas--;
            context.SaveChanges();
            return RedirectToAction("Detail", new { Id = Id });
        }

        [AllowAnonymous]
        public ActionResult Answer(Guid Id)
        {
            return RedirectToAction("Index","Answer",new {questionId = Id});
        }
          
        public ActionResult CreateAnswer(Guid Id)
        {
        
            return RedirectToAction("CreateAnswer", "Answer", new { questionId = Id });
        }

        public ActionResult makeChange()
        {
            if (HttpContext.Session["Order"] != null)
                HttpContext.Session["Order"] = (int)HttpContext.Session["Order"] + 1;
            return RedirectToAction("Index");
        }

        public ActionResult CargarMas()
        {
            HttpContext.Session["Multiplicador"] = (int)HttpContext.Session["Multiplicador"]+1;
            return RedirectToAction("Index");
        }
    }
}