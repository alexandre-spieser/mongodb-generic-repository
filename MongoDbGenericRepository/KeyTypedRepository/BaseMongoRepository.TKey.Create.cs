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
    public abstract partial class BaseMongoRepository<TKey> : IBaseMongoRepository_Create<TKey>
        where TKey : IEquatable<TKey>
    {
        private IMongoDbCreator _mongoDbCreator;

        /// <summary>
        ///     The MongoDb accessor to insert data.
        /// </summary>
        protected virtual IMongoDbCreator MongoDbCreator
        {
            get
            {
                if (_mongoDbCreator == null)
                {
                    lock (_initLock)
                    {
                        if (_mongoDbCreator == null)
                        {
                            _mongoDbCreator = new MongoDbCreator(MongoDbContext);
                        }
                    }
                }

                return _mongoDbCreator;
            }

            set => _mongoDbCreator = value;
        }

        /// <inheritdoc />
        public virtual async Task AddOneAsync<TDocument>(TDocument document)
            where TDocument : IDocument<TKey>
        {
            await AddOneAsync(document, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task AddOneAsync<TDocument>(TDocument document, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            await MongoDbCreator.AddOneAsync<TDocument, TKey>(document, cancellationToken);
        }

        /// <inheritdoc />
        public virtual void AddOne<TDocument>(TDocument document)
            where TDocument : IDocument<TKey>
        {
            AddOne(document, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual void AddOne<TDocument>(TDocument document, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            MongoDbCreator.AddOne<TDocument, TKey>(document, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task AddManyAsync<TDocument>(IEnumerable<TDocument> documents)
            where TDocument : IDocument<TKey>
        {
            await AddManyAsync(documents, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task AddManyAsync<TDocument>(IEnumerable<TDocument> documents, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            await MongoDbCreator.AddManyAsync<TDocument, TKey>(documents, cancellationToken);
        }

        /// <inheritdoc />
        public virtual void AddMany<TDocument>(IEnumerable<TDocument> documents)
            where TDocument : IDocument<TKey>
        {
            AddMany(documents, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual void AddMany<TDocument>(IEnumerable<TDocument> documents, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            MongoDbCreator.AddMany<TDocument, TKey>(documents, cancellationToken);
        }
    }
}