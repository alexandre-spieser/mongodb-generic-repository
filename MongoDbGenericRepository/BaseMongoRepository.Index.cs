using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDbGenericRepository.DataAccess.Index;
using MongoDbGenericRepository.Models;

namespace MongoDbGenericRepository
{
    /// <summary>
    ///     The base Repository, it is meant to be inherited from by your custom custom MongoRepository implementation.
    ///     Its constructor must be given a connection string and a database name.
    /// </summary>
    public abstract partial class BaseMongoRepository : IBaseMongoRepository_Index
    {
        private IMongoDbIndexHandler _mongoDbIndexHandler;

        /// <summary>
        ///     The MongoDb accessor to manage indexes.
        /// </summary>
        protected virtual IMongoDbIndexHandler MongoDbIndexHandler
        {
            get
            {
                if (_mongoDbIndexHandler != null)
                {
                    return _mongoDbIndexHandler;
                }

                lock (_initLock)
                {
                    if (_mongoDbIndexHandler == null)
                    {
                        _mongoDbIndexHandler = new MongoDbIndexHandler(MongoDbContext);
                    }
                }

                return _mongoDbIndexHandler;
            }
            set => _mongoDbIndexHandler = value;
        }

        /// <inheritdoc />
        public async Task<List<string>> GetIndexesNamesAsync<TDocument>()
            where TDocument : IDocument<Guid>
        {
            return await GetIndexesNamesAsync<TDocument, Guid>(null, CancellationToken.None);
        }


