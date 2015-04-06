using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Stackoverflow_CGVM.Domain.Entities
{
    public class Sessions
    {
        public Guid Id { get; private set; }
        public string Token { get; set; }

        public Guid whosLoggedId { get; set; }
        public Sessions()
        {
            Id = Guid.NewGuid();
        }
    }
}
