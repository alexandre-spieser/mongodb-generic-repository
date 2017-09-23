using MongoDB.Driver;
using MongoDbGenericRepository.Models;

namespace MongoDbGenericRepository
{
    /// <summary>
    /// This is the interface of the IMongoDbContext which is managed by the <see cref="BaseMongoRepository"/>.
    /// </summary>
    public interface IMongoDbContext
    {
        /// <summary>
        /// The IMongoClient from the official MongoDb driver
        /// </summary>
        IMongoClient Client { get; }

        /// <summary>
        /// The IMongoDatabase from the official Mongodb driver
        /// </summary>
        IMongoDatabase Database { get; }

        /// <summary>
        /// The private GetCollection method
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        IMongoCollection<TDocument> GetCollection<TDocument>();

        /// <summary>
        /// Returns a collection for a document type that has a partition key.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="partitionKey">The value of the partition key.</param>
        IMongoCollection<TDocument> GetCollection<TDocument>(string partitionKey) where TDocument : IDocument;

        /// <summary>
        /// Drops a collection, use very carefully.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        void DropCollection<TDocument>();

        /// <summary>
        /// Drops a collection having a partitionkey, use very carefully.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        void DropCollection<TDocument>(string partitionKey);
    }
}