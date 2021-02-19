using Leifez.DataAccess.Interfaces;
using Leifez.DataAccess.PostgreSQL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
//using Microsoft.EntityFrameworkCore.Storage;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;

namespace Leifez.DataAccess.PostgreSQL
{
    public class DataContext : IdentityDbContext, IDataContext
    {
        private readonly string _connectionString;

        public DataContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DataContext(string connectionString)
        {
            _connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql();
            base.OnConfiguring(optionsBuilder);
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

        //public Database GetDatabase()
        //{
        //    return Database;
        //}

        //public ICollection<T> SqlQuery<T>(string sql, params object[] parameters)
        //{
        //    return Database.SqlQuery<T>(sql, parameters).ToList();
        //}

        //private IQueryable<T> GetQueryable<T>(Expression<Func<T, bool>> expression, bool trackChanges = true)
        //    where T : class, new()
        //{
        //    var query = GetQueryableNonAudit(expression, trackChanges);

        //    return query;
        //}

        //private IQueryable<T> GetQueryableNonAudit<T>(Expression<Func<T, bool>> expression, bool trackChanges = true)
        //    where T : class, new()
        //{
        //    var query = trackChanges
        //        ? Set<T>().AsQueryable()
        //        : Set<T>().AsNoTracking();

        //    if (expression != null)
        //    {
        //        query = query.Where(expression);
        //    }

        //    return query;
        //}

        //public T Delete<T>(T item) where T : class, new()
        //{
        //    return Set<T>().Remove(item);
        //}

        //public void SqlCommand(string sql, params object[] parameters)
        //{
        //    Database.ExecuteSqlCommand(sql, parameters);
        //}

        //public IEnumerable<T> DeleteRange<T>(IEnumerable<T> item) where T : class, new()
        //{
        //    return Set<T>().RemoveRange(item);
        //}

        //public T Insert<T>(T item) where T : class, new()
        //{
        //    return PerformAction(item, EntityState.Added);
        //}

        //public IEnumerable<T> InsertMany<T>(IEnumerable<T> items) where T : class, new()
        //{
        //    var result = new List<T>();
        //    foreach (var item in items)
        //    {
        //        result.Add(PerformAction(item, EntityState.Added));
        //    }
        //    return result;
        //}

        //public T Update<T>(T item) where T : class, new()
        //{
        //    return PerformAction(item, EntityState.Modified);
        //}

        //protected virtual TItem PerformAction<TItem>(TItem item, EntityState entityState) where TItem : class, new()
        //{
        //    Entry(item).State = entityState;
        //    return item;
        //}

        //public int Save()
        //{
        //    int changes = 0;
        //    try
        //    {
        //        changes = SaveChanges();
        //    }
        //    catch (Exception e)
        //    {

        //        throw;
        //    }
        //    return changes;
        //}

        //public DbAccount GetAccount(string query)
        //{
        //    using (IDbConnection dbConnection = Connection)
        //    {
        //        dbConnection.Open();

        //        string script = $"INSERT INTO trip_sms (trip_item_id,event_date,mobile_phone) VALUES " +
        //            $"({tripSms.TripItemId},Now(),'{tripSms.MobilePhone}')";

        //        try
        //        {
        //            dbConnection.Execute(script);
        //        }
        //        catch (Exception e)
        //        {
        //            _commonLog.Write(MessageLevel.Error, $"Ошибка сохранения информации об отправленном смс-сообщении.");
        //            _commonLog.Write(MessageLevel.Error, e);
        //        }
        //    }
        //}
    }
}
