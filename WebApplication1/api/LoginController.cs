using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using Stackoverflow_CGVM.Data;
using Stackoverflow_CGVM.Domain.Entities;
using WebApplication1.Models;

namespace WebApplication1.api
{
    public class LoginController :ApiController
    {
        private Encriptar _encrypt = new Encriptar();

        public string PostLogin(LoginModel model)
        {
            var password = _encrypt.EncryptKey(model.Passw);
            var context = new StackoverflowContext();
            model.Passw = password;
            if (!model.Email.IsNullOrWhiteSpace() && !model.Passw.IsNullOrWhiteSpace())
            {
                Account account = context.Accounts.FirstOrDefault(x => x.Email == model.Email && x.Passw == model.Passw);
                if (account != null)
                {
                    Sessions session = new Sessions();
                    session.Token = _encrypt.EncryptKey(model.Email);
                    session.Token += _encrypt.EncryptKey(model.Passw);
                    session.whosLoggedId = account.Id;

                    context.sessions.Add(session);
                    context.SaveChanges();
                    HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.Created, model);

                    return session.Token;
                }
                return null;
            }
            return null;
        }
    }
    }
