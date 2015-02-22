using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stackoverflow_CGVM.Domain.Entities
{
    public class Answer
    {
          public Guid Id { get; private set; }


        public int Votes { get; set; }
        public string Description { get; set; }
        public virtual Account Owner { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

        public Answer()
        {

           Id= Guid.NewGuid();
        }

    }
}
