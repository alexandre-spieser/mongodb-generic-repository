﻿using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MongoDbGenericRepository.DataAccess.Read
{
    public partial class MongoDbReader
    {
        /// <summary>
        /// Asynchronously returns a projected document matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="projection">A project definition.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public virtual Task<TProjection> ProjectOneAsync<TDocument, TProjection, TKey>(
            FilterDefinition<TDocument> condition, 
            ProjectionDefinition<TDocument, TProjection> projection,
            FindOptions findOption = null,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).Find(condition, findOption)
                .Project(projection)
                .FirstOrDefaultAsync(cancellationToken);
        }
        
        /// <summary>
        /// Asynchronously returns a projected document matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public virtual Task<TProjection> ProjectOneAsync<TDocument, TProjection, TKey>(
            FilterDefinition<TDocument> condition, 
            Expression<Func<TDocument, TProjection>> projection,
            FindOptions findOption = null,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).Find(condition, findOption)
                .Project(projection)
                .FirstOrDefaultAsync(cancellationToken);
        }
        
        /// <summary>
        /// Asynchronously returns a projected document matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public virtual async Task<TProjection> ProjectOneAsync<TDocument, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter, 
            Expression<Func<TDocument, TProjection>> projection, 
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return await HandlePartitioned<TDocument, TKey>(partitionKey).Find(filter)
                                                                         .Project(projection)
                                                                         .FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Returns a projected document matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="projection">A project definition.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual TProjection ProjectOne<TDocument, TProjection, TKey>(FilterDefinition<TDocument> condition,
            ProjectionDefinition<TDocument, TProjection> projection, FindOptions findOption = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).Find(condition, findOption)
                .Project(projection)
                .FirstOrDefault();
        }
        
        /// <summary>
        /// Returns a projected document matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual TProjection ProjectOne<TDocument, TProjection, TKey>(FilterDefinition<TDocument> condition,
            Expression<Func<TDocument, TProjection>> projection, FindOptions findOption = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).Find(condition, findOption)
                .Project(projection)
                .FirstOrDefault();
        }
        
        /// <summary>
        /// Returns a projected document matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual TProjection ProjectOne<TDocument, TProjection, TKey>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).Find(filter)
                                                                   .Project(projection)
                                                                   .FirstOrDefault();
        }

        /// <summary>
        /// Asynchronously returns a list of projected documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="projection">A project definition.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public virtual Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection, TKey>(
            FilterDefinition<TDocument> condition, 
            ProjectionDefinition<TDocument, TProjection> projection, 
            FindOptions findOption = null,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).Find(condition, findOption)
                .Project(projection)
                .ToListAsync(cancellationToken);
        }
        
        /// <summary>
        /// Asynchronously returns a list of projected documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public virtual Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection, TKey>(
            FilterDefinition<TDocument> condition, 
            Expression<Func<TDocument, TProjection>> projection, 
            FindOptions findOption = null,
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).Find(condition, findOption)
                .Project(projection)
                .ToListAsync(cancellationToken);
        }
        
        /// <summary>
        /// Asynchronously returns a list of projected documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public virtual async Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter, 
            Expression<Func<TDocument, TProjection>> projection, 
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return await HandlePartitioned<TDocument, TKey>(partitionKey).Find(filter)
                                                                   .Project(projection)
                                                                   .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Asynchronously returns a list of projected documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="projection">A project definition.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual List<TProjection> ProjectMany<TDocument, TProjection, TKey>(FilterDefinition<TDocument> condition,
            ProjectionDefinition<TDocument, TProjection> projection, FindOptions findOption = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).Find(condition, findOption)
                .Project(projection)
                .ToList();
        }
        
        /// <summary>
        /// Asynchronously returns a list of projected documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual List<TProjection> ProjectMany<TDocument, TProjection, TKey>(FilterDefinition<TDocument> condition,
            Expression<Func<TDocument, TProjection>> projection, FindOptions findOption = null, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).Find(condition, findOption)
                .Project(projection)
                .ToList();
        }
        
        /// <summary>
        /// Asynchronously returns a list of projected documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual List<TProjection> ProjectMany<TDocument, TProjection, TKey>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey).Find(filter)
                                                                   .Project(projection)
                                                                   .ToList();
        }
    }
}
