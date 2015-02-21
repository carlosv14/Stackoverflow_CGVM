using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            QuestionListModel question1 = new QuestionListModel();
            question1.Title = "Title Test";
            question1.Votes = 1;
            question1.CreationTime = DateTime.Now;
            question1.OwnerName = "Carlos";
            question1.QuestionId = Guid.NewGuid();
            question1.OwnerId = Guid.NewGuid();
            models.Add(question1);
            QuestionListModel question2 = new QuestionListModel();
            question2.Title = "Title Test2";
            question2.Votes = 2;
            question2.CreationTime = DateTime.Now;
            question2.QuestionId = Guid.NewGuid();
            question2.OwnerName = "Varela";
            question2.OwnerId = Guid.NewGuid();
            models.Add(question2);
            return View(models);
        }

        public ActionResult CreateQuestion(CreateQuestionModel modelo)
        {

            if (ModelState.IsValid)
            {
                var newQuestion = _mappingEngine.Map<CreateQuestionModel, Question>(modelo);
                var context = new StackoverflowContext();
                context.Questions.Add(newQuestion);
                context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return View(modelo);
        }

	}
}