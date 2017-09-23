using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using MongoDbGenericRepository.Models;
using System.Linq;

namespace MongoDbGenericRepository
{
    /// <summary>
    /// The IBaseMongoRepository exposes the functionality of the BaseMongoRepository.
    /// </summary>
    public interface IBaseMongoRepository
    {
        /// <summary>
        /// The connection string.
        /// </summary>
        string ConnectionString { get; set; }
        /// <summary>
        /// The database name.
        /// </summary>
        string DatabaseName { get; set; }

        #region Create

        /// <summary>
        /// Asynchronously adds a document to the collection.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="document">The document you want to add.</param>
        Task AddOneAsync<TDocument>(TDocument document) where TDocument : IDocument;

        /// <summary>
        /// Adds a document to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="document">The document you want to add.</param>
        void AddOne<TDocument>(TDocument document) where TDocument : IDocument;

        /// <summary>
        /// Asynchronously adds a list of documents to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="documents">The document you want to add.</param>
        Task AddManyAsync<TDocument>(IEnumerable<TDocument> documents) where TDocument : IDocument;

        /// <summary>
        /// Adds a list of documents to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="documents">The document you want to add.</param>
        void AddMany<TDocument>(IEnumerable<TDocument> documents) where TDocument : IDocument;

        #endregion

        #region Read

        /// <summary>
        /// Asynchronously returns one document given its id.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="id">The Id of the document you want to get.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        Task<TDocument> GetByIdAsync<TDocument>(Guid id, string partitionKey = null) where TDocument : IDocument;

        /// <summary>
        /// Returns one document given its id.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="id">The Id of the document you want to get.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        TDocument GetById<TDocument>(Guid id, string partitionKey = null) where TDocument : IDocument;

        /// <summary>
        /// Asynchronously returns one document given an expression filter.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        Task<TDocument> GetOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument;

        /// <summary>
        /// Returns one document given an expression filter.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        TDocument GetOne<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument;

        /// <summary>
        /// Returns a collection cursor.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        IFindFluent<TDocument, TDocument> GetCursor<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument;

        /// <summary>
        /// Asynchronously returns true if any of the document of the collection matches the filter condition.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        Task<bool> AnyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument;

        /// <summary>
        /// Returns true if any of the document of the collection matches the filter condition.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        bool Any<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument;

        /// <summary>
        /// Asynchronously returns a list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        Task<List<TDocument>> GetAllAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument;

        /// <summary>
        /// Returns a list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        List<TDocument> GetAll<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument;

        /// <summary>
        /// Asynchronously counts how many documents match the filter condition.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        Task<long> CountAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument;

        /// <summary>
        /// Counts how many documents match the filter condition.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        long Count<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument;

        #endregion

        #region Update

        /// <summary>
        /// Asynchronously Updates a document.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="modifiedDocument">The document with the modifications you want to persist.</param>
        Task<bool> UpdateOneAsync<TDocument>(TDocument modifiedDocument) where TDocument : IDocument;

        /// <summary>
        /// Updates a document.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="modifiedDocument">The document with the modifications you want to persist.</param>
        bool UpdateOne<TDocument>(TDocument modifiedDocument) where TDocument : IDocument;

        #endregion

        #region Delete

        /// <summary>
        /// Asynchronously deletes a document.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="document">The document you want to delete.</param>
        /// <returns>The number of documents deleted.</returns>
        Task<long> DeleteOneAsync<TDocument>(TDocument document) where TDocument : IDocument;

        /// <summary>
        /// Asynchronously deletes a document matching the condition of the LINQ expression filter.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        Task<long> DeleteOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument;

        /// <summary>
        /// Deletes a document.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="document">The document you want to delete.</param>
        /// <returns>The number of documents deleted.</returns>
        long DeleteOne<TDocument>(TDocument document) where TDocument : IDocument;

        /// <summary>
        /// Deletes a document matching the condition of the LINQ expression filter.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        long DeleteOne<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument;

        /// <summary>
        /// Asynchronously deletes the documents matching the condition of the LINQ expression filter.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        Task<long> DeleteManyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument;

        /// <summary>
        /// Asynchronously deletes a list of documents.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="documents">The list of documents to delete.</param>
        /// <returns>The number of documents deleted.</returns>
        Task<long> DeleteManyAsync<TDocument>(IEnumerable<TDocument> documents) where TDocument : IDocument;

        /// <summary>
        /// Deletes a list of documents.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="documents">The list of documents to delete.</param>
        /// <returns>The number of documents deleted.</returns>
        long DeleteMany<TDocument>(IEnumerable<TDocument> documents) where TDocument : IDocument;

        /// <summary>
        /// Deletes the documents matching the condition of the LINQ expression filter.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        long DeleteMany<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument;

