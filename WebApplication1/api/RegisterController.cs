using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using Stackoverflow_CGVM.Data;
using Stackoverflow_CGVM.Domain.Entities;
using Stackoverflow_CGVM.Domain.Services;
using WebApplication1.Controllers;

namespace WebApplication1.api
{
    public class RegisterController:ApiController
    {
        private Encriptar _encrypt = new Encriptar();

        public HttpResponseMessage PostRegister(AccountRegisterModel model)
        {
            var context = new StackoverflowContext();
            model.Passw = _encrypt.EncryptKey(model.Passw);
            model.ConfirmPassword = _encrypt.EncryptKey(model.ConfirmPassword);
            bool modelIsValid = !model.Email.IsNullOrWhiteSpace() && !model.Name.IsNullOrWhiteSpace()
                                && !model.Passw.IsNullOrWhiteSpace();
            var existent = context.Accounts.FirstOrDefault(x => x.Email == model.Email);
            if (modelIsValid && model.ConfirmPassword == model.Passw && existent == null)
            {
                Account newAccount = Mapper.Map<AccountRegisterModel, Account>(model);
                newAccount.confirmed = false;
                newAccount.RegistrationDate = DateTime.Now.ToString();
                newAccount.LastSeen = DateTime.Now.ToString();
                newAccount.Vistas = 0;
                context.Accounts.Add(newAccount);
                context.SaveChanges();
                
                PasswordRecovering.SendEmail(newAccount.Email,
                    "Porfavor confirme su cuenta para hacer Login en el siguiente Link: " + 
                    "http://localhost:1885/Account/ConfirmarCuenta/" + newAccount.Id, "Confirmando Cuenta");


                HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.Created, model);

                return response;
            }
            throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest));
        }
    }
}