using MongoDbGenericRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoDbGenericRepository
{
    /// <summary>
    /// The IReadOnlyMongoRepository exposes the readonly functionality of the BaseMongoRepository.
    /// </summary>
    public interface IReadOnlyMongoRepository : IBaseReadOnlyRepository, IReadOnlyMongoRepository<Guid>
    {

    }
}
