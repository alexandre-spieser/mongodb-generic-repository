using MongoDB.Driver;
using System;

namespace MongoDbGenericRepository
{
    /// <summary>
    /// The base Repository, it is meant to be inherited from by your custom custom MongoRepository implementation.
    /// Its constructor must be given a connection string and a database name.
    /// </summary>
    public abstract class ReadOnlyMongoRepository : KeyTypedReadOnlyMongoRepository<Guid>, IReadOnlyMongoRepository
    {
        /// <summary>
        /// The constructor taking a connection string and a database name.
        /// </summary>
        /// <param name="connectionString">The connection string of the MongoDb server.</param>
        /// <param name="databaseName">The name of the database against which you want to perform operations.</param>
        protected ReadOnlyMongoRepository(string connectionString, string databaseName = null) : base(connectionString, databaseName)
        {
        }

        /// <summary>
        /// The contructor taking a <see cref="IMongoDbContext"/>.
        /// </summary>
        /// <param name="mongoDbContext">A mongodb context implementing <see cref="IMongoDbContext"/></param>
        protected ReadOnlyMongoRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {
        }

        /// <summary>
        /// The contructor taking a <see cref="IMongoDatabase"/>.
        /// </summary>
        /// <param name="mongoDatabase">A mongodb context implementing <see cref="IMongoDatabase"/></param>
        protected ReadOnlyMongoRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }
    }
}