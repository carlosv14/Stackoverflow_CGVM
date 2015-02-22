using System.Collections.Generic;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AnswerController : Controller
    {
        public ActionResult Index()
        {
            IList<AnswerListModel> models = new ListStack<AnswerListModel>();
            return View(models);
        }
    }

   
}