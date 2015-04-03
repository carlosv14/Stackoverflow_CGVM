using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CommentListModel
    {
        public DateTime CreationDate { get; set; }
        public string CommentDescription { get; set; }
        public string OwnerName { get; set; }
        public Guid OwnerId { get; set; }
        public Guid QorAId { get; set; }
        public Guid Id { get; set; }

    }
}