using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Linq.Expressions;
using MongoDbGenericRepository.Models;

namespace MongoDbGenericRepository
{
    public abstract class BaseMongoRepository
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        /// <summary>
        /// The base constructor
        /// </summary>
        /// <param name="connectionString">The connection string of the MongoDb server.</param>
        /// <param name="databaseName">The name of the database against which you want to perform operations.</param>
        protected BaseMongoRepository(string connectionString, string databaseName)
        {
            _mongoDbContext = new MongoDbContext(connectionString, databaseName);
        }

        protected IMongoDbContext _mongoDbContext = null;

        #region Get
        /// <summary>
        /// A generic GetOne method
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TDocument> GetOne<TDocument>(string id) where TDocument : IDocument
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", id);
            return await GetOne<TDocument>(filter);
        }

        /// <summary>
        /// A generic GetOne method
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TDocument> GetOne<TDocument>(FilterDefinition<TDocument> filter) where TDocument : IDocument
        {
            return await GetCollection<TDocument>().Find(filter).FirstOrDefaultAsync();
        }

        /// <summary>
        /// A generic get many method with filter
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <returns></returns>
        public async Task<List<TDocument>> GetAll<TDocument>(FilterDefinition<TDocument> filter) where TDocument : IDocument
        {
            return await GetCollection<TDocument>().Find(filter).ToListAsync();
        }

        /// <summary>
        /// FindCursor
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter"></param>
        /// <returns>A cursor for the query</returns>
        public IFindFluent<TDocument, TDocument> FindCursor<TDocument>(FilterDefinition<TDocument> filter) where TDocument : IDocument
        {
            var collection = GetCollection<TDocument>();
            var cursor = collection.Find(filter);
            return cursor;
        }

        /// <summary>
        /// A generic get all method
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <returns></returns>
        public async Task<List<TDocument>> GetAll<TDocument>() where TDocument : IDocument
        {
            var collection = GetCollection<TDocument>();
            return await collection.Find(new BsonDocument()).ToListAsync();
        }

        /// <summary>
        /// A generic Exists method
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> Exists<TDocument>(string id) where TDocument : IDocument
        {
            var collection = GetCollection<TDocument>();
            var query = new BsonDocument("Id", id);
            var cursor = collection.Find(query);
            var count = await cursor.CountAsync();
            return (count > 0);
        }

        /// <summary>
        /// A generic count method
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<long> Count<TDocument>(string id) where TDocument : IDocument
        {
            var filter = new FilterDefinitionBuilder<TDocument>().Eq("Id", id);
            return await Count<TDocument>(filter);
        }

        /// <summary>
        /// A generic count method
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<long> Count<TDocument>(FilterDefinition<TDocument> filter) where TDocument : IDocument
        {
            var collection = GetCollection<TDocument>();
            var cursor = collection.Find(filter);
            var count = await cursor.CountAsync();
            return count;
        }

        /// <summary>
        /// A generic count method
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<long> CountAsync<TDocument>(Expression<Func<TDocument, bool>> filter) where TDocument : IDocument
        {
            var collection = GetCollection<TDocument>();
            var cursor = collection.Find(filter);
            var count = await cursor.CountAsync();
            return count;
        }

        /// <summary>
        /// A generic count method
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public long Count<TDocument>(Expression<Func<TDocument, bool>> filter) where TDocument : IDocument
        {
            return GetCollection<TDocument>().Find(filter).Count();
        }

        /// <summary>
        /// Returns a list of projected objects
        /// </summary>
        /// <typeparam name="TDocument">T is a DbEntity</typeparam>
        /// <typeparam name="TProjection"></typeparam>
        /// <returns></returns>
        public async Task<TProjection> ProjectBy<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection)
            where TDocument : IDocument
            where TProjection : class, new()
        {
            return await GetCollection<TDocument>().Find(Builders<TDocument>.Filter.Where(filter))
                                                   .Project(projection)
                                                   .FirstOrDefaultAsync();
        }

        #endregion Get

        #region Create
        /// <summary>
        /// A generic Add One method async
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task AddOneAsync<TDocument>(TDocument item) where TDocument : IDocument
        {
            await GetCollection<TDocument>().InsertOneAsync(item);
        }

        /// <summary>
        /// A generic method to add a document
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public void AddOne<TDocument>(TDocument item) where TDocument : IDocument
        {
            if (item.Id == default(Guid))
            {
                item.Id = Guid.NewGuid();
            }

            if (item.AddedAtUtc == default(DateTime))
            {
                item.AddedAtUtc = DateTime.UtcNow;
            }

            GetCollection<TDocument>().InsertOne(item);
        }

        /// <summary>
        /// A generic Add Many method, performs a bulk insert in mongo
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task AddManyAsync<TDocument>(IEnumerable<TDocument> items) where TDocument : IDocument
        {
            await GetCollection<TDocument>().InsertManyAsync(items);
        }

        /// <summary>
        /// A generic Add Many method, performs a bulk insert in mongo
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public void AddMany<TDocument>(IEnumerable<TDocument> items) where TDocument : IDocument
        {
            GetCollection<TDocument>().InsertMany(items);
        }

        #endregion Create

        #region Delete

        /// <summary>
        /// A generic delete one method
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<long> DeleteOneAsync<TDocument>(TDocument document) where TDocument : IDocument
        {
            return await DeleteOneAsync<TDocument>(x => x.Id == document.Id);
        }

        /// <summary>
        /// A generic delete one method
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public long DeleteOne<TDocument>(TDocument document) where TDocument : IDocument
        {
            return DeleteOne<TDocument>(x => x.Id == document.Id);
        }

        /// <summary>
        /// A generic delete one method
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public long DeleteOne<TDocument>(Expression<Func<TDocument, bool>> filter) where TDocument : IDocument
        {
            return GetCollection<TDocument>().DeleteOne(filter).DeletedCount;
        }

        /// <summary>
        /// A generic delete one method
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<long> DeleteOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter) where TDocument : IDocument
        {
            return (await GetCollection<TDocument>().DeleteOneAsync(filter)).DeletedCount;
        }

        /// <summary>
        /// A generic delete many method
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<long> DeleteMany<TDocument>(Expression<Func<TDocument, bool>> filter) where TDocument : IDocument
        {
            var deleteRes = await GetCollection<TDocument>().DeleteManyAsync(filter);
            return deleteRes.DeletedCount;
        }
        #endregion Delete

        #region Update

        /// <summary>
        /// Updates a document
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateOneAsync<TDocument>(TDocument entity) where TDocument : IDocument
        {
            var updateRes = await GetCollection<TDocument>().ReplaceOneAsync(x => x.Id == entity.Id, entity);
            return updateRes.ModifiedCount < 1;
        }

        /// <summary>
        /// UpdateOne with filter
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        private async Task<long> UpdateOne<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update) where TDocument : IDocument
        {
            var updateRes = await GetCollection<TDocument>().UpdateOneAsync(filter, update);
            return updateRes.ModifiedCount;
        }

        /// <summary>
        /// UpdateMany with filter
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<long> UpdateMany<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update) where TDocument : IDocument
        {
            var collection = GetCollection<TDocument>();
            var updateRes = await collection.UpdateManyAsync(filter, update);
            return updateRes.ModifiedCount;
        }
        #endregion Update

        #region Find And Update

        /// <summary>
        /// GetAndUpdateOne with filter
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<TDocument> GetAndUpdateOne<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update, FindOneAndUpdateOptions<TDocument, TDocument> options) where TDocument : IDocument
        {
            return await GetCollection<TDocument>().FindOneAndUpdateAsync(filter, update, options);
        }

        #endregion Find And Update


        /// <summary>
        /// The private GetCollection method
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <returns></returns>
        private IMongoCollection<TDocument> GetCollection<TDocument>(TDocument document) where TDocument : IDocument
        {
            return _mongoDbContext.GetCollection<TDocument>(document);
        }

        /// <summary>
        /// The private GetCollection method
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <returns></returns>
        private IMongoCollection<TDocument> GetCollection<TDocument>() where TDocument : IDocument
        {
            return _mongoDbContext.GetCollection<TDocument>();
        }
    }
}