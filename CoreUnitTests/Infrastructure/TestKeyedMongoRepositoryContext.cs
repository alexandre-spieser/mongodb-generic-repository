using System;
using AutoFixture;
using MongoDB.Driver;
using MongoDbGenericRepository.DataAccess.Create;
using MongoDbGenericRepository.DataAccess.Delete;
using MongoDbGenericRepository.DataAccess.Index;
using MongoDbGenericRepository.DataAccess.Read;
using Moq;

namespace CoreUnitTests.Infrastructure;

public class TestKeyedMongoRepositoryContext<TKey>
    where TKey : IEquatable<TKey>
{
    private readonly Mock<IMongoDatabase> mongoDatabase;

    private TestKeyedMongoRepository<TKey> sut;

    protected TestKeyedMongoRepositoryContext()
    {
        mongoDatabase = new Mock<IMongoDatabase>();
        Fixture = new Fixture();
    }

    protected Fixture Fixture { get; set; }

    protected TestKeyedMongoRepository<TKey> Sut
    {
        get
        {
            if (sut != null)
            {
                return sut;
            }

            sut = new TestKeyedMongoRepository<TKey>(mongoDatabase.Object);
            if (IndexHandler != null)
            {
                sut.SetIndexHandler(IndexHandler.Object);
            }

            if (Creator != null)
            {
                sut.SetDbCreator(Creator.Object);
            }

            if (Reader != null)
            {
                sut.SetReader(Reader.Object);
            }

            if (Eraser != null)
            {
                sut.SetEraser(Eraser.Object);
            }

            return sut;
        }
    }

    protected Mock<IMongoDbIndexHandler> IndexHandler { get; set; }

    protected Mock<IMongoDbCreator> Creator { get; set; }

    protected Mock<IMongoDbReader> Reader { get; set; }

    protected Mock<IMongoDbEraser> Eraser { get; set; }
}
