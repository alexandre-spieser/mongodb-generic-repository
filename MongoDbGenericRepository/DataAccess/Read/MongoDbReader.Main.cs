using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDbGenericRepository.DataAccess.Base;
using MongoDbGenericRepository.Models;

namespace MongoDbGenericRepository.DataAccess.Read
{
    /// <summary>
    ///     A class to read MongoDb document.
    /// </summary>
    public partial class MongoDbReader : DataAccessBase, IMongoDbReader
    {
        /// <summary>
        ///     The construct of the MongoDbReader class.
        /// </summary>
        /// <param name="mongoDbContext">A <see cref="IMongoDbContext" /> instance.</param>
        public MongoDbReader(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {
        }

        #region Read TKey

        /// <inheritdoc />
        public virtual async Task<TDocument> GetByIdAsync<TDocument, TKey>(TKey id, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", id);
            return await HandlePartitioned<TDocument, TKey>(partitionKey).Find(filter).FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc />
        public virtual TDocument GetById<TDocument, TKey>(TKey id, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", id);
            return HandlePartitioned<TDocument, TKey>(partitionKey).Find(filter).FirstOrDefault(cancellationToken);
        }

        /// <inheritdoc />
        public virtual Task<TDocument> GetOneAsync<TDocument, TKey>(
            FilterDefinition<TDocument> condition,
            FindOptions findOption = null,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).Find(condition, findOption).FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc />
        public virtual TDocument GetOne<TDocument, TKey>(
            FilterDefinition<TDocument> condition,
            FindOptions findOption = null,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).Find(condition, findOption).FirstOrDefault(cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<TDocument> GetOneAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await HandlePartitioned<TDocument, TKey>(partitionKey).Find(filter).FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc />
        public virtual TDocument GetOne<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).Find(filter).FirstOrDefault(cancellationToken);
        }

        /// <inheritdoc />
        public virtual IFindFluent<TDocument, TDocument> GetCursor<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).Find(filter);
        }

        /// <inheritdoc />
        public virtual async Task<bool> AnyAsync<TDocument, TKey>(
            FilterDefinition<TDocument> condition,
            CountOptions countOption = null,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var count = await HandlePartitioned<TDocument, TKey>(partitionKey).CountDocumentsAsync(condition, countOption, cancellationToken);
            return count > 0;
        }

        /// <inheritdoc />
        public virtual bool Any<TDocument, TKey>(
            FilterDefinition<TDocument> condition,
            CountOptions countOption = null,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var count = HandlePartitioned<TDocument, TKey>(partitionKey).CountDocuments(condition, countOption, cancellationToken);
            return count > 0;
        }

        /// <inheritdoc />
        public virtual async Task<bool> AnyAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var count = await HandlePartitioned<TDocument, TKey>(partitionKey).CountDocumentsAsync(filter, cancellationToken: cancellationToken);
            return count > 0;
        }

        /// <inheritdoc />
        public virtual bool Any<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var count = HandlePartitioned<TDocument, TKey>(partitionKey).CountDocuments(filter, cancellationToken: cancellationToken);
            return count > 0;
        }

        /// <inheritdoc />
        public virtual Task<List<TDocument>> GetAllAsync<TDocument, TKey>(
            FilterDefinition<TDocument> condition,
            FindOptions findOption = null,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).Find(condition, findOption).ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public virtual List<TDocument> GetAll<TDocument, TKey>(
            FilterDefinition<TDocument> condition,
            FindOptions findOption = null,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).Find(condition, findOption).ToList(cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<List<TDocument>> GetAllAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await HandlePartitioned<TDocument, TKey>(partitionKey).Find(filter).ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public virtual List<TDocument> GetAll<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).Find(filter).ToList(cancellationToken);
        }

        /// <inheritdoc />
        public virtual Task<long> CountAsync<TDocument, TKey>(
            FilterDefinition<TDocument> condition,
            CountOptions countOption = null,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).CountDocumentsAsync(condition, countOption, cancellationToken);
        }

        /// <inheritdoc />
        public virtual long Count<TDocument, TKey>(
            FilterDefinition<TDocument> condition,
            CountOptions countOption = null,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).CountDocuments(condition, countOption, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<long> CountAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await HandlePartitioned<TDocument, TKey>(partitionKey).CountDocumentsAsync(filter, cancellationToken: cancellationToken);
        }

        /// <inheritdoc />
        public virtual long Count<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).Find(filter).CountDocuments(cancellationToken);
        }

        #endregion

        #region Min / Max

        /// <inheritdoc />
        public virtual async Task<TDocument> GetByMaxAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> maxValueSelector,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetCollection<TDocument, TKey>(partitionKey).Find(Builders<TDocument>.Filter.Where(filter))
                .SortByDescending(maxValueSelector)
                .Limit(1)
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc />
        public virtual TDocument GetByMax<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> maxValueSelector,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetCollection<TDocument, TKey>(partitionKey).Find(Builders<TDocument>.Filter.Where(filter))
                .SortByDescending(maxValueSelector)
                .Limit(1)
                .FirstOrDefault(cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<TDocument> GetByMinAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> minValueSelector,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetCollection<TDocument, TKey>(partitionKey).Find(Builders<TDocument>.Filter.Where(filter))
                .SortBy(minValueSelector)
                .Limit(1)
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc />
        public virtual TDocument GetByMin<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> minValueSelector,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetCollection<TDocument, TKey>(partitionKey).Find(Builders<TDocument>.Filter.Where(filter))
                .SortBy(minValueSelector)
                .Limit(1)
                .FirstOrDefault(cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<TValue> GetMaxValueAsync<TDocument, TKey, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> maxValueSelector,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetMaxMongoQuery<TDocument, TKey, TValue>(filter, maxValueSelector, partitionKey)
                .Project(maxValueSelector)
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc />
        public virtual TValue GetMaxValue<TDocument, TKey, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> maxValueSelector,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetMaxMongoQuery<TDocument, TKey, TValue>(filter, maxValueSelector, partitionKey)
                .Project(maxValueSelector)
                .FirstOrDefault(cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<TValue> GetMinValueAsync<TDocument, TKey, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> minValueSelector,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetMinMongoQuery<TDocument, TKey, TValue>(filter, minValueSelector, partitionKey).Project(minValueSelector)
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc />
        public virtual TValue GetMinValue<TDocument, TKey, TValue>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TValue>> minValueSelector,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetMinMongoQuery<TDocument, TKey, TValue>(filter, minValueSelector, partitionKey).Project(minValueSelector)
                .FirstOrDefault(cancellationToken);
        }

        #endregion Min / Max

        #region Sum TKey

        /// <inheritdoc />
        public virtual async Task<int> SumByAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, int>> selector,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetQuery<TDocument, TKey>(filter, partitionKey).SumAsync(selector, cancellationToken);
        }

        /// <inheritdoc />
        public virtual int SumBy<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, int>> selector,
            string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetQuery<TDocument, TKey>(filter, partitionKey).Sum(selector);
        }

        /// <inheritdoc />
        public virtual async Task<decimal> SumByAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, decimal>> selector,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await GetQuery<TDocument, TKey>(filter, partitionKey).SumAsync(selector, cancellationToken);
        }

        /// <inheritdoc />
        public virtual decimal SumBy<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, decimal>> selector,
            string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetQuery<TDocument, TKey>(filter, partitionKey).Sum(selector);
        }

        #endregion Sum TKey
    }
}