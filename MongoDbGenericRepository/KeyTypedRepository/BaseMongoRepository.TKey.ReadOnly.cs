using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDbGenericRepository.DataAccess.Read;
using MongoDbGenericRepository.Models;

namespace MongoDbGenericRepository
{
    /// <summary>
    ///     The base Repository, it is meant to be inherited from by your custom custom MongoRepository implementation.
    ///     Its constructor must be given a connection string and a database name.
    /// </summary>
    public abstract class ReadOnlyMongoRepository<TKey> : IReadOnlyMongoRepository<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        ///     The MongoDbContext
        /// </summary>
        protected IMongoDbContext MongoDbContext { get; set; }

        /// <summary>
        ///     A MongoDb Reader for read operations
        /// </summary>
        protected IMongoDbReader MongoDbReader { get; set; }

        /// <summary>
        ///     The constructor taking a connection string and a database name.
        /// </summary>
        /// <param name="connectionString">The connection string of the MongoDb server.</param>
        /// <param name="databaseName">The name of the database against which you want to perform operations.</param>
        protected ReadOnlyMongoRepository(string connectionString, string databaseName = null)
        {
            SetupMongoDbContext(connectionString, databaseName);
        }

        /// <summary>
        ///     The constructor taking a <see cref="IMongoDatabase" />.
        /// </summary>
        /// <param name="mongoDatabase">A mongodb context implementing <see cref="IMongoDatabase" /></param>
        protected ReadOnlyMongoRepository(IMongoDatabase mongoDatabase)
            : this(new MongoDbContext(mongoDatabase))
        {
        }

        /// <summary>
        ///     The constructor taking a <see cref="IMongoDbContext" />.
        /// </summary>
        /// <param name="mongoDbContext">A mongodb context implementing <see cref="IMongoDbContext" /></param>
        protected ReadOnlyMongoRepository(IMongoDbContext mongoDbContext)
        {
            SetupMongoDbContext(mongoDbContext);
        }

        /// <summary>
        ///     The connection string.
        /// </summary>
        public string ConnectionString { get; protected set; }

        /// <summary>
        ///     The database name.
        /// </summary>
        public string DatabaseName { get; protected set; }

