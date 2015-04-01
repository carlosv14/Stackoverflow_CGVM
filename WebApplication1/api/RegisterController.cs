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
using WebApplication1.Controllers;

namespace WebApplication1.api
{
    public class RegisterController:ApiController
    {
          public HttpResponseMessage PostRegister(AccountRegisterModel model)
          {
              var context = new StackoverflowContext();
            bool modelIsValid = model != null && !model.Email.IsNullOrWhiteSpace() && !model.Name.IsNullOrWhiteSpace()
                                && !model.Passw.IsNullOrWhiteSpace();
            if (modelIsValid && model.ConfirmPassword == model.Passw)
            {
                Account newAccount = Mapper.Map<AccountRegisterModel, Account>(model);
                context.Accounts.Add(newAccount);
                context.SaveChanges();

                HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.Created, model);

                return response;
            }
            throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest));
        }
    }
}