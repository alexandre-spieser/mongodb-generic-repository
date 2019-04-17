using MongoDbGenericRepository.DataAccess.Index;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using MongoDbGenericRepository.Models;

namespace MongoDbGenericRepository
{
    public interface IBaseMongoRepository_Index : IBaseMongoRepository_Index<Guid>
    {
        /// <summary>
        /// Returns the names of the indexes present on a collection.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="partitionKey">An optional partition key</param>
        /// <returns>A list containing the names of the indexes on on the concerned collection.</returns>
        Task<List<string>> GetIndexesNamesAsync<TDocument, TKey>(string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Create a text index on the given field.
        /// IndexCreationOptions can be supplied to further specify 
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateTextIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Creates an index on the given field in ascending order.
        /// IndexCreationOptions can be supplied to further specify 
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateAscendingIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Creates an index on the given field in descending order.
        /// IndexCreationOptions can be supplied to further specify 
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateDescendingIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
        where TDocument : IDocument<TKey>
        where TKey : IEquatable<TKey>;

        /// <summary>
        /// Creates a hashed index on the given field.
        /// IndexCreationOptions can be supplied to further specify 
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateHashedIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Creates a combined text index.
        /// IndexCreationOptions can be supplied to further specify 
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="fields">The fields we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateCombinedTextIndexAsync<TDocument, TKey>(IEnumerable<Expression<Func<TDocument, object>>> fields, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Drops the index given a field name
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="indexName">The name of the index</param>
        /// <param name="partitionKey">An optional partition key</param>
        Task DropIndexAsync<TDocument, TKey>(string indexName, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
    }
    /// <summary>
    /// The base Repository, it is meant to be inherited from by your custom custom MongoRepository implementation.
    /// Its constructor must be given a connection string and a database name.
    /// </summary>
    public abstract partial class BaseMongoRepository : IBaseMongoRepository_Index
    {
        private MongoDbIndexHandler _mongoDbIndexHandler;
        protected virtual MongoDbIndexHandler MongoDbIndexHandler
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
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="partitionKey">An optional partition key</param>
        /// <returns>A list containing the names of the indexes on on the concerned collection.</returns>
        public async Task<List<string>> GetIndexesNamesAsync<TDocument>(string partitionKey = null)
            where TDocument : IDocument<Guid>
        {
            return await MongoDbIndexHandler.GetIndexesNamesAsync<TDocument, Guid>(partitionKey);
        }

        /// <summary>
        /// Returns the names of the indexes present on a collection.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="partitionKey">An optional partition key</param>
        /// <returns>A list containing the names of the indexes on on the concerned collection.</returns>
        public async virtual Task<List<string>> GetIndexesNamesAsync<TDocument, TKey>(string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
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
        public async Task<string> CreateTextIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<Guid>
        {
            return await MongoDbIndexHandler.CreateTextIndexAsync<TDocument, Guid>(field, indexCreationOptions, partitionKey);
        }

        /// <summary>
        /// Create a text index on the given field.
        /// IndexCreationOptions can be supplied to further specify 
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The result of the create index operation.</returns>
        public async virtual Task<string> CreateTextIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
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
        public async Task<string> CreateAscendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null) 
            where TDocument : IDocument<Guid>
        {
            return await MongoDbIndexHandler.CreateAscendingIndexAsync<TDocument, Guid>(field, indexCreationOptions, partitionKey);
        }

        /// <summary>
        /// Creates an index on the given field in ascending order.
        /// IndexCreationOptions can be supplied to further specify 
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The result of the create index operation.</returns>
        public async virtual Task<string> CreateAscendingIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
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
        public async Task<string> CreateDescendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<Guid>
        {
            return await MongoDbIndexHandler.CreateDescendingIndexAsync<TDocument, Guid>(field, indexCreationOptions, partitionKey);
        }

        /// <inheritdoc />
        public async virtual Task<string> CreateDescendingIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
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
        public async Task<string> CreateHashedIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<Guid>
        {
            return await MongoDbIndexHandler.CreateHashedIndexAsync<TDocument, Guid>(field, indexCreationOptions, partitionKey);
        }

        /// <inheritdoc />
        public async virtual Task<string> CreateHashedIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
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
        public async Task<string> CreateCombinedTextIndexAsync<TDocument>(IEnumerable<Expression<Func<TDocument, object>>> fields, IndexCreationOptions indexCreationOptions = null, string partitionKey = null) 
            where TDocument : IDocument<Guid>
        {
            return await MongoDbIndexHandler.CreateCombinedTextIndexAsync<TDocument, Guid>(fields, indexCreationOptions, partitionKey);
        }

        /// <inheritdoc />
        public async virtual Task<string> CreateCombinedTextIndexAsync<TDocument, TKey>(IEnumerable<Expression<Func<TDocument, object>>> fields, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbIndexHandler.CreateCombinedTextIndexAsync<TDocument, TKey>(fields, indexCreationOptions, partitionKey);
        }

        /// <summary>
        /// Drops the index given a field name
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="indexName">The name of the index</param>
        /// <param name="partitionKey">An optional partition key</param>
        public async Task DropIndexAsync<TDocument>(string indexName, string partitionKey = null)
            where TDocument : IDocument<Guid>
        {
            await MongoDbIndexHandler.DropIndexAsync<TDocument, Guid>(indexName, partitionKey);
        }

        /// <inheritdoc />
        public async virtual Task DropIndexAsync<TDocument, TKey>(string indexName, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            await MongoDbIndexHandler.DropIndexAsync<TDocument, TKey>(indexName, partitionKey);
        }
    }
}
