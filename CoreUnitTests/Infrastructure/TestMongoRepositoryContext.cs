using MongoDB.Driver;
using MongoDbGenericRepository.DataAccess.Create;
using MongoDbGenericRepository.DataAccess.Index;
using MongoDbGenericRepository.DataAccess.Read;
using Moq;

namespace CoreUnitTests.Infrastructure;

public class TestMongoRepositoryContext
{
    private readonly Mock<IMongoDatabase> _mongoDatabase;

    private TestMongoRepository _sut;

    protected TestMongoRepositoryContext()
    {
        _mongoDatabase = new Mock<IMongoDatabase>();
    }

    protected TestMongoRepository Sut
    {
        get
        {
            if (_sut == null)
            {
                _sut = new TestMongoRepository(_mongoDatabase.Object);
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
            }

            return _sut;
        }
    }

    protected Mock<IMongoDbIndexHandler> IndexHandler { get; set; }
    protected Mock<IMongoDbCreator> Creator { get; set; }

    protected Mock<IMongoDbReader> Reader { get; set; }
}