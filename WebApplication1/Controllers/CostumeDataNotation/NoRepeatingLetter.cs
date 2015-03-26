using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Controllers.CostumeDataNotation
{
    public class NoRepeatingLetter : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            var stringvalue = value.ToString();
            stringvalue = stringvalue.ToLower();
        for(int i = 0; i<stringvalue.Length-1;i++)
        {
            if (stringvalue[i].Equals(stringvalue[i + 1]))
                return false;
        }
            
            return true;
        }
    }
}