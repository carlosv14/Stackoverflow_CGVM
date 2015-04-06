using System;

namespace Stackoverflow_CGVM.Phone
{
    public class AnswerCreateModel
    {
       
        public string AnswerDescription { get; set; }
        public Guid QuestionId { get; set; }
        public string token { get; set; }
    }
}