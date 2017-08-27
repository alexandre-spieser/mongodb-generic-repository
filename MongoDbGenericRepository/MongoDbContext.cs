using MongoDB.Driver;
using MongoDbGenericRepository.Models;

namespace MongoDbGenericRepository
{
    /// <summary>
    /// The MongoDb context
    /// </summary>
    public class MongoDbContext : IMongoDbContext
    {
        static MongoDbContext()
        {
            // Avoid legacy UUID representation: use Binary 0x04 subtype.
            MongoDefaults.GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard;
        }

        public MongoDbContext(string connectionString, string databaseName)
        {
            
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseName);
        }

        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        /// <summary>
        /// The private GetCollection method
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <returns></returns>
        public IMongoCollection<TDocument> GetCollection<TDocument>()
        {
            return _database.GetCollection<TDocument>(Pluralize<TDocument>());
        }

        /// <summary>
        /// Returns a collection for a document type that has a partition key.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="partitionKey">The value of the partition key.</param>
        public IMongoCollection<TDocument> GetCollection<TDocument>(string partitionKey) where TDocument : IDocument
        {
            return _database.GetCollection<TDocument>(partitionKey +"-"+ Pluralize<TDocument>());
        }

        /// <summary>
        /// Drops a collection, use very carefully.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        public void DropCollection<TDocument>()
        {
            _database.DropCollection(Pluralize<TDocument>());
        }

        /// <summary>
        /// Drops a collection having a partitionkey, use very carefully.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        public void DropCollection<TDocument>(string partitionKey)
        {
            _database.DropCollection(partitionKey + "-" + Pluralize<TDocument>());
        }

        private string Pluralize<TDocument>()
        {
            return typeof(TDocument).Name.ToLower() + "s";
        }
    }
}