        /// <inheritdoc />
        public async Task<List<string>> GetIndexesNamesAsync<TDocument>(CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await GetIndexesNamesAsync<TDocument, Guid>(null, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<List<string>> GetIndexesNamesAsync<TDocument>(string partitionKey)
            where TDocument : IDocument<Guid>
        {
            return await GetIndexesNamesAsync<TDocument, Guid>(partitionKey);
        }

        /// <inheritdoc />
        public async Task<List<string>> GetIndexesNamesAsync<TDocument>(string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await GetIndexesNamesAsync<TDocument, Guid>(partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<List<string>> GetIndexesNamesAsync<TDocument, TKey>()
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetIndexesNamesAsync<TDocument, TKey>(null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<List<string>> GetIndexesNamesAsync<TDocument, TKey>(CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetIndexesNamesAsync<TDocument, TKey>(null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<List<string>> GetIndexesNamesAsync<TDocument, TKey>(string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetIndexesNamesAsync<TDocument, TKey>(partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<List<string>> GetIndexesNamesAsync<TDocument, TKey>(string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbIndexHandler.GetIndexesNamesAsync<TDocument, TKey>(partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<string> CreateTextIndexAsync<TDocument>(Expression<Func<TDocument, object>> field)
            where TDocument : IDocument<Guid>
        {
            return await CreateTextIndexAsync<TDocument, Guid>(field, null, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<string> CreateTextIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await CreateTextIndexAsync<TDocument, Guid>(field, null, null, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<string> CreateTextIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, string partitionKey)
            where TDocument : IDocument<Guid>
        {
            return await CreateTextIndexAsync<TDocument, Guid>(field, null, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<string> CreateTextIndexAsync<TDocument>(
            Expression<Func<TDocument, object>> field,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await CreateTextIndexAsync<TDocument, Guid>(field, null, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<string> CreateTextIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions)
            where TDocument : IDocument<Guid>
        {
            return await CreateTextIndexAsync<TDocument, Guid>(field, indexCreationOptions, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<string> CreateTextIndexAsync<TDocument>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await CreateTextIndexAsync<TDocument, Guid>(field, indexCreationOptions, null, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<string> CreateTextIndexAsync<TDocument>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions,
            string partitionKey)
            where TDocument : IDocument<Guid>
        {
            return await CreateTextIndexAsync<TDocument, Guid>(field, indexCreationOptions, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<string> CreateTextIndexAsync<TDocument>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await CreateTextIndexAsync<TDocument, Guid>(field, indexCreationOptions, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateTextIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateTextIndexAsync<TDocument, TKey>(field, null, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateTextIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateTextIndexAsync<TDocument, TKey>(field, null, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateTextIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateTextIndexAsync<TDocument, TKey>(field, null, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateTextIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateTextIndexAsync<TDocument, TKey>(field, null, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateTextIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateTextIndexAsync<TDocument, TKey>(field, indexCreationOptions, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateTextIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateTextIndexAsync<TDocument, TKey>(field, indexCreationOptions, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateTextIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateTextIndexAsync<TDocument, TKey>(field, indexCreationOptions, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateTextIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbIndexHandler.CreateTextIndexAsync<TDocument, TKey>(field, indexCreationOptions, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<string> CreateAscendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field)
            where TDocument : IDocument<Guid>
        {
            return await CreateAscendingIndexAsync<TDocument, Guid>(field, null, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<string> CreateAscendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await CreateAscendingIndexAsync<TDocument, Guid>(field, null, null, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<string> CreateAscendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions)
            where TDocument : IDocument<Guid>
        {
            return await CreateAscendingIndexAsync<TDocument, Guid>(field, indexCreationOptions, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<string> CreateAscendingIndexAsync<TDocument>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await CreateAscendingIndexAsync<TDocument, Guid>(field, indexCreationOptions, null, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<string> CreateAscendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, string partitionKey)
            where TDocument : IDocument<Guid>
        {
            return await CreateAscendingIndexAsync<TDocument, Guid>(field, null, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<string> CreateAscendingIndexAsync<TDocument>(
            Expression<Func<TDocument, object>> field,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await CreateAscendingIndexAsync<TDocument, Guid>(field, null, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<string> CreateAscendingIndexAsync<TDocument>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions,
            string partitionKey)
            where TDocument : IDocument<Guid>
        {
            return await CreateAscendingIndexAsync<TDocument, Guid>(field, indexCreationOptions, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<string> CreateAscendingIndexAsync<TDocument>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await CreateAscendingIndexAsync<TDocument, Guid>(field, indexCreationOptions, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateAscendingIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateAscendingIndexAsync<TDocument, TKey>(field, null, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateAscendingIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateAscendingIndexAsync<TDocument, TKey>(field, null, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateAscendingIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateAscendingIndexAsync<TDocument, TKey>(field, indexCreationOptions, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateAscendingIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateAscendingIndexAsync<TDocument, TKey>(field, indexCreationOptions, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateAscendingIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateAscendingIndexAsync<TDocument, TKey>(field, null, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateAscendingIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateAscendingIndexAsync<TDocument, TKey>(field, null, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateAscendingIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateAscendingIndexAsync<TDocument, TKey>(field, indexCreationOptions, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateAscendingIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbIndexHandler.CreateAscendingIndexAsync<TDocument, TKey>(field, indexCreationOptions, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<string> CreateDescendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field)
            where TDocument : IDocument<Guid>
        {
            return await CreateDescendingIndexAsync<TDocument, Guid>(field, null, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<string> CreateDescendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await CreateDescendingIndexAsync<TDocument, Guid>(field, null, null, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<string> CreateDescendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions)
            where TDocument : IDocument<Guid>
        {
            return await CreateDescendingIndexAsync<TDocument, Guid>(field, indexCreationOptions, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<string> CreateDescendingIndexAsync<TDocument>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await CreateDescendingIndexAsync<TDocument, Guid>(field, indexCreationOptions, null, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<string> CreateDescendingIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, string partitionKey)
            where TDocument : IDocument<Guid>
        {
            return await CreateDescendingIndexAsync<TDocument, Guid>(field, null, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<string> CreateDescendingIndexAsync<TDocument>(
            Expression<Func<TDocument, object>> field,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await CreateDescendingIndexAsync<TDocument, Guid>(field, null, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<string> CreateDescendingIndexAsync<TDocument>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions,
            string partitionKey)
            where TDocument : IDocument<Guid>
        {
            return await CreateDescendingIndexAsync<TDocument, Guid>(field, indexCreationOptions, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<string> CreateDescendingIndexAsync<TDocument>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await CreateDescendingIndexAsync<TDocument, Guid>(field, indexCreationOptions, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateDescendingIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateDescendingIndexAsync<TDocument, TKey>(field, null, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateDescendingIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateDescendingIndexAsync<TDocument, TKey>(field, null, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateDescendingIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateDescendingIndexAsync<TDocument, TKey>(field, indexCreationOptions, null, CancellationToken.None);
        }


        /// <inheritdoc />
        public virtual async Task<string> CreateDescendingIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateDescendingIndexAsync<TDocument, TKey>(field, indexCreationOptions, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateDescendingIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateDescendingIndexAsync<TDocument, TKey>(field, null, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateDescendingIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateDescendingIndexAsync<TDocument, TKey>(field, null, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateDescendingIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateDescendingIndexAsync<TDocument, TKey>(field, indexCreationOptions, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateDescendingIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbIndexHandler.CreateDescendingIndexAsync<TDocument, TKey>(field, indexCreationOptions, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<string> CreateHashedIndexAsync<TDocument>(Expression<Func<TDocument, object>> field)
            where TDocument : IDocument<Guid>
        {
            return await CreateHashedIndexAsync<TDocument, Guid>(field, null, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<string> CreateHashedIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await CreateHashedIndexAsync<TDocument, Guid>(field, null, null, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<string> CreateHashedIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, IndexCreationOptions indexCreationOptions)
            where TDocument : IDocument<Guid>
        {
            return await CreateHashedIndexAsync<TDocument, Guid>(field, indexCreationOptions, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<string> CreateHashedIndexAsync<TDocument>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await CreateHashedIndexAsync<TDocument, Guid>(field, indexCreationOptions, null, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<string> CreateHashedIndexAsync<TDocument>(Expression<Func<TDocument, object>> field, string partitionKey)
            where TDocument : IDocument<Guid>
        {
            return await CreateHashedIndexAsync<TDocument, Guid>(field, null, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<string> CreateHashedIndexAsync<TDocument>(
            Expression<Func<TDocument, object>> field,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await CreateHashedIndexAsync<TDocument, Guid>(field, null, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<string> CreateHashedIndexAsync<TDocument>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions,
            string partitionKey)
            where TDocument : IDocument<Guid>
        {
            return await CreateHashedIndexAsync<TDocument, Guid>(field, indexCreationOptions, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<string> CreateHashedIndexAsync<TDocument>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await CreateHashedIndexAsync<TDocument, Guid>(field, indexCreationOptions, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateHashedIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateHashedIndexAsync<TDocument, TKey>(field, null, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateHashedIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateHashedIndexAsync<TDocument, TKey>(field, null, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateHashedIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateHashedIndexAsync<TDocument, TKey>(field, indexCreationOptions, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateHashedIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateHashedIndexAsync<TDocument, TKey>(field, indexCreationOptions, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateHashedIndexAsync<TDocument, TKey>(Expression<Func<TDocument, object>> field, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateHashedIndexAsync<TDocument, TKey>(field, null, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateHashedIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateHashedIndexAsync<TDocument, TKey>(field, null, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateHashedIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateHashedIndexAsync<TDocument, TKey>(field, indexCreationOptions, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateHashedIndexAsync<TDocument, TKey>(
            Expression<Func<TDocument, object>> field,
            IndexCreationOptions indexCreationOptions,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbIndexHandler.CreateHashedIndexAsync<TDocument, TKey>(field, indexCreationOptions, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<string> CreateCombinedTextIndexAsync<TDocument>(IEnumerable<Expression<Func<TDocument, object>>> fields)
            where TDocument : IDocument<Guid>
        {
            return await CreateCombinedTextIndexAsync<TDocument, Guid>(fields, null, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<string> CreateCombinedTextIndexAsync<TDocument>(
            IEnumerable<Expression<Func<TDocument, object>>> fields,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await CreateCombinedTextIndexAsync<TDocument, Guid>(fields, null, null, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<string> CreateCombinedTextIndexAsync<TDocument>(
            IEnumerable<Expression<Func<TDocument, object>>> fields,
            IndexCreationOptions indexCreationOptions)
            where TDocument : IDocument<Guid>
        {
            return await CreateCombinedTextIndexAsync<TDocument, Guid>(fields, indexCreationOptions, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<string> CreateCombinedTextIndexAsync<TDocument>(
            IEnumerable<Expression<Func<TDocument, object>>> fields,
            IndexCreationOptions indexCreationOptions,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await CreateCombinedTextIndexAsync<TDocument, Guid>(fields, indexCreationOptions, null, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<string> CreateCombinedTextIndexAsync<TDocument>(IEnumerable<Expression<Func<TDocument, object>>> fields, string partitionKey)
            where TDocument : IDocument<Guid>
        {
            return await CreateCombinedTextIndexAsync<TDocument, Guid>(fields, null, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<string> CreateCombinedTextIndexAsync<TDocument>(
            IEnumerable<Expression<Func<TDocument, object>>> fields,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await CreateCombinedTextIndexAsync<TDocument, Guid>(fields, null, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<string> CreateCombinedTextIndexAsync<TDocument>(
            IEnumerable<Expression<Func<TDocument, object>>> fields,
            IndexCreationOptions indexCreationOptions,
            string partitionKey)
            where TDocument : IDocument<Guid>
        {
            return await CreateCombinedTextIndexAsync<TDocument, Guid>(fields, indexCreationOptions, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<string> CreateCombinedTextIndexAsync<TDocument>(
            IEnumerable<Expression<Func<TDocument, object>>> fields,
            IndexCreationOptions indexCreationOptions,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            return await CreateCombinedTextIndexAsync<TDocument, Guid>(fields, indexCreationOptions, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateCombinedTextIndexAsync<TDocument, TKey>(IEnumerable<Expression<Func<TDocument, object>>> fields)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateCombinedTextIndexAsync<TDocument, TKey>(fields, null, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateCombinedTextIndexAsync<TDocument, TKey>(
            IEnumerable<Expression<Func<TDocument, object>>> fields,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateCombinedTextIndexAsync<TDocument, TKey>(fields, null, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateCombinedTextIndexAsync<TDocument, TKey>(
            IEnumerable<Expression<Func<TDocument, object>>> fields,
            IndexCreationOptions indexCreationOptions)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateCombinedTextIndexAsync<TDocument, TKey>(fields, indexCreationOptions, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateCombinedTextIndexAsync<TDocument, TKey>(
            IEnumerable<Expression<Func<TDocument, object>>> fields,
            IndexCreationOptions indexCreationOptions,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateCombinedTextIndexAsync<TDocument, TKey>(fields, indexCreationOptions, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateCombinedTextIndexAsync<TDocument, TKey>(
            IEnumerable<Expression<Func<TDocument, object>>> fields,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateCombinedTextIndexAsync<TDocument, TKey>(fields, null, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateCombinedTextIndexAsync<TDocument, TKey>(
            IEnumerable<Expression<Func<TDocument, object>>> fields,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateCombinedTextIndexAsync<TDocument, TKey>(fields, null, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateCombinedTextIndexAsync<TDocument, TKey>(
            IEnumerable<Expression<Func<TDocument, object>>> fields,
            IndexCreationOptions indexCreationOptions,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await CreateCombinedTextIndexAsync<TDocument, TKey>(fields, indexCreationOptions, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<string> CreateCombinedTextIndexAsync<TDocument, TKey>(
            IEnumerable<Expression<Func<TDocument, object>>> fields,
            IndexCreationOptions indexCreationOptions,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbIndexHandler.CreateCombinedTextIndexAsync<TDocument, TKey>(fields, indexCreationOptions, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public async Task DropIndexAsync<TDocument>(string indexName)
            where TDocument : IDocument<Guid>
        {
            await DropIndexAsync<TDocument, Guid>(indexName, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task DropIndexAsync<TDocument>(string indexName, CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            await DropIndexAsync<TDocument, Guid>(indexName, null, cancellationToken);
        }

        /// <inheritdoc />
        public async Task DropIndexAsync<TDocument>(string indexName, string partitionKey)
            where TDocument : IDocument<Guid>
        {
            await DropIndexAsync<TDocument, Guid>(indexName, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task DropIndexAsync<TDocument>(string indexName, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<Guid>
        {
            await DropIndexAsync<TDocument, Guid>(indexName, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task DropIndexAsync<TDocument, TKey>(string indexName)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            await DropIndexAsync<TDocument, TKey>(indexName, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task DropIndexAsync<TDocument, TKey>(string indexName, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            await DropIndexAsync<TDocument, TKey>(indexName, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task DropIndexAsync<TDocument, TKey>(string indexName, string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            await DropIndexAsync<TDocument, TKey>(indexName, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task DropIndexAsync<TDocument, TKey>(string indexName, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            await MongoDbIndexHandler.DropIndexAsync<TDocument, TKey>(indexName, partitionKey, cancellationToken);
        }
    }
}