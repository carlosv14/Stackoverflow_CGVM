using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stackoverflow_CGVM.Domain.Entities;

namespace Stackoverflow_CGVM.Data
{
    public class StackoverflowContext:DbContext
    {
        public StackoverflowContext() : base("Steckoverflow")
        {
            
        }
        public DbSet<Answer> Answers { get; set; } 
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Question> Questions { get; set; }
        
        
    }
}
