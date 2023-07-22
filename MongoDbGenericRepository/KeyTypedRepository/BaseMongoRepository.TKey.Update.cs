using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDbGenericRepository.DataAccess.Update;
using MongoDbGenericRepository.Models;

namespace MongoDbGenericRepository
{
    public abstract partial class BaseMongoRepository<TKey> : IBaseMongoRepository_Update<TKey>
        where TKey : IEquatable<TKey>
    {
        private IMongoDbUpdater _mongoDbUpdater;

        /// <summary>
        ///     The MongoDb accessor to update data.
        /// </summary>
        protected virtual IMongoDbUpdater MongoDbUpdater
        {
            get
            {
                if (_mongoDbUpdater != null)
                {
                    return _mongoDbUpdater;
                }

                lock (_initLock)
                {
                    if (_mongoDbUpdater == null)
                    {
                        _mongoDbUpdater = new MongoDbUpdater(MongoDbContext);
                    }
                }

                return _mongoDbUpdater;
            }
            set => _mongoDbUpdater = value;
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument>(TDocument modifiedDocument)
            where TDocument : IDocument<TKey>
        {
            return await UpdateOneAsync(modifiedDocument, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument>(TDocument modifiedDocument, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, TKey>(modifiedDocument, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument>(TDocument modifiedDocument)
            where TDocument : IDocument<TKey>
        {
            return UpdateOne(modifiedDocument, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument>(TDocument modifiedDocument, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return MongoDbUpdater.UpdateOne<TDocument, TKey>(modifiedDocument, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument>(TDocument documentToModify, UpdateDefinition<TDocument> update)
            where TDocument : IDocument<TKey>
        {
            return await UpdateOneAsync(documentToModify, update, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument>(
            TDocument documentToModify,
            UpdateDefinition<TDocument> update,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, TKey>(documentToModify, update, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument>(TDocument documentToModify, UpdateDefinition<TDocument> update)
            where TDocument : IDocument<TKey>
        {
            return UpdateOne(documentToModify, update, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument>(TDocument documentToModify, UpdateDefinition<TDocument> update, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return MongoDbUpdater.UpdateOne<TDocument, TKey>(documentToModify, update, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TField>(TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<TKey>
        {
            return UpdateOne(documentToModify, field, value, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TField>(
            TDocument documentToModify,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return MongoDbUpdater.UpdateOne<TDocument, TKey, TField>(documentToModify, field, value, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TField>(TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<TKey>
        {
            return await UpdateOneAsync(documentToModify, field, value, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TField>(
            TDocument documentToModify,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, TKey, TField>(documentToModify, field, value, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<TKey>
        {
            return UpdateOne(filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return UpdateOne(filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return UpdateOne(filter, field, value, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return MongoDbUpdater.UpdateOne<TDocument, TKey, TField>(filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<TKey>
        {
            return UpdateOne(filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return UpdateOne(filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return UpdateOne(filter, field, value, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return MongoDbUpdater.UpdateOne<TDocument, TKey, TField>(filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value)
            where TDocument : IDocument<TKey>
        {
            return await UpdateOneAsync(filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await UpdateOneAsync(filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return await UpdateOneAsync(filter, field, value, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, TKey, TField>(filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value)
            where TDocument : IDocument<TKey>
        {
            return await UpdateOneAsync(filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await UpdateOneAsync(filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return await UpdateOneAsync(filter, field, value, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, TKey, TField>(filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value)
            where TDocument : IDocument<TKey>
        {
            return await UpdateManyAsync(filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await UpdateManyAsync(filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return await UpdateManyAsync(filter, field, value, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbUpdater.UpdateManyAsync<TDocument, TKey, TField>(filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value)
            where TDocument : IDocument<TKey>
        {
            return await UpdateManyAsync(filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await UpdateManyAsync(filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return await UpdateManyAsync(filter, field, value, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbUpdater.UpdateManyAsync<TDocument, TKey, TField>(filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition)
            where TDocument : IDocument<TKey>
        {
            return await UpdateManyAsync(filter, updateDefinition, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument>(
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> updateDefinition,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await UpdateManyAsync(filter, updateDefinition, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument>(
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return await UpdateManyAsync(filter, updateDefinition, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument>(
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbUpdater.UpdateManyAsync<TDocument, TKey>(filter, updateDefinition, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition)
            where TDocument : IDocument<TKey>
        {
            return await UpdateManyAsync(filter, updateDefinition, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            UpdateDefinition<TDocument> updateDefinition,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await UpdateManyAsync(filter, updateDefinition, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return await UpdateManyAsync(filter, updateDefinition, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbUpdater.UpdateManyAsync<TDocument, TKey>(filter, updateDefinition, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<TKey>
        {
            return UpdateMany(filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return UpdateMany(filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return UpdateMany(filter, field, value, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return MongoDbUpdater.UpdateMany<TDocument, TKey, TField>(filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<TKey>
        {
            return MongoDbUpdater.UpdateMany<TDocument, TKey, TField>(filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return MongoDbUpdater.UpdateMany<TDocument, TKey, TField>(filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return MongoDbUpdater.UpdateMany<TDocument, TKey, TField>(filter, field, value, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return MongoDbUpdater.UpdateMany<TDocument, TKey, TField>(filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition)
            where TDocument : IDocument<TKey>
        {
            return UpdateMany(filter, updateDefinition, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument>(
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> updateDefinition,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return UpdateMany(filter, updateDefinition, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition, string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return UpdateMany(filter, updateDefinition, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument>(
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return MongoDbUpdater.UpdateMany<TDocument, TKey>(filter, updateDefinition, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition)
            where TDocument : IDocument<TKey>
        {
            return UpdateMany(filter, updateDefinition, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            UpdateDefinition<TDocument> updateDefinition,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return UpdateMany(filter, updateDefinition, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition, string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return UpdateMany(filter, updateDefinition, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return MongoDbUpdater.UpdateMany<TDocument, TKey>(filter, updateDefinition, partitionKey, cancellationToken);
        }
    }
}