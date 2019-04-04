using System;

namespace MongoDbGenericRepository
{
    /// <summary>
    /// The ReadOnlyMongoRepository implements the readonly functionality of the IReadOnlyMongoRepository.
    /// </summary>
    public abstract partial class ReadOnlyMongoRepository : KeyTypedReadOnlyMongoRepository<Guid>, IReadOnlyMongoRepository
    {
    }
}
