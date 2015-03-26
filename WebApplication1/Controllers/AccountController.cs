using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using Stackoverflow_CGVM.Data;
using Stackoverflow_CGVM.Domain.Entities;
using Stackoverflow_CGVM.Domain.Services;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMappingEngine _mappingEngine;
        private UnitOfWork _unitOfWork = new UnitOfWork();
        private Encriptar _encrypt = new Encriptar();
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

          
           
            if ( ModelState.IsValid)
            {
                 
                if(modelo.Passw !=modelo.ConfirmPassword)
                    return RedirectToAction("Register");
                modelo.Passw = _encrypt.EncryptKey(modelo.Passw);
                IEnumerable<Account> list = _unitOfWork.AccountRepository.Get(x => x.Email == modelo.Email);
                if (list.Any())
                {
                    ViewBag.Message = "Ya Existe un Usuario con este correo Electronico";
                    return RedirectToAction("Register");
                }
                    
                var newAccount = _mappingEngine.Map<AccountRegisterModel, Account>(modelo);
                newAccount.confirmed = false;
               _unitOfWork.AccountRepository.Insert(newAccount);
                _unitOfWork.Save();
                var host = HttpContext.Request.Url.Host;
                if (host == "localhost")
                    host = Request.Url.GetLeftPart(UriPartial.Authority);
               
                PasswordRecovering.SendEmail(newAccount.Email, "Porfavor confirme su cuenta para hacer Login en el siguiente Link: " + host + "/Account/ConfirmarCuenta/"+newAccount.Id,"Confirmando Cuenta");

                return RedirectToAction("Login");
               
            }
            return View(modelo);
        }

        public ActionResult ConfirmarCuenta(Guid Id)
        {
            _unitOfWork.AccountRepository.GetById(Id).confirmed = true;
              _unitOfWork.Save();  
            return RedirectToAction("Login");
        }
       
        public ActionResult Login()
        {
            
         return View(new LoginModel());   
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            LoginModel model = new LoginModel();
            model.Email = username;
            model.Passw = _encrypt.EncryptKey(password);
            var Account = new Account();
            IEnumerable<Account> list =_unitOfWork.AccountRepository.Get(x => x.Email == model.Email && x.Passw == model.Passw);
            if (list.Any())
                Account = list.First();
            else
                Account = null;
            if (Account != null)
            {
                if (Account.confirmed == false)
                {
                    ViewBag.Message = "Debe Confirmar su cuenta para Ingresar";
                    return View(model);
                }
                FormsAuthentication.SetAuthCookie(Account.Id.ToString(), false);
               
                return RedirectToAction("Index", "Question");
            }
            
            ViewBag.Message = "El Correo electronico o contraseña incorrecta";
            IEnumerable<Account> list2 = _unitOfWork.AccountRepository.Get(x => x.Email == model.Email);
            if(list2.Any())
                PasswordRecovering.SendEmail(model.Email, "Alguien intento entrar con su cuenta a stackoverflow con información incorrecta" , "Alerta");

            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Question");

        }
        [Authorize]  
        public ActionResult Profile(ProfileModel modelo, Guid Id)
        {

            modelo.Name = _unitOfWork.AccountRepository.Get(x => x.Id == Id).First().Name;
            modelo.Email = _unitOfWork.AccountRepository.Get(x => x.Id == Id).First().Email;
            modelo.Reputation = 0;
            return View(modelo);
        }

         [Authorize] 
        public ActionResult CurrentProfile()
        {
            ProfileModel
            modelo = new ProfileModel();
          

            HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                Guid UserId = Guid.Parse(ticket.Name);
                modelo.Email = _unitOfWork.AccountRepository.Get(x => x.Id == UserId).First().Email;
                modelo.Name = _unitOfWork.AccountRepository.Get(x => x.Id == UserId).First().Name;
               return RedirectToAction("Profile",new {id = UserId});
               
            }
            return RedirectToAction("Index","Question");
        }

        public ActionResult VerifyingPassword()
        {
            VerifyingPasswordModel model = new VerifyingPasswordModel();
            
            return View(model);
        }

        [HttpPost]
        public ActionResult VerifyingPassword(Guid id, VerifyingPasswordModel model)
        {
           
            var Account = _unitOfWork.AccountRepository.GetById(id);
            if (Account != null && model.Passw== model.confirmPassw && model.Passw!=null)
            {
                Account.Passw = _encrypt.EncryptKey(model.Passw);
                _unitOfWork.Save();
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }
        public ActionResult PasswordRecovery()
        {
            return View(new PasswordRecoveryModel());
        }
        [HttpPost]
        public ActionResult PasswordRecovery(string username)
        {
            PasswordRecoveryModel modelo = new PasswordRecoveryModel();
            modelo.Email = username;
            var Account = new Account();
            IEnumerable<Account> list =
                _unitOfWork.AccountRepository.Get(x => x.Email == modelo.Email);
            if (list.Any())
                Account = list.First();
            else
                Account = null;
            
            if (Account != null)
            {
                var host = HttpContext.Request.Url.Host;
                if (host == "localhost")
                    host = Request.Url.GetLeftPart(UriPartial.Authority);
                PasswordRecovering.SendEmail(Account.Email, "Restablezca su contraseña en el siguiente Link: " + host + "/Account/VerifyingPassword/" + Account.Id, "Restablecer Contraseña");
               
                ViewBag.Message = "Se envio un correo electronico con las intrucciones para restablecer su contraseña";

                return View(modelo);
            }
            ViewBag.Message = "El usuario con el correo "+ username.ToString() +"no existe";
            return View(modelo);
        }

       

      
    }
}
