﻿using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDbGenericRepository.Models;
using System.Linq.Expressions;

namespace MongoDbGenericRepository.DataAccess.Base
{
    /// <summary>
    /// A base class for accessing the Database and its Collections.
    /// </summary>
    public class DataAccessBase : IDataAccessBase
    {
        /// <summary>
        /// The MongoDbContext
        /// </summary>
        protected IMongoDbContext MongoDbContext;

        /// <summary>
        /// The constructor of the DataAccessBase class
        /// </summary>
        /// <param name="mongoDbContext"></param>
        public DataAccessBase(IMongoDbContext mongoDbContext)
        {
            MongoDbContext = mongoDbContext;
        }

        #region Utility Methods

        /// <summary>
        /// Gets a IMongoQueryable for a potentially partitioned document type and a filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="filter">The filter definition.</param>
        /// <param name="partitionKey">The collection partition key.</param>
        /// <returns></returns>
#if NETSTANDARD2_0 || NET472
        public virtual IMongoQueryable<TDocument> GetQuery<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetCollection<TDocument, TKey>(partitionKey).AsQueryable().Where(filter);
        }
#endif
#if NETSTANDARD2_1_OR_GREATER
        public virtual IQueryable<TDocument> GetQuery<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return GetCollection<TDocument, TKey>(partitionKey).AsQueryable().Where(filter);
        }
#endif

        /// <summary>
        /// Gets a collections for a potentially partitioned document type.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="document">The document.</param>
        /// <returns></returns>
        public virtual IMongoCollection<TDocument> HandlePartitioned<TDocument, TKey>(TDocument document)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            if (document is IPartitionedDocument)
            {
                return GetCollection<TDocument, TKey>(((IPartitionedDocument)document).PartitionKey);
            }
            return GetCollection<TDocument, TKey>();
        }

        /// <summary>
        /// Gets a collections for the type TDocument with a partition key.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="partitionKey">The collection partition key.</param>
        /// <returns></returns>
        public virtual IMongoCollection<TDocument> GetCollection<TDocument, TKey>(string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbContext.GetCollection<TDocument>(partitionKey);
        }

        /// <summary>
        /// Gets a collections for a potentially partitioned document type.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="partitionKey">The collection partition key.</param>
        /// <returns></returns>
        public virtual IMongoCollection<TDocument> HandlePartitioned<TDocument, TKey>(string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            if (!string.IsNullOrEmpty(partitionKey))
            {
                return GetCollection<TDocument, TKey>(partitionKey);
            }
            return GetCollection<TDocument, TKey>();
        }

        /// <summary>
        /// Converts a LINQ expression of TDocument, TValue to a LINQ expression of TDocument, object
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="expression">The expression to convert</param>
        protected virtual Expression<Func<TDocument, object>> ConvertExpression<TDocument, TValue>(Expression<Func<TDocument, TValue>> expression)
        {
            var param = expression.Parameters[0];
            Expression body = expression.Body;
            var convert = Expression.Convert(body, typeof(object));
            return Expression.Lambda<Func<TDocument, object>>(convert, param);
        }

        /// <summary>
        /// Maps a IndexCreationOptions object to a MongoDB.Driver.CreateIndexOptions object
        /// </summary>
        /// <param name="indexCreationOptions">The options for creating an index.</param>
        /// <returns></returns>
        protected virtual CreateIndexOptions MapIndexOptions(IndexCreationOptions indexCreationOptions)
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
                Bits = indexCreationOptions.Bits,
                Background = indexCreationOptions.Background,
                Version = indexCreationOptions.Version
            };
        }


        /// <summary>
        /// Gets the minimum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="minValueSelector">A property selector to order by ascending.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        protected virtual IFindFluent<TDocument, TDocument> GetMinMongoQuery<TDocument, TKey, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector, string partitionKey = null)
                    where TDocument : IDocument<TKey>
                    where TKey : IEquatable<TKey>
        {
            return GetCollection<TDocument, TKey>(partitionKey).Find(Builders<TDocument>.Filter.Where(filter))
                                                               .SortBy(ConvertExpression(minValueSelector))
                                                               .Limit(1);
        }

        /// <summary>
        /// Gets the minimum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="maxValueSelector">A property selector to order by descending.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        protected virtual IFindFluent<TDocument, TDocument> GetMaxMongoQuery<TDocument, TKey, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector, string partitionKey = null)
                    where TDocument : IDocument<TKey>
                    where TKey : IEquatable<TKey>
        {
            return GetCollection<TDocument, TKey>(partitionKey).Find(Builders<TDocument>.Filter.Where(filter))
                                                               .SortByDescending(ConvertExpression(maxValueSelector))
                                                               .Limit(1);
        }

        #endregion
    }
}
