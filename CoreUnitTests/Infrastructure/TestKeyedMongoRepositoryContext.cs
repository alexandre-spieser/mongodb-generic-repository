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
    private readonly Mock<IMongoDatabase> _mongoDatabase;

    private TestKeyedMongoRepository<TKey> _sut;

    protected TestKeyedMongoRepositoryContext()
    {
        _mongoDatabase = new Mock<IMongoDatabase>();
        Fixture = new Fixture();
    }

    protected Fixture Fixture { get; set; }

    protected TestKeyedMongoRepository<TKey> Sut
    {
        get
        {
            if (_sut != null)
            {
                return _sut;
            }

            _sut = new TestKeyedMongoRepository<TKey>(_mongoDatabase.Object);
            if (IndexHandler != null)
            {
                _sut.SetIndexHandler(IndexHandler.Object);
            }

            if (Creator != null)
            {
                _sut.SetDbCreator(Creator.Object);
            }

            if (Reader != null)
            {
                _sut.SetReader(Reader.Object);
            }

            if (Eraser != null)
            {
                _sut.SetEraser(Eraser.Object);
            }

            return _sut;
        }
    }

    protected Mock<IMongoDbIndexHandler> IndexHandler { get; set; }

    protected Mock<IMongoDbCreator> Creator { get; set; }

    protected Mock<IMongoDbReader> Reader { get; set; }

    protected Mock<IMongoDbEraser> Eraser { get; set; }
}