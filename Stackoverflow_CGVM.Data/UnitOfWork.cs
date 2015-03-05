using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stackoverflow_CGVM.Domain.Entities;

namespace Stackoverflow_CGVM.Data
{
    public class UnitOfWork:IUnitOfWork
    {
        private StackoverflowContext context = new StackoverflowContext();
        private Repository<Account> _accountRepository;
        private Repository<Question> questionRepository;
        private Repository<Answer> answerRepository;

        public Repository<Account> AccountRepository
        {
            get
            {

                if (this._accountRepository== null)
                {
                    this._accountRepository = new Repository<Account>(context);
                }
                return _accountRepository;
            }
        }

        public Repository<Question> QuestionRepository
        {
            get
            {

                if (this.questionRepository == null)
                {
                    this.questionRepository = new Repository<Question>(context);
                }
                return questionRepository;
            }
        }
        public Repository<Answer> AnswerRepository
        {
            get
            {

                if (this.answerRepository == null)
                {
                    this.answerRepository = new Repository<Answer>(context);
                }
                return answerRepository;
            }
        }


        public void Save()
        {
            context.SaveChanges();
        }


    }
}
