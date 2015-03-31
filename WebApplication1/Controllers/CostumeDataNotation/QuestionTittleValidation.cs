using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Controllers.CostumeDataNotation
{
    public class QuestionTittleValidation : ValidationAttribute
    {
        
            public override bool IsValid(object value)
            {
                if (value == null)
                    return false;
                var stringvalue = value.ToString();
                System.Text.RegularExpressions.MatchCollection wordColl = System.Text.RegularExpressions.Regex.Matches(stringvalue, @"[\S]+");
                System.Text.RegularExpressions.MatchCollection charColl = System.Text.RegularExpressions.Regex.Matches(stringvalue, @".");
                if (wordColl.Count < 3 || charColl.Count > 50)
                {
                    return false;
                }

                return true;
            }
        }
    }