using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebGrease.Css.ImageAssemblyAnalysis.LogModel;

namespace WebApplication1.Controllers.CostumeDataNotation
{
    public class PasswordAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var stringvalue = value.ToString();
                var stringvalue2 = stringvalue.ToLower();
                if (stringvalue.Equals(stringvalue2))
                    return false;

                return true;
            }
            return false;
        }

    }
}