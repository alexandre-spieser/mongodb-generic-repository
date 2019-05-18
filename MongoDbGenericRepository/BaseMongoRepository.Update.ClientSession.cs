using MongoDB.Driver;
using MongoDbGenericRepository.DataAccess.Update;
using MongoDbGenericRepository.Models;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MongoDbGenericRepository
{
    public abstract partial class BaseMongoRepository : IBaseMongoRepository_Update_ClientSession
    {
        /// <summary>
        /// Updates a document.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="session">The client session.</param>
        /// <param name="modifiedDocument">The document with the modifications you want to persist.</param>
        /// <param name="cancellationToken">The optional cancellation token.</param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey>(IClientSessionHandle session, TDocument modifiedDocument, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, TKey>(session, modifiedDocument, cancellationToken);
        }

        /// <summary>
        /// Updates a document.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="session">The client session.</param>
        /// <param name="modifiedDocument">The document with the modifications you want to persist.</param>
        /// <param name="cancellationToken">The optional cancellation token.</param>
        /// <returns></returns>
        public virtual bool UpdateOne<TDocument, TKey>(IClientSessionHandle session, TDocument modifiedDocument, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbUpdater.UpdateOne<TDocument, TKey>(session, modifiedDocument, cancellationToken);
        }

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
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey>(IClientSessionHandle session, TDocument documentToModify, UpdateDefinition<TDocument> update, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, TKey>(session, documentToModify, update, cancellationToken);
        }

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
        public virtual bool UpdateOne<TDocument, TKey>(IClientSessionHandle session, TDocument documentToModify, UpdateDefinition<TDocument> update, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbUpdater.UpdateOne<TDocument, TKey>(session, documentToModify, update, cancellationToken);
        }

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
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(IClientSessionHandle session, TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, TKey, TField>(session, documentToModify, field, value, cancellationToken);
        }

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
        public virtual bool UpdateOne<TDocument, TKey, TField>(IClientSessionHandle session, TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbUpdater.UpdateOne<TDocument, TKey, TField>(session, documentToModify, field, value, cancellationToken);
        }

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
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(IClientSessionHandle session, FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, TKey, TField>(session, filter, field, value, partitionKey, cancellationToken);
        }

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
        public virtual async Task<bool> UpdateOneAsync<TDocument, TKey, TField>(IClientSessionHandle session, Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return await MongoDbUpdater.UpdateOneAsync<TDocument, TKey, TField>(session, filter, field, value, partitionKey, cancellationToken);
        }

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
        public virtual bool UpdateOne<TDocument, TKey, TField>(IClientSessionHandle session, FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return MongoDbUpdater.UpdateOne<TDocument, TKey, TField>(session, filter, field, value, partitionKey, cancellationToken);
        }

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
        public virtual bool UpdateOne<TDocument, TKey, TField>(IClientSessionHandle session, Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null, CancellationToken cancellationToken = default(CancellationToken))
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            return UpdateOne<TDocument, TKey, TField>(session, Builders<TDocument>.Filter.Where(filter), field, value, partitionKey, cancellationToken);
        }
    }
}
