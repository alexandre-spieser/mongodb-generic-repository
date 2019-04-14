using MongoDB.Bson;
using MongoDbGenericRepository;
using System;

namespace CoreIntegrationTests.Infrastructure
{
    public interface ITestRepository<TKey> : IBaseMongoRepository<TKey> where TKey : IEquatable<TKey>
    {
        void DropTestCollection<TDocument>();
        void DropTestCollection<TDocument>(string partitionKey);
    }

    public class TestTKeyRepository<TKey> : BaseMongoRepository<TKey>, ITestRepository<TKey> where TKey : IEquatable<TKey>
    {
        const string connectionString = "mongodb://localhost:27017/MongoDbTests";
        private static readonly ITestRepository<TKey> _instance = new TestTKeyRepository<TKey>(connectionString);
        /// <inheritdoc />
        private TestTKeyRepository(string connectionString) : base(connectionString)
        {
        }

        public static ITestRepository<TKey> Instance
        {
            get
            {
                return _instance;
            }
        }

        public void DropTestCollection<TDocument>()
        {
            MongoDbContext.DropCollection<TDocument>();
        }

        public void DropTestCollection<TDocument>(string partitionKey)
        {
            MongoDbContext.DropCollection<TDocument>(partitionKey);
        }
    }

    /// <summary>
    /// A singleton implementation of the TestRepository
    /// </summary>
    public sealed class TestRepository : BaseMongoRepository, ITestRepository
    {

        const string connectionString = "mongodb://localhost:27017";
        private static readonly ITestRepository _instance = new TestRepository(connectionString, "MongoDbTests");

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static TestRepository()
        {
        }

        /// <inheritdoc />
        private TestRepository(string connectionString, string databaseName) : base(connectionString, databaseName)
        {
        }

        public static ITestRepository Instance
        {
            get
            {
                return _instance;
            }
        }

        public void DropTestCollection<TDocument>()
        {
            MongoDbContext.DropCollection<TDocument>();
        }

        public void DropTestCollection<TDocument>(string partitionKey)
        {
            MongoDbContext.DropCollection<TDocument>(partitionKey);
        }
    }
}
