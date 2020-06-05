using MongoDB.Bson;
using MongoDbGenericRepository;
using System;
using MongoDbGenericRepository.Models;
using Mongodb.Driver.Extensions;

namespace CoreIntegrationTests.Infrastructure
{
    internal static class Consts
    {
        //TODO Run Before Need Modify With Your ConnectionString
        public const string DbConnectString = "mongodb://localhost:27017/MongoDbTests";

    }

    public interface ITestRepository<TKey> : IBaseMongoRepository<TKey> where TKey : IEquatable<TKey>
    {
        void DropTestCollection<TDocument>();
        void DropTestCollection<TDocument>(string partitionKey);
    }

    public class TestTKeyRepository<TKey> : BaseMongoRepository<TKey>, ITestRepository<TKey> where TKey : IEquatable<TKey>
    {
        const string connectionString = Consts.DbConnectString;
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

        const string connectionString = Consts.DbConnectString;
        private static readonly ITestRepository _instance = new TestRepository(connectionString);

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static TestRepository()
        {
        }

        /// <inheritdoc />
        private TestRepository(string connectionString) : base(connectionString)
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

    public class TestDefaultBaseMongodbRepository<TDocument> : DefaultBaseMongodbRepository<TDocument>
        where TDocument : IDocument, new()
    {
        protected override string MongodbConnectionString => Consts.DbConnectString;

        public static TestDefaultBaseMongodbRepository<TDocument> Instance =>
            new TestDefaultBaseMongodbRepository<TDocument>();
    }

    public class
        TestDefaultBaseTKeyMongodbRepository<TDocument, TKey> : DefaultBaseTKeyMongodbRepository<TDocument, TKey>
        where TDocument : IDocument<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        protected override string MongodbConnectionString => Consts.DbConnectString;

        public static TestDefaultBaseTKeyMongodbRepository<TDocument, TKey> Instance =>
            new TestDefaultBaseTKeyMongodbRepository<TDocument, TKey>();
    }



}
