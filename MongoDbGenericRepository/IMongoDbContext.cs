using MongoDB.Driver;
using MongoDbGenericRepository.Models;

namespace MongoDbGenericRepository
{
    public interface IMongoDbContext
    {
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