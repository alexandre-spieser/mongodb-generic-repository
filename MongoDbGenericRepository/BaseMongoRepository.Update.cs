using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDbGenericRepository.DataAccess.Update;
using MongoDbGenericRepository.Models;

namespace MongoDbGenericRepository
{
    /// <summary>
    ///     The base Repository, it is meant to be inherited from by your custom custom MongoRepository implementation.
    ///     Its constructor must be given a connection string and a database name.
    /// </summary>
    public abstract partial class BaseMongoRepository : IBaseMongoRepository_Update
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

        #region Update

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument>(TDocument modifiedDocument)
            where TDocument : IDocument<Guid>
        {
            return await UpdateOneAsync(modifiedDocument, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument>(TDocument modifiedDocument, CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, Guid>(modifiedDocument, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument>(TDocument modifiedDocument)
            where TDocument : IDocument<Guid>
        {
            return UpdateOne(modifiedDocument, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument>(TDocument modifiedDocument, CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return MongoDbUpdater.UpdateOne<TDocument, Guid>(modifiedDocument, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument>(TDocument documentToModify, UpdateDefinition<TDocument> update)
            where TDocument : IDocument<Guid>
        {
            return await UpdateOneAsync(documentToModify, update, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument>(
            TDocument documentToModify,
            UpdateDefinition<TDocument> update,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, Guid>(documentToModify, update, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument>(TDocument documentToModify, UpdateDefinition<TDocument> update)
            where TDocument : IDocument<Guid>
        {
            return UpdateOne(documentToModify, update, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument>(TDocument documentToModify, UpdateDefinition<TDocument> update, CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return MongoDbUpdater.UpdateOne<TDocument, Guid>(documentToModify, update, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TField>(TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<Guid>
        {
            return UpdateOne(documentToModify, field, value, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TField>(
            TDocument documentToModify,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return MongoDbUpdater.UpdateOne<TDocument, Guid, TField>(documentToModify, field, value, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TField>(TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<Guid>
        {
            return await UpdateOneAsync(documentToModify, field, value, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TField>(
            TDocument documentToModify,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, Guid, TField>(documentToModify, field, value, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<Guid>
        {
            return UpdateOne(filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return UpdateOne(filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<Guid>
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
            where TDocument : IDocument<Guid>
        {
            return MongoDbUpdater.UpdateOne<TDocument, Guid, TField>(filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<Guid>
        {
            return UpdateOne(filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return UpdateOne(filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<Guid>
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
            where TDocument : IDocument<Guid>
        {
            return MongoDbUpdater.UpdateOne<TDocument, Guid, TField>(filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value)
            where TDocument : IDocument<Guid>
        {
            return await UpdateOneAsync(filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await UpdateOneAsync(filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<Guid>
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
            where TDocument : IDocument<Guid>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, Guid, TField>(filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value)
            where TDocument : IDocument<Guid>
        {
            return await UpdateOneAsync(filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await UpdateOneAsync(filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<Guid>
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
            where TDocument : IDocument<Guid>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, Guid, TField>(filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value)
            where TDocument : IDocument<Guid>
        {
            return await UpdateManyAsync(filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await UpdateManyAsync(filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<Guid>
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
            where TDocument : IDocument<Guid>
        {
            return await MongoDbUpdater.UpdateManyAsync<TDocument, Guid, TField>(filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value)
            where TDocument : IDocument<Guid>
        {
            return await UpdateManyAsync(filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await UpdateManyAsync(filter, field, value, null, cancellationToken);
        }


        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<Guid>
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
            where TDocument : IDocument<Guid>
        {
            return await MongoDbUpdater.UpdateManyAsync<TDocument, Guid, TField>(filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition)
            where TDocument : IDocument<Guid>
        {
            return await UpdateManyAsync(filter, updateDefinition, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument>(
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> updateDefinition,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await UpdateManyAsync(filter, updateDefinition, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument>(
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey)
            where TDocument : IDocument<Guid>
        {
            return await UpdateManyAsync(filter, updateDefinition, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument>(
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await MongoDbUpdater.UpdateManyAsync<TDocument, Guid>(filter, updateDefinition, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<long> UpdateManyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition)
            where TDocument : IDocument<Guid>
        {
            return await UpdateManyAsync(filter, updateDefinition, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<long> UpdateManyAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            UpdateDefinition<TDocument> updateDefinition,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await UpdateManyAsync(filter, updateDefinition, null, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<long> UpdateManyAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey)
            where TDocument : IDocument<Guid>
        {
            return await UpdateManyAsync(filter, updateDefinition, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<long> UpdateManyAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await MongoDbUpdater.UpdateManyAsync<TDocument, Guid>(filter, updateDefinition, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<Guid>
        {
            return UpdateMany(filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return UpdateMany(filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<Guid>
        {
            return UpdateMany(filter, field, value, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return MongoDbUpdater.UpdateMany<TDocument, Guid, TField>(filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<Guid>
        {
            return UpdateMany(filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return UpdateMany(filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<Guid>
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
            where TDocument : IDocument<Guid>
        {
            return MongoDbUpdater.UpdateMany<TDocument, Guid, TField>(filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition)
            where TDocument : IDocument<Guid>
        {
            return UpdateMany(filter, updateDefinition, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument>(
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> updateDefinition,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return UpdateMany(filter, updateDefinition, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition, string partitionKey)
            where TDocument : IDocument<Guid>
        {
            return UpdateMany(filter, updateDefinition, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument>(
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return MongoDbUpdater.UpdateMany<TDocument, Guid>(filter, updateDefinition, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public long UpdateMany<TDocument>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition)
            where TDocument : IDocument<Guid>
        {
            return UpdateMany(filter, updateDefinition, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public long UpdateMany<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            UpdateDefinition<TDocument> updateDefinition,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return UpdateMany(filter, updateDefinition, null, cancellationToken);
        }

        /// <inheritdoc />
        public long UpdateMany<TDocument>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition, string partitionKey)
            where TDocument : IDocument<Guid>
        {
            return UpdateMany(filter, updateDefinition, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public long UpdateMany<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return MongoDbUpdater.UpdateMany<TDocument, Guid>(filter, updateDefinition, partitionKey, cancellationToken);
        }

        #endregion Update

        #region Update TKey

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey>(TDocument modifiedDocument)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateOneAsync<TDocument, TKey>(modifiedDocument, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey>(TDocument modifiedDocument, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, TKey>(modifiedDocument, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey>(TDocument modifiedDocument)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateOne<TDocument, TKey>(modifiedDocument, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey>(TDocument modifiedDocument, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbUpdater.UpdateOne<TDocument, TKey>(modifiedDocument, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey>(TDocument documentToModify, UpdateDefinition<TDocument> update)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateOneAsync<TDocument, TKey>(documentToModify, update, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey>(
            TDocument documentToModify,
            UpdateDefinition<TDocument> update,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, TKey>(documentToModify, update, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey>(TDocument documentToModify, UpdateDefinition<TDocument> update)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateOne<TDocument, TKey>(documentToModify, update, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey>(TDocument documentToModify, UpdateDefinition<TDocument> update, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbUpdater.UpdateOne<TDocument, TKey>(documentToModify, update, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey, TField>(TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateOne<TDocument, TKey, TField>(documentToModify, field, value, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey, TField>(
            TDocument documentToModify,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbUpdater.UpdateOne<TDocument, TKey, TField>(documentToModify, field, value, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(
            TDocument documentToModify,
            Expression<Func<TDocument, TField>> field,
            TField value)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateOneAsync<TDocument, TKey, TField>(documentToModify, field, value, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(
            TDocument documentToModify,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, TKey, TField>(documentToModify, field, value, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateOne<TDocument, TKey, TField>(filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateOne<TDocument, TKey, TField>(filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateOne<TDocument, TKey, TField>(filter, field, value, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbUpdater.UpdateOne<TDocument, TKey, TField>(filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateOne<TDocument, TKey, TField>(filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateOne<TDocument, TKey, TField>(filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateOne<TDocument, TKey, TField>(filter, field, value, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbUpdater.UpdateOne<TDocument, TKey, TField>(filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateOneAsync<TDocument, TKey, TField>(filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateOneAsync<TDocument, TKey, TField>(filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateOneAsync<TDocument, TKey, TField>(filter, field, value, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, TKey, TField>(filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateOneAsync<TDocument, TKey, TField>(Builders<TDocument>.Filter.Where(filter), field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateOneAsync<TDocument, TKey, TField>(Builders<TDocument>.Filter.Where(filter), field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateOneAsync<TDocument, TKey, TField>(Builders<TDocument>.Filter.Where(filter), field, value, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, TKey, TField>(
                Builders<TDocument>.Filter.Where(filter),
                field,
                value,
                partitionKey,
                cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TKey, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateManyAsync<TDocument, TKey, TField>(filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TKey, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateManyAsync<TDocument, TKey, TField>(filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TKey, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateManyAsync<TDocument, TKey, TField>(filter, field, value, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TKey, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbUpdater.UpdateManyAsync<TDocument, TKey, TField>(filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TKey, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateManyAsync<TDocument, TKey, TField>(filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TKey, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateManyAsync<TDocument, TKey, TField>(filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TKey, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateManyAsync<TDocument, TKey, TField>(filter, field, value, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TKey, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbUpdater.UpdateManyAsync<TDocument, TKey, TField>(filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TKey>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateManyAsync<TDocument, TKey>(filter, updateDefinition, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TKey>(
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> updateDefinition,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateManyAsync<TDocument, TKey>(filter, updateDefinition, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TKey>(
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateManyAsync<TDocument, TKey>(filter, updateDefinition, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TKey>(
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbUpdater.UpdateManyAsync<TDocument, TKey>(filter, updateDefinition, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateManyAsync<TDocument, TKey>(filter, updateDefinition, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            UpdateDefinition<TDocument> updateDefinition,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateManyAsync<TDocument, TKey>(filter, updateDefinition, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateManyAsync<TDocument, TKey>(filter, updateDefinition, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<long> UpdateManyAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbUpdater.UpdateManyAsync<TDocument, TKey>(filter, updateDefinition, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TKey, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateMany<TDocument, TKey, TField>(filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TKey, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateMany<TDocument, TKey, TField>(filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TKey, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateMany<TDocument, TKey, TField>(filter, field, value, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TKey, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbUpdater.UpdateMany<TDocument, TKey, TField>(filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TKey, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateMany<TDocument, TKey, TField>(filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TKey, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateMany<TDocument, TKey, TField>(filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TKey, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateMany<TDocument, TKey, TField>(filter, field, value, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TKey, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbUpdater.UpdateMany<TDocument, TKey, TField>(filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateMany<TDocument, TKey>(filter, updateDefinition, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            UpdateDefinition<TDocument> updateDefinition,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateMany<TDocument, TKey>(filter, updateDefinition, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateMany<TDocument, TKey>(filter, updateDefinition, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbUpdater.UpdateMany<TDocument, TKey>(filter, updateDefinition, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TKey>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateMany<TDocument, TKey>(filter, updateDefinition, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TKey>(
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> updateDefinition,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateMany<TDocument, TKey>(filter, updateDefinition, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TKey>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateMany<TDocument, TKey>(filter, updateDefinition, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual long UpdateMany<TDocument, TKey>(
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbUpdater.UpdateMany<TDocument, TKey>(filter, updateDefinition, partitionKey, cancellationToken);
        }

        #endregion Update
    }
}