using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDbGenericRepository.Models;

namespace MongoDbGenericRepository
{
    /// <summary>
    ///     The interface exposing data insertion functionality for Key typed repositories.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IBaseMongoRepository_Create<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        ///     Asynchronously adds a document to the collection.
        ///     Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="document">The document you want to add.</param>
        Task AddOneAsync<TDocument>(TDocument document)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Asynchronously adds a document to the collection.
        ///     Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="document">The document you want to add.</param>
        /// <param name="cancellationToken">The cancellation Token.</param>
        Task AddOneAsync<TDocument>(TDocument document, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Adds a document to the collection.
        ///     Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="document">The document you want to add.</param>
        void AddOne<TDocument>(TDocument document)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Adds a document to the collection.
        ///     Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="document">The document you want to add.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        void AddOne<TDocument>(TDocument document, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Asynchronously adds a list of documents to the collection.
        ///     Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="documents">The documents you want to add.</param>
        Task AddManyAsync<TDocument>(IEnumerable<TDocument> documents)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Asynchronously adds a list of documents to the collection.
        ///     Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="documents">The documents you want to add.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        Task AddManyAsync<TDocument>(IEnumerable<TDocument> documents, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Adds a list of documents to the collection.
        ///     Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="documents">The documents you want to add.</param>
        void AddMany<TDocument>(IEnumerable<TDocument> documents)
            where TDocument : IDocument<TKey>;


        /// <summary>
        ///     Adds a list of documents to the collection.
        ///     Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="documents">The documents you want to add.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        void AddMany<TDocument>(IEnumerable<TDocument> documents, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;
    }
}