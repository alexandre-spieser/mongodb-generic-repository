using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDbGenericRepository.Models;

namespace MongoDbGenericRepository
{
    /// <summary>
    ///     The base Repository, it is meant to be inherited from by your custom custom MongoRepository implementation.
    ///     Its constructor must be given a connection string and a database name.
    /// </summary>
    public abstract class ReadOnlyMongoRepository : ReadOnlyMongoRepository<Guid>, IReadOnlyMongoRepository
    {
        /// <summary>
        ///     The constructor taking a connection string and a database name.
        /// </summary>
        /// <param name="connectionString">The connection string of the MongoDb server.</param>
        /// <param name="databaseName">The name of the database against which you want to perform operations.</param>
        protected ReadOnlyMongoRepository(string connectionString, string databaseName = null) : base(connectionString, databaseName)
        {
        }

        /// <summary>
        ///     The constructor taking a <see cref="IMongoDbContext" />.
        /// </summary>
        /// <param name="mongoDbContext">A mongodb context implementing <see cref="IMongoDbContext" /></param>
        protected ReadOnlyMongoRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {
        }

        /// <summary>
        ///     The constructor taking a <see cref="IMongoDatabase" />.
        /// </summary>
        /// <param name="mongoDatabase">A mongodb context implementing <see cref="IMongoDatabase" /></param>
        protected ReadOnlyMongoRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }


        /// <summary>
        ///     Gets a collections for a potentially partitioned document type.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="document">The document.</param>
        /// <returns></returns>
        public virtual IMongoCollection<TDocument> HandlePartitioned<TDocument, TKey>(TDocument document)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.HandlePartitioned<TDocument, TKey>(document);
        }

        /// <summary>
        ///     Gets a collections for a potentially partitioned document type.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="partitionKey">The collection partition key.</param>
        /// <returns></returns>
        public virtual IMongoCollection<TDocument> HandlePartitioned<TDocument, TKey>(string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            if (!string.IsNullOrEmpty(partitionKey))
            {
                return GetCollection<TDocument, TKey>(partitionKey);
            }

            return GetCollection<TDocument, TKey>();
        }

        /// <summary>
        ///     Gets a collections for a potentially partitioned document type.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="document">The document.</param>
        /// <returns></returns>
        public virtual IMongoCollection<TDocument> HandlePartitioned<TDocument>(TDocument document)
            where TDocument : IDocument<Guid>
        {
            return MongoDbReader.HandlePartitioned<TDocument, Guid>(document);
        }

        /// <summary>
        ///     Gets a collections for the type TDocument with a partition key.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="partitionKey">The collection partition key.</param>
        /// <returns></returns>
        public virtual IMongoCollection<TDocument> GetCollection<TDocument, TKey>(string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetCollection<TDocument, TKey>(partitionKey);
        }

        #region Read TKey

        /// <inheritdoc />
        public virtual async Task<TDocument> GetByIdAsync<TDocument, TKey>(TKey id)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetByIdAsync<TDocument, TKey>(id, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<TDocument> GetByIdAsync<TDocument, TKey>(TKey id, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetByIdAsync<TDocument, TKey>(id, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<TDocument> GetByIdAsync<TDocument, TKey>(TKey id, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetByIdAsync<TDocument, TKey>(id, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<TDocument> GetByIdAsync<TDocument, TKey>(TKey id, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbReader.GetByIdAsync<TDocument, TKey>(id, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual TDocument GetById<TDocument, TKey>(TKey id)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetById<TDocument, TKey>(id, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual TDocument GetById<TDocument, TKey>(TKey id, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetById<TDocument, TKey>(id, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual TDocument GetById<TDocument, TKey>(TKey id, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetById<TDocument, TKey>(id, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual TDocument GetById<TDocument, TKey>(TKey id, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetById<TDocument, TKey>(id, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task<TDocument> GetOneAsync<TDocument, TKey>(FilterDefinition<TDocument> condition)
            where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return GetOneAsync<TDocument, TKey>(condition, null, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public Task<TDocument> GetOneAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return GetOneAsync<TDocument, TKey>(condition, null, null, cancellationToken);
        }

        /// <inheritdoc />
        public Task<TDocument> GetOneAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, FindOptions findOption)
            where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return GetOneAsync<TDocument, TKey>(condition, findOption, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public Task<TDocument> GetOneAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, string partitionKey)
            where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return GetOneAsync<TDocument, TKey>(condition, null, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public Task<TDocument> GetOneAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return GetOneAsync<TDocument, TKey>(condition, null, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task<TDocument> GetOneAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, FindOptions findOption, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return GetOneAsync<TDocument, TKey>(condition, findOption, null, cancellationToken);
        }

        /// <inheritdoc />
        public Task<TDocument> GetOneAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, FindOptions findOption, string partitionKey)
            where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return GetOneAsync<TDocument, TKey>(condition, findOption, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public Task<TDocument> GetOneAsync<TDocument, TKey>(
            FilterDefinition<TDocument> condition,
            FindOptions findOption,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetOneAsync<TDocument, TKey>(condition, findOption, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public TDocument GetOne<TDocument, TKey>(FilterDefinition<TDocument> condition)
            where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return GetOne<TDocument, TKey>(condition, null, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public TDocument GetOne<TDocument, TKey>(FilterDefinition<TDocument> condition, FindOptions findOption)
            where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return GetOne<TDocument, TKey>(condition, findOption, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public TDocument GetOne<TDocument, TKey>(FilterDefinition<TDocument> condition, string partitionKey)
            where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return GetOne<TDocument, TKey>(condition, null, partitionKey, CancellationToken.None);
        }


        /// <inheritdoc />
        public TDocument GetOne<TDocument, TKey>(FilterDefinition<TDocument> condition, FindOptions findOption, string partitionKey)
            where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return GetOne<TDocument, TKey>(condition, findOption, partitionKey, CancellationToken.None);
        }


        /// <inheritdoc />
        public TDocument GetOne<TDocument, TKey>(FilterDefinition<TDocument> condition, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return GetOne<TDocument, TKey>(condition, null, null, cancellationToken);
        }

        /// <inheritdoc />
        public TDocument GetOne<TDocument, TKey>(FilterDefinition<TDocument> condition, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return GetOne<TDocument, TKey>(condition, null, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public TDocument GetOne<TDocument, TKey>(FilterDefinition<TDocument> condition, FindOptions findOption, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return GetOne<TDocument, TKey>(condition, findOption, null, cancellationToken);
        }

        /// <inheritdoc />
        public TDocument GetOne<TDocument, TKey>(
            FilterDefinition<TDocument> condition,
            FindOptions findOption,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetOne<TDocument, TKey>(condition, findOption, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<TDocument> GetOneAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetOneAsync<TDocument, TKey>(filter, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<TDocument> GetOneAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetOneAsync<TDocument, TKey>(filter, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<TDocument> GetOneAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetOneAsync<TDocument, TKey>(filter, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<TDocument> GetOneAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbReader.GetOneAsync<TDocument, TKey>(filter, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual TDocument GetOne<TDocument, TKey>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetOne<TDocument, TKey>(filter, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual TDocument GetOne<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetOne<TDocument, TKey>(filter, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual TDocument GetOne<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetOne<TDocument, TKey>(filter, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual TDocument GetOne<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetOne<TDocument, TKey>(filter, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual IFindFluent<TDocument, TDocument> GetCursor<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetCursor<TDocument, TKey>(filter, partitionKey);
        }

        /// <inheritdoc />
        public Task<bool> AnyAsync<TDocument, TKey>(FilterDefinition<TDocument> condition)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return AnyAsync<TDocument, TKey>(condition, null, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public Task<bool> AnyAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return AnyAsync<TDocument, TKey>(condition, null, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public Task<bool> AnyAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, CountOptions countOption)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return AnyAsync<TDocument, TKey>(condition, countOption, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public Task<bool> AnyAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, CountOptions countOption, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return AnyAsync<TDocument, TKey>(condition, countOption, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public Task<bool> AnyAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return AnyAsync<TDocument, TKey>(condition, null, null, cancellationToken);
        }

        /// <inheritdoc />
        public Task<bool> AnyAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return AnyAsync<TDocument, TKey>(condition, null, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task<bool> AnyAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, CountOptions countOption, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return AnyAsync<TDocument, TKey>(condition, countOption, null, cancellationToken);
        }

        /// <inheritdoc />
        public Task<bool> AnyAsync<TDocument, TKey>(
            FilterDefinition<TDocument> condition,
            CountOptions countOption,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.AnyAsync<TDocument, TKey>(condition, countOption, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public bool Any<TDocument, TKey>(FilterDefinition<TDocument> condition)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return Any<TDocument, TKey>(condition, null, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public bool Any<TDocument, TKey>(FilterDefinition<TDocument> condition, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return Any<TDocument, TKey>(condition, null, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public bool Any<TDocument, TKey>(FilterDefinition<TDocument> condition, CountOptions countOption)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return Any<TDocument, TKey>(condition, countOption, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public bool Any<TDocument, TKey>(FilterDefinition<TDocument> condition, CountOptions countOption, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.Any<TDocument, TKey>(condition, countOption, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public bool Any<TDocument, TKey>(FilterDefinition<TDocument> condition, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return Any<TDocument, TKey>(condition, null, null, cancellationToken);
        }

        /// <inheritdoc />
        public bool Any<TDocument, TKey>(FilterDefinition<TDocument> condition, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return Any<TDocument, TKey>(condition, null, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public bool Any<TDocument, TKey>(FilterDefinition<TDocument> condition, CountOptions countOption, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return Any<TDocument, TKey>(condition, countOption, null, cancellationToken);
        }

        /// <inheritdoc />
        public bool Any<TDocument, TKey>(
            FilterDefinition<TDocument> condition,
            CountOptions countOption,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.Any<TDocument, TKey>(condition, countOption, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> AnyAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await AnyAsync<TDocument, TKey>(filter, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> AnyAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await AnyAsync<TDocument, TKey>(filter, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> AnyAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await AnyAsync<TDocument, TKey>(filter, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> AnyAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbReader.AnyAsync<TDocument, TKey>(filter, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool Any<TDocument, TKey>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.Any<TDocument, TKey>(filter, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool Any<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.Any<TDocument, TKey>(filter, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool Any<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return Any<TDocument, TKey>(filter, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool Any<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.Any<TDocument, TKey>(filter, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task<List<TDocument>> GetAllAsync<TDocument, TKey>(FilterDefinition<TDocument> condition)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetAllAsync<TDocument, TKey>(condition, null, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public Task<List<TDocument>> GetAllAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetAllAsync<TDocument, TKey>(condition, null, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public Task<List<TDocument>> GetAllAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, FindOptions findOption)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetAllAsync<TDocument, TKey>(condition, findOption, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public Task<List<TDocument>> GetAllAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, FindOptions findOption, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetAllAsync<TDocument, TKey>(condition, findOption, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public Task<List<TDocument>> GetAllAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetAllAsync<TDocument, TKey>(condition, null, null, cancellationToken);
        }

        /// <inheritdoc />
        public Task<List<TDocument>> GetAllAsync<TDocument, TKey>(
            FilterDefinition<TDocument> condition,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetAllAsync<TDocument, TKey>(condition, null, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task<List<TDocument>> GetAllAsync<TDocument, TKey>(
            FilterDefinition<TDocument> condition,
            FindOptions findOption,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetAllAsync<TDocument, TKey>(condition, findOption, null, cancellationToken);
        }

        /// <inheritdoc />
        public Task<List<TDocument>> GetAllAsync<TDocument, TKey>(
            FilterDefinition<TDocument> condition,
            FindOptions findOption,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetAllAsync<TDocument, TKey>(condition, findOption, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public List<TDocument> GetAll<TDocument, TKey>(FilterDefinition<TDocument> condition)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetAll<TDocument, TKey>(condition, null, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public List<TDocument> GetAll<TDocument, TKey>(FilterDefinition<TDocument> condition, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetAll<TDocument, TKey>(condition, null, null, cancellationToken);
        }

        /// <inheritdoc />
        public List<TDocument> GetAll<TDocument, TKey>(FilterDefinition<TDocument> condition, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetAll<TDocument, TKey>(condition, null, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public List<TDocument> GetAll<TDocument, TKey>(FilterDefinition<TDocument> condition, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetAll<TDocument, TKey>(condition, null, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public List<TDocument> GetAll<TDocument, TKey>(FilterDefinition<TDocument> condition, FindOptions findOption)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetAll<TDocument, TKey>(condition, findOption, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public List<TDocument> GetAll<TDocument, TKey>(FilterDefinition<TDocument> condition, FindOptions findOption, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetAll<TDocument, TKey>(condition, findOption, null, cancellationToken);
        }

        /// <inheritdoc />
        public List<TDocument> GetAll<TDocument, TKey>(FilterDefinition<TDocument> condition, FindOptions findOption, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetAll<TDocument, TKey>(condition, findOption, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public List<TDocument> GetAll<TDocument, TKey>(
            FilterDefinition<TDocument> condition,
            FindOptions findOption,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetAll<TDocument, TKey>(condition, findOption, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<List<TDocument>> GetAllAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetAllAsync<TDocument, TKey>(filter, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<List<TDocument>> GetAllAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetAllAsync<TDocument, TKey>(filter, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<List<TDocument>> GetAllAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetAllAsync<TDocument, TKey>(filter, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<List<TDocument>> GetAllAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbReader.GetAllAsync<TDocument, TKey>(filter, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual List<TDocument> GetAll<TDocument, TKey>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetAll<TDocument, TKey>(filter, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual List<TDocument> GetAll<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetAll<TDocument, TKey>(filter, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual List<TDocument> GetAll<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetAll<TDocument, TKey>(filter, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual List<TDocument> GetAll<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetAll<TDocument, TKey>(filter, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task<long> CountAsync<TDocument, TKey>(FilterDefinition<TDocument> condition)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return CountAsync<TDocument, TKey>(condition, null, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public Task<long> CountAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return CountAsync<TDocument, TKey>(condition, null, null, cancellationToken);
        }

        /// <inheritdoc />
        public Task<long> CountAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, CountOptions countOption)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return CountAsync<TDocument, TKey>(condition, countOption, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public Task<long> CountAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return CountAsync<TDocument, TKey>(condition, null, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public Task<long> CountAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return CountAsync<TDocument, TKey>(condition, null, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public Task<long> CountAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, CountOptions countOption, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return CountAsync<TDocument, TKey>(condition, countOption, null, cancellationToken);
        }

        /// <inheritdoc />
        public Task<long> CountAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, CountOptions countOption, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return CountAsync<TDocument, TKey>(condition, countOption, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public Task<long> CountAsync<TDocument, TKey>(
            FilterDefinition<TDocument> condition,
            CountOptions countOption,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.CountAsync<TDocument, TKey>(condition, countOption, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public long Count<TDocument, TKey>(FilterDefinition<TDocument> condition)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return Count<TDocument, TKey>(condition, null, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public long Count<TDocument, TKey>(FilterDefinition<TDocument> condition, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return Count<TDocument, TKey>(condition, null, null, cancellationToken);
        }

        /// <inheritdoc />
        public long Count<TDocument, TKey>(FilterDefinition<TDocument> condition, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return Count<TDocument, TKey>(condition, null, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public long Count<TDocument, TKey>(FilterDefinition<TDocument> condition, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return Count<TDocument, TKey>(condition, null, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public long Count<TDocument, TKey>(FilterDefinition<TDocument> condition, CountOptions countOption)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return Count<TDocument, TKey>(condition, countOption, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public long Count<TDocument, TKey>(FilterDefinition<TDocument> condition, CountOptions countOption, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return Count<TDocument, TKey>(condition, countOption, null, cancellationToken);
        }

        /// <inheritdoc />
        public long Count<TDocument, TKey>(FilterDefinition<TDocument> condition, CountOptions countOption, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return Count<TDocument, TKey>(condition, countOption, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public long Count<TDocument, TKey>(
            FilterDefinition<TDocument> condition,
            CountOptions countOption,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.Count<TDocument, TKey>(condition, countOption, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> CountAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CountAsync<TDocument, TKey>(filter, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> CountAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CountAsync<TDocument, TKey>(filter, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> CountAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CountAsync<TDocument, TKey>(filter, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> CountAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbReader.CountAsync<TDocument, TKey>(filter, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long Count<TDocument, TKey>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return Count<TDocument, TKey>(filter, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long Count<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return Count<TDocument, TKey>(filter, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long Count<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return Count<TDocument, TKey>(filter, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long Count<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.Count<TDocument, TKey>(filter, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<TDocument> GetByMaxAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> maxValueSelector)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetByMaxAsync<TDocument, TKey>(filter, maxValueSelector, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<TDocument> GetByMaxAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> maxValueSelector,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetByMaxAsync<TDocument, TKey>(filter, maxValueSelector, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<TDocument> GetByMaxAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> maxValueSelector,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetByMaxAsync<TDocument, TKey>(filter, maxValueSelector, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<TDocument> GetByMaxAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> maxValueSelector,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbReader.GetByMaxAsync<TDocument, TKey>(filter, maxValueSelector, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual TDocument GetByMax<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> maxValueSelector)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetByMax<TDocument, TKey>(filter, maxValueSelector, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual TDocument GetByMax<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> maxValueSelector,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetByMax<TDocument, TKey>(filter, maxValueSelector, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual TDocument GetByMax<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> maxValueSelector,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetByMax<TDocument, TKey>(filter, maxValueSelector, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual TDocument GetByMax<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> maxValueSelector,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetByMax<TDocument, TKey>(filter, maxValueSelector, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<TDocument> GetByMinAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> minValueSelector)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetByMinAsync<TDocument, TKey>(filter, minValueSelector, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<TDocument> GetByMinAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> minValueSelector,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetByMinAsync<TDocument, TKey>(filter, minValueSelector, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<TDocument> GetByMinAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> minValueSelector,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetByMinAsync<TDocument, TKey>(filter, minValueSelector, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<TDocument> GetByMinAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> minValueSelector,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbReader.GetByMinAsync<TDocument, TKey>(filter, minValueSelector, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual TDocument GetByMin<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> minValueSelector)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetByMin<TDocument, TKey>(filter, minValueSelector, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual TDocument GetByMin<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> minValueSelector,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetByMin<TDocument, TKey>(filter, minValueSelector, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual TDocument GetByMin<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> minValueSelector,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetByMin<TDocument, TKey>(filter, minValueSelector, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual TDocument GetByMin<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> minValueSelector,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetByMin<TDocument, TKey>(filter, minValueSelector, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<TValue> GetMaxValueAsync<TDocument, TKey, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> maxValueSelector)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetMaxValueAsync<TDocument, TKey, TValue>(filter, maxValueSelector, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<TValue> GetMaxValueAsync<TDocument, TKey, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> maxValueSelector,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetMaxValueAsync<TDocument, TKey, TValue>(filter, maxValueSelector, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<TValue> GetMaxValueAsync<TDocument, TKey, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> maxValueSelector,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetMaxValueAsync<TDocument, TKey, TValue>(filter, maxValueSelector, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<TValue> GetMaxValueAsync<TDocument, TKey, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> maxValueSelector,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbReader.GetMaxValueAsync<TDocument, TKey, TValue>(filter, maxValueSelector, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual TValue GetMaxValue<TDocument, TKey, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> maxValueSelector)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetMaxValue<TDocument, TKey, TValue>(filter, maxValueSelector, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual TValue GetMaxValue<TDocument, TKey, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> maxValueSelector,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetMaxValue<TDocument, TKey, TValue>(filter, maxValueSelector, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual TValue GetMaxValue<TDocument, TKey, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> maxValueSelector,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetMaxValue<TDocument, TKey, TValue>(filter, maxValueSelector, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual TValue GetMaxValue<TDocument, TKey, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> maxValueSelector,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetMaxValue<TDocument, TKey, TValue>(filter, maxValueSelector, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<TValue> GetMinValueAsync<TDocument, TKey, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> minValueSelector)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetMinValueAsync<TDocument, TKey, TValue>(filter, minValueSelector, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<TValue> GetMinValueAsync<TDocument, TKey, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> minValueSelector,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetMinValueAsync<TDocument, TKey, TValue>(filter, minValueSelector, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<TValue> GetMinValueAsync<TDocument, TKey, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> minValueSelector,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetMinValueAsync<TDocument, TKey, TValue>(filter, minValueSelector, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<TValue> GetMinValueAsync<TDocument, TKey, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> minValueSelector,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbReader.GetMinValueAsync<TDocument, TKey, TValue>(filter, minValueSelector, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual TValue GetMinValue<TDocument, TKey, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> minValueSelector)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetMinValue<TDocument, TKey, TValue>(filter, minValueSelector, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual TValue GetMinValue<TDocument, TKey, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> minValueSelector,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetMinValue<TDocument, TKey, TValue>(filter, minValueSelector, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual TValue GetMinValue<TDocument, TKey, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> minValueSelector,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetMinValue<TDocument, TKey, TValue>(filter, minValueSelector, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual TValue GetMinValue<TDocument, TKey, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> minValueSelector,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetMinValue<TDocument, TKey, TValue>(filter, minValueSelector, partitionKey, cancellationToken);
        }

        #endregion

        #region Sum TKey

        /// <inheritdoc />
        public virtual async Task<int> SumByAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, int>> selector)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await SumByAsync<TDocument, TKey>(filter, selector, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<int> SumByAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, int>> selector,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await SumByAsync<TDocument, TKey>(filter, selector, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<int> SumByAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, int>> selector,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await SumByAsync<TDocument, TKey>(filter, selector, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<int> SumByAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, int>> selector,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbReader.SumByAsync<TDocument, TKey>(filter, selector, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual int SumBy<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, int>> selector,
            string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.SumBy<TDocument, TKey>(filter, selector, partitionKey);
        }

        /// <inheritdoc />
        public virtual async Task<decimal> SumByAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, decimal>> selector)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await SumByAsync<TDocument, TKey>(filter, selector, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<decimal> SumByAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, decimal>> selector,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await SumByAsync<TDocument, TKey>(filter, selector, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<decimal> SumByAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, decimal>> selector,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await SumByAsync<TDocument, TKey>(filter, selector, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<decimal> SumByAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, decimal>> selector,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbReader.SumByAsync<TDocument, TKey>(filter, selector, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual decimal SumBy<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, decimal>> selector,
            string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.SumBy<TDocument, TKey>(filter, selector, partitionKey);
        }

        #endregion Sum TKey

        #region Project TKey

        /// <inheritdoc />
        public virtual async Task<TProjection> ProjectOneAsync<TDocument, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return await ProjectOneAsync<TDocument, TProjection, TKey>(filter, projection, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<TProjection> ProjectOneAsync<TDocument, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return await ProjectOneAsync<TDocument, TProjection, TKey>(filter, projection, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<TProjection> ProjectOneAsync<TDocument, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return await ProjectOneAsync<TDocument, TProjection, TKey>(filter, projection, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<TProjection> ProjectOneAsync<TDocument, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return await MongoDbReader.ProjectOneAsync<TDocument, TProjection, TKey>(filter, projection, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual TProjection ProjectOne<TDocument, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return ProjectOne<TDocument, TProjection, TKey>(filter, projection, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual TProjection ProjectOne<TDocument, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return ProjectOne<TDocument, TProjection, TKey>(filter, projection, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual TProjection ProjectOne<TDocument, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return ProjectOne<TDocument, TProjection, TKey>(filter, projection, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual TProjection ProjectOne<TDocument, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return MongoDbReader.ProjectOne<TDocument, TProjection, TKey>(filter, projection, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return await ProjectManyAsync<TDocument, TProjection, TKey>(filter, projection, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return await ProjectManyAsync<TDocument, TProjection, TKey>(filter, projection, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return await ProjectManyAsync<TDocument, TProjection, TKey>(filter, projection, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return await MongoDbReader.ProjectManyAsync<TDocument, TProjection, TKey>(filter, projection, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual List<TProjection> ProjectMany<TDocument, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return ProjectMany<TDocument, TProjection, TKey>(filter, projection, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual List<TProjection> ProjectMany<TDocument, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return ProjectMany<TDocument, TProjection, TKey>(filter, projection, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual List<TProjection> ProjectMany<TDocument, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return ProjectMany<TDocument, TProjection, TKey>(filter, projection, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual List<TProjection> ProjectMany<TDocument, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return MongoDbReader.ProjectMany<TDocument, TProjection, TKey>(filter, projection, partitionKey, cancellationToken);
        }

        #endregion Project TKey

        #region Group By TKey

        /// <inheritdoc />
        public virtual List<TProjection> GroupBy<TDocument, TGroupKey, TProjection, TKey>(
            Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class, new()
        {
            return GroupBy<TDocument, TGroupKey, TProjection, TKey>(groupingCriteria, groupProjection, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual List<TProjection> GroupBy<TDocument, TGroupKey, TProjection, TKey>(
            Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class, new()
        {
            return GroupBy<TDocument, TGroupKey, TProjection, TKey>(groupingCriteria, groupProjection, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual List<TProjection> GroupBy<TDocument, TGroupKey, TProjection, TKey>(
            Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class, new()
        {
            return GroupBy<TDocument, TGroupKey, TProjection, TKey>(groupingCriteria, groupProjection, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual List<TProjection> GroupBy<TDocument, TGroupKey, TProjection, TKey>(
            Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class, new()
        {
            return MongoDbReader.GroupBy<TDocument, TGroupKey, TProjection, TKey>(groupingCriteria, groupProjection, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual List<TProjection> GroupBy<TDocument, TGroupKey, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class, new()
        {
            return GroupBy<TDocument, TGroupKey, TProjection, TKey>(filter, groupingCriteria, groupProjection, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual List<TProjection> GroupBy<TDocument, TGroupKey, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class, new()
        {
            return GroupBy<TDocument, TGroupKey, TProjection, TKey>(filter, groupingCriteria, groupProjection, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual List<TProjection> GroupBy<TDocument, TGroupKey, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class, new()
        {
            return GroupBy<TDocument, TGroupKey, TProjection, TKey>(filter, groupingCriteria, groupProjection, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual List<TProjection> GroupBy<TDocument, TGroupKey, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class, new()
        {
            return MongoDbReader.GroupBy<TDocument, TGroupKey, TProjection, TKey>(filter, groupingCriteria, groupProjection, partitionKey, cancellationToken);
        }

        #endregion Group By TKey

        #region Pagination

        /// <inheritdoc />
        public virtual async Task<List<TDocument>> GetSortedPaginatedAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> sortSelector,
            bool ascending = true,
            int skipNumber = 0,
            int takeNumber = 50,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var sorting = ascending
                ? Builders<TDocument>.Sort.Ascending(sortSelector)
                : Builders<TDocument>.Sort.Descending(sortSelector);

            return await HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(filter)
                .Sort(sorting)
                .Skip(skipNumber)
                .Limit(takeNumber)
                .ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<List<TDocument>> GetSortedPaginatedAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            SortDefinition<TDocument> sortDefinition,
            int skipNumber = 0,
            int takeNumber = 50,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(filter)
                .Sort(sortDefinition)
                .Skip(skipNumber)
                .Limit(takeNumber)
                .ToListAsync(cancellationToken);
        }

        #endregion Pagination
    }
}