        #endregion

        #region Project

        /// <summary>
        /// Asynchronously returns a projected document matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <typeparam name="TProjection"></typeparam>
        /// <param name="filter"></param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        Task<TProjection> ProjectOneAsync<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null)
                                        where TDocument : IDocument
                                        where TProjection : class;

        /// <summary>
        /// Returns a projected document matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <typeparam name="TProjection"></typeparam>
        /// <param name="filter"></param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        TProjection ProjectOne<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null)
                                        where TDocument : IDocument
                                        where TProjection : class;

        /// <summary>
        /// Asynchronously returns a list of projected documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <typeparam name="TProjection"></typeparam>
        /// <param name="filter"></param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null)
                                        where TDocument : IDocument
                                        where TProjection : class;

        /// <summary>
        /// Asynchronously returns a list of projected documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <typeparam name="TProjection"></typeparam>
        /// <param name="filter"></param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        List<TProjection> ProjectMany<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null)
                                        where TDocument : IDocument
                                        where TProjection : class;

        #endregion

    }

    /// <summary>
    /// The base Repository, it is meant to be inherited from by your custom custom MongoRepository implementation.
    /// Its constructor must be given a connection string and a database name.
    /// </summary>
    public abstract class BaseMongoRepository : IBaseMongoRepository
    {
        /// <summary>
        /// The connection string.
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// The database name.
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// The constructor taking a connection string and a database name.
        /// </summary>
        /// <param name="connectionString">The connection string of the MongoDb server.</param>
        /// <param name="databaseName">The name of the database against which you want to perform operations.</param>
        protected BaseMongoRepository(string connectionString, string databaseName)
        {
            MongoDbContext = new MongoDbContext(connectionString, databaseName);
        }

        /// <summary>
        /// The contructor taking a <see cref="IMongoDbContext"/>.
        /// </summary>
        /// <param name="mongoDbContext">A mongodb context implementing <see cref="IMongoDbContext"/></param>
        protected BaseMongoRepository(IMongoDbContext mongoDbContext)
        {
            MongoDbContext = mongoDbContext;
        }

        /// <summary>
        /// The MongoDbContext
        /// </summary>
        protected IMongoDbContext MongoDbContext = null;

        #region Create

        /// <summary>
        /// Asynchronously adds a document to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="document">The document you want to add.</param>
        public async Task AddOneAsync<TDocument>(TDocument document) where TDocument : IDocument
        {
            FormatDocument(document);
            await HandlePartitioned(document).InsertOneAsync(document);
        }

        /// <summary>
        /// Adds a document to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="document">The document you want to add.</param>
        public void AddOne<TDocument>(TDocument document) where TDocument : IDocument
        {
            FormatDocument(document);
            HandlePartitioned(document).InsertOne(document);
        }

        /// <summary>
        /// Asynchronously adds a list of documents to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="documents">The documents you want to add.</param>
        public async Task AddManyAsync<TDocument>(IEnumerable<TDocument> documents) where TDocument : IDocument
        {
            if (!documents.Any())
            {
                return;
            }
            foreach (var doc in documents)
            {
                FormatDocument(doc);
            }
            await HandlePartitioned(documents.FirstOrDefault()).InsertManyAsync(documents);
        }

        /// <summary>
        /// Adds a list of documents to the collection.
        /// Populates the Id and AddedAtUtc fields if necessary.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="documents">The documents you want to add.</param>
        public void AddMany<TDocument>(IEnumerable<TDocument> documents) where TDocument : IDocument
        {
            if (!documents.Any())
            {
                return;
            }
            foreach (var document in documents)
            {
                FormatDocument(document);
            }
            HandlePartitioned(documents.FirstOrDefault()).InsertMany(documents.ToList());
        }

        #endregion Create

        #region Read

        /// <summary>
        /// Asynchronously returns one document given its id.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="id">The Id of the document you want to get.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public async Task<TDocument> GetByIdAsync<TDocument>(Guid id, string partitionKey = null) where TDocument : IDocument
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", id);
            return await HandlePartitioned<TDocument>(partitionKey).Find(filter).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Returns one document given its id.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="id">The Id of the document you want to get.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public TDocument GetById<TDocument>(Guid id, string partitionKey = null) where TDocument : IDocument
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", id);
            return HandlePartitioned<TDocument>(partitionKey).Find(filter).FirstOrDefault();
        }

