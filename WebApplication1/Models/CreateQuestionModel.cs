using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApplication1.Controllers.CostumeDataNotation;

namespace WebApplication1.Models
{
    public class CreateQuestionModel
    {
        [Required(ErrorMessage = "Required")]
        [QuestionTittleValidation(ErrorMessage = "Must be between 3 words and 50 characters long")]
        public string Title { get; set; }

          [Required(ErrorMessage = "Required")]
        [NewQuestionDescriptionValidation(ErrorMessage = "Must contain at least 5 words")]
        public string Description { get; set; }

          public string token { get; set; }

    }
}