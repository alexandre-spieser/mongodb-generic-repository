using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDbGenericRepository.Models;

namespace MongoDbGenericRepository.DataAccess.Read
{
    public partial class MongoDbReader
    {
        /// <inheritdoc />
        public virtual List<TProjection> GroupBy<TDocument, TGroupKey, TProjection, TKey>(
            Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class, new()
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey)
                .Aggregate()
                .Group(groupingCriteria, groupProjection)
                .ToList(cancellationToken);
        }

        /// <inheritdoc />
        public virtual List<TProjection> GroupBy<TDocument, TGroupKey, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TGroupKey>> selector,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> projection,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class, new()
        {
            var collection = HandlePartitioned<TDocument, TKey>(partitionKey);
            return collection.Aggregate()
                .Match(Builders<TDocument>.Filter.Where(filter))
                .Group(selector, projection)
                .ToList(cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<List<TProjection>> GroupByAsync<TDocument, TGroupKey, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TGroupKey>> selector,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> projection,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class, new()
        {
            var collection = HandlePartitioned<TDocument, TKey>(partitionKey);
            return await collection.Aggregate()
                .Match(Builders<TDocument>.Filter.Where(filter))
                .Group(selector, projection)
                .ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<List<TDocument>> GetSortedPaginatedAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> sortSelector,
            bool ascending = true,
            int skipNumber = 0,
            int takeNumber = 50,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var sorting = ascending
                ? Builders<TDocument>.Sort.Ascending(sortSelector)
                : Builders<TDocument>.Sort.Descending(sortSelector);

            return await HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(filter)
                .Sort(sorting)
                .Skip(skipNumber)
                .Limit(takeNumber)
                .ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<List<TDocument>> GetSortedPaginatedAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            SortDefinition<TDocument> sortDefinition,
            int skipNumber = 0,
            int takeNumber = 50,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(filter)
                .Sort(sortDefinition)
                .Skip(skipNumber)
                .Limit(takeNumber)
                .ToListAsync(cancellationToken);
        }
    }
}