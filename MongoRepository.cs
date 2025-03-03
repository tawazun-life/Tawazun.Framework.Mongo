using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Tawazun.Framework.Models;

namespace Tawazun.Framework.Mongo
{
    public class MongoRepository<T> : IMongoRepository<T> where T : BaseModel
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IMongoDatabase mongoDatabase)
        {
            var collectionName = typeof(T).Name;
            _collection = mongoDatabase.GetCollection<T>(collectionName);
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter)
        {
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(string id, T entity)
        {
            await _collection.ReplaceOneAsync(e => e.Id == id, entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(e => e.Id == id);
        }

        public IMongoCollection<T> Collection => _collection;
        public IQueryable<T> QueryableCollection => _collection.AsQueryable();
    }
}
