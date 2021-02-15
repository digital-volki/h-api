using System.Collections.Generic;
using System.Data.Entity;

namespace Leifez.DataAccess.Interfaces
{
    public interface IDataContext
    {
        Database GetDatabase();

        T Insert<T>(T item) where T : class, new();

        IEnumerable<T> InsertMany<T>(IEnumerable<T> items) where T : class, new();

        T Update<T>(T item) where T : class, new();

        int Save();

        T Delete<T>(T item) where T : class, new();

        IEnumerable<T> DeleteRange<T>(IEnumerable<T> item) where T : class, new();

        ICollection<T> SqlQuery<T>(string sql, params object[] parameters);

        void SqlCommand(string sql, params object[] parameters);
    }
}
