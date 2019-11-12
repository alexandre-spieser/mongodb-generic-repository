using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Linq.Expressions;
using MongoDbGenericRepository;
using MongoDbGenericRepository.Models;

namespace Mongodb.Driver.Extensions.Abstractions
{
    public interface ICustomDataAccess<TDocument, TKey>
    where TDocument : IDocument<TKey>, new()
    where TKey : IEquatable<TKey>
    {
        #region Insert

        /// <summary>
        ///     Asynchronously adds a document to the collection.
        ///     Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <param name="document">The document you want to add.</param>
        /// <param name="partitionKey">The collection partition key.</param>
        Task InsertOneAsync(TDocument document, string partitionKey = null);

        /// <summary>
        ///     Adds a document to the collection.
        ///     Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <param name="document">The document you want to add.</param>
        /// <param name="partitionKey">The collection partition key.</param>
        void InsertOne(TDocument document, string partitionKey = null);

        /// <summary>
        ///     Asynchronously adds a list of documents to the collection.
        ///     Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <param name="documents">The documents you want to add.</param>
        /// <param name="partitionKey">The collection partition key.</param>
        Task InsertManyAsync(IEnumerable<TDocument> documents, string partitionKey = null);
        /// <summary>
        ///     Adds a list of documents to the collection.
        ///     Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <param name="documents">The documents you want to add.</param>
        /// <param name="partitionKey">The collection partition key.</param>
        void InsertMany(IEnumerable<TDocument> documents, string partitionKey = null);

        #endregion

        #region Read

        /// <summary>
        ///     Asynchronously returns one document given its id.
        /// </summary>
        /// <param name="id">The Id of the document you want to get.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        Task<TDocument> GetByIdAsync(TKey id, string partitionKey = null);

        /// <summary>
        ///     Returns one document given its id.
        /// </summary>
        /// <param name="id">The Id of the document you want to get.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        TDocument GetById(TKey id, string partitionKey = null);

        /// <summary>
        ///     Asynchronously returns one document given an expression filter.
        /// </summary>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        Task<TDocument> FirstOrDefaultAsync(Expression<Func<TDocument, bool>> filter, string partitionKey = null);

        /// <summary>
        ///     Returns one document given an expression filter.
        /// </summary>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        TDocument FirstOrDefault(Expression<Func<TDocument, bool>> filter, string partitionKey = null);

        /// <summary>
        ///     Returns a collection cursor.
        /// </summary>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        IFindFluent<TDocument, TDocument> GetCursor(Expression<Func<TDocument, bool>> filter, string partitionKey = null);

        /// <summary>
        ///     Returns true if any of the document of the collection matches the filter condition.
        /// </summary>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        Task<bool> AnyAsync(Expression<Func<TDocument, bool>> filter, string partitionKey = null);

        /// <summary>
        ///     Returns true if any of the document of the collection matches the filter condition.
        /// </summary> 
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        bool Any(Expression<Func<TDocument, bool>> filter, string partitionKey = null);

        /// <summary>
        ///     Asynchronously returns a list of the documents matching the filter condition.
        /// </summary>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        Task<List<TDocument>> QueryListAsync(Expression<Func<TDocument, bool>> filter, string partitionKey = null);

        /// <summary>
        ///     Returns a list of the documents matching the filter condition.
        /// </summary>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        List<TDocument> QueryList(Expression<Func<TDocument, bool>> filter, string partitionKey = null);

        /// <summary>
        ///     Asynchronously counts how many documents match the filter condition.
        /// </summary>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partitionKey</param>
        Task<long> CountDocumentsAsync(Expression<Func<TDocument, bool>> filter, string partitionKey = null);

        /// <summary>
        ///     Counts how many documents match the filter condition.
        /// </summary>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partitionKey</param>
        long CountDocuments(Expression<Func<TDocument, bool>> filter, string partitionKey = null);

        #endregion


    }

    public interface IDefaultBaseMongodbRepository<TDocument> : IBaseMongoRepository, ICustomDataAccess<TDocument, Guid>
    where TDocument : IDocument, new()
    {

    }

}
