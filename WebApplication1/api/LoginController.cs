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
        public HttpResponseMessage PostLogin(LoginModel model)
        {
            var _unitOfWork = new UnitOfWork();
            if (!model.Email.IsNullOrWhiteSpace() && !model.Passw.IsNullOrWhiteSpace())
            {
                Account account = _unitOfWork.AccountRepository.Get(x => x.Email == model.Email && x.Passw == model.Passw).First();
                if (account != null)
                {
                    HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.Created, model);

                    return response;
                }
            }
            throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest));
        }
    }
    }
