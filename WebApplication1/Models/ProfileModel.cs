using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stackoverflow_CGVM.Domain.Entities;

namespace WebApplication1.Models
{
    public class ProfileModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string RegistrationDate { get; set; }
        public int Vistas { get; set; }
        public string LastSeen { get; set; }

        public IEnumerable<Question> preguntas { get; set; }
        public IEnumerable<Answer> respuestas { get; set; } 
    }
}