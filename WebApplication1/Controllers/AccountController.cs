using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using Stackoverflow_CGVM.Data;
using Stackoverflow_CGVM.Domain.Entities;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMappingEngine _mappingEngine;
        public AccountController(IMappingEngine mappingEngine)
        {
            _mappingEngine = mappingEngine;
        }
        public ActionResult Register()
        {
            return View(new AccountRegisterModel());
        }

        [HttpPost]
        public ActionResult Register(AccountRegisterModel modelo)
        {
            if (ModelState.IsValid)
            {
                var newAccount = _mappingEngine.Map<AccountRegisterModel, Account>(modelo);
               
                var context = new StackoverflowContext();
                context.Accounts.Add(newAccount);
                context.SaveChanges();
                return RedirectToAction("Login");
               
            }
            return View(modelo);
        }

        public ActionResult Login()
        {
            
         return View(new LoginModel());   
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var context = new StackoverflowContext();
            var Account = context.Accounts.FirstOrDefault(x => x.Email == model.Email && x.Passw == model.Passw);
            if (Account != null)
            {
                FormsAuthentication.SetAuthCookie(model.Email, false);
                return RedirectToAction("Index", "Question");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Question");

        }
       
    }
}
