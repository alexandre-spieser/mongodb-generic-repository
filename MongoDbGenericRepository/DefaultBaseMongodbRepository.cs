using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Mongodb.Driver.Extensions.Abstractions;
using MongoDbGenericRepository;
using MongoDbGenericRepository.Models;

namespace Mongodb.Driver.Extensions
{
    public abstract class DefaultBaseMongodbRepository<TDocument> : BaseMongoRepository, IDefaultBaseMongodbRepository<TDocument>
        where TDocument : IDocument, new()
    {
        protected abstract string MongodbConnectionString { get; }
        protected virtual string DefaultCollectionName => GetDefaultCollectionName<TDocument>();

        #region ctor

        protected DefaultBaseMongodbRepository()
        {
            InitRepository();
        }

        /// <summary>
        ///     The constructor taking a connection string and a database name.
        /// </summary>
        /// <param name="connectionString">The connection string of the MongoDb server.</param>
        /// <param name="databaseName">The name of the database against which you want to perform operations.</param>
        protected DefaultBaseMongodbRepository(string connectionString, string databaseName = null) : base(
            connectionString, databaseName)
        {
        }

        /// <summary>
        ///     The contructor taking a <see cref="IMongoDbContext" />.
        /// </summary>
        /// <param name="mongodbContext">A mongodb context implementing <see cref="IMongoDbContext" /></param>
        protected DefaultBaseMongodbRepository(IMongoDbContext mongodbContext) : base(mongodbContext)
        {
        }

        /// <summary>
        ///     The contructor taking a <see cref="IMongoDatabase" />.
        /// </summary>
        /// <param name="mongodbDatabase">A mongodb context implementing <see cref="IMongoDatabase" /></param>
        protected DefaultBaseMongodbRepository(IMongoDatabase mongodbDatabase) : base(mongodbDatabase)
        {
        }


        protected void InitRepository()
        {
            if (string.IsNullOrEmpty(MongodbConnectionString))
                throw new ArgumentNullException(nameof(MongodbConnectionString),
                    "need to impl  protected abstract string MongodbConnectionString { get; }");
            var mongodbUrl = new MongoUrl(MongodbConnectionString);
            var databaseName = mongodbUrl.DatabaseName;
            SetupMongoDbContext(MongodbConnectionString, databaseName);
        }

        #endregion

        #region Insert

        /// <summary>
        ///     Asynchronously adds a document to the collection.
        ///     Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <param name="document">The document you want to add.</param>
        /// <param name="partitionKey">The collection partition key.</param>
        public virtual async Task InsertOneAsync(TDocument document, string partitionKey = null) => await AddOneAsync<TDocument>(document, partitionKey);

        /// <summary>
        ///     Adds a document to the collection.
        ///     Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <param name="document">The document you want to add.</param>
        /// <param name="partitionKey">The collection partition key.</param>
        public virtual void InsertOne(TDocument document, string partitionKey = null) => AddOne<TDocument>(document, partitionKey);

        /// <summary>
        ///     Asynchronously adds a list of documents to the collection.
        ///     Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <param name="documents">The documents you want to add.</param>
        /// <param name="partitionKey">The collection partition key.</param>
        public virtual async Task InsertManyAsync(IEnumerable<TDocument> documents, string partitionKey = null) => await AddManyAsync<TDocument>(documents, partitionKey);

        /// <summary>
        ///     Adds a list of documents to the collection.
        ///     Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <param name="documents">The documents you want to add.</param>
        /// <param name="partitionKey">The collection partition key.</param>
        public virtual void InsertMany(IEnumerable<TDocument> documents, string partitionKey = null) => AddMany<TDocument>(documents, partitionKey);

        #endregion

        #region Read

        /// <summary>
        ///     Asynchronously returns one document given its id.
        /// </summary>
        /// <param name="id">The Id of the document you want to get.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual async Task<TDocument> GetByIdAsync(Guid id, string partitionKey = null) => await GetByIdAsync<TDocument>(id, partitionKey);

        /// <summary>
        ///     Returns one document given its id.
        /// </summary>
        /// <param name="id">The Id of the document you want to get.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual TDocument GetById(Guid id, string partitionKey = null) => GetById<TDocument>(id, partitionKey);

        /// <summary>
        ///     Asynchronously returns one document given an expression filter.
        /// </summary>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual async Task<TDocument> FirstOrDefaultAsync(Expression<Func<TDocument, bool>> filter,
            string partitionKey = null) =>
            await GetOneAsync<TDocument>(filter, partitionKey);

        /// <summary>
        ///     Returns one document given an expression filter.
        /// </summary>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual TDocument FirstOrDefault(Expression<Func<TDocument, bool>> filter, string partitionKey = null) => GetOne<TDocument>(filter, partitionKey);


        /// <summary>
        ///     Returns a collection cursor.
        /// </summary>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual IFindFluent<TDocument, TDocument> GetCursor(Expression<Func<TDocument, bool>> filter,
            string partitionKey = null) =>
            GetCursor<TDocument>(filter, partitionKey);

        /// <summary>
        ///     Returns true if any of the document of the collection matches the filter condition.
        /// </summary>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual async Task<bool> AnyAsync(Expression<Func<TDocument, bool>> filter, string partitionKey = null) => await AnyAsync<TDocument>(filter, partitionKey);

        /// <summary>
        ///     Returns true if any of the document of the collection matches the filter condition.
        /// </summary>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual bool Any(Expression<Func<TDocument, bool>> filter, string partitionKey = null) => Any<TDocument>(filter, partitionKey);

        /// <summary>
        ///     Asynchronously returns a list of the documents matching the filter condition.
        /// </summary>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual async Task<List<TDocument>> QueryListAsync(Expression<Func<TDocument, bool>> filter, string partitionKey = null) => await GetAllAsync<TDocument>(filter, partitionKey);

        /// <summary>
        ///     Returns a list of the documents matching the filter condition.
        /// </summary>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual List<TDocument> QueryList(Expression<Func<TDocument, bool>> filter, string partitionKey = null) => GetAll<TDocument>(filter, partitionKey);

        /// <summary>
        ///     Asynchronously counts how many documents match the filter condition.
        /// </summary>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partitionKey</param>
        public virtual async Task<long> CountDocumentsAsync(Expression<Func<TDocument, bool>> filter, string partitionKey = null) => await CountAsync<TDocument>(filter, partitionKey);

        /// <summary>
        ///     Counts how many documents match the filter condition.
        /// </summary>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partitionKey</param>
        public virtual long CountDocuments(Expression<Func<TDocument, bool>> filter, string partitionKey = null) => Count<TDocument>(filter, partitionKey);

        #endregion
    }
}