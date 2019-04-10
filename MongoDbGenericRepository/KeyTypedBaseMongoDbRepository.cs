using MongoDB.Driver;
using MongoDbGenericRepository.Models;
using MongoDbGenericRepository.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbGenericRepository
{
    /// <summary>
    /// The base Repository, it is meant to be inherited from by your custom custom MongoRepository implementation.
    /// Its constructor must be given a connection string and a database name.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class KeyTypedBaseMongoDbRepository<TKey> : KeyTypedReadOnlyMongoRepository<TKey>, IKeyTypedReadOnlyMongoRepository<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// The constructor taking a connection string and a database name.
        /// </summary>
        /// <param name="connectionString">The connection string of the MongoDb server.</param>
        /// <param name="databaseName">The name of the database against which you want to perform operations.</param>
        protected KeyTypedBaseMongoDbRepository(string connectionString, string databaseName = null) : base(connectionString, databaseName)
        {
        }

        /// <summary>
        /// The contructor taking a <see cref="IMongoDbContext"/>.
        /// </summary>
        /// <param name="mongoDbContext">A mongodb context implementing <see cref="IMongoDbContext"/></param>
        protected KeyTypedBaseMongoDbRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {
        }

        /// <summary>
        /// The contructor taking a <see cref="IMongoDatabase"/>.
        /// </summary>
        /// <param name="mongoDatabase">A mongodb context implementing <see cref="IMongoDatabase"/></param>
        protected KeyTypedBaseMongoDbRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }

        #region Create

        /// <summary>
        /// Asynchronously adds a document to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="document">The document you want to add.</param>
        public virtual async Task AddOneAsync<TDocument>(TDocument document) where TDocument : IDocument<TKey>
        {
            FormatDocument(document);
            await HandlePartitioned(document).InsertOneAsync(document);
        }

        /// <summary>
        /// Adds a document to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="document">The document you want to add.</param>
        public virtual void AddOne<TDocument>(TDocument document) where TDocument : IDocument<TKey>
        {
            FormatDocument(document);
            HandlePartitioned(document).InsertOne(document);
        }

        /// <summary>
        /// Asynchronously adds a list of documents to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="documents">The documents you want to add.</param>
        public virtual async Task AddManyAsync<TDocument>(IEnumerable<TDocument> documents) where TDocument : IDocument<TKey>
        {
            if (!documents.Any())
            {
                return;
            }
            foreach (var document in documents)
            {
                FormatDocument(document);
            }
            // cannot use typeof(IPartitionedDocument).IsAssignableFrom(typeof(TDocument)), not available in netstandard 1.5
            if (documents.Any(e => e is IPartitionedDocument))
            {
                foreach (var group in documents.GroupBy(e => ((IPartitionedDocument)e).PartitionKey))
                {
                    await HandlePartitioned(group.FirstOrDefault()).InsertManyAsync(group.ToList());
                }
            }
            else
            {
                await GetCollection<TDocument>().InsertManyAsync(documents.ToList());
            }
        }

        /// <summary>
        /// Adds a list of documents to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="documents">The documents you want to add.</param>
        public virtual void AddMany<TDocument>(IEnumerable<TDocument> documents) where TDocument : IDocument<TKey>
        {
            if (!documents.Any())
            {
                return;
            }
            foreach (var document in documents)
            {
                FormatDocument(document);
            }
            // cannot use typeof(IPartitionedDocument).IsAssignableFrom(typeof(TDocument)), not available in netstandard 1.5
            if (documents.Any(e => e is IPartitionedDocument))
            {
                foreach (var group in documents.GroupBy(e => ((IPartitionedDocument)e).PartitionKey))
                {
                    HandlePartitioned(group.FirstOrDefault()).InsertMany(group.ToList());
                }
            }
            else
            {
                GetCollection<TDocument>().InsertMany(documents.ToList());
            }
        }

        #endregion Create

        /// <summary>
        /// Sets the value of the document Id if it is not set already.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="document">The document.</param>
        protected void FormatDocument<TDocument>(TDocument document) where TDocument : IDocument<TKey>
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }
            var defaultTKey = default(TKey);
            if (document.Id == null
                || (defaultTKey != null
                    && defaultTKey.Equals(document.Id)))
            {
                document.Id = IdGenerator.GetId<TKey>();
            }
        }
    }
}