        /// <inheritdoc />
        public virtual List<TProjection> GroupBy<TDocument, TGroupKey, TProjection>(
            Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection)
            where TDocument : IDocument<TKey>
            where TProjection : class, new()
        {
            return GroupBy(groupingCriteria, groupProjection, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual List<TProjection> GroupBy<TDocument, TGroupKey, TProjection>(
            Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TProjection : class, new()
        {
            return GroupBy(groupingCriteria, groupProjection, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual List<TProjection> GroupBy<TDocument, TGroupKey, TProjection>(
            Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TProjection : class, new()
        {
            return GroupBy(groupingCriteria, groupProjection, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual List<TProjection> GroupBy<TDocument, TGroupKey, TProjection>(
            Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TProjection : class, new()
        {
            return MongoDbReader.GroupBy<TDocument, TGroupKey, TProjection, TKey>(groupingCriteria, groupProjection, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual List<TProjection> GroupBy<TDocument, TGroupKey, TProjection>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection)
            where TDocument : IDocument<TKey>
            where TProjection : class, new()
        {
            return GroupBy(filter, groupingCriteria, groupProjection, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual List<TProjection> GroupBy<TDocument, TGroupKey, TProjection>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TProjection : class, new()
        {
            return GroupBy(filter, groupingCriteria, groupProjection, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual List<TProjection> GroupBy<TDocument, TGroupKey, TProjection>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TProjection : class, new()
        {
            return GroupBy(filter, groupingCriteria, groupProjection, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual List<TProjection> GroupBy<TDocument, TGroupKey, TProjection>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TProjection : class, new()
        {
            return MongoDbReader.GroupBy<TDocument, TGroupKey, TProjection, TKey>(filter, groupingCriteria, groupProjection, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<List<TDocument>> GetSortedPaginatedAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> sortSelector,
            bool ascending = true,
            int skipNumber = 0,
            int takeNumber = 50,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbReader.GetSortedPaginatedAsync<TDocument, TKey>(
                filter,
                sortSelector,
                ascending,
                skipNumber,
                takeNumber,
                partitionKey,
                cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<List<TDocument>> GetSortedPaginatedAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            SortDefinition<TDocument> sortDefinition,
            int skipNumber = 0,
            int takeNumber = 50,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbReader.GetSortedPaginatedAsync<TDocument, TKey>(
                filter,
                sortDefinition,
                skipNumber,
                takeNumber,
                partitionKey,
                cancellationToken);
        }

        /// <summary>
        ///     Setups the repository with a <see cref="IMongoDbContext" />.
        /// </summary>
        /// <param name="mongoDbContext"></param>
        protected void SetupMongoDbContext(IMongoDbContext mongoDbContext)
        {
            MongoDbContext = MongoDbContext ?? mongoDbContext;
            MongoDbReader = MongoDbReader ?? new MongoDbReader(MongoDbContext);
        }

        /// <summary>
        ///     Setups the repository with a connection string and a database name.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="databaseName">
        ///     The database name. If the database name is null or whitespace it is taken from the
        ///     connection string
        /// </param>
        protected void SetupMongoDbContext(string connectionString, string databaseName)
        {
            if (string.IsNullOrWhiteSpace(databaseName))
            {
                var mongoUrl = new MongoUrl(connectionString);
                databaseName = mongoUrl.DatabaseName;
            }

            ConnectionString = connectionString;
            DatabaseName = databaseName;
            SetupMongoDbContext(new MongoDbContext(connectionString, databaseName));
        }

        #region Read

        /// <inheritdoc />
        public async Task<TDocument> GetByIdAsync<TDocument>(TKey id)
            where TDocument : IDocument<TKey>
        {
            return await GetByIdAsync<TDocument>(id, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<TDocument> GetByIdAsync<TDocument>(TKey id, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await GetByIdAsync<TDocument>(id, null, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<TDocument> GetByIdAsync<TDocument>(TKey id, string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return await GetByIdAsync<TDocument>(id, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<TDocument> GetByIdAsync<TDocument>(TKey id, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbReader.GetByIdAsync<TDocument, TKey>(id, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public TDocument GetById<TDocument>(TKey id)
            where TDocument : IDocument<TKey>
        {
            return GetById<TDocument>(id, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public TDocument GetById<TDocument>(TKey id, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return GetById<TDocument>(id, null, cancellationToken);
        }

        /// <inheritdoc />
        public TDocument GetById<TDocument>(TKey id, string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return GetById<TDocument>(id, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public TDocument GetById<TDocument>(TKey id, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return MongoDbReader.GetById<TDocument, TKey>(id, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<TDocument> GetOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>
        {
            return await GetOneAsync(filter, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<TDocument> GetOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await GetOneAsync(filter, null, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<TDocument> GetOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return await GetOneAsync(filter, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<TDocument> GetOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbReader.GetOneAsync<TDocument, TKey>(filter, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public TDocument GetOne<TDocument>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>
        {
            return GetOne(filter, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public TDocument GetOne<TDocument>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return GetOne(filter, null, cancellationToken);
        }

        /// <inheritdoc />
        public TDocument GetOne<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return GetOne(filter, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public TDocument GetOne<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return MongoDbReader.GetOne<TDocument, TKey>(filter, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public IFindFluent<TDocument, TDocument> GetCursor<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
        {
            return MongoDbReader.GetCursor<TDocument, TKey>(filter, partitionKey);
        }

        /// <inheritdoc />
        public async Task<bool> AnyAsync<TDocument>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>
        {
            return await AnyAsync(filter, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<bool> AnyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await AnyAsync(filter, null, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<bool> AnyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return await AnyAsync(filter, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<bool> AnyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbReader.AnyAsync<TDocument, TKey>(filter, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public bool Any<TDocument>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>
        {
            return Any(filter, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public bool Any<TDocument>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return Any(filter, null, cancellationToken);
        }

        /// <inheritdoc />
        public bool Any<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return Any(filter, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public bool Any<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return MongoDbReader.Any<TDocument, TKey>(filter, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<List<TDocument>> GetAllAsync<TDocument>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>
        {
            return await GetAllAsync(filter, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<List<TDocument>> GetAllAsync<TDocument>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await GetAllAsync(filter, null, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<List<TDocument>> GetAllAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return await GetAllAsync(filter, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<List<TDocument>> GetAllAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbReader.GetAllAsync<TDocument, TKey>(filter, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public List<TDocument> GetAll<TDocument>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>
        {
            return GetAll(filter, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public List<TDocument> GetAll<TDocument>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return GetAll(filter, null, cancellationToken);
        }

        /// <inheritdoc />
        public List<TDocument> GetAll<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return GetAll(filter, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public List<TDocument> GetAll<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return MongoDbReader.GetAll<TDocument, TKey>(filter, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<long> CountAsync<TDocument>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>
        {
            return await CountAsync(filter, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<long> CountAsync<TDocument>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await CountAsync(filter, null, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<long> CountAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return await CountAsync(filter, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<long> CountAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbReader.CountAsync<TDocument, TKey>(filter, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public long Count<TDocument>(Expression<Func<TDocument, bool>> filter)
            where TDocument : IDocument<TKey>
        {
            return Count(filter, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public long Count<TDocument>(Expression<Func<TDocument, bool>> filter, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return Count(filter, null, cancellationToken);
        }

        /// <inheritdoc />
        public long Count<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return Count(filter, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public long Count<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return MongoDbReader.Count<TDocument, TKey>(filter, partitionKey, cancellationToken);
        }


        /// <inheritdoc />
        public async Task<TDocument> GetByMaxAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> maxValueSelector)
            where TDocument : IDocument<TKey>
        {
            return await GetByMaxAsync(filter, maxValueSelector, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<TDocument> GetByMaxAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> maxValueSelector,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await GetByMaxAsync(filter, maxValueSelector, null, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<TDocument> GetByMaxAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> maxValueSelector,
            string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return await GetByMaxAsync(filter, maxValueSelector, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<TDocument> GetByMaxAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> maxValueSelector,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbReader.GetByMaxAsync<TDocument, TKey>(filter, maxValueSelector, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public TDocument GetByMax<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> maxValueSelector)
            where TDocument : IDocument<TKey>
        {
            return GetByMax(filter, maxValueSelector, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public TDocument GetByMax<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> maxValueSelector,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return GetByMax(filter, maxValueSelector, null, cancellationToken);
        }

        /// <inheritdoc />
        public TDocument GetByMax<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> maxValueSelector,
            string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return GetByMax(filter, maxValueSelector, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public TDocument GetByMax<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> maxValueSelector,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return MongoDbReader.GetByMax<TDocument, TKey>(filter, maxValueSelector, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<TDocument> GetByMinAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> minValueSelector)
            where TDocument : IDocument<TKey>
        {
            return await GetByMinAsync(filter, minValueSelector, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<TDocument> GetByMinAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> minValueSelector,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await GetByMinAsync(filter, minValueSelector, null, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<TDocument> GetByMinAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> minValueSelector,
            string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return await GetByMinAsync(filter, minValueSelector, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<TDocument> GetByMinAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> minValueSelector,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbReader.GetByMinAsync<TDocument, TKey>(filter, minValueSelector, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public TDocument GetByMin<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> minValueSelector)
            where TDocument : IDocument<TKey>
        {
            return GetByMin(filter, minValueSelector, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public TDocument GetByMin<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> minValueSelector,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return GetByMin(filter, minValueSelector, null, cancellationToken);
        }

        /// <inheritdoc />
        public TDocument GetByMin<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> minValueSelector,
            string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return GetByMin(filter, minValueSelector, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public TDocument GetByMin<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> minValueSelector,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return MongoDbReader.GetByMin<TDocument, TKey>(filter, minValueSelector, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<TValue> GetMaxValueAsync<TDocument, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> maxValueSelector)
            where TDocument : IDocument<TKey>
        {
            return await GetMaxValueAsync(filter, maxValueSelector, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<TValue> GetMaxValueAsync<TDocument, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> maxValueSelector,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await GetMaxValueAsync(filter, maxValueSelector, null, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<TValue> GetMaxValueAsync<TDocument, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> maxValueSelector,
            string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return await GetMaxValueAsync(filter, maxValueSelector, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public async Task<TValue> GetMaxValueAsync<TDocument, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> maxValueSelector,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbReader.GetMaxValueAsync<TDocument, TKey, TValue>(filter, maxValueSelector, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public TValue GetMaxValue<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector)
            where TDocument : IDocument<TKey>
        {
            return GetMaxValue(filter, maxValueSelector, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public TValue GetMaxValue<TDocument, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> maxValueSelector,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return GetMaxValue(filter, maxValueSelector, null, cancellationToken);
        }

        /// <inheritdoc />
        public TValue GetMaxValue<TDocument, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> maxValueSelector,
            string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return GetMaxValue(filter, maxValueSelector, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public TValue GetMaxValue<TDocument, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> maxValueSelector,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return MongoDbReader.GetMaxValue<TDocument, TKey, TValue>(filter, maxValueSelector, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<TValue> GetMinValueAsync<TDocument, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> minValueSelector)
            where TDocument : IDocument<TKey>
        {
            return await GetMinValueAsync(filter, minValueSelector, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<TValue> GetMinValueAsync<TDocument, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> minValueSelector,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await GetMinValueAsync(filter, minValueSelector, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<TValue> GetMinValueAsync<TDocument, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> minValueSelector,
            string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return await GetMinValueAsync(filter, minValueSelector, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<TValue> GetMinValueAsync<TDocument, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> minValueSelector,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbReader.GetMinValueAsync<TDocument, TKey, TValue>(filter, minValueSelector, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual TValue GetMinValue<TDocument, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector)
            where TDocument : IDocument<TKey>
        {
            return GetMinValue(filter, minValueSelector, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual TValue GetMinValue<TDocument, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> minValueSelector,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return GetMinValue(filter, minValueSelector, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual TValue GetMinValue<TDocument, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> minValueSelector,
            string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return GetMinValue(filter, minValueSelector, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual TValue GetMinValue<TDocument, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> minValueSelector,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return MongoDbReader.GetMinValue<TDocument, TKey, TValue>(filter, minValueSelector, partitionKey, cancellationToken);
        }

        #endregion

        #region Maths

        /// <inheritdoc />
        public virtual async Task<int> SumByAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, int>> selector)
            where TDocument : IDocument<TKey>
        {
            return await SumByAsync(filter, selector, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<int> SumByAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, int>> selector,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await SumByAsync(filter, selector, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<int> SumByAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, int>> selector,
            string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return await SumByAsync(filter, selector, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<int> SumByAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, int>> selector,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbReader.SumByAsync<TDocument, TKey>(filter, selector, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<decimal> SumByAsync<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, decimal>> selector)
            where TDocument : IDocument<TKey>
        {
            return await SumByAsync(filter, selector, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<decimal> SumByAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, decimal>> selector,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await SumByAsync(filter, selector, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<decimal> SumByAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, decimal>> selector,
            string partitionKey)
            where TDocument : IDocument<TKey>
        {
            return await SumByAsync(filter, selector, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<decimal> SumByAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, decimal>> selector,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
        {
            return await MongoDbReader.SumByAsync<TDocument, TKey>(filter, selector, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual int SumBy<TDocument>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, int>> selector, string partitionKey = null)
            where TDocument : IDocument<TKey>
        {
            return MongoDbReader.SumBy<TDocument, TKey>(filter, selector, partitionKey);
        }

        /// <inheritdoc />
        public virtual decimal SumBy<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, decimal>> selector,
            string partitionKey = null)
            where TDocument : IDocument<TKey>
        {
            return MongoDbReader.SumBy<TDocument, TKey>(filter, selector, partitionKey);
        }

        #endregion Maths

        #region Project

        /// <inheritdoc />
        public virtual async Task<TProjection> ProjectOneAsync<TDocument, TProjection>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection)
            where TDocument : IDocument<TKey>
            where TProjection : class
        {
            return await ProjectOneAsync(filter, projection, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<TProjection> ProjectOneAsync<TDocument, TProjection>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TProjection : class
        {
            return await ProjectOneAsync(filter, projection, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<TProjection> ProjectOneAsync<TDocument, TProjection>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TProjection : class
        {
            return await ProjectOneAsync(filter, projection, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<TProjection> ProjectOneAsync<TDocument, TProjection>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TProjection : class
        {
            return await MongoDbReader.ProjectOneAsync<TDocument, TProjection, TKey>(filter, projection, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual TProjection ProjectOne<TDocument, TProjection>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection)
            where TDocument : IDocument<TKey>
            where TProjection : class
        {
            return ProjectOne(filter, projection, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual TProjection ProjectOne<TDocument, TProjection>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TProjection : class
        {
            return ProjectOne(filter, projection, null, cancellationToken);
        }


        /// <inheritdoc />
        public virtual TProjection ProjectOne<TDocument, TProjection>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TProjection : class
        {
            return ProjectOne(filter, projection, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual TProjection ProjectOne<TDocument, TProjection>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TProjection : class
        {
            return MongoDbReader.ProjectOne<TDocument, TProjection, TKey>(filter, projection, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection)
            where TDocument : IDocument<TKey>
            where TProjection : class
        {
            return await ProjectManyAsync(filter, projection, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TProjection : class
        {
            return await ProjectManyAsync(filter, projection, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TProjection : class
        {
            return await ProjectManyAsync(filter, projection, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual async Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TProjection : class
        {
            return await MongoDbReader.ProjectManyAsync<TDocument, TProjection, TKey>(filter, projection, partitionKey, cancellationToken);
        }

        /// <inheritdoc />
        public virtual List<TProjection> ProjectMany<TDocument, TProjection>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection)
            where TDocument : IDocument<TKey>
            where TProjection : class
        {
            return ProjectMany(filter, projection, null, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual List<TProjection> ProjectMany<TDocument, TProjection>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TProjection : class
        {
            return ProjectMany(filter, projection, null, cancellationToken);
        }

        /// <inheritdoc />
        public virtual List<TProjection> ProjectMany<TDocument, TProjection>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            string partitionKey)
            where TDocument : IDocument<TKey>
            where TProjection : class
        {
            return ProjectMany(filter, projection, partitionKey, CancellationToken.None);
        }

        /// <inheritdoc />
        public virtual List<TProjection> ProjectMany<TDocument, TProjection>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TProjection : class
        {
            return MongoDbReader.ProjectMany<TDocument, TProjection, TKey>(filter, projection, partitionKey, cancellationToken);
        }

        #endregion Project
    }
}