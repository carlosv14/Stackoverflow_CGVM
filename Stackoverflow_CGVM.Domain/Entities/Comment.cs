using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stackoverflow_CGVM.Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; private set; }
        public virtual Account Owner { get; set; }
        public DateTime CreationDate { get; set; }
        public string CommentDescription { get; set; }
        public Guid QorAId { get; set; }
        public Comment()
        {
            Id = Guid.NewGuid();
        }

    }
    }

