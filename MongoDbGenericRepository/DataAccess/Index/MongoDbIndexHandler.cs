using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDbGenericRepository.DataAccess.Base;
using MongoDbGenericRepository.Models;

namespace MongoDbGenericRepository.DataAccess.Index
{
    /// <inheritdoc cref="MongoDbGenericRepository.DataAccess.Index.IMongoDbIndexHandler" />
    public class MongoDbIndexHandler : DataAccessBase, IMongoDbIndexHandler
    {
        /// <summary>
        ///     The MongoDbIndexHandler constructor.
        /// </summary>
        /// <param name="mongoDbContext">The mongo db context</param>
        public MongoDbIndexHandler(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {
        }

        /// <inheritdoc />
        public virtual async Task<List<string>> GetIndexesNamesAsync<TDocument, TKey>(string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var indexCursor = await HandlePartitioned<TDocument, TKey>(partitionKey).Indexes.ListAsync(cancellationToken);
            var indexes = await indexCursor.ToListAsync(cancellationToken);
            return indexes.Select(e => e["name"].ToString()).ToList();
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateTextIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions = null,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var model = new CreateIndexModel<TDocument>(
                Builders<TDocument>.IndexKeys.Text(field),
                indexCreationOptions == null ? null : MapIndexOptions(indexCreationOptions)
            );

            return await HandlePartitioned<TDocument, TKey>(partitionKey).Indexes
                .CreateOneAsync(
                    model,
                    cancellationToken: cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateAscendingIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions = null,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var collection = HandlePartitioned<TDocument, TKey>(partitionKey);
            var createOptions = indexCreationOptions == null ? null : MapIndexOptions(indexCreationOptions);
            var indexKey = Builders<TDocument>.IndexKeys;
            return await
                collection.Indexes.CreateOneAsync(
                    new CreateIndexModel<TDocument>(indexKey.Ascending(field), createOptions),
                    cancellationToken: cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateDescendingIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions = null,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var collection = HandlePartitioned<TDocument, TKey>(partitionKey);
            var createOptions = indexCreationOptions == null ? null : MapIndexOptions(indexCreationOptions);
            var indexKey = Builders<TDocument>.IndexKeys;
            return await collection.Indexes
                .CreateOneAsync(
                    new CreateIndexModel<TDocument>(indexKey.Descending(field), createOptions),
                    cancellationToken: cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateHashedIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions = null,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var collection = HandlePartitioned<TDocument, TKey>(partitionKey);
            var createOptions = indexCreationOptions == null ? null : MapIndexOptions(indexCreationOptions);
            var indexKey = Builders<TDocument>.IndexKeys;
            return await collection.Indexes
                .CreateOneAsync(
                    new CreateIndexModel<TDocument>(indexKey.Hashed(field), createOptions),
                    cancellationToken: cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateCombinedTextIndexAsync<TDocument, TKey>(
            IEnumerable<Expression<Func<TDocument, object>>> fields,
            IndexCreationOptions indexCreationOptions = null,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var collection = HandlePartitioned<TDocument, TKey>(partitionKey);
            var createOptions = indexCreationOptions == null ? null : MapIndexOptions(indexCreationOptions);
            var listOfDefs = new List<IndexKeysDefinition<TDocument>>();
            foreach (var field in fields)
            {
                listOfDefs.Add(Builders<TDocument>.IndexKeys.Text(field));
            }

            return await collection.Indexes
                .CreateOneAsync(
                    new CreateIndexModel<TDocument>(Builders<TDocument>.IndexKeys.Combine(listOfDefs), createOptions),
                    cancellationToken: cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task DropIndexAsync<TDocument, TKey>(string indexName, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            await HandlePartitioned<TDocument, TKey>(partitionKey).Indexes.DropOneAsync(indexName, cancellationToken);
        }
    }
}