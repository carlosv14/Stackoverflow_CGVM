using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stackoverflow_CGVM.Domain.Entities
{
    public class Question : IEntity
    {
        public Guid Id { get; private set; }

        public Question()
        {
            Guid.NewGuid();
        }

        public int Votes { get; set; }
        public string Description { get; set; }
        public Account Owner { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }




    }

}
