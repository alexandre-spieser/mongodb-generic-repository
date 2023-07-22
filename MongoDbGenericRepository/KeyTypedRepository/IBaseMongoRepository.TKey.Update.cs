using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDbGenericRepository.Models;

namespace MongoDbGenericRepository
{
    /// <summary>
    ///     The base Mongo Repository Update interface. used to update documents in the collections.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IBaseMongoRepository_Update<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        ///     Asynchronously Updates a document.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="modifiedDocument">The document with the modifications you want to persist.</param>
        Task<bool> UpdateOneAsync<TDocument>(TDocument modifiedDocument)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Asynchronously Updates a document.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="modifiedDocument">The document with the modifications you want to persist.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<bool> UpdateOneAsync<TDocument>(TDocument modifiedDocument, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Updates a document.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="modifiedDocument">The document with the modifications you want to persist.</param>
        bool UpdateOne<TDocument>(TDocument modifiedDocument)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Updates a document.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="modifiedDocument">The document with the modifications you want to persist.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        bool UpdateOne<TDocument>(TDocument modifiedDocument, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Takes a document you want to modify and applies the update you have defined in MongoDb.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="documentToModify">The document you want to modify.</param>
        /// <param name="update">The update definition for the document.</param>
        Task<bool> UpdateOneAsync<TDocument>(TDocument documentToModify, UpdateDefinition<TDocument> update)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Takes a document you want to modify and applies the update you have defined in MongoDb.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="documentToModify">The document you want to modify.</param>
        /// <param name="update">The update definition for the document.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<bool> UpdateOneAsync<TDocument>(TDocument documentToModify, UpdateDefinition<TDocument> update, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Takes a document you want to modify and applies the update you have defined in MongoDb.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="documentToModify">The document you want to modify.</param>
        /// <param name="update">The update definition for the document.</param>
        bool UpdateOne<TDocument>(TDocument documentToModify, UpdateDefinition<TDocument> update)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Takes a document you want to modify and applies the update you have defined in MongoDb.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="documentToModify">The document you want to modify.</param>
        /// <param name="update">The update definition for the document.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        bool UpdateOne<TDocument>(TDocument documentToModify, UpdateDefinition<TDocument> update, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Updates the property field with the given value update a property field in entities.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="documentToModify">The document you want to modify.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        bool UpdateOne<TDocument, TField>(TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Updates the property field with the given value update a property field in entities.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="documentToModify">The document you want to modify.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        bool UpdateOne<TDocument, TField>(
            TDocument documentToModify,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Updates the property field with the given value update a property field in entities.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="documentToModify">The document you want to modify.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        Task<bool> UpdateOneAsync<TDocument, TField>(TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Updates the property field with the given value update a property field in entities.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="documentToModify">The document you want to modify.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<bool> UpdateOneAsync<TDocument, TField>(
            TDocument documentToModify,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Updates the property field with the given value update a property field in entities.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        bool UpdateOne<TDocument, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Updates the property field with the given value update a property field in entities.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        bool UpdateOne<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Updates the property field with the given value update a property field in entities.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The value of the partition key.</param>
        bool UpdateOne<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Updates the property field with the given value update a property field in entities.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The value of the partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        bool UpdateOne<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entity selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        bool UpdateOne<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entity selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        bool UpdateOne<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entity selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The partition key for the document.</param>
        bool UpdateOne<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entity selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The partition key for the document.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        bool UpdateOne<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Updates the property field with the given value update a property field in entities.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        Task<bool> UpdateOneAsync<TDocument, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Updates the property field with the given value update a property field in entities.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<bool> UpdateOneAsync<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Updates the property field with the given value update a property field in entities.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The value of the partition key.</param>
        Task<bool> UpdateOneAsync<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     Updates the property field with the given value update a property field in entities.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The value of the partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<bool> UpdateOneAsync<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entity selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        Task<bool> UpdateOneAsync<TDocument, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entity selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<bool> UpdateOneAsync<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entity selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The partition key for the document.</param>
        Task<bool> UpdateOneAsync<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entity selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The partition key for the document.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<bool> UpdateOneAsync<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        Task<long> UpdateManyAsync<TDocument, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<long> UpdateManyAsync<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The partition key for the document.</param>
        Task<long> UpdateManyAsync<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The partition key for the document.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<long> UpdateManyAsync<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        Task<long> UpdateManyAsync<TDocument, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<long> UpdateManyAsync<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The value of the partition key.</param>
        Task<long> UpdateManyAsync<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The value of the partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<long> UpdateManyAsync<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, applies the update you have defined in MongoDb.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="updateDefinition">The update definition to apply.</param>
        Task<long> UpdateManyAsync<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, applies the update you have defined in MongoDb.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="updateDefinition">The update definition to apply.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<long> UpdateManyAsync<TDocument>(
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> updateDefinition,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, applies the update you have defined in MongoDb.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="updateDefinition">The update definition to apply.</param>
        /// <param name="partitionKey">The value of the partition key.</param>
        Task<long> UpdateManyAsync<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, applies the update you have defined in MongoDb.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="updateDefinition">The update definition to apply.</param>
        /// <param name="partitionKey">The value of the partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<long> UpdateManyAsync<TDocument>(
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, applies the update you have defined in MongoDb.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="updateDefinition">The update definition to apply.</param>
        Task<long> UpdateManyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, applies the update you have defined in MongoDb.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="updateDefinition">The update definition to apply.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<long> UpdateManyAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            UpdateDefinition<TDocument> updateDefinition,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, applies the update you have defined in MongoDb.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="updateDefinition">The update definition to apply.</param>
        /// <param name="partitionKey">The value of the partition key.</param>
        Task<long> UpdateManyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, applies the update you have defined in MongoDb.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="updateDefinition">The update definition to apply.</param>
        /// <param name="partitionKey">The value of the partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<long> UpdateManyAsync<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        long UpdateMany<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        long UpdateMany<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The partition key for the document.</param>
        long UpdateMany<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The partition key for the document.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        long UpdateMany<TDocument, TField>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        long UpdateMany<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        long UpdateMany<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The value of the partition key.</param>
        long UpdateMany<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, updates the property field with the given value.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="field">The field selector.</param>
        /// <param name="value">The new value of the property field.</param>
        /// <param name="partitionKey">The value of the partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        long UpdateMany<TDocument, TField>(
            FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, TField>> field,
            TField value,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, applies the update you have defined in MongoDb.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="updateDefinition">The update definition to apply.</param>
        long UpdateMany<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, applies the update you have defined in MongoDb.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="updateDefinition">The update definition to apply.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        long UpdateMany<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, applies the update you have defined in MongoDb.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="updateDefinition">The update definition to apply.</param>
        /// <param name="partitionKey">The value of the partition key.</param>
        long UpdateMany<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, applies the update you have defined in MongoDb.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="updateDefinition">The update definition to apply.</param>
        /// <param name="partitionKey">The value of the partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        long UpdateMany<TDocument>(
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, applies the update you have defined in MongoDb.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="updateDefinition">The update definition to apply.</param>
        long UpdateMany<TDocument>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, applies the update you have defined in MongoDb.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="updateDefinition">The update definition to apply.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        long UpdateMany<TDocument>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition, CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, applies the update you have defined in MongoDb.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="updateDefinition">The update definition to apply.</param>
        /// <param name="partitionKey">The value of the partition key.</param>
        long UpdateMany<TDocument>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition, string partitionKey)
            where TDocument : IDocument<TKey>;

        /// <summary>
        ///     For the entities selected by the filter, applies the update you have defined in MongoDb.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <param name="filter">The document filter.</param>
        /// <param name="updateDefinition">The update definition to apply.</param>
        /// <param name="partitionKey">The value of the partition key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        long UpdateMany<TDocument>(
            Expression<Func<TDocument, bool>> filter,
            UpdateDefinition<TDocument> updateDefinition,
            string partitionKey,
            CancellationToken cancellationToken)
            where TDocument : IDocument<TKey>;
    }
}