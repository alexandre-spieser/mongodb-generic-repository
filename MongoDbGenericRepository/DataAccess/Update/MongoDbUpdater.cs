using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDbGenericRepository.DataAccess.Base;
using MongoDbGenericRepository.Models;

namespace MongoDbGenericRepository.DataAccess.Update
{
    /// <summary>
    ///     The MongoDb updater.
    /// </summary>
    public partial class MongoDbUpdater : DataAccessBase, IMongoDbUpdater
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="mongoDbContext"></param>
        public MongoDbUpdater(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey>(TDocument modifiedDocument, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", modifiedDocument.Id);
            var updateRes = await HandlePartitioned<TDocument, TKey>(modifiedDocument)
                .ReplaceOneAsync(filter, modifiedDocument, cancellationToken: cancellationToken);
            return updateRes.ModifiedCount == 1;
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual bool UpdateOne<TDocument, TKey>(TDocument modifiedDocument, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", modifiedDocument.Id);
            var updateRes = HandlePartitioned<TDocument, TKey>(modifiedDocument).ReplaceOne(filter, modifiedDocument, cancellationToken: cancellationToken);
            return updateRes.ModifiedCount == 1;
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey>(
            TDocument documentToModify,
            UpdateDefinition<TDocument> update,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", documentToModify.Id);
            var updateRes = await HandlePartitioned<TDocument, TKey>(documentToModify).UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
            return updateRes.ModifiedCount == 1;
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual bool UpdateOne<TDocument, TKey>(
            TDocument documentToModify,
            UpdateDefinition<TDocument> update,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", documentToModify.Id);
            var updateRes = HandlePartitioned<TDocument, TKey>(documentToModify).UpdateOne(filter, update, cancellationToken: cancellationToken);
            return updateRes.ModifiedCount == 1;
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(
            TDocument documentToModify,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", documentToModify.Id);
            var updateRes = await HandlePartitioned<TDocument, TKey>(documentToModify).UpdateOneAsync(
                filter,
                Builders<TDocument>.Update.Set(field, value),
                cancellationToken: cancellationToken);
            return updateRes.ModifiedCount == 1;
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual bool UpdateOne<TDocument, TKey, TField>(
            TDocument documentToModify,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", documentToModify.Id);
            var updateRes = HandlePartitioned<TDocument, TKey>(documentToModify).UpdateOne(
                filter,
                Builders<TDocument>.Update.Set(field, value),
                cancellationToken: cancellationToken);
            return updateRes.ModifiedCount == 1;
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var collection = string.IsNullOrEmpty(partitionKey) ? GetCollection<TDocument, TKey>() : GetCollection<TDocument, TKey>(partitionKey);
            var updateRes = await collection.UpdateOneAsync(filter, Builders<TDocument>.Update.Set(field, value), cancellationToken: cancellationToken);
            return updateRes.ModifiedCount == 1;
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateOneAsync<TDocument, TKey, TField>(Builders<TDocument>.Filter.Where(filter), field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual bool UpdateOne<TDocument, TKey, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var collection = string.IsNullOrEmpty(partitionKey) ? GetCollection<TDocument, TKey>() : GetCollection<TDocument, TKey>(partitionKey);
            var updateRes = collection.UpdateOne(filter, Builders<TDocument>.Update.Set(field, value), cancellationToken: cancellationToken);
            return updateRes.ModifiedCount == 1;
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual bool UpdateOne<TDocument, TKey, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateOne<TDocument, TKey, TField>(Builders<TDocument>.Filter.Where(filter), field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual async Task<long> UpdateManyAsync<TDocument, TKey, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateManyAsync<TDocument, TKey, TField>(Builders<TDocument>.Filter.Where(filter), field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual async Task<long> UpdateManyAsync<TDocument, TKey, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var collection = string.IsNullOrEmpty(partitionKey) ? GetCollection<TDocument, TKey>() : GetCollection<TDocument, TKey>(partitionKey);
            var updateRes = await collection.UpdateManyAsync(filter, Builders<TDocument>.Update.Set(field, value), cancellationToken: cancellationToken);
            return updateRes.ModifiedCount;
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual async Task<long> UpdateManyAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            UpdateDefinition<TDocument> update,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await UpdateManyAsync<TDocument, TKey>(Builders<TDocument>.Filter.Where(filter), update, partitionKey, cancellationToken);
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual async Task<long> UpdateManyAsync<TDocument, TKey>(
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var collection = string.IsNullOrEmpty(partitionKey) ? GetCollection<TDocument, TKey>() : GetCollection<TDocument, TKey>(partitionKey);
            var updateRes = await collection.UpdateManyAsync(filter, updateDefinition, cancellationToken: cancellationToken);
            return updateRes.ModifiedCount;
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual long UpdateMany<TDocument, TKey, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateMany<TDocument, TKey, TField>(Builders<TDocument>.Filter.Where(filter), field, value, partitionKey, cancellationToken);
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual long UpdateMany<TDocument, TKey, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var collection = string.IsNullOrEmpty(partitionKey) ? GetCollection<TDocument, TKey>() : GetCollection<TDocument, TKey>(partitionKey);
            var updateRes = collection.UpdateMany(filter, Builders<TDocument>.Update.Set(field, value), cancellationToken: cancellationToken);
            return updateRes.ModifiedCount;
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual long UpdateMany<TDocument, TKey>(
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var collection = string.IsNullOrEmpty(partitionKey) ? GetCollection<TDocument, TKey>() : GetCollection<TDocument, TKey>(partitionKey);
            var updateRes = collection.UpdateMany(filter, updateDefinition, cancellationToken: cancellationToken);
            return updateRes.ModifiedCount;
        }

        /// <inheritdoc cref="IMongoDbUpdater" />
        public virtual long UpdateMany<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var collection = string.IsNullOrEmpty(partitionKey) ? GetCollection<TDocument, TKey>() : GetCollection<TDocument, TKey>(partitionKey);
            var updateRes = collection.UpdateMany(filter, updateDefinition, cancellationToken: cancellationToken);
            return updateRes.ModifiedCount;
        }
    }
}