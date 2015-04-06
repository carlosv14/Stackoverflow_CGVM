using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class AnswerCreateModel
    {
        [Required(ErrorMessage = "Required")]
        [StringLength(250, ErrorMessage = "Answer must be between 5 and 250 characters", MinimumLength = 5)]
        public string AnswerDescription { get; set; }
        public Guid QuestionId { get; set; }

        public string token { get; set; }
    }
}