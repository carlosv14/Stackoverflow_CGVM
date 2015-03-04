using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Stackoverflow_CGVM.Data
{
    class Repository<TEntity> : IRepository<TEntity> where TEntity:class
    {
        internal StackoverflowContext Context;
        internal DbSet<TEntity> DbSet;
         public Repository(StackoverflowContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(object id)
        {
            return DbSet.Find(id);
        }

        public void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        public void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
