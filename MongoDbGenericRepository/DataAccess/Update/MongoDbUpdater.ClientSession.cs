using MongoDB.Driver;
using MongoDbGenericRepository.Models;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MongoDbGenericRepository.DataAccess.Update
{
    public partial class MongoDbUpdater
    {
        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey>(IClientSessionHandle session, TDocument modifiedDocument, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", modifiedDocument.Id);
            var updateRes = await HandlePartitioned<TDocument, TKey>(modifiedDocument)
                    .ReplaceOneAsync(session, filter, modifiedDocument, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
            return updateRes.ModifiedCount == 1;
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual bool UpdateOne<TDocument, TKey>(IClientSessionHandle session, TDocument modifiedDocument, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", modifiedDocument.Id);
            var updateRes = HandlePartitioned<TDocument, TKey>(modifiedDocument).ReplaceOne(session, filter, modifiedDocument, cancellationToken: cancellationToken);
            return updateRes.ModifiedCount == 1;
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey>(IClientSessionHandle session, TDocument documentToModify, UpdateDefinition<TDocument> update, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", documentToModify.Id);
            var updateRes = await HandlePartitioned<TDocument, TKey>(documentToModify).UpdateOneAsync(session, filter, update, null, cancellationToken).ConfigureAwait(false);
            return updateRes.ModifiedCount == 1;
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual bool UpdateOne<TDocument, TKey>(IClientSessionHandle session, TDocument documentToModify, UpdateDefinition<TDocument> update, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", documentToModify.Id);
            var updateRes = HandlePartitioned<TDocument, TKey>(documentToModify).UpdateOne(session, filter, update, null, cancellationToken);
            return updateRes.ModifiedCount == 1;
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(IClientSessionHandle session, TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", documentToModify.Id);
            var updateRes = await HandlePartitioned<TDocument, TKey>(documentToModify)
                    .UpdateOneAsync(session, filter, Builders<TDocument>.Update.Set(field, value), cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

            return updateRes.ModifiedCount == 1;
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual bool UpdateOne<TDocument, TKey, TField>(IClientSessionHandle session, TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", documentToModify.Id);
            var updateRes = HandlePartitioned<TDocument, TKey>(documentToModify).UpdateOne(session, filter, Builders<TDocument>.Update.Set(field, value), cancellationToken: cancellationToken);
            return updateRes.ModifiedCount == 1;
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(IClientSessionHandle session, FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var collection = string.IsNullOrEmpty(partitionKey) ? GetCollection<TDocument, TKey>() : GetCollection<TDocument, TKey>(partitionKey);
            var updateRes = await collection.UpdateOneAsync(session, filter, Builders<TDocument>.Update.Set(field, value), cancellationToken: cancellationToken).ConfigureAwait(false);
            return updateRes.ModifiedCount == 1;
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual Task<bool> UpdateOneAsync<TDocument, TKey, TField>(IClientSessionHandle session, Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateOneAsync<TDocument, TKey, TField>(session, Builders<TDocument>.Filter.Where(filter), field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual bool UpdateOne<TDocument, TKey, TField>(IClientSessionHandle session, FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var collection = string.IsNullOrEmpty(partitionKey) ? GetCollection<TDocument, TKey>() : GetCollection<TDocument, TKey>(partitionKey);
            var updateRes = collection.UpdateOne(session, filter, Builders<TDocument>.Update.Set(field, value), cancellationToken: cancellationToken);
            return updateRes.ModifiedCount == 1;
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual bool UpdateOne<TDocument, TKey, TField>(IClientSessionHandle session, Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateOne<TDocument, TKey, TField>(session, Builders<TDocument>.Filter.Where(filter), field, value, partitionKey, cancellationToken);
        }
    }
}
