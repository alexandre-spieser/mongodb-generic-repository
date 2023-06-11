using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDbGenericRepository.DataAccess.Base;
using MongoDbGenericRepository.Models;

namespace MongoDbGenericRepository.DataAccess.Create
{
    public interface IMongoDbCreator : IDataAccessBase
    {
        /// <summary>
        /// Asynchronously adds a document to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="document">The document you want to add.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        Task AddOneAsync<TDocument, TKey>(TDocument document, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Adds a document to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="document">The document you want to add.</param>
        void AddOne<TDocument, TKey>(TDocument document)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Asynchronously adds a list of documents to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="documents">The documents you want to add.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        Task AddManyAsync<TDocument, TKey>(IEnumerable<TDocument> documents, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Adds a list of documents to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="documents">The documents you want to add.</param>
        void AddMany<TDocument, TKey>(IEnumerable<TDocument> documents)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Gets a IMongoQueryable for a potentially partitioned document type and a filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="filter">The filter definition.</param>
        /// <param name="partitionKey">The collection partition key.</param>
        /// <returns></returns>
        IMongoQueryable<TDocument> GetQuery<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Gets a collections for a potentially partitioned document type.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="document">The document.</param>
        /// <returns></returns>
        IMongoCollection<TDocument> HandlePartitioned<TDocument, TKey>(TDocument document)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Gets a collections for a potentially partitioned document type.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="partitionKey">The collection partition key.</param>
        /// <returns></returns>
        IMongoCollection<TDocument> HandlePartitioned<TDocument, TKey>(string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Gets a collections for the type TDocument with a partition key.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="partitionKey">The collection partition key.</param>
        /// <returns></returns>
        IMongoCollection<TDocument> GetCollection<TDocument, TKey>(string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
    }
}