using System;
using AutoFixture;
using MongoDB.Driver;
using MongoDbGenericRepository.DataAccess.Read;
using Moq;

namespace CoreUnitTests.Infrastructure;

public class TestKeyedReadOnlyMongoRepositoryContext<TKey>
    where TKey : IEquatable<TKey>
{
    private readonly Mock<IMongoDatabase> mongoDatabase;

    private TestKeyedReadOnlyMongoRepository<TKey> sut;

    protected TestKeyedReadOnlyMongoRepositoryContext()
    {
        mongoDatabase = new Mock<IMongoDatabase>();
        Fixture = new Fixture();
    }

    protected Fixture Fixture { get; set; }

    protected TestKeyedReadOnlyMongoRepository<TKey> Sut
    {
        get
        {
            if (sut != null)
            {
                return sut;
            }

            sut = new TestKeyedReadOnlyMongoRepository<TKey>(mongoDatabase.Object);

            if (Reader != null)
            {
                sut.SetReader(Reader.Object);
            }

            return sut;
        }
    }

    protected Mock<IMongoDbReader> Reader { get; set; }
}
