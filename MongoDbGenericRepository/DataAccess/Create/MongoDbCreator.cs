using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDbGenericRepository.DataAccess.Base;
using MongoDbGenericRepository.Models;
using MongoDbGenericRepository.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MongoDbGenericRepository.DataAccess.Create
{
    /// <summary>
    /// A class to insert MongoDb document.
    /// </summary>
    public class MongoDbCreator : DataAccessBase, IMongoDbCreator
    {
        /// <summary>
        /// The construct of the MongoDbCreator class.
        /// </summary>
        /// <param name="mongoDbContext">A <see cref="IMongoDbContext"/> instance.</param>
        public MongoDbCreator(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {
        }

        #region Create TKey

        /// <summary>
        /// Asynchronously adds a document to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="document">The document you want to add.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public virtual async Task AddOneAsync<TDocument, TKey>(TDocument document, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            FormatDocument<TDocument, TKey>(document);
            await HandlePartitioned<TDocument, TKey>(document).InsertOneAsync(document, null, cancellationToken);
        }

        /// <summary>
        /// Adds a document to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="document">The document you want to add.</param>
        public virtual void AddOne<TDocument, TKey>(TDocument document)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            FormatDocument<TDocument, TKey>(document);
            HandlePartitioned<TDocument, TKey>(document).InsertOne(document);
        }

        /// <summary>
        /// Asynchronously adds a list of documents to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="documents">The documents you want to add.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public virtual async Task AddManyAsync<TDocument, TKey>(IEnumerable<TDocument> documents, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            if (!documents.Any())
            {
                return;
            }
            foreach (var document in documents)
            {
                FormatDocument<TDocument, TKey>(document);
            }
            // cannot use typeof(IPartitionedDocument).IsAssignableFrom(typeof(TDocument)), not available in netstandard 1.5
            if (documents.Any(e => e is IPartitionedDocument))
            {
                foreach (var group in documents.GroupBy(e => ((IPartitionedDocument)e).PartitionKey))
                {
                    await HandlePartitioned<TDocument, TKey>(group.FirstOrDefault()).InsertManyAsync(group.ToList(), null, cancellationToken);
                }
            }
            else
            {
                await GetCollection<TDocument, TKey>().InsertManyAsync(documents.ToList(), null, cancellationToken);
            }
        }

        /// <summary>
        /// Adds a list of documents to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="documents">The documents you want to add.</param>
        public virtual void AddMany<TDocument, TKey>(IEnumerable<TDocument> documents)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            if (!documents.Any())
            {
                return;
            }
            foreach (var document in documents)
            {
                FormatDocument<TDocument, TKey>(document);
            }
            // cannot use typeof(IPartitionedDocument).IsAssignableFrom(typeof(TDocument)), not available in netstandard 1.5
            if (documents.Any(e => e is IPartitionedDocument))
            {
                foreach (var group in documents.GroupBy(e => ((IPartitionedDocument)e).PartitionKey))
                {
                    HandlePartitioned<TDocument, TKey>(group.FirstOrDefault()).InsertMany(group.ToList());
                }
            }
            else
            {
                GetCollection<TDocument, TKey>().InsertMany(documents.ToList());
            }
        }

        #endregion

        /// <summary>
        /// Sets the value of the document Id if it is not set already.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="document">The document.</param>
        protected void FormatDocument<TDocument, TKey>(TDocument document)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
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
