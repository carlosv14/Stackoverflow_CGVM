using System;

namespace Stackoverflow_CGVM.Phone
{
    public class AnswerListModel
    {
        public int Votes { get; set; }
        public DateTime CreationTime { get; set; }
        public string OwnerName { get; set; }
        public Guid OwnerId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid AnswerId { get; set; }
        public bool Best { get; set; }
        public string name { get; set; }
        public string CommentDescription { get; set; }
        public string Description { get; set; }
       
    }
}