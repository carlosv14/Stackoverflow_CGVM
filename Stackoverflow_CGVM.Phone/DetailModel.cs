using System;

namespace Stackoverflow_CGVM.Phone
{
    public class DetailModel
    {
       
        public string Title { get; set; }
        
        public string Description { get; set; }
        public Guid Id { get;  set; }
        public int Votes { get; set; }
        public DateTime CreationDate  { get; set; }
        
        public string AnswerDescription { get; set; }
        public string CommentDescription { get; set; }
        public int Vistas { get; set; }
    }
}