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
    ///     read only repository interface
    /// </summary>
    /// <typeparam name="TKey">The key type</typeparam>
    public interface IReadOnlyMongoRepository<TKey>
        where TKey : IEquatable<TKey>
    {
        #region Read

        /// <summary>
        ///     Asynchronously returns one document given its id.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="id">The Id of the document you want to get.</param>
        Task<TDocument> GetByIdAsync<TDocument>(TKey id)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Asynchronously returns one document given its id.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="id">The Id of the document you want to get.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<TDocument> GetByIdAsync<TDocument>(TKey id, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Asynchronously returns one document given its id.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="id">The Id of the document you want to get.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        Task<TDocument> GetByIdAsync<TDocument>(TKey id, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Asynchronously returns one document given its id.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="id">The Id of the document you want to get.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<TDocument> GetByIdAsync<TDocument>(TKey id, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Returns one document given its id.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="id">The Id of the document you want to get.</param>
        TDocument GetById<TDocument>(TKey id)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Returns one document given its id.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="id">The Id of the document you want to get.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        TDocument GetById<TDocument>(TKey id, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Returns one document given its id.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="id">The Id of the document you want to get.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        TDocument GetById<TDocument>(TKey id, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Returns one document given its id.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="id">The Id of the document you want to get.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        TDocument GetById<TDocument>(TKey id, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Asynchronously returns one document given an expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        Task<TDocument> GetOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>;


        /// <summary>
        ///     Asynchronously returns one document given an expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task<TDocument> GetOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Asynchronously returns one document given an expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        Task<TDocument> GetOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Asynchronously returns one document given an expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task<TDocument> GetOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Returns one document given an expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        TDocument GetOne<TDocument>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Returns one document given an expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        TDocument GetOne<TDocument>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Returns one document given an expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        TDocument GetOne<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Returns one document given an expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        TDocument GetOne<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Returns a collection cursor.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        IFindFluent<TDocument, TDocument> GetCursor<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Asynchronously returns true if any of the document of the collection matches the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        Task<bool> AnyAsync<TDocument>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Asynchronously returns true if any of the document of the collection matches the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<bool> AnyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Asynchronously returns true if any of the document of the collection matches the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        Task<bool> AnyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Asynchronously returns true if any of the document of the collection matches the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<bool> AnyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Returns true if any of the document of the collection matches the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        bool Any<TDocument>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Returns true if any of the document of the collection matches the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        bool Any<TDocument>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Returns true if any of the document of the collection matches the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        bool Any<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Returns true if any of the document of the collection matches the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        bool Any<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Asynchronously returns a list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        Task<List<TDocument>> GetAllAsync<TDocument>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Asynchronously returns a list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task<List<TDocument>> GetAllAsync<TDocument>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Asynchronously returns a list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        Task<List<TDocument>> GetAllAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Asynchronously returns a list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task<List<TDocument>> GetAllAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Returns a list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        List<TDocument> GetAll<TDocument>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Returns a list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        List<TDocument> GetAll<TDocument>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Returns a list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        List<TDocument> GetAll<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Returns a list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        List<TDocument> GetAll<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Asynchronously counts how many documents match the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        Task<long> CountAsync<TDocument>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Asynchronously counts how many documents match the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<long> CountAsync<TDocument>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Asynchronously counts how many documents match the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        Task<long> CountAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Asynchronously counts how many documents match the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<long> CountAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Counts how many documents match the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        long Count<TDocument>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Counts how many documents match the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="cancellationToken">The Cancellation token.</param>
        long Count<TDocument>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Counts how many documents match the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        long Count<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Counts how many documents match the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">The Cancellation token.</param>
        long Count<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        #endregion

        #region Min / Max

        /// <summary>
        ///     Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the
        ///     filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="orderByDescending">A property selector to order by descending.</param>
        Task<TDocument> GetByMaxAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByDescending)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the
        ///     filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="orderByDescending">A property selector to order by descending.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<TDocument> GetByMaxAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByDescending, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the
        ///     filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="orderByDescending">A property selector to order by descending.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        Task<TDocument> GetByMaxAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByDescending, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the
        ///     filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="orderByDescending">A property selector to order by descending.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<TDocument> GetByMaxAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByDescending, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the
        ///     filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="orderByDescending">A property selector to order by descending.</param>
        /// <returns></returns>
        TDocument GetByMax<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByDescending)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the
        ///     filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="orderByDescending">A property selector to order by descending.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        TDocument GetByMax<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByDescending, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the
        ///     filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="orderByDescending">A property selector to order by descending.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        /// <returns></returns>
        TDocument GetByMax<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByDescending, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the
        ///     filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="orderByDescending">A property selector to order by descending.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        TDocument GetByMax<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByDescending, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the
        ///     filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="orderByAscending">A property selector to order by ascending.</param>
        Task<TDocument> GetByMinAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByAscending)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the
        ///     filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="orderByAscending">A property selector to order by ascending.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<TDocument> GetByMinAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByAscending, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the
        ///     filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="orderByAscending">A property selector to order by ascending.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        Task<TDocument> GetByMinAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByAscending, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the
        ///     filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="orderByAscending">A property selector to order by ascending.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<TDocument> GetByMinAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByAscending, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the
        ///     filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="orderByAscending">A property selector to order by ascending.</param>
        TDocument GetByMin<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByAscending)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the
        ///     filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="orderByAscending">A property selector to order by ascending.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        TDocument GetByMin<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByAscending, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the
        ///     filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="orderByAscending">A property selector to order by ascending.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        TDocument GetByMin<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByAscending, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the
        ///     filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="orderByAscending">A property selector to order by ascending.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        TDocument GetByMin<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByAscending, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the maximum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="maxValueSelector">A property selector to order by ascending.</param>
        Task<TValue> GetMaxValueAsync<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the maximum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="maxValueSelector">A property selector to order by ascending.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<TValue> GetMaxValueAsync<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the maximum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="maxValueSelector">A property selector to order by ascending.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        Task<TValue> GetMaxValueAsync<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the maximum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="maxValueSelector">A property selector to order by ascending.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<TValue> GetMaxValueAsync<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the maximum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="maxValueSelector">A property selector to order by ascending.</param>
        TValue GetMaxValue<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the maximum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="maxValueSelector">A property selector to order by ascending.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        TValue GetMaxValue<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;


        /// <summary>
        ///     Gets the maximum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="maxValueSelector">A property selector to order by ascending.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        TValue GetMaxValue<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the maximum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="maxValueSelector">A property selector to order by ascending.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        TValue GetMaxValue<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the minimum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="minValueSelector">A property selector to order by ascending.</param>
        Task<TValue> GetMinValueAsync<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector)
            where TDocument : IDocument<TKey>;


        /// <summary>
        ///     Gets the minimum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="minValueSelector">A property selector to order by ascending.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<TValue> GetMinValueAsync<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;


        /// <summary>
        ///     Gets the minimum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="minValueSelector">A property selector to order by ascending.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        Task<TValue> GetMinValueAsync<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the minimum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="minValueSelector">A property selector to order by ascending.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<TValue> GetMinValueAsync<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the minimum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="minValueSelector">A property selector to order by ascending.</param>
        TValue GetMinValue<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the minimum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="minValueSelector">A property selector to order by ascending.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        TValue GetMinValue<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;


        /// <summary>
        ///     Gets the minimum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="minValueSelector">A property selector to order by ascending.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        TValue GetMinValue<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Gets the minimum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="minValueSelector">A property selector to order by ascending.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        TValue GetMinValue<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        #endregion

        #region Maths

        /// <summary>
        ///     Sums the values of a selected field for a given filtered collection of documents.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="selector">The field you want to sum.</param>
        Task<int> SumByAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, int>> selector)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Sums the values of a selected field for a given filtered collection of documents.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="selector">The field you want to sum.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<int> SumByAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, int>> selector, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Sums the values of a selected field for a given filtered collection of documents.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="selector">The field you want to sum.</param>
        /// <param name="partitionKey">The partition key of your document, if any.</param>
        Task<int> SumByAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, int>> selector, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Sums the values of a selected field for a given filtered collection of documents.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="selector">The field you want to sum.</param>
        /// <param name="partitionKey">The partition key of your document, if any.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<int> SumByAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, int>> selector, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Sums the values of a selected field for a given filtered collection of documents.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="selector">The field you want to sum.</param>
        Task<decimal> SumByAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, decimal>> selector)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Sums the values of a selected field for a given filtered collection of documents.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="selector">The field you want to sum.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<decimal> SumByAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, decimal>> selector, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Sums the values of a selected field for a given filtered collection of documents.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="selector">The field you want to sum.</param>
        /// <param name="partitionKey">The partition key of your document, if any.</param>
        Task<decimal> SumByAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, decimal>> selector, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Sums the values of a selected field for a given filtered collection of documents.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="selector">The field you want to sum.</param>
        /// <param name="partitionKey">The partition key of your document, if any.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<decimal> SumByAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, decimal>> selector, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Sums the values of a selected field for a given filtered collection of documents.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="selector">The field you want to sum.</param>
        /// <param name="partitionKey">The partition key of your document, if any.</param>
        int SumBy<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, int>> selector,
            string partitionKey = null)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Sums the values of a selected field for a given filtered collection of documents.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="selector">The field you want to sum.</param>
        /// <param name="partitionKey">The partition key of your document, if any.</param>
        decimal SumBy<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, decimal>> selector,
            string partitionKey = null)
            where TDocument : IDocument<TKey>;

        #endregion Maths

        #region Project

        /// <summary>
        ///     Asynchronously returns a projected document matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="projection">The projection expression.</param>
        Task<TProjection> ProjectOneAsync<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection)
            where TDocument : IDocument<TKey>
            where TProjection : class;

        /// <summary>
        ///     Asynchronously returns a projected document matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task<TProjection> ProjectOneAsync<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TProjection : class;

        /// <summary>
        ///     Asynchronously returns a projected document matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        Task<TProjection> ProjectOneAsync<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey)
            where TDocument : IDocument<TKey>
            where TProjection : class;

        /// <summary>
        ///     Asynchronously returns a projected document matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task<TProjection> ProjectOneAsync<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TProjection : class;
        /// <summary>
        ///     Returns a projected document matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="projection">The projection expression.</param>
        TProjection ProjectOne<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection)
            where TDocument : IDocument<TKey>
            where TProjection : class;

        /// <summary>
        ///     Returns a projected document matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        TProjection ProjectOne<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TProjection : class;

        /// <summary>
        ///     Returns a projected document matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        TProjection ProjectOne<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey)
            where TDocument : IDocument<TKey>
            where TProjection : class;

        /// <summary>
        ///     Returns a projected document matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        TProjection ProjectOne<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TProjection : class;

        /// <summary>
        ///     Asynchronously returns a list of projected documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="projection">The projection expression.</param>
        Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection)
            where TDocument : IDocument<TKey>
            where TProjection : class;

        /// <summary>
        ///     Asynchronously returns a list of projected documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TProjection : class;

        /// <summary>
        ///     Asynchronously returns a list of projected documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey)
            where TDocument : IDocument<TKey>
            where TProjection : class;

        /// <summary>
        ///     Asynchronously returns a list of projected documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TProjection : class;

        /// <summary>
        ///     Asynchronously returns a list of projected documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="projection">The projection expression.</param>
        List<TProjection> ProjectMany<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection)
            where TDocument : IDocument<TKey>
            where TProjection : class;

        /// <summary>
        ///     Asynchronously returns a list of projected documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        List<TProjection> ProjectMany<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TProjection : class;

        /// <summary>
        ///     Asynchronously returns a list of projected documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        List<TProjection> ProjectMany<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey)
            where TDocument : IDocument<TKey>
            where TProjection : class;

        /// <summary>
        ///     Asynchronously returns a list of projected documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        List<TProjection> ProjectMany<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TProjection : class;

        #endregion Project

        #region Group By

        /// <summary>
        ///     Groups a collection of documents given a grouping criteria,
        ///     and returns a dictionary of listed document groups with keys having the different values of the grouping criteria.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TGroupKey">The type of the grouping criteria.</typeparam>
        /// <typeparam name="TProjection">The type of the projected group.</typeparam>
        /// <param name="groupingCriteria">The grouping criteria.</param>
        /// <param name="groupProjection">The projected group result.</param>
        List<TProjection> GroupBy<TDocument, TGroupKey, TProjection>(Expression<Func<TDocument, TGroupKey>> groupingCriteria, Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection)
            where TDocument : IDocument<TKey>
            where TProjection : class, new();

        /// <summary>
        ///     Groups a collection of documents given a grouping criteria,
        ///     and returns a dictionary of listed document groups with keys having the different values of the grouping criteria.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TGroupKey">The type of the grouping criteria.</typeparam>
        /// <typeparam name="TProjection">The type of the projected group.</typeparam>
        /// <param name="groupingCriteria">The grouping criteria.</param>
        /// <param name="groupProjection">The projected group result.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        List<TProjection> GroupBy<TDocument, TGroupKey, TProjection>(Expression<Func<TDocument, TGroupKey>> groupingCriteria, Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TProjection : class, new();

        /// <summary>
        ///     Groups a collection of documents given a grouping criteria,
        ///     and returns a dictionary of listed document groups with keys having the different values of the grouping criteria.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TGroupKey">The type of the grouping criteria.</typeparam>
        /// <typeparam name="TProjection">The type of the projected group.</typeparam>
        /// <param name="groupingCriteria">The grouping criteria.</param>
        /// <param name="groupProjection">The projected group result.</param>
        /// <param name="partitionKey">The partition key of your document, if any.</param>
        List<TProjection> GroupBy<TDocument, TGroupKey, TProjection>(Expression<Func<TDocument, TGroupKey>> groupingCriteria, Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection, string partitionKey)
            where TDocument : IDocument<TKey>
            where TProjection : class, new();

        /// <summary>
        ///     Groups a collection of documents given a grouping criteria,
        ///     and returns a dictionary of listed document groups with keys having the different values of the grouping criteria.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TGroupKey">The type of the grouping criteria.</typeparam>
        /// <typeparam name="TProjection">The type of the projected group.</typeparam>
        /// <param name="groupingCriteria">The grouping criteria.</param>
        /// <param name="groupProjection">The projected group result.</param>
        /// <param name="partitionKey">The partition key of your document, if any.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        List<TProjection> GroupBy<TDocument, TGroupKey, TProjection>(Expression<Func<TDocument, TGroupKey>> groupingCriteria, Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TProjection : class, new();

        /// <summary>
        ///     Groups filtered a collection of documents given a grouping criteria,
        ///     and returns a dictionary of listed document groups with keys having the different values of the grouping criteria.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TGroupKey">The type of the grouping criteria.</typeparam>
        /// <typeparam name="TProjection">The type of the projected group.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="groupingCriteria">The grouping criteria.</param>
        /// <param name="groupProjection">The projected group result.</param>
        List<TProjection> GroupBy<TDocument, TGroupKey, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TGroupKey>> groupingCriteria, Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection)
            where TDocument : IDocument<TKey>
            where TProjection : class, new();

        /// <summary>
        ///     Groups filtered a collection of documents given a grouping criteria,
        ///     and returns a dictionary of listed document groups with keys having the different values of the grouping criteria.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TGroupKey">The type of the grouping criteria.</typeparam>
        /// <typeparam name="TProjection">The type of the projected group.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="groupingCriteria">The grouping criteria.</param>
        /// <param name="groupProjection">The projected group result.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        List<TProjection> GroupBy<TDocument, TGroupKey, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TGroupKey>> groupingCriteria, Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TProjection : class, new();

        /// <summary>
        ///     Groups filtered a collection of documents given a grouping criteria,
        ///     and returns a dictionary of listed document groups with keys having the different values of the grouping criteria.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TGroupKey">The type of the grouping criteria.</typeparam>
        /// <typeparam name="TProjection">The type of the projected group.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="groupingCriteria">The grouping criteria.</param>
        /// <param name="groupProjection">The projected group result.</param>
        /// <param name="partitionKey">The partition key of your document, if any.</param>
        List<TProjection> GroupBy<TDocument, TGroupKey, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TGroupKey>> groupingCriteria, Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection, string partitionKey)
            where TDocument : IDocument<TKey>
            where TProjection : class, new();

        /// <summary>
        ///     Groups filtered a collection of documents given a grouping criteria,
        ///     and returns a dictionary of listed document groups with keys having the different values of the grouping criteria.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TGroupKey">The type of the grouping criteria.</typeparam>
        /// <typeparam name="TProjection">The type of the projected group.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="groupingCriteria">The grouping criteria.</param>
        /// <param name="groupProjection">The projected group result.</param>
        /// <param name="partitionKey">The partition key of your document, if any.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        List<TProjection> GroupBy<TDocument, TGroupKey, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TGroupKey>> groupingCriteria, Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TProjection : class, new();

        #endregion Group By

        #region Pagination

        /// <summary>
        ///     Asynchronously returns a paginated list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="sortSelector">The property selector.</param>
        /// <param name="ascending">Order of the sorting.</param>
        /// <param name="skipNumber">The number of documents you want to skip. Default value is 0.</param>
        /// <param name="takeNumber">The number of documents you want to take. Default value is 50.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        Task<List<TDocument>> GetSortedPaginatedAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> sortSelector,
            bool ascending = true,
            int skipNumber = 0,
            int takeNumber = 50,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Asynchronously returns a paginated list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="sortDefinition">The sort definition.</param>
        /// <param name="skipNumber">The number of documents you want to skip. Default value is 0.</param>
        /// <param name="takeNumber">The number of documents you want to take. Default value is 50.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">An optional cancellation token.</param>
        Task<List<TDocument>> GetSortedPaginatedAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            SortDefinition<TDocument> sortDefinition,
            int skipNumber = 0,
            int takeNumber = 50,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>;

        #endregion Pagination
    }
}