﻿using MongoDB.Driver;
using MongoDbGenericRepository.DataAccess.Read;
using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MongoDbGenericRepository
{
    /// <summary>
    /// The base Repository, it is meant to be inherited from by your custom custom MongoRepository implementation.
    /// Its constructor must be given a connection string and a database name.
    /// </summary>
    public abstract class ReadOnlyMongoRepository : ReadOnlyMongoRepository<Guid>, IReadOnlyMongoRepository
    {
        /// <summary>
        /// The constructor taking a connection string and a database name.
        /// </summary>
        /// <param name="connectionString">The connection string of the MongoDb server.</param>
        /// <param name="databaseName">The name of the database against which you want to perform operations.</param>
        protected ReadOnlyMongoRepository(string connectionString, string databaseName = null) : base(connectionString, databaseName)
        {
        }

        /// <summary>
        /// The contructor taking a <see cref="IMongoDbContext"/>.
        /// </summary>
        /// <param name="mongoDbContext">A mongodb context implementing <see cref="IMongoDbContext"/></param>
        protected ReadOnlyMongoRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {
        }

        /// <summary>
        /// The contructor taking a <see cref="IMongoDatabase"/>.
        /// </summary>
        /// <param name="mongoDatabase">A mongodb context implementing <see cref="IMongoDatabase"/></param>
        protected ReadOnlyMongoRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }

        #region Read TKey

        /// <summary>
        /// Asynchronously returns one document given its id.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="id">The Id of the document you want to get.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public async virtual Task<TDocument> GetByIdAsync<TDocument, TKey>(TKey id, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbReader.GetByIdAsync<TDocument, TKey>(id, partitionKey, cancellationToken);
        }

        /// <summary>
        /// Returns one document given its id.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="id">The Id of the document you want to get.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual TDocument GetById<TDocument, TKey>(TKey id, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetById<TDocument, TKey>(id, partitionKey);
        }
        
        /// <summary>
        /// Asynchronously returns one document given filter definition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public Task<TDocument> GetOneAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, FindOptions findOption = null, string partitionKey = null,
            CancellationToken cancellationToken = default) where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetOneAsync<TDocument, TKey>(condition, findOption, partitionKey, cancellationToken);
        }

        /// <summary>
        /// Returns one document given filter definition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public TDocument GetOne<TDocument, TKey>(FilterDefinition<TDocument> condition, FindOptions findOption = null,
            string partitionKey = null) where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetOne<TDocument, TKey>(condition, findOption, partitionKey);
        }

        /// <summary>
        /// Asynchronously returns one document given an expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public async virtual Task<TDocument> GetOneAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbReader.GetOneAsync<TDocument, TKey>(filter, partitionKey, cancellationToken);
        }

        /// <summary>
        /// Returns one document given an expression filter.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual TDocument GetOne<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetOne<TDocument, TKey>(filter, partitionKey);
        }

        /// <summary>
        /// Returns a collection cursor.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual IFindFluent<TDocument, TDocument> GetCursor<TDocument, TKey>(FilterDefinition<TDocument> condition, FindOptions findOption = null, string partitionKey = null) where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetCursor<TDocument, TKey>(condition, findOption, partitionKey);
        }

        /// <summary>
        /// Returns a collection cursor.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual IFindFluent<TDocument, TDocument> GetCursor<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetCursor<TDocument, TKey>(filter, partitionKey);
        }
        
        /// <summary>
        /// Returns true if any of the document of the collection matches the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="countOption">A mongodb counting option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public Task<bool> AnyAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, CountOptions countOption = null, string partitionKey = null,
            CancellationToken cancellationToken = default) where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return MongoDbReader.AnyAsync<TDocument, TKey>(condition, countOption, partitionKey, cancellationToken);
        }

        /// <summary>
        /// Returns true if any of the document of the collection matches the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="countOption">A mongodb counting option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public bool Any<TDocument, TKey>(FilterDefinition<TDocument> condition, CountOptions countOption = null, string partitionKey = null) where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return MongoDbReader.Any<TDocument, TKey>(condition, countOption, partitionKey);
        }

        /// <summary>
        /// Returns true if any of the document of the collection matches the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public async virtual Task<bool> AnyAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbReader.AnyAsync<TDocument, TKey>(filter, partitionKey, cancellationToken);
        }

        /// <summary>
        /// Returns true if any of the document of the collection matches the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual bool Any<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.Any<TDocument, TKey>(filter, partitionKey);
        }
        
        /// <summary>
        /// Asynchronously returns a list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public Task<List<TDocument>> GetAllAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, FindOptions findOption = null, string partitionKey = null,
            CancellationToken cancellationToken = default) where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetAllAsync<TDocument, TKey>(condition, findOption, partitionKey, cancellationToken);
        }

        /// <summary>
        /// Returns a list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public List<TDocument> GetAll<TDocument, TKey>(FilterDefinition<TDocument> condition, FindOptions findOption = null,
            string partitionKey = null) where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetAll<TDocument, TKey>(condition, findOption, partitionKey);
        }

        /// <summary>
        /// Asynchronously returns a list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public async virtual Task<List<TDocument>> GetAllAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbReader.GetAllAsync<TDocument, TKey>(filter, partitionKey, cancellationToken);
        }

        /// <summary>
        /// Returns a list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual List<TDocument> GetAll<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetAll<TDocument, TKey>(filter, partitionKey);
        }
        
        /// <summary>
        /// Asynchronously counts how many documents match the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="countOption">A mongodb counting option.</param>
        /// <param name="partitionKey">An optional partitionKey</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public Task<long> CountAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, CountOptions countOption = null, string partitionKey = null,
            CancellationToken cancellationToken = default) where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return MongoDbReader.CountAsync<TDocument, TKey>(condition, countOption, partitionKey, cancellationToken);
        }

        /// <summary>
        /// Counts how many documents match the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="countOption">A mongodb counting option.</param>
        /// <param name="partitionKey">An optional partitionKey</param>
        public long Count<TDocument, TKey>(FilterDefinition<TDocument> condition, CountOptions countOption = null,
            string partitionKey = null) where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return MongoDbReader.Count<TDocument, TKey>(condition, countOption, partitionKey);
        }

        /// <summary>
        /// Asynchronously counts how many documents match the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partitionKey</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public async virtual Task<long> CountAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbReader.CountAsync<TDocument, TKey>(filter, partitionKey, cancellationToken);
        }

        /// <summary>
        /// Counts how many documents match the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partitionKey</param>
        public virtual long Count<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.Count<TDocument, TKey>(filter, partitionKey);
        }

        /// <summary>
        /// Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="maxValueSelector">A property selector to order by descending.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public virtual Task<TDocument> GetByMaxAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, Expression<Func<TDocument, object>> maxValueSelector, FindOptions findOption = null,
            string partitionKey = null, CancellationToken cancellationToken = default) where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetByMaxAsync<TDocument, TKey>(condition, maxValueSelector, findOption, partitionKey, cancellationToken);
        }

        /// <summary>
        /// Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="maxValueSelector">A property selector to order by descending.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public async virtual Task<TDocument> GetByMaxAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter, 
            Expression<Func<TDocument, object>> maxValueSelector, 
            string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbReader.GetByMaxAsync<TDocument, TKey>(filter, maxValueSelector, partitionKey, cancellationToken);
        }

        /// <summary>
        /// Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="maxValueSelector">A property selector to order by descending.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        public virtual TDocument GetByMax<TDocument, TKey>(FilterDefinition<TDocument> condition,
            Expression<Func<TDocument, object>> maxValueSelector, FindOptions findOption = null, string partitionKey = null)
            where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetByMax<TDocument, TKey>(condition, maxValueSelector, findOption, partitionKey);
        }

        /// <summary>
        /// Gets the document with the maximum value of a specified property in a MongoDB collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="maxValueSelector">A property selector to order by descending.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        public virtual TDocument GetByMax<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> maxValueSelector, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetByMax<TDocument, TKey>(filter, maxValueSelector, partitionKey);
        }

        /// <summary>
        /// Gets the document with the minimum value of a specified property in a MongoDB collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="minValueSelector">A property selector for the minimum value you are looking for.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public virtual Task<TDocument> GetByMinAsync<TDocument, TKey>(FilterDefinition<TDocument> condition,
            Expression<Func<TDocument, object>> minValueSelector, FindOptions findOption = null, string partitionKey = null,
            CancellationToken cancellationToken = default) where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetByMinAsync<TDocument, TKey>(condition, minValueSelector, findOption, partitionKey, cancellationToken);
        }

        /// <summary>
        /// Gets the document with the minimum value of a specified property in a MongoDB collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="minValueSelector">A property selector for the minimum value you are looking for.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public async virtual Task<TDocument> GetByMinAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, 
            Expression<Func<TDocument, object>> minValueSelector, 
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbReader.GetByMinAsync<TDocument, TKey>(filter, minValueSelector, partitionKey, cancellationToken);
        }

        /// <summary>
        /// Gets the document with the minimum value of a specified property in a MongoDB collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="minValueSelector">A property selector for the minimum value you are looking for.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        public virtual TDocument GetByMin<TDocument, TKey>(FilterDefinition<TDocument> condition,
            Expression<Func<TDocument, object>> minValueSelector, FindOptions findOption = null, string partitionKey = null)
            where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetByMin<TDocument, TKey>(condition, minValueSelector, findOption, partitionKey);
        }

        /// <summary>
        /// Gets the document with the minimum value of a specified property in a MongoDB collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="minValueSelector">A property selector for the minimum value you are looking for.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        public virtual TDocument GetByMin<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> minValueSelector, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetByMin<TDocument, TKey>(filter, minValueSelector, partitionKey);
        }

        public virtual Task<TValue> GetMaxValueAsync<TDocument, TKey, TValue>(FilterDefinition<TDocument> condition,
            Expression<Func<TDocument, TValue>> maxValueSelector, FindOptions findOption = null, string partitionKey = null,
            CancellationToken cancellationToken = default) where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetMaxValueAsync<TDocument, TKey, TValue>(condition, maxValueSelector, findOption, partitionKey, cancellationToken);
        }

        /// <summary>
        /// Gets the maximum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="maxValueSelector">A property selector for the maximum value you are looking for.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public async virtual Task<TValue> GetMaxValueAsync<TDocument, TKey, TValue>(
            Expression<Func<TDocument, bool>> filter, 
            Expression<Func<TDocument, TValue>> maxValueSelector, 
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbReader.GetMaxValueAsync<TDocument, TKey, TValue>(filter, maxValueSelector, partitionKey, cancellationToken);
        }

        /// <summary>
        /// Gets the maximum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="maxValueSelector">A property selector to order by ascending.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        public virtual TValue GetMaxValue<TDocument, TKey, TValue>(FilterDefinition<TDocument> condition,
            Expression<Func<TDocument, TValue>> maxValueSelector, FindOptions findOption = null, string partitionKey = null)
            where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetMaxValue<TDocument, TKey, TValue>(condition, maxValueSelector, findOption, partitionKey);
        }

        /// <summary>
        /// Gets the maximum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="maxValueSelector">A property selector to order by ascending.</param>
        /// <param name="partitionKey">An optional partitionKey.</param>
        public virtual TValue GetMaxValue<TDocument, TKey, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> maxValueSelector, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetMaxValue<TDocument, TKey, TValue>(filter, maxValueSelector, partitionKey);
        }

        /// <summary>
        /// Gets the minimum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="minValueSelector">A property selector to order by ascending.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public virtual Task<TValue> GetMinValueAsync<TDocument, TKey, TValue>(FilterDefinition<TDocument> condition,
            Expression<Func<TDocument, TValue>> minValueSelector, FindOptions findOption = null, string partitionKey = null,
            CancellationToken cancellationToken = default) where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetMinValueAsync<TDocument, TKey, TValue>(condition, minValueSelector, findOption, partitionKey, cancellationToken);
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
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public virtual async Task<TValue> GetMinValueAsync<TDocument, TKey, TValue>(
            Expression<Func<TDocument, bool>> filter, 
            Expression<Func<TDocument, TValue>> minValueSelector, 
            string partitionKey = null,
            CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbReader.GetMinValueAsync<TDocument, TKey, TValue>(filter, minValueSelector, partitionKey, cancellationToken);
        }

        /// <summary>
        /// Gets the minimum value of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <typeparam name="TValue">The type of the value used to order the query.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="minValueSelector">A property selector to order by ascending.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public TValue GetMinValue<TDocument, TKey, TValue>(FilterDefinition<TDocument> condition,
            Expression<Func<TDocument, TValue>> minValueSelector, FindOptions findOption = null, string partitionKey = null)
            where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetMinValue<TDocument, TKey, TValue>(condition, minValueSelector, findOption, partitionKey);
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
        public virtual TValue GetMinValue<TDocument, TKey, TValue>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TValue>> minValueSelector, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.GetMinValue<TDocument, TKey, TValue>(filter, minValueSelector, partitionKey);
        }

        #endregion

        #region Sum TKey

        /// <summary>
        /// Sums the values of a selected field for a given filtered collection of documents.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="selector">The field you want to sum.</param>
        /// <param name="partitionKey">The partition key of your document, if any.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public virtual async Task<int> SumByAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter,
                                                       Expression<Func<TDocument, int>> selector,
                                                       string partitionKey = null,
                                                       CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbReader.SumByAsync<TDocument, TKey>(filter, selector, partitionKey, cancellationToken);
        }

        /// <summary>
        /// Sums the values of a selected field for a given filtered collection of documents.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="selector">The field you want to sum.</param>
        /// <param name="partitionKey">The partition key of your document, if any.</param>
        public virtual int SumBy<TDocument, TKey>(Expression<Func<TDocument, bool>> filter,
                                                       Expression<Func<TDocument, int>> selector,
                                                       string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.SumBy<TDocument, TKey>(filter, selector, partitionKey);
        }

        /// <summary>
        /// Sums the values of a selected field for a given filtered collection of documents.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="selector">The field you want to sum.</param>
        /// <param name="partitionKey">The partition key of your document, if any.</param>
        public virtual async Task<decimal> SumByAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter,
                                                       Expression<Func<TDocument, decimal>> selector,
                                                       string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbReader.SumByAsync<TDocument, TKey>(filter, selector, partitionKey);
        }

        /// <summary>
        /// Sums the values of a selected field for a given filtered collection of documents.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="selector">The field you want to sum.</param>
        /// <param name="partitionKey">The partition key of your document, if any.</param>
        public virtual decimal SumBy<TDocument, TKey>(Expression<Func<TDocument, bool>> filter,
                                                       Expression<Func<TDocument, decimal>> selector,
                                                       string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.SumBy<TDocument, TKey>(filter, selector, partitionKey);
        }

        #endregion Sum TKey

        #region Project TKey

        /// <summary>
        /// Asynchronously returns a projected document matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="projection">A projection definition.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public virtual Task<TProjection> ProjectOneAsync<TDocument, TProjection, TKey>(FilterDefinition<TDocument> condition,
            ProjectionDefinition<TDocument, TProjection> projection, FindOptions findOption = null, string partitionKey = null,
            CancellationToken cancellationToken = default) 
            where TDocument : IDocument<TKey>
            where TProjection : class
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.ProjectOneAsync<TDocument, TProjection, TKey>(condition, projection, findOption, partitionKey, cancellationToken);
        }

        /// <summary>
        /// Asynchronously returns a projected document matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public virtual Task<TProjection> ProjectOneAsync<TDocument, TProjection, TKey>(FilterDefinition<TDocument> condition,
            Expression<Func<TDocument, TProjection>> projection, FindOptions findOption = null, string partitionKey = null,
            CancellationToken cancellationToken = default) 
            where TDocument : IDocument<TKey>
            where TProjection : class
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.ProjectOneAsync<TDocument, TProjection, TKey>(condition, projection, findOption, partitionKey, cancellationToken);
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
            return await MongoDbReader.ProjectOneAsync<TDocument, TProjection, TKey>(filter, projection, partitionKey, cancellationToken);
        }

        /// <summary>
        /// Returns a projected document matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="projection">A projection definition.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual TProjection ProjectOne<TDocument, TProjection, TKey>(FilterDefinition<TDocument> condition,
            ProjectionDefinition<TDocument, TProjection> projection, FindOptions findOption = null, string partitionKey = null)
            where TDocument : IDocument<TKey> 
            where TProjection : class 
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.ProjectOne<TDocument, TProjection, TKey>(condition, projection, findOption, partitionKey);
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
            where TProjection : class 
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.ProjectOne<TDocument, TProjection, TKey>(condition, projection, findOption, partitionKey);
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
            return MongoDbReader.ProjectOne<TDocument, TProjection, TKey>(filter, projection, partitionKey);
        }

        /// <summary>
        /// Asynchronously returns a list of projected documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="projection">A projection definition.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public virtual Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection, TKey>(FilterDefinition<TDocument> condition,
            ProjectionDefinition<TDocument, TProjection> projection, FindOptions findOption = null, string partitionKey = null,
            CancellationToken cancellationToken = default) 
            where TDocument : IDocument<TKey>
            where TProjection : class
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.ProjectManyAsync<TDocument, TProjection, TKey>(condition, projection, findOption, partitionKey, cancellationToken);
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
        public virtual Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection, TKey>(FilterDefinition<TDocument> condition,
            Expression<Func<TDocument, TProjection>> projection, FindOptions findOption = null, string partitionKey = null,
            CancellationToken cancellationToken = default) 
            where TDocument : IDocument<TKey>
            where TProjection : class
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.ProjectManyAsync<TDocument, TProjection, TKey>(condition, projection, findOption, partitionKey, cancellationToken);
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
            string partitionKey = null, CancellationToken cancellationToken = default)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class
        {
            return await MongoDbReader.ProjectManyAsync<TDocument, TProjection, TKey>(filter, projection, partitionKey, cancellationToken);
        }

        /// <summary>
        /// Asynchronously returns a list of projected documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TProjection">The type representing the model you want to project to.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="projection">A projection definition.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual List<TProjection> ProjectMany<TDocument, TProjection, TKey>(FilterDefinition<TDocument> condition,
            ProjectionDefinition<TDocument, TProjection> projection, FindOptions findOption = null, string partitionKey = null)
            where TDocument : IDocument<TKey> 
            where TProjection : class 
            where TKey : IEquatable<TKey>
        {
            return MongoDbReader.ProjectMany<TDocument, TProjection, TKey>(condition, projection, findOption, partitionKey);
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
        public virtual List<TProjection> ProjectMany<TDocument, TProjection, TKey>(FilterDefinition<TDocument> condition, Expression<Func<TDocument, TProjection>> projection, FindOptions findOption = null,
            string partitionKey = null) where TDocument : IDocument<TKey> where TProjection : class where TKey : IEquatable<TKey>
        {
            return MongoDbReader.ProjectMany<TDocument, TProjection, TKey>(condition, projection, findOption, partitionKey);
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
            return MongoDbReader.ProjectMany<TDocument, TProjection, TKey>(filter, projection, partitionKey);
        }

        #endregion Project TKey

        #region Group By TKey

        /// <summary>
        /// Groups filtered a collection of documents given a grouping criteria, 
        /// and returns a dictionary of listed document groups with keys having the different values of the grouping criteria.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TGroupKey">The type of the grouping criteria.</typeparam>
        /// <typeparam name="TProjection">The type of the projected group.</typeparam>
        /// <param name="groupingCriteria">The grouping criteria.</param>
        /// <param name="groupProjection">The projected group result.</param>
        /// <param name="partitionKey">The partition key of your document, if any.</param>
        public virtual List<TProjection> GroupBy<TDocument, TGroupKey, TProjection, TKey>(
            Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection,
            string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
            where TProjection : class, new()
        {
            return MongoDbReader.GroupBy<TDocument, TGroupKey, TProjection, TKey>(groupingCriteria, groupProjection, partitionKey);
        }

        /// <summary>
        /// Groups filtered a collection of documents given a grouping criteria, 
        /// and returns a dictionary of listed document groups with keys having the different values of the grouping criteria.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TGroupKey">The type of the grouping criteria.</typeparam>
        /// <typeparam name="TProjection">The type of the projected group.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="groupingCriteria">The grouping criteria.</param>
        /// <param name="groupProjection">The projected group result.</param>
        /// <param name="partitionKey">The partition key of your document, if any.</param>
        public virtual List<TProjection> GroupBy<TDocument, TGroupKey, TProjection, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TGroupKey>> groupingCriteria,
            Expression<Func<IGrouping<TGroupKey, TDocument>, TProjection>> groupProjection,
            string partitionKey = null)
                where TDocument : IDocument<TKey>
                where TKey : IEquatable<TKey>
                where TProjection : class, new()
        {
            return MongoDbReader.GroupBy<TDocument, TGroupKey, TProjection, TKey>(filter, groupingCriteria, groupProjection, partitionKey);
        }

        #endregion Group By TKey

        #region Pagination

        /// <summary>
        /// Asynchronously returns a paginated list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="sortSelector">The property selector.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="ascending">Order of the sorting.</param>
        /// <param name="skipNumber">The number of documents you want to skip. Default value is 0.</param>
        /// <param name="takeNumber">The number of documents you want to take. Default value is 50.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual Task<List<TDocument>> GetSortedPaginatedAsync<TDocument, TKey>(FilterDefinition<TDocument> condition,
            Expression<Func<TDocument, object>> sortSelector, FindOptions findOption = null, bool ascending = true, int skipNumber = 0,
            int takeNumber = 50, string partitionKey = null) where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            var sorting = ascending
                ? Builders<TDocument>.Sort.Ascending(sortSelector)
                : Builders<TDocument>.Sort.Descending(sortSelector);

            return HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(condition, findOption)
                .Sort(sorting)
                .Skip(skipNumber)
                .Limit(takeNumber)
                .ToListAsync();
        }

        /// <summary>
        /// Asynchronously returns a paginated list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="sortSelector">The property selector.</param>
        /// <param name="ascending">Order of the sorting.</param>
        /// <param name="skipNumber">The number of documents you want to skip. Default value is 0.</param>
        /// <param name="takeNumber">The number of documents you want to take. Default value is 50.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public virtual async Task<List<TDocument>> GetSortedPaginatedAsync<TDocument, TKey>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, object>> sortSelector,
            bool ascending = true,
            int skipNumber = 0,
            int takeNumber = 50,
            string partitionKey = null)
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
                                    .ToListAsync();
        }

        /// <summary>
        /// Asynchronously returns a paginated list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="condition">A mongodb filter definition.</param>
        /// <param name="sortDefinition">The sort definition.</param>
        /// <param name="findOption">A mongodb filter option.</param>
        /// <param name="skipNumber">The number of documents you want to skip. Default value is 0.</param>
        /// <param name="takeNumber">The number of documents you want to take. Default value is 50.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
        public virtual Task<List<TDocument>> GetSortedPaginatedAsync<TDocument, TKey>(FilterDefinition<TDocument> condition, SortDefinition<TDocument> sortDefinition, FindOptions findOption = null,
            int skipNumber = 0, int takeNumber = 50, string partitionKey = null, CancellationToken cancellationToken = default) where TDocument : IDocument<TKey> where TKey : IEquatable<TKey>
        {
            return HandlePartitioned<TDocument, TKey>(partitionKey)
                .Find(condition, findOption)
                .Sort(sortDefinition)
                .Skip(skipNumber)
                .Limit(takeNumber)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Asynchronously returns a paginated list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="sortDefinition">The sort definition.</param>
        /// <param name="skipNumber">The number of documents you want to skip. Default value is 0.</param>
        /// <param name="takeNumber">The number of documents you want to take. Default value is 50.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <param name="cancellationToken">An optional cancellation Token.</param>
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

        #endregion Pagination

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
            return MongoDbReader.HandlePartitioned<TDocument, TKey>(document);
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
        /// Gets a collections for a potentially partitioned document type.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <param name="document">The document.</param>
        /// <returns></returns>
        public virtual IMongoCollection<TDocument> HandlePartitioned<TDocument>(TDocument document)
            where TDocument : IDocument<Guid>
        {
            return MongoDbReader.HandlePartitioned<TDocument, Guid>(document);
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
            return MongoDbReader.GetCollection<TDocument, TKey>(partitionKey);
        }
    }
}