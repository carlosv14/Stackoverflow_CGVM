using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class AnswerListModel
    {
        public int Votes { get; set; }
        public DateTime CreationTime { get; set; }
        public string OwnerName { get; set; }
        public Guid OwnerId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid AnswerId { get; set; }

        public string name { get; set; }
    }
}