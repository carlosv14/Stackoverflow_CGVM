using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using AutoMapper;
using Stackoverflow_CGVM.Data;
using Stackoverflow_CGVM.Domain.Entities;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CommentController :Controller
    {
        private readonly IMappingEngine _mappingEngine;

        public CommentController(IMappingEngine mappingEngine)
        {
            _mappingEngine = mappingEngine;
        }

        public ActionResult Index(Guid id)
        {
            List<CommentListModel> models = new List<CommentListModel>();
            var context = new StackoverflowContext();
            foreach (Comment c in context.Comments.Include("Owner"))
            {
                CommentListModel comment = new CommentListModel();
                if (c.QorAId == id)
                {
                    comment.CreationDate = c.CreationDate;
                    comment.CommentDescription = c.CommentDescription;
                    comment.OwnerId = c.Owner.Id;
                    comment.OwnerName = c.Owner.Name + " " + c.Owner.LastName;
                    comment.Id = c.Id;
                    comment.QorAId = c.QorAId;
                    models.Add(comment);
                }
            }
            return PartialView(models);

        }

        [Authorize]
        public ActionResult CreateComment()
        {
            return View(new CreateCommentModel());
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateComment(CreateCommentModel model,Guid Id)
        {
            if (model.CommentDescription != null)
            {
                if (ModelState.IsValid)
                {
                    var context = new StackoverflowContext();
                    var newComment = _mappingEngine.Map<CreateCommentModel, Comment>(model);
                    HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                    if (cookie != null)
                    {
                        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                        Guid ownerId = Guid.Parse(ticket.Name);
                        newComment.Owner = context.Accounts.FirstOrDefault(x => x.Id == ownerId);
                        newComment.CreationDate = DateTime.Now;
                        newComment.QorAId = Id;
                        context.Comments.Add(newComment);
                        context.SaveChanges();
                    }
                    if (isItAnswer(Id) != null)
                    {
                        var question = context.Questions.Find(isItAnswer(Id).QuestionId);
                        context.Questions.Find(Id).Vistas--;
                        context.SaveChanges();
                        return RedirectToAction("Detail", "Question", new {Id = question.Id});
                    }
                    context.Questions.Find(Id).Vistas--;
                    context.SaveChanges();
                    return RedirectToAction("Detail", "Question", new {Id = Id});

                }
            }
            return PartialView(model);
         
        }

        public Answer isItAnswer(Guid id)
        {
            var context = new StackoverflowContext();
            return context.Answers.Find(id);
        }
    }
}