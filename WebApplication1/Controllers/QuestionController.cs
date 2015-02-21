using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
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

        public ActionResult CreateQuestion()
        {
            return View(new CreateQuestionModel());
        }

	}
}