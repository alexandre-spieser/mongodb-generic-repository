using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Linq.Expressions;
using MongoDbGenericRepository;
using MongoDbGenericRepository.Models;


namespace Mongodb.Driver.Extensions.Abstractions
{
    public interface IDefaultBaseTKeyMongodbRepository<TDocument, TKey> : IBaseMongoRepository<TKey>, ICustomDataAccess<TDocument, TKey>
    where TDocument : IDocument<TKey>, new()
    where TKey : IEquatable<TKey>
    {
 
    }

}
