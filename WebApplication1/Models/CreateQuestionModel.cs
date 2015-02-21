using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CreateQuestionModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }
    }
}