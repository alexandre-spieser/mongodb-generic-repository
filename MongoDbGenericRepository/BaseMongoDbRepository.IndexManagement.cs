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
        public async Task<string> CreateAscendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null) where TDocument : IDocument
        {
            return await CreateAscendingIndexAsync<TDocument, Guid>(field, indexCreationOptions, partitionKey);
        }

        /// <inheritdoc />
        public async Task<string> CreateDescendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument
        {
            return await CreateDescendingIndexAsync<TDocument, Guid>(field, indexCreationOptions, partitionKey);
        }


        /// <inheritdoc />
        public async Task<string> CreateHashedIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions = null, string partitionKey = null)
            where TDocument : IDocument
        {
            return await CreateHashedIndexAsync<TDocument, Guid>(field, indexCreationOptions, partitionKey);
        }

        /// <inheritdoc />
        public async Task<string> CreateCombinedTextIndexAsync<TDocument>(IEnumerable<Expression<Func<TDocument, object>>> fields, IndexCreationOptions indexCreationOptions = null, string partitionKey = null) where TDocument : IDocument
        {
            return await CreateCombinedTextIndexAsync<TDocument, Guid>(fields, indexCreationOptions, partitionKey);
        }


        /// <inheritdoc />
        public async Task DropIndexAsync<TDocument>(string indexName, string partitionKey = null)
            where TDocument : IDocument
        {
            await DropIndexAsync<TDocument, Guid>(indexName, partitionKey);
        }

        /// <inheritdoc />
        public async Task<List<string>> GetIndexesNamesAsync<TDocument>(string partitionKey = null)
            where TDocument : IDocument
        {
            return await GetIndexesNamesAsync<TDocument, Guid>(partitionKey);
        }

        #endregion Index Management


    }
}