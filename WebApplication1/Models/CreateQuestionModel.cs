using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CreateQuestionModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

    }
}