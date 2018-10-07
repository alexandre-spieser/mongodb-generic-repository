using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using MongoDbGenericRepository.Models;
using System.Linq;

namespace MongoDbGenericRepository
{
    /// <summary>
    /// The base Repository, it is meant to be inherited from by your custom custom MongoRepository implementation.
    /// Its constructor must be given a connection string and a database name.
    /// </summary>
    public abstract partial class BaseMongoRepository : ReadOnlyMongoRepository, IBaseMongoRepository
    {
        #region Index Management

        /// <inheritdoc />
        public async Task<string> CreateTextIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument
        {
            return await CreateTextIndexAsync<TDocument, Guid>(field, indexCreationOptions, partitionKey);
        }

        /// <inheritdoc />
        public async Task<string> CreateTextIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await HandlePartitioned<TDocument, TKey>(partitionKey).Indexes
                                                                   .CreateOneAsync(
                                                                     new CreateIndexModel<TDocument>(
                                                                     Builders<TDocument>.IndexKeys.Text(field),
                                                                     indexCreationOptions == null ? null : MapIndexOptions(indexCreationOptions)
                                                                   ));
        }

        /// <inheritdoc />
        public async Task<string> CreateAscendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null) where TDocument : IDocument
        {
            return await CreateAscendingIndexAsync<TDocument, Guid>(field, indexCreationOptions, partitionKey);
        }

        /// <inheritdoc />
        public async Task<string> CreateAscendingIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var collection = HandlePartitioned<TDocument, TKey>(partitionKey);
            var createOptions = indexCreationOptions == null ? null : MapIndexOptions(indexCreationOptions);
            var indexKey = Builders<TDocument>.IndexKeys;
            return await collection.Indexes
                                   .CreateOneAsync(
                new CreateIndexModel<TDocument>(indexKey.Ascending(field), createOptions));
        }

        /// <inheritdoc />
        public async Task<string> CreateDescendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument
        {
            return await CreateDescendingIndexAsync<TDocument, Guid>(field, indexCreationOptions, partitionKey);
        }

        /// <inheritdoc />
        public async Task<string> CreateDescendingIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var collection = HandlePartitioned<TDocument, TKey>(partitionKey);
            var createOptions = indexCreationOptions == null ? null : MapIndexOptions(indexCreationOptions);
            var indexKey = Builders<TDocument>.IndexKeys;
            return await collection.Indexes
                                   .CreateOneAsync(
                new CreateIndexModel<TDocument>(indexKey.Descending(field), createOptions));
        }

        /// <inheritdoc />
        public async Task<string> CreateHashedIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument
        {
            return await CreateHashedIndexAsync<TDocument, Guid>(field, indexCreationOptions, partitionKey);
        }

        /// <inheritdoc />
        public async Task<string> CreateHashedIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var collection = HandlePartitioned<TDocument, TKey>(partitionKey);
            var createOptions = indexCreationOptions == null ? null : MapIndexOptions(indexCreationOptions);
            var indexKey = Builders<TDocument>.IndexKeys;
            return await collection.Indexes
                                   .CreateOneAsync(
                new CreateIndexModel<TDocument>(indexKey.Hashed(field), createOptions));
        }

        /// <inheritdoc />
        public async Task<string> CreateCombinedTextIndexAsync<TDocument>(IEnumerable<Expression<Func<TDocument, object>>> fields, IndexCreationOptions indexCreationOptions = null, string partitionKey = null) where TDocument : IDocument
        {
            return await CreateCombinedTextIndexAsync<TDocument, Guid>(fields, indexCreationOptions, partitionKey);
        }

        /// <inheritdoc />
        public async Task<string> CreateCombinedTextIndexAsync<TDocument, TKey>(IEnumerable<Expression<Func<TDocument, object>>> fields, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
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
                                   .CreateOneAsync(new CreateIndexModel<TDocument>(Builders<TDocument>.IndexKeys.Combine(listOfDefs), createOptions));
        }

        /// <inheritdoc />
        public async Task DropIndexAsync<TDocument>(string indexName, string partitionKey = null)
            where TDocument : IDocument
        {
            await DropIndexAsync<TDocument, Guid>(indexName, partitionKey);
        }

        /// <inheritdoc />
        public async Task DropIndexAsync<TDocument, TKey>(string indexName, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            await HandlePartitioned<TDocument, TKey>(partitionKey).Indexes.DropOneAsync(indexName);
        }

        /// <inheritdoc />
        public async Task<List<string>> GetIndexesNamesAsync<TDocument>(string partitionKey = null)
            where TDocument : IDocument
        {
            return await GetIndexesNamesAsync<TDocument, Guid>(partitionKey);
        }

        /// <inheritdoc />
        public async Task<List<string>> GetIndexesNamesAsync<TDocument, TKey>(string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var indexCursor = await HandlePartitioned<TDocument, TKey>(partitionKey).Indexes.ListAsync();
            var indexes = await indexCursor.ToListAsync();
            return indexes.Select(e => e["name"].ToString()).ToList();
        }


        #endregion Index Management

        private CreateIndexOptions MapIndexOptions(IndexCreationOptions indexCreationOptions)
        {
            return new CreateIndexOptions
            {
                Unique = indexCreationOptions.Unique,
                TextIndexVersion = indexCreationOptions.TextIndexVersion,
                SphereIndexVersion = indexCreationOptions.SphereIndexVersion,
                Sparse = indexCreationOptions.Sparse,
                Name = indexCreationOptions.Name,
                Min = indexCreationOptions.Min,
                Max = indexCreationOptions.Max,
                LanguageOverride = indexCreationOptions.LanguageOverride,
                ExpireAfter = indexCreationOptions.ExpireAfter,
                DefaultLanguage = indexCreationOptions.DefaultLanguage,
                BucketSize = indexCreationOptions.BucketSize,
                Bits = indexCreationOptions.Bits,
                Background = indexCreationOptions.Background,
                Version = indexCreationOptions.Version
            };
        }
    }
}