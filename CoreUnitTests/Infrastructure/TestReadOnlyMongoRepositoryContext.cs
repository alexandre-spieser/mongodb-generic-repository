using System;
using AutoFixture;
using AutoFixture.AutoMoq;
using MongoDB.Driver;
using MongoDbGenericRepository.DataAccess.Read;
using Moq;

namespace CoreUnitTests.Infrastructure;

public class TestReadOnlyMongoRepositoryContext
{
    private readonly Mock<IMongoDatabase> mongoDatabase;

    private TestReadOnlyMongoRepository sut;

    protected TestReadOnlyMongoRepositoryContext()
    {
        mongoDatabase = new Mock<IMongoDatabase>();
        Fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    protected IFixture Fixture { get; set; }

    protected TestReadOnlyMongoRepository Sut
    {
        get
        {
            if (sut != null)
            {
                return sut;
            }

            sut = Fixture.Create<TestReadOnlyMongoRepository>();

            if (Reader != null)
            {
                sut.SetReader(Reader.Object);
            }

            return sut;
        }
    }

    protected Mock<IMongoDbReader> Reader { get; set; }

    protected Mock<T> MockOf<T>()
        where T : class =>
        Fixture.Freeze<Mock<T>>();
}
