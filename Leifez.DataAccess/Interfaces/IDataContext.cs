using System.Collections.Generic;
using System.Linq;

namespace Leifez.DataAccess.Interfaces
{
    public interface IDataContext
    {
        //Database GetDatabase();

        IQueryable<T> GetQueryable<T>(bool trackChanges = true, bool disabled = false)
            where T : class, new();

        //T Insert<T>(T item) where T : class, new();

        //IEnumerable<T> InsertMany<T>(IEnumerable<T> items) where T : class, new();

        //T Update<T>(T item) where T : class, new();

        //int Save();

        //T Delete<T>(T item) where T : class, new();

        //IEnumerable<T> DeleteRange<T>(IEnumerable<T> item) where T : class, new();

        //ICollection<T> SqlQuery<T>(string sql, params object[] parameters);

        //void SqlCommand(string sql, params object[] parameters);
    }
}
