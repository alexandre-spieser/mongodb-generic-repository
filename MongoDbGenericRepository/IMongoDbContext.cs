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
        /// <returns></returns>
        IMongoCollection<TDocument> GetCollection<TDocument>();

        /// <summary>
        /// The private GetCollection method
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <returns></returns>
        IMongoCollection<TDocument> GetCollection<TDocument>(TDocument document) where TDocument : IDocument;

        /// <summary>
        /// Drops a collection, use very carefully.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        void DropCollection<TDocument>();
    }
}