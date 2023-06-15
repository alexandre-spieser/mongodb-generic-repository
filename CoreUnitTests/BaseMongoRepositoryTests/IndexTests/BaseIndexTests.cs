using System.Threading;
using CoreUnitTests.Infrastructure;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;

namespace CoreUnitTests.BaseMongoRepositoryTests.IndexTests;

public class BaseIndexTests : TestMongoRepositoryContext
{
    protected Mock<IAsyncCursor<BsonDocument>> SetupIndex<TDocument>(BsonDocument index, Mock<IMongoCollection<TDocument>> collection)
    {
        var asyncCursor = new Mock<IAsyncCursor<BsonDocument>>();
        asyncCursor
            .SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true)
            .ReturnsAsync(false);

        asyncCursor
            .SetupGet(x => x.Current)
            .Returns(new[] {index});

        var indexManager = new Mock<IMongoIndexManager<TDocument>>();
        indexManager
            .Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(asyncCursor.Object);

        collection
            .SetupGet(x => x.Indexes)
            .Returns(indexManager.Object);
        
        return asyncCursor;
    }
}