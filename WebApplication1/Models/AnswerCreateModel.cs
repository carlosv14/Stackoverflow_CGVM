using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class AnswerCreateModel
    {
        [Required]
        public string Description { get; set; }
        public Guid QuestionId { get; set; }
    }
}