using Leifez.Core.PostgreSQL.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Leifez.Common.Configuration;

namespace Leifez.Core.PostgreSQL
{
    public class DataContext : IdentityDbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder
                .UseNpgsql(AppConfiguration.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]));
        }

        public IQueryable<T> GetQueryable<T>(bool trackChanges = true, bool disabled = false) where T : class, new()
        {
            return GetQueryable<T>(null, trackChanges);
        }

        private IQueryable<T> GetQueryable<T>(Expression<Func<T, bool>> expression, bool trackChanges = true)
            where T : class, new()
        {
            var query = GetQueryableNonAudit(expression, trackChanges);

            return query;
        }

        private IQueryable<T> GetQueryableNonAudit<T>(Expression<Func<T, bool>> expression, bool trackChanges = true)
            where T : class, new()
        {
            var query = trackChanges
                ? Set<T>().AsQueryable()
                : Set<T>().AsNoTracking();

            if (expression != null)
            {
                query = query.Where(expression);
            }

            return query;
        }

        public ICollection<T> SqlQuery<T>(string sql, params object[] parameters)
        {
            return SqlQuery<T>(sql, parameters).ToList();
        }

        public T Delete<T>(T item) where T : class, new()
        {
            return Set<T>().Remove(item).Entity;
        }

        public void SqlCommand(string sql, params object[] parameters)
        {
            SqlCommand(sql, parameters);
        }

        public void DeleteRange<T>(IEnumerable<T> item) where T : class, new()
        {
            Set<T>().RemoveRange(item);
        }

        public T Insert<T>(T item) where T : class, new()
        {
            return PerformAction(item, EntityState.Added);
        }

        public IEnumerable<T> InsertMany<T>(IEnumerable<T> items) where T : class, new()
        {
            var result = new List<T>();
            foreach (var item in items)
            {
                result.Add(PerformAction(item, EntityState.Added));
            }
            return result;
        }

#pragma warning disable CS0114 // Член скрывает унаследованный член: отсутствует ключевое слово переопределения
        public T Update<T>(T item) where T : class, new()
#pragma warning restore CS0114 // Член скрывает унаследованный член: отсутствует ключевое слово переопределения
        {
            return PerformAction(item, EntityState.Modified);
        }

        protected virtual TItem PerformAction<TItem>(TItem item, EntityState entityState) where TItem : class, new()
        {
            Entry(item).State = entityState;
            return item;
        }

        public int Save()
        {
            int changes = 0;
            try
            {
                changes = SaveChanges();
            }
            catch (Exception e)
            {

                throw;
            }
            return changes;
        }
    }
}
