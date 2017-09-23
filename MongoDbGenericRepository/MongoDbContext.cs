using MongoDB.Driver;
using MongoDbGenericRepository.Models;

namespace MongoDbGenericRepository
{
    /// <summary>
    /// The MongoDb context
    /// </summary>
    public class MongoDbContext : IMongoDbContext
    {
        /// <summary>
        /// The IMongoClient from the official MongoDb driver
        /// </summary>
        public IMongoClient Client { get; }

        /// <summary>
        /// The IMongoDatabase from the official Mongodb driver
        /// </summary>
        public IMongoDatabase Database { get; }

        static MongoDbContext()
        {
            // Avoid legacy UUID representation: use Binary 0x04 subtype.
            MongoDefaults.GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard;
        }

        /// <summary>
        /// The constructor of the MongoDbContext, it needs a an object implementing <see cref="IMongoDatabase"/>.
        /// </summary>
        /// <param name="mongoDatabase">An object implementing IMongoDatabase</param>
        public MongoDbContext(IMongoDatabase mongoDatabase)
        {
            Database = mongoDatabase;
            Client = Database.Client;
        }

        /// <summary>
        /// The constructor of the MongoDbContext, it needs a connection string and a database name. 
        /// </summary>
        /// <param name="connectionString">The connections string.</param>
        /// <param name="databaseName">The name of your database.</param>
        public MongoDbContext(string connectionString, string databaseName)
        {
            Client = new MongoClient(connectionString);
            Database = Client.GetDatabase(databaseName);
        }

        /// <summary>
        /// The private GetCollection method
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <returns></returns>
        public IMongoCollection<TDocument> GetCollection<TDocument>()
        {
            return Database.GetCollection<TDocument>(Pluralize<TDocument>());
        }

        /// <summary>
        /// Returns a collection for a document type that has a partition key.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="partitionKey">The value of the partition key.</param>
        public IMongoCollection<TDocument> GetCollection<TDocument>(string partitionKey) where TDocument : IDocument
        {
            return Database.GetCollection<TDocument>(partitionKey +"-"+ Pluralize<TDocument>());
        }

        /// <summary>
        /// Drops a collection, use very carefully.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        public void DropCollection<TDocument>()
        {
            Database.DropCollection(Pluralize<TDocument>());
        }

        /// <summary>
        /// Drops a collection having a partitionkey, use very carefully.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        public void DropCollection<TDocument>(string partitionKey)
        {
            Database.DropCollection(partitionKey + "-" + Pluralize<TDocument>());
        }

        /// <summary>
        /// Very naively pluralizes a TDocument type name.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <returns></returns>
        private string Pluralize<TDocument>()
        {
            return (typeof(TDocument).Name.Pluralize()).Camelize();
        }
    }
}