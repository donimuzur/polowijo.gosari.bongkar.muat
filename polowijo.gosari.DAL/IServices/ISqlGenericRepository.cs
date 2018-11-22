﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace polowijo.gosari.DAL.IServices
{
    public interface ISqlGenericRepository<TEntity>
        where TEntity : class
    {
        void Delete(object id);
        void Delete(TEntity entityToDelete);

        System.Collections.Generic.IEnumerable<TEntity> Get(
            System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null,
            Func<System.Linq.IQueryable<TEntity>, System.Linq.IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        TEntity GetByID(object id);
        TEntity GetByID(params object[] keyValues);
        int Count(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null);
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
        void InsertOrUpdate(TEntity entity);
        bool Exists(TEntity entity);
        void Detach(TEntity entity);

        IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate = null);

        void ExecuteSql(string sql);

        void ExecuteQuery(string sql);
    }
}
