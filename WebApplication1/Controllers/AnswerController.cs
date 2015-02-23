﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Antlr.Runtime.Misc;
using AutoMapper;
using Stackoverflow_CGVM.Data;
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
        public ActionResult Index()
        {
            IList<AnswerListModel> models = new ListStack<AnswerListModel>();
            var context = new StackoverflowContext();
            int cont = 1;
            foreach (Answer a in context.Answers)
            {
                AnswerListModel answer = new AnswerListModel();
                answer.name = "Ref" + cont++;
                answer.Votes = a.Votes;
                answer.CreationTime = a.CreationDate;
                answer.OwnerName = a.Owner.Name;
                answer.AnswerId = a.Id;
                answer.OwnerId = a.Owner.Id;
                models.Add(answer);
            }
            return View(models);
        }

        
        public ActionResult CreateAnswer()
        {
            return View(new AnswerCreateModel());
        }


        [HttpPost]
        public ActionResult CreateAnswer(AnswerModel modelo)
        {
            if (ModelState.IsValid)
            {
                var context = new StackoverflowContext();
                var newAnswer = _mappingEngine.Map<AnswerModel, Answer>(modelo);
                HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                    Guid ownerId = Guid.Parse(ticket.Name);
                    newAnswer.CreationDate = DateTime.Now;
                    newAnswer.ModificationDate = newAnswer.CreationDate;
                    newAnswer.Owner = context.Accounts.FirstOrDefault(x => x.Id == ownerId);
                    newAnswer.Votes = 0;
                    
                    context.Answers.Add(newAnswer);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");

            }
           
            return View(modelo);
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
            context.Answers.Find(Id).Votes ++;
            context.SaveChanges();
            return RedirectToAction("Index","Answer");
        }

        public ActionResult NoMeGusta(Guid Id)
        {
            var context = new StackoverflowContext();
            context.Answers.Find(Id).Votes--;
            context.SaveChanges();
            return RedirectToAction("Index", "Answer");
        }
    }

   
}