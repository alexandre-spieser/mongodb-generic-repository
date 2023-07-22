using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDbGenericRepository.DataAccess.Base;
using MongoDbGenericRepository.Models;
using MongoDbGenericRepository.Utils;

namespace MongoDbGenericRepository.DataAccess.Create
{
    /// <summary>
    ///     A class to insert MongoDb document.
    /// </summary>
    public class MongoDbCreator : DataAccessBase, IMongoDbCreator
    {
        /// <summary>
        ///     The construct of the MongoDbCreator class.
        /// </summary>
        /// <param name="mongoDbContext">A <see cref="IMongoDbContext" /> instance.</param>
        public MongoDbCreator(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {
        }

        /// <inheritdoc />
        public virtual async Task AddOneAsync<TDocument, TKey>(TDocument document, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            FormatDocument<TDocument, TKey>(document);
            await HandlePartitioned<TDocument, TKey>(document)
                .InsertOneAsync(document, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual void AddOne<TDocument, TKey>(TDocument document, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            FormatDocument<TDocument, TKey>(document);
            HandlePartitioned<TDocument, TKey>(document)
                .InsertOne(document, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task AddManyAsync<TDocument, TKey>(IEnumerable<TDocument> documents, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var documentsList = documents.ToList();

            if (!documentsList.Any())
            {
                return;
            }

            foreach (var document in documentsList)
            {
                FormatDocument<TDocument, TKey>(document);
            }

            // cannot use typeof(IPartitionedDocument).IsAssignableFrom(typeof(TDocument)), not available in netstandard 1.5
            if (documentsList.Any(e => e is IPartitionedDocument))
            {
                foreach (var group in documentsList.GroupBy(e => ((IPartitionedDocument) e).PartitionKey))
                {
                    await HandlePartitioned<TDocument, TKey>(group.FirstOrDefault()).InsertManyAsync(group.ToList(), null, cancellationToken);
                }
            }
            else
            {
                await GetCollection<TDocument, TKey>().InsertManyAsync(documentsList, null, cancellationToken);
            }
        }

        /// <inheritdoc />
        public virtual void AddMany<TDocument, TKey>(IEnumerable<TDocument> documents, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var documentList = documents.ToList();

            if (!documentList.Any())
            {
                return;
            }

            foreach (var document in documentList)
            {
                FormatDocument<TDocument, TKey>(document);
            }

            // cannot use typeof(IPartitionedDocument).IsAssignableFrom(typeof(TDocument)), not available in netstandard 1.5
            if (documentList.Any(e => e is IPartitionedDocument))
            {
                foreach (var group in documentList.GroupBy(e => ((IPartitionedDocument) e).PartitionKey))
                {
                    HandlePartitioned<TDocument, TKey>(group.FirstOrDefault()).InsertMany(group.ToList(), cancellationToken: cancellationToken);
                }
            }
            else
            {
                GetCollection<TDocument, TKey>().InsertMany(documentList, cancellationToken: cancellationToken);
            }
        }

        /// <summary>
        ///     Sets the value of the document Id if it is not set already.
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