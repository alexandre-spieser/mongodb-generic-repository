using MongoDbGenericRepository.DataAccess.Delete;
using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MongoDbGenericRepository
{
    /// <summary>
    /// The interface exposing deletion functionality for Key typed repositories.
    /// </summary>
    /// <typeparam name="TKey">The type of the document Id.</typeparam>
    public interface IBaseMongoRepository_Delete<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Deletes a document.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="document">The document you want to delete.</param>
        /// <returns>The number of documents deleted.</returns>
        long DeleteOne<TDocument>(TDocument document)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Asynchronously deletes a document matching the condition of the LINQ expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="document">The document you want to delete.</param>
        /// <returns>The number of documents deleted.</returns>
        Task<long> DeleteOneAsync<TDocument>(TDocument document)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Deletes a document matching the condition of the filter definition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="deleteOption">A mongodb delete option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        long DeleteOne<TDocument>(FilterDefinition<TDocument> condition, DeleteOptions deleteOption = null, string partitionKey = null)
            where TDocument : IDocument<TKey>;
        
        /// <summary>
        /// Deletes a document matching the condition of the LINQ expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        long DeleteOne<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>;
        
        /// <summary>
        /// Asynchronously deletes a document matching the condition of the filter definition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="deleteOption">A mongodb delete option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        Task<long> DeleteOneAsync<TDocument>(FilterDefinition<TDocument> condition, DeleteOptions deleteOption = null, string partitionKey = null)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Asynchronously deletes a document matching the condition of the LINQ expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        Task<long> DeleteOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Asynchronously deletes the documents matching the condition of the filter definition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="deleteOption">A mongodb delete option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        Task<long> DeleteManyAsync<TDocument>(FilterDefinition<TDocument> condition, DeleteOptions deleteOption = null, string partitionKey = null)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Asynchronously deletes the documents matching the condition of the LINQ expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        Task<long> DeleteManyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Asynchronously deletes a list of documents.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="documents">The list of documents to delete.</param>
        /// <returns>The number of documents deleted.</returns>
        Task<long> DeleteManyAsync<TDocument>(IEnumerable<TDocument> documents)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Deletes a list of documents.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="documents">The list of documents to delete.</param>
        /// <returns>The number of documents deleted.</returns>
        long DeleteMany<TDocument>(IEnumerable<TDocument> documents)
            where TDocument : IDocument<TKey>;

        /// <summary>
        /// Deletes the documents matching the condition of the filter definition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="deleteOption">A mongodb delete option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        long DeleteMany<TDocument>(FilterDefinition<TDocument> condition, DeleteOptions deleteOption = null, string partitionKey = null)
            where TDocument : IDocument<TKey>;
        
        /// <summary>
        /// Deletes the documents matching the condition of the LINQ expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        long DeleteMany<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>;
    }

    public abstract partial class BaseMongoRepository<TKey>: IBaseMongoRepository_Delete<TKey> 
        where TKey : IEquatable<TKey>
    {
        private MongoDbEraser _mongoDbEraser;

        /// <summary>
        /// The MongoDb accessor to delete data.
        /// </summary>
        protected virtual MongoDbEraser MongoDbEraser
        {
            get
            {
                if (_mongoDbEraser != null) { return _mongoDbEraser; }

                lock (_initLock)
                {
                    if (_mongoDbEraser == null)
                    {
                        _mongoDbEraser = new MongoDbEraser(MongoDbContext);
                    }
                }
                return _mongoDbEraser;
            }
            set { _mongoDbEraser = value; }
        }

        /// <summary>
        /// Deletes a document.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="document">The document you want to delete.</param>
        /// <returns>The number of documents deleted.</returns>
        public virtual long DeleteOne<TDocument>(TDocument document)
            where TDocument : IDocument<TKey>
        {
            return MongoDbEraser.DeleteOne<TDocument, TKey>(document);
        }

        /// <summary>
        /// Asynchronously deletes a document matching the condition of the LINQ expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="document">The document you want to delete.</param>
        /// <returns>The number of documents deleted.</returns>
        public virtual async Task<long> DeleteOneAsync<TDocument>(TDocument document)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbEraser.DeleteOneAsync<TDocument, TKey>(document);
        }

        /// <summary>
        /// Deletes a document matching the condition of the filter definition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="deleteOption">A mongodb delete option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        public virtual long DeleteOne<TDocument>(FilterDefinition<TDocument> condition, DeleteOptions deleteOption = null,
            string partitionKey = null) where TDocument : IDocument<TKey>
        {
            return MongoDbEraser.DeleteOne<TDocument, TKey>(condition, deleteOption, partitionKey).DeletedCount;
        }

        /// <summary>
        /// Deletes a document matching the condition of the LINQ expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        public virtual long DeleteOne<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
        {
            return MongoDbEraser.DeleteOne<TDocument, TKey>(filter, partitionKey);
        }

        /// <summary>
        /// Deletes a document matching the condition of the filter definition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="deleteOption">A mongodb delete option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        public virtual async Task<long> DeleteOneAsync<TDocument>(FilterDefinition<TDocument> condition, DeleteOptions deleteOption = null, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            var result = await MongoDbEraser.DeleteOneAsync<TDocument, TKey>(condition, deleteOption, partitionKey);
            return result.DeletedCount;
        }

        /// <summary>
        /// Asynchronously deletes a document matching the condition of the LINQ expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        public virtual async Task<long> DeleteOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbEraser.DeleteOneAsync<TDocument, TKey>(filter, partitionKey);
        }

        /// <summary>
        /// Asynchronously deletes the documents matching the condition of the filter definition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="deleteOption">A mongodb delete option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        public virtual async Task<long> DeleteManyAsync<TDocument>(FilterDefinition<TDocument> condition, DeleteOptions deleteOption = null,
            string partitionKey = null) where TDocument : IDocument<TKey>
        {
            var result = await MongoDbEraser.DeleteManyAsync<TDocument, TKey>(condition, deleteOption, partitionKey);
            return result.DeletedCount;
        }

        /// <summary>
        /// Asynchronously deletes the documents matching the condition of the LINQ expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        public virtual async Task<long> DeleteManyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbEraser.DeleteManyAsync<TDocument, TKey>(filter, partitionKey);
        }

        /// <summary>
        /// Asynchronously deletes a list of documents.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="documents">The list of documents to delete.</param>
        /// <returns>The number of documents deleted.</returns>
        public virtual async Task<long> DeleteManyAsync<TDocument>(IEnumerable<TDocument> documents)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbEraser.DeleteManyAsync<TDocument, TKey>(documents);
        }

        /// <summary>
        /// Deletes a list of documents.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="documents">The list of documents to delete.</param>
        /// <returns>The number of documents deleted.</returns>
        public virtual long DeleteMany<TDocument>(IEnumerable<TDocument> documents)
            where TDocument : IDocument<TKey>
        {
            return MongoDbEraser.DeleteMany<TDocument, TKey>(documents);
        }

        /// <summary>
        /// Deletes the documents matching the condition of the filter definition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="deleteOption">A mongodb delete option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        public long DeleteMany<TDocument>(FilterDefinition<TDocument> condition, DeleteOptions deleteOption = null, string partitionKey = null) where TDocument : IDocument<TKey>
        {
            return MongoDbEraser.DeleteMany<TDocument, TKey>(condition, deleteOption, partitionKey).DeletedCount;
        }

        /// <summary>
        /// Deletes the documents matching the condition of the LINQ expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        public virtual long DeleteMany<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
        {
            return MongoDbEraser.DeleteMany<TDocument, TKey>(filter, partitionKey);
        }
    }
}
