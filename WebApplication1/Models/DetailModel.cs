using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class DetailModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public Guid Id { get;  set; }
        public int Votes { get; set; }
        public DateTime CreationDate  { get; set; }
        [Required]
        public string AnswerDescription { get; set; }
        public string CommentDescription { get; set; }
        public int Vistas { get; set; }
    }
}