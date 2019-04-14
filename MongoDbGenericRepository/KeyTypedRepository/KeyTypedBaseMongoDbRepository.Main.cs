using MongoDB.Driver;
using MongoDbGenericRepository.Models;
using System;

namespace MongoDbGenericRepository
{
    public interface IKeyTypedBaseMongoDbRepository<TKey> :
        IKeyTypedReadOnlyMongoRepository<TKey>,
        IKeyTypedBaseMongoDbRepository_Create<TKey>,
        IKeyTypedBaseMongoDbRepository_Delete<TKey>,
        IKeyTypedBaseMongoDbRepository_Index<TKey>,
        IKeyTypedBaseMongoDbRepository_Update<TKey>
        where TKey : IEquatable<TKey>
    {
    }

    /// <summary>
    /// The base Repository, it is meant to be inherited from by your custom custom MongoRepository implementation.
    /// Its constructor must be given a connection string and a database name.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract partial class KeyTypedBaseMongoDbRepository<TKey> : 
        KeyTypedReadOnlyMongoRepository<TKey>, 
        IKeyTypedBaseMongoDbRepository<TKey>
        where TKey : IEquatable<TKey>
    {
        protected readonly object _initLock = new object();

        /// <summary>
        /// The constructor taking a connection string and a database name.
        /// </summary>
        /// <param name="connectionString">The connection string of the MongoDb server.</param>
        /// <param name="databaseName">The name of the database against which you want to perform operations.</param>
        protected KeyTypedBaseMongoDbRepository(string connectionString, string databaseName = null) : base(connectionString, databaseName)
        {
        }

        /// <summary>
        /// The contructor taking a <see cref="IMongoDbContext"/>.
        /// </summary>
        /// <param name="mongoDbContext">A mongodb context implementing <see cref="IMongoDbContext"/></param>
        protected KeyTypedBaseMongoDbRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {
        }

        /// <summary>
        /// The contructor taking a <see cref="IMongoDatabase"/>.
        /// </summary>
        /// <param name="mongoDatabase">A mongodb context implementing <see cref="IMongoDatabase"/></param>
        protected KeyTypedBaseMongoDbRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }

        /// <summary>
        /// Gets a collections for a potentially partitioned document type.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="partitionKey">The collection partition key.</param>
        /// <returns></returns>
        protected virtual IMongoCollection<TDocument> HandlePartitioned<TDocument>(string partitionKey)
            where TDocument : IDocument<TKey>
        {
            if (!string.IsNullOrEmpty(partitionKey))
            {
                return GetCollection<TDocument>(partitionKey);
            }
            return GetCollection<TDocument>();
        }

        /// <summary>
        /// Gets a collections for the type TDocument with a partition key.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="partitionKey">The collection partition key.</param>
        /// <returns></returns>
        protected virtual IMongoCollection<TDocument> GetCollection<TDocument>(string partitionKey = null)
            where TDocument : IDocument<TKey>
        {
            return MongoDbContext.GetCollection<TDocument>(partitionKey);
        }
    }
}
