using System;

namespace MongoDbGenericRepository
{
    /// <summary>
    /// The interface exposing all the CRUD and Index functionalities for Key typed repositories.
    /// </summary>
    /// <typeparam name="TKey">The type of the document Id.</typeparam>
    public interface IBaseMongoRepository<TKey> :
        IReadOnlyMongoRepository<TKey>,
        IBaseMongoRepository_Create<TKey>,
        IBaseMongoRepository_Delete<TKey>,
        IBaseMongoRepository_Index<TKey>,
        IBaseMongoRepository_Update<TKey>
        where TKey : IEquatable<TKey>
    {
    }
}