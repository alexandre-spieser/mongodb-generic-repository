using MongoDbGenericRepository.DataAccess.Index;
using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoDbGenericRepository
{
    /// <summary>
    /// The base Repository, it is meant to be inherited from by your custom custom MongoRepository implementation.
    /// Its constructor must be given a connection string and a database name.
    /// </summary>
    public abstract partial class BaseMongoRepository<TKey> : IBaseMongoRepository_Index<TKey>
        where TKey : IEquatable<TKey>
    {
        private volatile IMongoDbIndexHandler _mongoDbIndexHandler;

        /// <summary>
        /// The MongoDb accessor to manage indexes.
        /// </summary>
        protected virtual IMongoDbIndexHandler MongoDbIndexHandler
        {
            get
            {
                if (_mongoDbIndexHandler != null) { return _mongoDbIndexHandler; }

                lock (_initLock)
                {
                    if (_mongoDbIndexHandler == null)
                    {
                        _mongoDbIndexHandler = new MongoDbIndexHandler(MongoDbContext);
                    }
                }

                return _mongoDbIndexHandler;
            }
            set { _mongoDbIndexHandler = value; }
        }

        /// <summary>
        /// Returns the names of the indexes present on a collection.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="partitionKey">An optional partition key</param>
        /// <returns>A list containing the names of the indexes on on the concerned collection.</returns>
        public virtual async Task<List<string>> GetIndexesNamesAsync<TDocument>(string partitionKey = null)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbIndexHandler.GetIndexesNamesAsync<TDocument, TKey>(partitionKey);
        }

        /// <summary>
        /// Create a text index on the given field.
        /// IndexCreationOptions can be supplied to further specify 
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The result of the create index operation.</returns>
        public virtual async Task<string> CreateTextIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbIndexHandler.CreateTextIndexAsync<TDocument, TKey>(field, indexCreationOptions, partitionKey);
        }

        /// <summary>
        /// Creates an index on the given field in ascending order.
        /// IndexCreationOptions can be supplied to further specify 
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The result of the create index operation.</returns>
        public virtual async Task<string> CreateAscendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbIndexHandler.CreateAscendingIndexAsync<TDocument, TKey>(field, indexCreationOptions, partitionKey);
        }

        /// <summary>
        /// Creates an index on the given field in descending order.
        /// IndexCreationOptions can be supplied to further specify 
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The result of the create index operation.</returns>
        public virtual async Task<string> CreateDescendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbIndexHandler.CreateDescendingIndexAsync<TDocument, TKey>(field, indexCreationOptions, partitionKey);
        }

        /// <summary>
        /// Creates a hashed index on the given field.
        /// IndexCreationOptions can be supplied to further specify 
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The result of the create index operation.</returns>
        public virtual async Task<string> CreateHashedIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbIndexHandler.CreateHashedIndexAsync<TDocument, TKey>(field, indexCreationOptions, partitionKey);
        }

        /// <summary>
        /// Creates a combined text index.
        /// IndexCreationOptions can be supplied to further specify 
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="fields">The fields we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The result of the create index operation.</returns>
        public virtual async Task<string> CreateCombinedTextIndexAsync<TDocument>(IEnumerable<Expression<Func<TDocument, object>>> fields, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbIndexHandler.CreateCombinedTextIndexAsync<TDocument, TKey>(fields, indexCreationOptions, partitionKey);
        }

        /// <summary>
        /// Drops the index given a field name
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="indexName">The name of the index</param>
        /// <param name="partitionKey">An optional partition key</param>
        public virtual async Task DropIndexAsync<TDocument>(string indexName, string partitionKey = null)
            where TDocument : IDocument<TKey>
        {
            await MongoDbIndexHandler.DropIndexAsync<TDocument, TKey>(indexName, partitionKey);
        }
    }
}
