using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Controllers.CostumeDataNotation
{
    public class ContainNumber : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var stringvalue = value.ToString();
                return stringvalue.Any(char.IsDigit);
            }
            return false;
        }
    }
}