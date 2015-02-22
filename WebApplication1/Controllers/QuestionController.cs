using System;
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
            IList<QuestionListModel> models = new ListStack<QuestionListModel>();
            var context = new StackoverflowContext();
            foreach (Question q in context.Questions)
            {
                QuestionListModel question1 = new QuestionListModel();
                question1.Title = q.Title;
                question1.Votes = q.Votes;
                question1.CreationTime = q.CreationDate;
                question1.OwnerName = " ";
                question1.QuestionId = q.Id;
                question1.OwnerId = Guid.NewGuid();
                models.Add(question1);
            }
            return View(models);
        }

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
                    newQuestion.Votes = 1;
                    newQuestion.Owner = context.Accounts.FirstOrDefault(x => x.Id == ownerId);
                    newQuestion.ModificationDate = DateTime.Now;
                    newQuestion.CreationDate = DateTime.Now;
                    context.Questions.Add(newQuestion);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");

            }
            return View(modelo);
        }

	}
}