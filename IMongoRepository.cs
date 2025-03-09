using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tawazun.Framework.Models;

namespace Tawazun.Framework.Mongo
{
    public interface IMongoRepository<T> where T : BaseModel
    {
        IMongoCollection<T> Collection { get; }
        IQueryable<T> QueryableCollection {  get; }

        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter);
        Task AddAsync(T entity);
        Task UpdateAsync(string id, T entity);
        Task DeleteAsync(string id);
    }
}