        /// <summary>
        /// Asynchronously returns one document given an expression filter.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public async Task<TDocument> GetOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument
        {
            return await HandlePartitioned<TDocument>(partitionKey).Find(filter).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Returns one document given an expression filter.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public TDocument GetOne<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument
        {
            return HandlePartitioned<TDocument>(partitionKey).Find(filter).FirstOrDefault();
        }

        /// <summary>
        /// Returns a collection cursor.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public IFindFluent<TDocument, TDocument> GetCursor<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument
        {
            return HandlePartitioned<TDocument>(partitionKey).Find(filter);
        }

        /// <summary>
        /// Returns true if any of the document of the collection matches the filter condition.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public async Task<bool> AnyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument
        {
            var count = await HandlePartitioned<TDocument>(partitionKey).CountAsync(filter);
            return (count > 0);
        }

        /// <summary>
        /// Returns true if any of the document of the collection matches the filter condition.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public bool Any<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument
        {
            var count = HandlePartitioned<TDocument>(partitionKey).Count(filter);
            return (count > 0);
        }

        /// <summary>
        /// Asynchronously returns a list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public async Task<List<TDocument>> GetAllAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument
        {
            return await HandlePartitioned<TDocument>(partitionKey).Find(filter).ToListAsync();
        }

        /// <summary>
        /// Returns a list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public List<TDocument> GetAll<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument
        {
            return HandlePartitioned<TDocument>(partitionKey).Find(filter).ToList();
        }

        /// <summary>
        /// Asynchronously counts how many documents match the filter condition.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partitionKey</param>
        public async Task<long> CountAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument
        {
            return await HandlePartitioned<TDocument>(partitionKey).CountAsync(filter);
        }

        /// <summary>
        /// Counts how many documents match the filter condition.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partitionKey</param>
        public long Count<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument
        {
            return HandlePartitioned<TDocument>(partitionKey).Find(filter).Count();
        }

        #endregion

        #region Update

        /// <summary>
        /// Asynchronously Updates a document.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="modifiedDocument">The document with the modifications you want to persist.</param>
        public async Task<bool> UpdateOneAsync<TDocument>(TDocument modifiedDocument) where TDocument : IDocument
        {
            var updateRes = await HandlePartitioned(modifiedDocument).ReplaceOneAsync(x => x.Id == modifiedDocument.Id, modifiedDocument);
            return updateRes.ModifiedCount == 1;
        }

        /// <summary>
        /// Updates a document.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="modifiedDocument">The document with the modifications you want to persist.</param>
        public bool UpdateOne<TDocument>(TDocument modifiedDocument) where TDocument : IDocument
        {
            var updateRes = HandlePartitioned(modifiedDocument).ReplaceOne(x => x.Id == modifiedDocument.Id, modifiedDocument);
            return updateRes.ModifiedCount == 1;
        }

        #endregion Update

        #region Delete

        /// <summary>
        /// Asynchronously deletes a document.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="document">The document you want to delete.</param>
        /// <returns>The number of documents deleted.</returns>
        public async Task<long> DeleteOneAsync<TDocument>(TDocument document) where TDocument : IDocument
        {
            return (await HandlePartitioned(document).DeleteOneAsync(x => x.Id == document.Id)).DeletedCount;
        }

        /// <summary>
        /// Deletes a document.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="document">The document you want to delete.</param>
        /// <returns>The number of documents deleted.</returns>
        public long DeleteOne<TDocument>(TDocument document) where TDocument : IDocument
        {
            return HandlePartitioned(document).DeleteOne(x => x.Id == document.Id).DeletedCount;
        }

        /// <summary>
        /// Deletes a document matching the condition of the LINQ expression filter.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        public long DeleteOne<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument
        {
            return HandlePartitioned<TDocument>(partitionKey).DeleteOne(filter).DeletedCount;
        }

        /// <summary>
        /// Asynchronously deletes a document matching the condition of the LINQ expression filter.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        public async Task<long> DeleteOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument
        {
            return (await HandlePartitioned<TDocument>(partitionKey).DeleteOneAsync(filter)).DeletedCount;
        }

        /// <summary>
        /// Asynchronously deletes the documents matching the condition of the LINQ expression filter.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        public async Task<long> DeleteManyAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument
        {
            return (await HandlePartitioned<TDocument>(partitionKey).DeleteManyAsync(filter)).DeletedCount;
        }

        /// <summary>
        /// Asynchronously deletes a list of documents.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="documents">The list of documents to delete.</param>
        /// <returns>The number of documents deleted.</returns>
        public async Task<long> DeleteManyAsync<TDocument>(IEnumerable<TDocument> documents) where TDocument : IDocument
        {
            if (!documents.Any())
            {
                return 0;
            }
            var idsTodelete = documents.Select(e => e.Id).ToArray();
            return (await HandlePartitioned(documents.FirstOrDefault()).DeleteManyAsync(x => idsTodelete.Contains(x.Id))).DeletedCount;
        }

        /// <summary>
        /// Deletes a list of documents.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="documents">The list of documents to delete.</param>
        /// <returns>The number of documents deleted.</returns>
        public long DeleteMany<TDocument>(IEnumerable<TDocument> documents) where TDocument : IDocument
        {
            if (!documents.Any())
            {
                return 0;
            }
            var idsTodelete = documents.Select(e => e.Id).ToArray();
            return HandlePartitioned(documents.FirstOrDefault()).DeleteMany(x => idsTodelete.Contains(x.Id)).DeletedCount;
        }

        /// <summary>
        /// Deletes the documents matching the condition of the LINQ expression filter.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter">A LINQ expression filter.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        /// <returns>The number of documents deleted.</returns>
        public long DeleteMany<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null) where TDocument : IDocument
        {
            return HandlePartitioned<TDocument>(partitionKey).DeleteMany(filter).DeletedCount;
        }

