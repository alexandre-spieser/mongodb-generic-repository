using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDbGenericRepository.DataAccess.Create;
using MongoDbGenericRepository.Models;

namespace MongoDbGenericRepository
{
    /// <summary>
    ///     The base Repository, it is meant to be inherited from by your custom custom MongoRepository implementation.
    ///     Its constructor must be given a connection string and a database name.
    /// </summary>
    public abstract partial class BaseMongoRepository : IBaseMongoRepository_Create
    {
        private readonly object _initLock = new object();
        private IMongoDbCreator _mongoDbCreator;

        /// <summary>
        ///     The MongoDbCreator field.
        /// </summary>
        protected virtual IMongoDbCreator MongoDbCreator
        {
            get
            {
                if (_mongoDbCreator != null)
                {
                    return _mongoDbCreator;
                }

                lock (_initLock)
                {
                    if (_mongoDbCreator == null)
                    {
                        _mongoDbCreator = new MongoDbCreator(MongoDbContext);
                    }
                }

                return _mongoDbCreator;
            }
            set => _mongoDbCreator = value;
        }

        /// <inheritdoc />
        public virtual async Task AddOneAsync<TDocument, TKey>(TDocument document)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            await AddOneAsync<TDocument, TKey>(document, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task AddOneAsync<TDocument, TKey>(TDocument document, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            await MongoDbCreator.AddOneAsync<TDocument, TKey>(document, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task AddOneAsync<TDocument>(TDocument document)
            where TDocument : IDocument<Guid>
        {
            await AddOneAsync<TDocument, Guid>(document, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task AddOneAsync<TDocument>(TDocument document, CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            await AddOneAsync<TDocument, Guid>(document, cancellationToken);
        }

        /// <inheritdoc />
        public virtual void AddOne<TDocument, TKey>(TDocument document)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            AddOne<TDocument, TKey>(document, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual void AddOne<TDocument, TKey>(TDocument document, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            MongoDbCreator.AddOne<TDocument, TKey>(document, cancellationToken);
        }

        /// <inheritdoc />
        public virtual void AddOne<TDocument>(TDocument document)
            where TDocument : IDocument<Guid>
        {
            AddOne<TDocument, Guid>(document, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual void AddOne<TDocument>(TDocument document, CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            AddOne<TDocument, Guid>(document, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task AddManyAsync<TDocument, TKey>(IEnumerable<TDocument> documents)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            await AddManyAsync<TDocument, TKey>(documents, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task AddManyAsync<TDocument, TKey>(IEnumerable<TDocument> documents, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            await MongoDbCreator.AddManyAsync<TDocument, TKey>(documents, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task AddManyAsync<TDocument>(IEnumerable<TDocument> documents)
            where TDocument : IDocument<Guid>
        {
            await AddManyAsync<TDocument, Guid>(documents, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task AddManyAsync<TDocument>(IEnumerable<TDocument> documents, CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            await AddManyAsync<TDocument, Guid>(documents, cancellationToken);
        }

        /// <inheritdoc />
        public virtual void AddMany<TDocument, TKey>(IEnumerable<TDocument> documents)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            AddMany<TDocument, TKey>(documents, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual void AddMany<TDocument, TKey>(IEnumerable<TDocument> documents, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            MongoDbCreator.AddMany<TDocument, TKey>(documents, cancellationToken);
        }

        /// <inheritdoc />
        public virtual void AddMany<TDocument>(IEnumerable<TDocument> documents)
            where TDocument : IDocument<Guid>
        {
            AddMany<TDocument, Guid>(documents, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual void AddMany<TDocument>(IEnumerable<TDocument> documents, CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            AddMany<TDocument, Guid>(documents, cancellationToken);
        }
    }
}