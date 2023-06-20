using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDbGenericRepository.DataAccess.Base;
using MongoDbGenericRepository.Models;

namespace MongoDbGenericRepository.DataAccess.Index
{
    /// <summary>
    ///     The MongoDbIndexHandler interface. used to create indexes on collections.
    /// </summary>
    public interface IMongoDbIndexHandler : IDataAccessBase
    {
        /// <summary>
        ///     Returns the names of the indexes present on a collection.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="partitionKey">An optional partition key</param>
        /// <param name="cancellationToken">An optional Cancellation Token.</param>
        /// <returns>A list containing the names of the indexes on on the concerned collection.</returns>
        Task<List<string>> GetIndexesNamesAsync<TDocument, TKey>(string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        ///     Create a text index on the given field.
        ///     IndexCreationOptions can be supplied to further specify
        ///     how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">An optional cancellation token</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateTextIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions = null,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        ///     Creates an index on the given field in ascending order.
        ///     IndexCreationOptions can be supplied to further specify
        ///     how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateAscendingIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions = null,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        ///     Creates an index on the given field in descending order.
        ///     IndexCreationOptions can be supplied to further specify
        ///     how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateDescendingIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions = null,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        ///     Creates a hashed index on the given field.
        ///     IndexCreationOptions can be supplied to further specify
        ///     how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="field">The field we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateHashedIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions = null,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        ///     Creates a combined text index.
        ///     IndexCreationOptions can be supplied to further specify
        ///     how the creation should be done.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="fields">The fields we want to index.</param>
        /// <param name="indexCreationOptions">Options for creating an index.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">An optional Cancellation token.</param>
        /// <returns>The result of the create index operation.</returns>
        Task<string> CreateCombinedTextIndexAsync<TDocument, TKey>(
            IEnumerable<Expression<Func<TDocument, object>>> fields,
            IndexCreationOptions indexCreationOptions = null,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        ///     Drops the index given a field name
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="indexName">The name of the index</param>
        /// <param name="partitionKey">An optional partition key</param>
        /// <param name="cancellationToken">An optional cancellation token,</param>
        Task DropIndexAsync<TDocument, TKey>(string indexName, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
    }
}