        #endregion Delete

        #region Project

        /// <summary>
        /// Asynchronously returns a projected document matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <typeparam name="TProjection"></typeparam>
        /// <param name="filter"></param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public async Task<TProjection> ProjectOneAsync<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null)
            where TDocument : IDocument
            where TProjection : class
        {
            return await HandlePartitioned<TDocument>(partitionKey).Find(filter)
                                                                   .Project(projection)
                                                                   .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Returns a projected document matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <typeparam name="TProjection"></typeparam>
        /// <param name="filter"></param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public TProjection ProjectOne<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null)
            where TDocument : IDocument
            where TProjection : class
        {
            return HandlePartitioned<TDocument>(partitionKey).Find(filter)
                                                             .Project(projection)
                                                             .FirstOrDefault();
        }

        /// <summary>
        /// Asynchronously returns a list of projected documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <typeparam name="TProjection"></typeparam>
        /// <param name="filter"></param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public async Task<List<TProjection>> ProjectManyAsync<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null)
            where TDocument : IDocument
            where TProjection : class
        {
            return await HandlePartitioned<TDocument>(partitionKey).Find(filter)
                                                                   .Project(projection)
                                                                   .ToListAsync();
        }

        /// <summary>
        /// Asynchronously returns a list of projected documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <typeparam name="TProjection"></typeparam>
        /// <param name="filter"></param>
        /// <param name="projection">The projection expression.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public List<TProjection> ProjectMany<TDocument, TProjection>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TProjection>> projection, string partitionKey = null)
            where TDocument : IDocument
            where TProjection : class
        {
            return HandlePartitioned<TDocument>(partitionKey).Find(filter)
                                                             .Project(projection)
                                                             .ToList();
        }

        #endregion

        /// <summary>
        /// Asynchronously returns a paginated list of the documents matching the filter condition.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter"></param>
        /// <param name="skipNumber">The number of documents you want to skip. Default value is 0.</param>
        /// <param name="takeNumber">The number of documents you want to take. Default value is 50.</param>
        /// <param name="partitionKey">An optional partition key.</param>
        public async Task<List<TDocument>> GetPaginatedAsync<TDocument>(Expression<Func<TDocument, bool>> filter, int skipNumber = 0, int takeNumber = 50, string partitionKey = null) where TDocument : IDocument
        {
            return await HandlePartitioned<TDocument>(partitionKey).Find(filter).Skip(skipNumber).Limit(takeNumber).ToListAsync();
        }

        #region Find And Update

        /// <summary>
        /// GetAndUpdateOne with filter
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<TDocument> GetAndUpdateOne<TDocument>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update, FindOneAndUpdateOptions<TDocument, TDocument> options) where TDocument : IDocument
        {
            return await GetCollection<TDocument>().FindOneAndUpdateAsync(filter, update, options);
        }

        #endregion Find And Update

        #region Private Methods

        private IMongoCollection<TDocument> GetCollection<TDocument>(string partitionKey) where TDocument : IDocument
        {
            return MongoDbContext.GetCollection<TDocument>(partitionKey);
        }

        private IMongoCollection<TDocument> GetCollection<TDocument>() where TDocument : IDocument
        {
            return MongoDbContext.GetCollection<TDocument>();
        }

        private IMongoCollection<TDocument> HandlePartitioned<TDocument>(TDocument document) where TDocument : IDocument
        {
            if (document is IPartitionedDocument)
            {
                return GetCollection<TDocument>(((IPartitionedDocument)document).PartitionKey);
            }
            return GetCollection<TDocument>();
        }

        private IMongoCollection<TDocument> HandlePartitioned<TDocument>(string partitionKey) where TDocument : IDocument
        {
            if (!string.IsNullOrEmpty(partitionKey))
            {
                return GetCollection<TDocument>(partitionKey);
            }
            return GetCollection<TDocument>();
        }

        private void FormatDocument<TDocument>(TDocument document) where TDocument : IDocument
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }
            if (document.Id == default(Guid))
            {
                document.Id = Guid.NewGuid();
            }
        }

        #endregion
    }
}