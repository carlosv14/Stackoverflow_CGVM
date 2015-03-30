using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stackoverflow_CGVM.Domain.Entities
{
    public class Vote : IEntity
    {
        public Guid Id { get; private set; }

        public Guid OwnerID { get; set; }
        public Guid QorA_ID { get; set; }
        public Vote()
        {
            Id = Guid.NewGuid();
        }

    }
}
