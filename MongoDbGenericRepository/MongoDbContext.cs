using MongoDB.Driver;
using System.Configuration;
namespace MongoDbGenericRepository
{
    public class MongoDbContext
    {
        public const string CONNECTION_STRING_NAME = "MongoDbTest";
        public const string DATABASE_NAME = "MongoDbTest";

        private static readonly IMongoClient _client;
        private static readonly IMongoDatabase _database;

        static MongoDbContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME].ConnectionString;
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(DATABASE_NAME);
        }

        /// <summary>
        /// The private GetCollection method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return _database.GetCollection<TEntity>(typeof(TEntity).Name.ToLower() + "s");
        }
    }
}