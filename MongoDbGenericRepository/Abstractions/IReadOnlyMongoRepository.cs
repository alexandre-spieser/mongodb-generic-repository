using System;

namespace MongoDbGenericRepository
{
    /// <summary>
    /// The IReadOnlyMongoRepository exposes the readonly functionality of the BaseMongoRepository.
    /// </summary>
    public interface IReadOnlyMongoRepository : IBaseReadOnlyRepository, IReadOnlyMongoRepository<Guid>
    {

    }
}
