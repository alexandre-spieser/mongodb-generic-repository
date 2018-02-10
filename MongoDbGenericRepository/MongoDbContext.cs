using MongoDB.Driver;
using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using MongoDbGenericRepository.Utils;
using System;
using System.Linq;
using System.Reflection;

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
        /// Sets the Guid representation of the MongoDb Driver.
        /// </summary>
        /// <param name="guidRepresentation">The new value of the GuidRepresentation</param>
        public void SetGuidRepresentation(MongoDB.Bson.GuidRepresentation guidRepresentation)
        {
            MongoDefaults.GuidRepresentation = guidRepresentation;
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
        /// Extracts the CollectionName attribute from the entity type, if any.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <returns>The name of the collection in which the TDocument is stored.</returns>
        private string GetAttributeCollectionName<TDocument>()
        {
            return (typeof(TDocument).GetTypeInfo()
                                     .GetCustomAttributes(typeof(CollectionNameAttribute))
                                     .FirstOrDefault() as CollectionNameAttribute)?.Name;
        }

		/// <summary>
		/// Returns a collection for a document type that has a partition key.
		/// </summary>
		/// <typeparam name="TDocument">The type representing a Document.</typeparam>
		/// <param name="partitionKey">The value of the partition key.</param>
		public IMongoCollection<TDocument> GetCollection<TDocument>(string partitionKey = null) where TDocument : IDocument
        {
            if (string.IsNullOrEmpty(partitionKey))
            {
                return Database.GetCollection<TDocument>(GetAttributeCollectionName<TDocument>() ?? Pluralize<TDocument>());
            }
			return Database.GetCollection<TDocument>(partitionKey + "-" + GetAttributeCollectionName<TDocument>() ?? Pluralize<TDocument>());
        }

        /// <summary>
        /// Returns a collection for a document type that has a partition key.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <typeparam name="TKey">The type of the primary key for a Document.</typeparam>
        /// <param name="partitionKey">The value of the partition key.</param>
        public IMongoCollection<TDocument> GetCollection<TDocument, TKey>(string partitionKey) 
            where TDocument : IDocument<TKey>
            where TKey : IEquatable<TKey>
        {
            if (string.IsNullOrEmpty(partitionKey))
            {
                return Database.GetCollection<TDocument>(GetAttributeCollectionName<TDocument>() ?? Pluralize<TDocument>());
            }
            return Database.GetCollection<TDocument>(partitionKey + "-" + GetAttributeCollectionName<TDocument>() ?? Pluralize<TDocument>());
        }

		/// <summary>
		/// Drops a collection, use very carefully.
		/// </summary>
		/// <typeparam name="TDocument">The type representing a Document.</typeparam>
		public void DropCollection<TDocument>()
        {
			Database.DropCollection(GetAttributeCollectionName<TDocument>() ?? Pluralize<TDocument>());
        }

        /// <summary>
        /// Drops a collection having a partitionkey, use very carefully.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        public void DropCollection<TDocument>(string partitionKey)
        {
			Database.DropCollection(partitionKey + "-" + GetAttributeCollectionName<TDocument>() ?? Pluralize<TDocument>());
        }

        /// <summary>
        /// Very naively pluralizes a TDocument type name.
        /// </summary>
        /// <typeparam name="TDocument">The type representing a Document.</typeparam>
        /// <returns></returns>
        private string Pluralize<TDocument>()
        {
            return (typeof(TDocument).Name.Pluralize()).Camelize();
        }
    }
}