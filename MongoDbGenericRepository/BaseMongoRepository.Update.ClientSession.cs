using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDbGenericRepository.DataAccess.Update;
using MongoDbGenericRepository.Models;

namespace MongoDbGenericRepository
{
    public abstract partial class BaseMongoRepository : IBaseMongoRepository_Update_ClientSession
    {
        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey>(IClientSessionHandle session, TDocument modifiedDocument)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateOneAsync<TDocument, TKey>(session, modifiedDocument, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey>(
            IClientSessionHandle session,
            TDocument modifiedDocument,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, TKey>(session, modifiedDocument, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey>(IClientSessionHandle session, TDocument modifiedDocument)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateOne<TDocument, TKey>(session, modifiedDocument, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey>(IClientSessionHandle session, TDocument modifiedDocument, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbUpdater.UpdateOne<TDocument, TKey>(session, modifiedDocument, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey>(
            IClientSessionHandle session,
            TDocument documentToModify,
            UpdateDefinition<TDocument> update)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateOneAsync<TDocument, TKey>(session, documentToModify, update, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey>(
            IClientSessionHandle session,
            TDocument documentToModify,
            UpdateDefinition<TDocument> update,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, TKey>(session, documentToModify, update, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey>(IClientSessionHandle session, TDocument documentToModify, UpdateDefinition<TDocument> update)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateOne<TDocument, TKey>(session, documentToModify, update, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey>(
            IClientSessionHandle session,
            TDocument documentToModify,
            UpdateDefinition<TDocument> update,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbUpdater.UpdateOne<TDocument, TKey>(session, documentToModify, update, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(
            IClientSessionHandle session,
            TDocument documentToModify,
            Expression<Func<TDocument, TField>> field,
            TField value)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateOneAsync<TDocument, TKey, TField>(session, documentToModify, field, value, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(
            IClientSessionHandle session,
            TDocument documentToModify,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, TKey, TField>(session, documentToModify, field, value, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey, TField>(
            IClientSessionHandle session,
            TDocument documentToModify,
            Expression<Func<TDocument, TField>> field,
            TField value)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateOne<TDocument, TKey, TField>(session, documentToModify, field, value, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey, TField>(
            IClientSessionHandle session,
            TDocument documentToModify,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbUpdater.UpdateOne<TDocument, TKey, TField>(session, documentToModify, field, value, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(
            IClientSessionHandle session,
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateOneAsync<TDocument, TKey, TField>(session, filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(
            IClientSessionHandle session,
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateOneAsync<TDocument, TKey, TField>(session, filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(
            IClientSessionHandle session,
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateOneAsync<TDocument, TKey, TField>(session, filter, field, value, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(
            IClientSessionHandle session,
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, TKey, TField>(session, filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(
            IClientSessionHandle session,
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateOneAsync<TDocument, TKey, TField>(session, filter, field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(
            IClientSessionHandle session,
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateOneAsync<TDocument, TKey, TField>(session, filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(
            IClientSessionHandle session,
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateOneAsync<TDocument, TKey, TField>(session, filter, field, value, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(
            IClientSessionHandle session,
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, TKey, TField>(session, filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey, TField>(
            IClientSessionHandle session,
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateOne<TDocument, TKey, TField>(session, filter, field, value, null, CancellationToken.None);
        }


        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey, TField>(
            IClientSessionHandle session,
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateOne<TDocument, TKey, TField>(session, filter, field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey, TField>(
            IClientSessionHandle session,
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateOne<TDocument, TKey, TField>(session, filter, field, value, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey, TField>(
            IClientSessionHandle session,
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbUpdater.UpdateOne<TDocument, TKey, TField>(session, filter, field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey, TField>(
            IClientSessionHandle session,
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateOne<TDocument, TKey, TField>(session, Builders<TDocument>.Filter.Where(filter), field, value, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey, TField>(
            IClientSessionHandle session,
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateOne<TDocument, TKey, TField>(session, Builders<TDocument>.Filter.Where(filter), field, value, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey, TField>(
            IClientSessionHandle session,
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateOne<TDocument, TKey, TField>(session, Builders<TDocument>.Filter.Where(filter), field, value, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual bool UpdateOne<TDocument, TKey, TField>(
            IClientSessionHandle session,
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateOne<TDocument, TKey, TField>(session, Builders<TDocument>.Filter.Where(filter), field, value, partitionKey, cancellationToken);
        }
    }
}