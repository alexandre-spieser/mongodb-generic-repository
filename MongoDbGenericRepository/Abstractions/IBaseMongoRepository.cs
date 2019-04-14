using MongoDB.Driver;
using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoDbGenericRepository
{
    /// <summary>
    /// The IBaseMongoRepository interface exposes the CRUD functionality of the BaseMongoRepository.
    /// </summary>
    public interface IBaseMongoRepository : 
        IReadOnlyMongoRepository, 
        IBaseMongoRepository_Create, 
        IBaseMongoRepository_Update, 
        IBaseMongoRepository_Delete, 
        IBaseMongoRepository_Index
    {
        /// <summary>
        /// Asynchronously returns a paginated list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter"></param>
        /// <param name="skipNumber">The number of documents you want to skip. Default value is 0.</param>
        /// <param name="takeNumber">The number of documents you want to take. Default value is 50.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        Task<List<TDocument>> GetPaginatedAsync<TDocument>(Expression<Func<TDocument, bool>> filter, int skipNumber = 0, int takeNumber = 50, string partitionKey = null)
            where TDocument : IDocument;

        /// <summary>
        /// Asynchronously returns a paginated list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="filter"></param>
        /// <param name="skipNumber">The number of documents you want to skip. Default value is 0.</param>
        /// <param name="takeNumber">The number of documents you want to take. Default value is 50.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        Task<List<TDocument>> GetPaginatedAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, int skipNumber = 0, int takeNumber = 50, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// GetAndUpdateOne with filter
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<TDocument> GetAndUpdateOne<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update, FindOneAndUpdateOptions<TDocument, TDocument> options)
            where TDocument : IDocument;

        /// <summary>
        /// GetAndUpdateOne with filter
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<TDocument> GetAndUpdateOne<TDocument, TKey>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update, FindOneAndUpdateOptions<TDocument, TDocument> options)
                                        where TDocument : IDocument<TKey>
                                        where TKey : IEquatable<TKey>;
    }

}
