using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDbGenericRepository.DataAccess.Base;
using MongoDbGenericRepository.Models;

namespace MongoDbGenericRepository.DataAccess.Delete
{
    /// <inheritdoc cref="MongoDbGenericRepository.DataAccess.Delete.IMongoDbEraser" />
    public class MongoDbEraser : DataAccessBase, IMongoDbEraser
    {
        /// <summary>
        ///     The MongoDbEraser constructor.
        /// </summary>
        /// <param name="mongoDbContext">the MongoDb Context</param>
        public MongoDbEraser(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {
        }

        #region Delete TKey

        /// <inheritdoc />
        public virtual long DeleteOne<TDocument, TKey>(TDocument document, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", document.Id);
            return HandlePartitioned<TDocument, TKey>(document).DeleteOne(filter, cancellationToken).DeletedCount;
        }

        /// <inheritdoc />
        public virtual long DeleteOne<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).DeleteOne(filter, cancellationToken).DeletedCount;
        }

        /// <inheritdoc />
        public virtual async Task<long> DeleteOneAsync<TDocument, TKey>(TDocument document, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", document.Id);
            return (await HandlePartitioned<TDocument, TKey>(document).DeleteOneAsync(filter, cancellationToken)).DeletedCount;
        }

        /// <inheritdoc />
        public virtual async Task<long> DeleteOneAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return (await HandlePartitioned<TDocument, TKey>(partitionKey).DeleteOneAsync(filter, cancellationToken)).DeletedCount;
        }

        /// <inheritdoc />
        public virtual async Task<long> DeleteManyAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return (await HandlePartitioned<TDocument, TKey>(partitionKey).DeleteManyAsync(filter, cancellationToken)).DeletedCount;
        }

        /// <inheritdoc />
        public virtual async Task<long> DeleteManyAsync<TDocument, TKey>(IEnumerable<TDocument> documents, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var documentList = documents.ToList();

            if (!documentList.Any())
            {
                return 0;
            }

            // cannot use typeof(IPartitionedDocument).IsAssignableFrom(typeof(TDocument)), not available in netstandard 1.5
            if (documentList.Any(e => e is IPartitionedDocument))
            {
                long deleteCount = 0;
                foreach (var group in documentList.GroupBy(e => ((IPartitionedDocument) e).PartitionKey))
                {
                    var groupIdsToDelete = group.Select(e => e.Id).ToArray();
                    deleteCount += (await HandlePartitioned<TDocument, TKey>(group.FirstOrDefault())
                            .DeleteManyAsync(x => groupIdsToDelete.Contains(x.Id), cancellationToken))
                        .DeletedCount;
                }

                return deleteCount;
            }

            var idsToDelete = documentList.Select(e => e.Id).ToArray();
            return (await HandlePartitioned<TDocument, TKey>(documentList.FirstOrDefault()).DeleteManyAsync(x => idsToDelete.Contains(x.Id), cancellationToken))
                .DeletedCount;
        }

        /// <inheritdoc />
        public virtual long DeleteMany<TDocument, TKey>(IEnumerable<TDocument> documents, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            var documentList = documents.ToList();

            if (!documentList.Any())
            {
                return 0;
            }

            // cannot use typeof(IPartitionedDocument).IsAssignableFrom(typeof(TDocument)), not available in netstandard 1.5
            if (documentList.Any(e => e is IPartitionedDocument))
            {
                long deleteCount = 0;
                foreach (var group in documentList.GroupBy(e => ((IPartitionedDocument) e).PartitionKey))
                {
                    var groupIdsToDelete = group.Select(e => e.Id).ToArray();
                    deleteCount += HandlePartitioned<TDocument, TKey>(group.FirstOrDefault()).DeleteMany(x => groupIdsToDelete.Contains(x.Id), cancellationToken).DeletedCount;
                }

                return deleteCount;
            }

            var idsToDelete = documentList.Select(e => e.Id).ToArray();
            return HandlePartitioned<TDocument, TKey>(documentList.FirstOrDefault()).DeleteMany(x => idsToDelete.Contains(x.Id), cancellationToken).DeletedCount;
        }

        /// <inheritdoc />
        public virtual long DeleteMany<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).DeleteMany(filter, cancellationToken).DeletedCount;
        }

        #endregion
    }
}