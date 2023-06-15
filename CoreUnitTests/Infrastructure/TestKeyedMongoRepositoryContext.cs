using MongoDB.Driver;
using MongoDbGenericRepository.DataAccess.Create;
using MongoDbGenericRepository.DataAccess.Index;
using MongoDbGenericRepository.DataAccess.Read;
using Moq;

namespace CoreUnitTests.Infrastructure;

public class TestKeyedMongoRepositoryContext
{
    private readonly Mock<IMongoDatabase> _mongoDatabase;

    private TestKeyedMongoRepository _sut;

    protected TestKeyedMongoRepositoryContext()
    {
        _mongoDatabase = new Mock<IMongoDatabase>();
    }

    protected TestKeyedMongoRepository Sut
    {
        get
        {
            if (_sut != null)
            {
                return _sut;
            }

            _sut = new TestKeyedMongoRepository(_mongoDatabase.Object);
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

            return _sut;
        }
    }

    protected Mock<IMongoDbIndexHandler> IndexHandler { get; set; }
    protected Mock<IMongoDbCreator> Creator { get; set; }

    protected Mock<IMongoDbReader> Reader { get; set; }
}