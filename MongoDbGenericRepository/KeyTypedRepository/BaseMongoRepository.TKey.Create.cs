using MongoDbGenericRepository.DataAccess.Create;
using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDbGenericRepository
{
    /// <summary>
    /// The interface exposing data insertion functionality for Key typed repositories.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IBaseMongoRepository_Create<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Asynchronously adds a document to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="document">The document you want to add.</param>
        Task AddOneAsync<TDocument>(TDocument document) where TDocument : IDocument<TKey>;

        /// <summary>
        /// Adds a document to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="document">The document you want to add.</param>
        void AddOne<TDocument>(TDocument document) where TDocument : IDocument<TKey>;

        /// <summary>
        /// Asynchronously adds a list of documents to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="documents">The documents you want to add.</param>
        Task AddManyAsync<TDocument>(IEnumerable<TDocument> documents) where TDocument : IDocument<TKey>;

        /// <summary>
        /// Adds a list of documents to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="documents">The documents you want to add.</param>
        void AddMany<TDocument>(IEnumerable<TDocument> documents) where TDocument : IDocument<TKey>;
    }

    /// <summary>
    /// The base Repository, it is meant to be inherited from by your custom custom MongoRepository implementation.
    /// Its constructor must be given a connection string and a database name.
    /// </summary>
    public abstract partial class BaseMongoRepository<TKey> : IBaseMongoRepository_Create<TKey> where TKey : IEquatable<TKey>
    {
        private MongoDbCreator _mongoDbCreator;

        /// <summary>
        /// The MongoDb accessor to insert data.
        /// </summary>
        protected virtual MongoDbCreator MongoDbCreator
        {
            get
            {
                if(_mongoDbCreator == null)
                {
                    lock (_initLock)
                    {
                        if(_mongoDbCreator == null)
                        {
                            _mongoDbCreator = new MongoDbCreator(MongoDbContext);
                        }
                    }
                }
                return _mongoDbCreator;
            }
            set { _mongoDbCreator = value; }
        }

        /// <summary>
        /// Asynchronously adds a document to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="document">The document you want to add.</param>
        public virtual async Task AddOneAsync<TDocument>(TDocument document) where TDocument : IDocument<TKey>
        {
            await MongoDbCreator.AddOneAsync<TDocument, TKey>(document);
        }

        /// <summary>
        /// Adds a document to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="document">The document you want to add.</param>
        public virtual void AddOne<TDocument>(TDocument document) where TDocument : IDocument<TKey>
        {
            MongoDbCreator.AddOne<TDocument, TKey>(document);
        }

        /// <summary>
        /// Asynchronously adds a list of documents to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="documents">The documents you want to add.</param>
        public virtual async Task AddManyAsync<TDocument>(IEnumerable<TDocument> documents) where TDocument : IDocument<TKey>
        {
            await MongoDbCreator.AddManyAsync<TDocument, TKey>(documents);
        }

        /// <summary>
        /// Adds a list of documents to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="documents">The documents you want to add.</param>
        public virtual void AddMany<TDocument>(IEnumerable<TDocument> documents) where TDocument : IDocument<TKey>
        {
            MongoDbCreator.AddMany<TDocument, TKey>(documents);
        }
    }
}
