using MongoDbGenericRepository.DataAccess.Delete;
using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MongoDbGenericRepository
{
    public abstract partial class BaseMongoRepository<TKey> : IBaseMongoRepository_Delete<TKey>
        where TKey : IEquatable<TKey>
    {
        private IMongoDbEraser _mongoDbEraser;

        /// <summary>
        /// The MongoDb accessor to delete data.
        /// </summary>
        protected virtual IMongoDbEraser MongoDbEraser
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

        /// <inheritdoc />
        public virtual long DeleteOne<TDocument>(TDocument document)
            where TDocument : IDocument<TKey>
        {
            return DeleteOne(document, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long DeleteOne<TDocument>(TDocument document, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return MongoDbEraser.DeleteOne<TDocument, TKey>(document, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long DeleteOne<TDocument>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>
        {
            return DeleteOne(filter, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long DeleteOne<TDocument>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return DeleteOne(filter, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long DeleteOne<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return DeleteOne(filter, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long DeleteOne<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return MongoDbEraser.DeleteOne<TDocument, TKey>(filter, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> DeleteOneAsync<TDocument>(TDocument document)
            where TDocument : IDocument<TKey>
        {
            return await DeleteOneAsync(document, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> DeleteOneAsync<TDocument>(TDocument document, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbEraser.DeleteOneAsync<TDocument, TKey>(document, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> DeleteOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>
        {
            return await DeleteOneAsync(filter, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> DeleteOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await DeleteOneAsync(filter, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> DeleteOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return await DeleteOneAsync(filter, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> DeleteOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbEraser.DeleteOneAsync<TDocument, TKey>(filter, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> DeleteManyAsync<TDocument>(IEnumerable<TDocument> documents)
            where TDocument : IDocument<TKey>
        {
            return await DeleteManyAsync(documents, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> DeleteManyAsync<TDocument>(IEnumerable<TDocument> documents, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbEraser.DeleteManyAsync<TDocument, TKey>(documents, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<long> DeleteManyAsync<TDocument>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>
        {
            return await DeleteManyAsync(filter, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<long> DeleteManyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await DeleteManyAsync(filter, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> DeleteManyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return await DeleteManyAsync(filter, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> DeleteManyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbEraser.DeleteManyAsync<TDocument, TKey>(filter, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long DeleteMany<TDocument>(IEnumerable<TDocument> documents)
            where TDocument : IDocument<TKey>
        {
            return DeleteMany(documents, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long DeleteMany<TDocument>(IEnumerable<TDocument> documents, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return MongoDbEraser.DeleteMany<TDocument, TKey>(documents, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long DeleteMany<TDocument>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>
        {
            return DeleteMany<TDocument>(filter, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long DeleteMany<TDocument>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return DeleteMany<TDocument>(filter, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long DeleteMany<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return DeleteMany<TDocument>(filter, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long DeleteMany<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return MongoDbEraser.DeleteMany<TDocument, TKey>(filter, partitionKey, cancellationToken);
        }
    }
}
