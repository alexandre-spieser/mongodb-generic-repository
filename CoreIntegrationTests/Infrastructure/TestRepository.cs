using MongoDbGenericRepository;

namespace CoreIntegrationTests.Infrastructure
{
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
