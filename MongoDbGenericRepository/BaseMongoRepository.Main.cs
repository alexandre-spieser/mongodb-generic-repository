using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDbGenericRepository.Models;
using MongoDbGenericRepository.Utils;

namespace MongoDbGenericRepository
{
    /// <summary>
    ///     The base Repository, it is meant to be inherited from by your custom custom MongoRepository implementation.
    ///     Its constructor must be given a connection string and a database name.
    /// </summary>
    public abstract partial class BaseMongoRepository : ReadOnlyMongoRepository, IBaseMongoRepository
    {
        /// <summary>
        ///     The constructor taking a connection string and a database name.
        /// </summary>
        /// <param name="connectionString">The connection string of the MongoDb server.</param>
        /// <param name="databaseName">The name of the database against which you want to perform operations.</param>
        protected BaseMongoRepository(string connectionString, string databaseName = null) : base(connectionString, databaseName)
        {
        }

        /// <summary>
        ///     The constructor taking a <see cref="IMongoDbContext" />.
        /// </summary>
        /// <param name="mongoDbContext">A mongodb context implementing <see cref="IMongoDbContext" /></param>
        protected BaseMongoRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {
        }

        /// <summary>
        ///     The constructor taking a <see cref="IMongoDatabase" />.
        /// </summary>
        /// <param name="mongoDatabase">A mongodb context implementing <see cref="IMongoDatabase" /></param>
        protected BaseMongoRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }

        /// <inheritdoc />
        public virtual async Task<List<TDocument>> GetPaginatedAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            int skipNumber = 0,
            int takeNumber = 50,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument
        {
            return await HandlePartitioned<TDocument>(partitionKey).Find(filter).Skip(skipNumber).Limit(takeNumber).ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<List<TDocument>> GetPaginatedAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            int skipNumber = 0,
            int takeNumber = 50,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await HandlePartitioned<TDocument, TKey>(partitionKey).Find(filter).Skip(skipNumber).Limit(takeNumber).ToListAsync(cancellationToken);
        }

        /// <summary>
        ///     Sets the value of the document Id if it is not set already.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="document">The document.</param>
        protected void FormatDocument<TDocument, TKey>(TDocument document)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var defaultTKey = default(TKey);
            if (document.Id == null
                || (defaultTKey != null
                    && defaultTKey.Equals(document.Id)))
            {
                document.Id = IdGenerator.GetId<TKey>();
            }
        }

        /// <summary>
        ///     Sets the value of the document Id if it is not set already.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="document">The document.</param>
        protected void FormatDocument<TDocument>(TDocument document)
            where TDocument : IDocument
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (document.Id == default)
            {
                document.Id = Guid.NewGuid();
            }
        }

        /// <summary>
        ///     Gets a collections for a potentially partitioned document type.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="partitionKey">The collection partition key.</param>
        /// <returns></returns>
        protected virtual IMongoCollection<TDocument> HandlePartitioned<TDocument>(string partitionKey)
            where TDocument : IDocument<Guid>
        {
            if (!string.IsNullOrEmpty(partitionKey))
            {
                return GetCollection<TDocument>(partitionKey);
            }

            return GetCollection<TDocument>();
        }

        /// <summary>
        ///     Gets a collections for the type TDocument with a partition key.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="partitionKey">The collection partition key.</param>
        /// <returns></returns>
        protected virtual IMongoCollection<TDocument> GetCollection<TDocument>(string partitionKey = null)
            where TDocument : IDocument<Guid>
        {
            return MongoDbContext.GetCollection<TDocument>(partitionKey);
        }

        #region Find And Update
        /// <inheritdoc />
        public virtual async Task<TDocument> GetAndUpdateOne<TDocument>(
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> update,
            FindOneAndUpdateOptions<TDocument, TDocument> options)
            where TDocument : IDocument
        {
            return await GetAndUpdateOne(filter, update, options, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<TDocument> GetAndUpdateOne<TDocument>(
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> update,
            FindOneAndUpdateOptions<TDocument, TDocument> options,
            CancellationToken cancellationToken)
            where TDocument : IDocument
        {
            return await GetCollection<TDocument>().FindOneAndUpdateAsync(filter, update, options, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<TDocument> GetAndUpdateOne<TDocument, TKey>(
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> update,
            FindOneAndUpdateOptions<TDocument, TDocument> options)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetAndUpdateOne<TDocument, TKey>(filter, update, options, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<TDocument> GetAndUpdateOne<TDocument, TKey>(
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> update,
            FindOneAndUpdateOptions<TDocument, TDocument> options,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetCollection<TDocument, TKey>().FindOneAndUpdateAsync(filter, update, options, cancellationToken);
        }

        #endregion Find And Update
    }
}