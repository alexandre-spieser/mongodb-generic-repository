using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDbGenericRepository.Models;

namespace MongoDbGenericRepository
{
    /// <summary>
    /// The interface exposing index management functionality for Key typed repositories.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IBaseMongoRepository_Index<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Returns the names of the indexes present on a collection.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <returns>A list containing the names of the indexes on on the concerned collection.</returns>
        Task<List<string>> GetIndexesNamesAsync<TDocument>()
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Returns the names of the indexes present on a collection.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list containing the names of the indexes on on the concerned collection.</returns>
        Task<List<string>> GetIndexesNamesAsync<TDocument>(CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Returns the names of the indexes present on a collection.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="partitionKey">An optional partition key</param>
        /// <returns>A list containing the names of the indexes on on the concerned collection.</returns>
        Task<List<string>> GetIndexesNamesAsync<TDocument>(string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Returns the names of the indexes present on a collection.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="partitionKey">An optional partition key</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A list containing the names of the indexes on on the concerned collection.</returns>
        Task<List<string>> GetIndexesNamesAsync<TDocument>(string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Create a text index on the given field.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateTextIndexAsync<TDocument>(Expression<Func<TDocument, object>> field)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Create a text index on the given field.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateTextIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Create a text index on the given field.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateTextIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Create a text index on the given field.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateTextIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Create a text index on the given field.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateTextIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Create a text index on the given field.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateTextIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

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
        Task<string> CreateTextIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Create a text index on the given field.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateTextIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;


        /// <summary>
        /// Creates an index on the given field in ascending order.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateAscendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Creates an index on the given field in ascending order.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateAscendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Creates an index on the given field in ascending order.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateAscendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Creates an index on the given field in ascending order.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateAscendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Creates an index on the given field in ascending order.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateAscendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Creates an index on the given field in ascending order.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateAscendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

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
        Task<string> CreateAscendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Creates an index on the given field in ascending order.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateAscendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Creates an index on the given field in descending order.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateDescendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Creates an index on the given field in descending order.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateDescendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Creates an index on the given field in descending order.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateDescendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Creates an index on the given field in descending order.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateDescendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Creates an index on the given field in descending order.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateDescendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Creates an index on the given field in descending order.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateDescendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

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
        Task<string> CreateDescendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Creates an index on the given field in descending order.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateDescendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Creates a hashed index on the given field.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateHashedIndexAsync<TDocument>(Expression<Func<TDocument, object>> field)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Creates a hashed index on the given field.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateHashedIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Creates a hashed index on the given field.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateHashedIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Creates a hashed index on the given field.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateHashedIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Creates a hashed index on the given field.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateHashedIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Creates a hashed index on the given field.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateHashedIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

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
        Task<string> CreateHashedIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Creates a hashed index on the given field.
        /// IndexCreationOptions can be supplied to further specify
        /// how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateHashedIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

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
        Task<string> CreateCombinedTextIndexAsync<TDocument>(IEnumerable<Expression<Func<TDocument, object>>> fields, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Drops the index given a field name
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="indexName">The name of the index</param>
        /// <param name="partitionKey">An optional partition key</param>
        Task DropIndexAsync<TDocument>(string indexName, string partitionKey = null)
            where TDocument : IDocument<TKey>;
    }
}