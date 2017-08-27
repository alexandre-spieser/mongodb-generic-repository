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
        /// The private GetCollection method
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <returns></returns>
        public IMongoCollection<TDocument> GetCollection<TDocument>(TDocument document) where TDocument : IDocument
        {
            return _database.GetCollection<TDocument>(PluralizePartitioned(document));
        }

        /// <summary>
        /// Drops a collection, use very carefully.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        public void DropCollection<TDocument>()
        {
            _database.DropCollection(Pluralize<TDocument>());
        }

        private string Pluralize<TDocument>()
        {
            return typeof(TDocument).Name.ToLower() + "s";
        }

        private string PluralizePartitioned<TDocument>(TDocument document) where TDocument : IDocument
        {
            return document.PartitionKey + typeof(TDocument).Name.ToLower() + "s";
        }
    }
}