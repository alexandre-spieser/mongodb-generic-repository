using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDbGenericRepository.DataAccess.Base;
using MongoDbGenericRepository.Models;

namespace MongoDbGenericRepository.DataAccess.Update
{
    public interface IMongoDbUpdater : IDataAccessBase
    {
        /// <summary>
        /// Asynchronously Updates a document.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="modifiedDocument">The document with the modifications you want to persist.</param>
        Task<bool> UpdateOneAsync<TDocument, TKey>(TDocument modifiedDocument)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Takes a document you want to modify and applies the update you have defined in MongoDb.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="documentToModify">The document you want to modify.</param>
        /// <param name="update">The update definition for the document.</param>
        Task<bool> UpdateOneAsync<TDocument, TKey>(TDocument documentToModify, UpdateDefinition<TDocument> update)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Updates the property field with the given value update a property field in entities.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="documentToModify">The document you want to modify.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        Task<bool> UpdateOneAsync<TDocument, TKey, TField>(TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Updates the property field with the given value update a property field in entities.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The value of the partition key.</param>
        Task<bool> UpdateOneAsync<TDocument, TKey, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// For the entity selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The partition key for the document.</param>
        Task<bool> UpdateOneAsync<TDocument, TKey, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Updates a document.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="session">The client session.</param>
        /// <param name="modifiedDocument">The document with the modifications you want to persist.</param>
        /// <param name="cancellationToken">The optional cancellation token.</param>
        /// <returns></returns>
        Task<bool> UpdateOneAsync<TDocument, TKey>(IClientSessionHandle session, TDocument modifiedDocument, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Updates a document.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="session">The client session.</param>
        /// <param name="documentToModify">The document to modify.</param>
        /// <param name="update">The update definition.</param>
        /// <param name="cancellationToken">The optional cancellation token.</param>
        /// <returns></returns>
        Task<bool> UpdateOneAsync<TDocument, TKey>(IClientSessionHandle session, TDocument documentToModify, UpdateDefinition<TDocument> update, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Updates a document.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TField">The type of the field to update.</typeparam>
        /// <param name="session">The client session.</param> 
        /// <param name="documentToModify">The document to modify.</param>
        /// <param name="field">The field to update.</param>
        /// <param name="value">The value of the field.</param>
        /// <param name="cancellationToken">The optional cancellation token.</param>
        /// <returns></returns>
        Task<bool> UpdateOneAsync<TDocument, TKey, TField>(IClientSessionHandle session, TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Updates a document.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TField">The type of the field to update.</typeparam>
        /// <param name="session">The client session.</param>
        /// <param name="filter">The filter for the update.</param>
        /// <param name="field">The field to update.</param>
        /// <param name="value">The value of the field.</param>
        /// <param name="partitionKey">The optional partition key.</param>
        /// <param name="cancellationToken">The optional cancellation token.</param>
        /// <returns></returns>
        Task<bool> UpdateOneAsync<TDocument, TKey, TField>(IClientSessionHandle session, FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Updates a document.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TField">The type of the field to update.</typeparam>
        /// <param name="session">The client session.</param>
        /// <param name="filter">The filter for the update.</param>
        /// <param name="field">The field to update.</param>
        /// <param name="value">The value of the field.</param>
        /// <param name="partitionKey">The optional partition key.</param>
        /// <param name="cancellationToken">The optional cancellation token.</param>
        /// <returns></returns>
        Task<bool> UpdateOneAsync<TDocument, TKey, TField>(IClientSessionHandle session, Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Updates a document.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="modifiedDocument">The document with the modifications you want to persist.</param>
        bool UpdateOne<TDocument, TKey>(TDocument modifiedDocument)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Takes a document you want to modify and applies the update you have defined in MongoDb.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="documentToModify">The document you want to modify.</param>
        /// <param name="update">The update definition for the document.</param>
        bool UpdateOne<TDocument, TKey>(TDocument documentToModify, UpdateDefinition<TDocument> update)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Updates the property field with the given value update a property field in entities.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="documentToModify">The document you want to modify.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        bool UpdateOne<TDocument, TKey, TField>(TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The value of the partition key.</param>
        bool UpdateOne<TDocument, TKey, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// For the entity selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The partition key for the document.</param>
        bool UpdateOne<TDocument, TKey, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Updates a document.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="session">The client session.</param>
        /// <param name="modifiedDocument">The document with the modifications you want to persist.</param>
        /// <param name="cancellationToken">The optional cancellation token.</param>
        /// <returns></returns>
        bool UpdateOne<TDocument, TKey>(IClientSessionHandle session, TDocument modifiedDocument, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Updates a document.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="session">The client session.</param>
        /// <param name="documentToModify">The document to modify.</param>
        /// <param name="update">The update definition.</param>
        /// <param name="cancellationToken">The optional cancellation token.</param>
        /// <returns></returns>
        bool UpdateOne<TDocument, TKey>(IClientSessionHandle session, TDocument documentToModify, UpdateDefinition<TDocument> update, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Updates a document.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TField">The type of the field to update.</typeparam>
        /// <param name="session">The client session.</param>
        /// <param name="documentToModify">The document to modify.</param>
        /// <param name="field">The field to update.</param>
        /// <param name="value">The value of the field.</param>
        /// <param name="cancellationToken">The optional cancellation token.</param>
        /// <returns></returns>
        bool UpdateOne<TDocument, TKey, TField>(IClientSessionHandle session, TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Updates a document.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TField">The type of the field to update.</typeparam>
        /// <param name="session">The client session.</param>
        /// <param name="filter">The filter for the update.</param>
        /// <param name="field">The field to update.</param>
        /// <param name="value">The value of the field.</param>
        /// <param name="partitionKey">The optional partition key.</param>
        /// <param name="cancellationToken">The optional cancellation token.</param>
        /// <returns></returns>
        bool UpdateOne<TDocument, TKey, TField>(IClientSessionHandle session, FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Updates a document.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TField">The type of the field to update.</typeparam>
        /// <param name="session">The client session.</param>
        /// <param name="filter">The filter for the update.</param>
        /// <param name="field">The field to update.</param>
        /// <param name="value">The value of the field.</param>
        /// <param name="partitionKey">The optional partition key.</param>
        /// <param name="cancellationToken">The optional cancellation token.</param>
        /// <returns></returns>
        bool UpdateOne<TDocument, TKey, TField>(IClientSessionHandle session, Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// For the entities selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The partition key for the document.</param>
        Task<long> UpdateManyAsync<TDocument, TKey, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// For the entities selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The value of the partition key.</param>
        Task<long> UpdateManyAsync<TDocument, TKey, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// For the entities selected by the filter, apply the update definition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The value of the partition key.</param>
        Task<long> UpdateManyAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> update, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// For the entities selected by the filter, apply the update definition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="updateDefinition">The update definition.</param>
        /// <param name="partitionKey">The value of the partition key.</param>
        Task<long> UpdateManyAsync<TDocument, TKey>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// For the entities selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The partition key for the document.</param>
        long UpdateMany<TDocument, TKey, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// For the entities selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The value of the partition key.</param>
        long UpdateMany<TDocument, TKey, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// For the entities selected by the filter, apply the update definition.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="UpdateDefinition">The update definition.</param>
        /// <param name="partitionKey">The value of the partition key.</param>
        long UpdateMany<TDocument, TKey>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> UpdateDefinition, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Gets a IMongoQueryable for a potentially partitioned document type and a filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="filter">The filter definition.</param>
        /// <param name="partitionKey">The collection partition key.</param>
        /// <returns></returns>
        IMongoQueryable<TDocument> GetQuery<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Gets a collections for a potentially partitioned document type.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="document">The document.</param>
        /// <returns></returns>
        IMongoCollection<TDocument> HandlePartitioned<TDocument, TKey>(TDocument document)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Gets a collections for a potentially partitioned document type.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="partitionKey">The collection partition key.</param>
        /// <returns></returns>
        IMongoCollection<TDocument> HandlePartitioned<TDocument, TKey>(string partitionKey)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;

        /// <summary>
        /// Gets a collections for the type TDocument with a partition key.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="partitionKey">The collection partition key.</param>
        /// <returns></returns>
        IMongoCollection<TDocument> GetCollection<TDocument, TKey>(string partitionKey = null)
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>;
    }
}