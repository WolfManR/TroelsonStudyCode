using AutoLotDAL_Core2.EF;
using AutoLotDAL_Core2.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AutoLotDAL_Core2.Repos
{
    public class BaseRepo<T> : IDisposable, IRepo<T> where T : EntityBase, new()
    {
        private readonly DbSet<T> _table;
        private readonly AutoLotContext _db;
        protected AutoLotContext Context => _db;

        public BaseRepo() : this(new AutoLotContext())
        {

        }
        public BaseRepo(AutoLotContext context)
        {
            _db = context;
            _table = _db.Set<T>();
        }

        public void Dispose()
        {
            _db?.Dispose();
        }


        public int Add(T entity)
        {
            _table.Add(entity);
            return SaveChanges();
        }
        public int Add(IList<T> entities)
        {
            _table.AddRange(entities);
            return SaveChanges();
        }

        public int Update(T entity)
        {
            _table.Update(entity);
            return SaveChanges();
        }
        public int Update(IList<T> entities)
        {
            _table.UpdateRange(entities);
            return SaveChanges();
        }

        public int Delete(int id, byte[] timeStamp)
        {
            _db.Entry(new T() { Id = id, Timestamp = timeStamp }).State = EntityState.Deleted;
            return SaveChanges();
        }
        public int Delete(T entity)
        {
            _db.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();
        }

        public T GetOne(int? id) => _table.Find(id);

        public List<T> GetSome(Expression<Func<T, bool>> where) => _table.Where(where).ToList();

        public virtual List<T> GetAll() => _table.ToList();
        public List<T> GetAll<TSortField>(Expression<Func<T, TSortField>> orderBy, bool ascending) => (ascending ? _table.OrderBy(orderBy) : _table.OrderByDescending(orderBy)).ToList();

        public List<T> ExecuteQuery(string sql) => _table.FromSqlRaw(sql).ToList();
        public List<T> ExecuteQuery(string sql, object[] sqlParametersObjects) => _table.FromSqlRaw(sql, sqlParametersObjects).ToList();

        private int SaveChanges()
        {
            try
            {
                return _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Генерируется, когда возникла ошибка, связанная с параллелизмом.
                throw;
            }
            catch (RetryLimitExceededException ex)
            {
                // Генерируется, когда достигнуто максимальное количество попыток.
                // Дополнительные детали можно найти во внутреннем исключении(исключениях).
                throw;
            }
            catch (DbUpdateException ex)
            {
                // Генерируется, когда обновление базы данных потерпело неудачу.
                // Дополнительные детали и затронутые объекты можно найти во внутреннем исключении(исключениях).
                throw;
            }
            catch (Exception ex)
            {
                // Случилось что-то ещё
                throw;
            }
        }
    }
}
