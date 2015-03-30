using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stackoverflow_CGVM.Domain.Entities;

namespace Stackoverflow_CGVM.Data
{
    public class StackoverflowContext:DbContext
    {
        public StackoverflowContext() : base(ConnectionString.get())
        {
            
        }
        public DbSet<Answer> Answers { get; set; } 
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Vote> Votes { get; set; }
        
    }

    public static class ConnectionString
    {
        public static string get()
        {
            var Environment = ConfigurationManager.AppSettings["Environment"];
            return string.Format("name={0}", Environment);
        
        } 
    }
}
