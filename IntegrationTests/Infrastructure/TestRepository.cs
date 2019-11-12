using MongoDB.Driver;
using MongoDbGenericRepository;
using MongoDbGenericRepository.Models;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IntegrationTests.Infrastructure
{
    public class TestRepository : BaseMongoRepository, ITestRepository
    {
      
        /// <inheritdoc />
        public TestRepository(string connectionString, string databaseName = null) : base(connectionString, databaseName)
        {
        }

        public void DropTestCollection<TDocument>()
        {
            MongoDbContext.DropCollection<TDocument>();
        }

        public void DropTestCollection<TDocument>(string partitionKey)
        {
            MongoDbContext.DropCollection<TDocument>(partitionKey);
        }

        /// <summary>
        /// Gets the max of a property in a mongodb collections that is satisfying the filter.
        /// </summary>
        /// <typeparam name="TDocument">The document type.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <returns></returns>
        public async Task<TDocument> GetByMaxAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, object>> orderByDescending)
            where TDocument : IDocument<TKey>
            where TKey : System.IEquatable<TKey>
        {
            return await GetCollection<TDocument, TKey>().Find(Builders<TDocument>.Filter.Where(filter))
                                                         .SortByDescending(orderByDescending)
                                                         .FirstOrDefaultAsync();
        }
    }
}
