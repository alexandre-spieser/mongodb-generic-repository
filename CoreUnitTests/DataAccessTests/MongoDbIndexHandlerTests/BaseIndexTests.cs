using System.Collections.Generic;
using System.Threading;
using CoreUnitTests.Infrastructure;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbGenericRepository.DataAccess.Index;
using Moq;

namespace CoreUnitTests.DataAccessTests.MongoDbIndexHandlerTests;

public class BaseIndexTests : GenericTestContext<MongoDbIndexHandler>
{
    protected (Mock<IAsyncCursor<BsonDocument>>, Mock<IMongoIndexManager<TDocument>>) SetupIndexes<TDocument>(
        List<BsonDocument> indexes,
        Mock<IMongoCollection<TDocument>> collection)
    {
        var asyncCursor = MockOf<IAsyncCursor<BsonDocument>>();
        var moveNextSequence = asyncCursor
            .SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()));

        var currentSequence = asyncCursor
            .SetupSequence(x => x.Current);

        foreach (var bsonDocument in indexes)
        {
            moveNextSequence.ReturnsAsync(true);
            currentSequence.Returns(new[] {bsonDocument});
        }

        moveNextSequence.ReturnsAsync(false);

        var indexManager = MockOf<IMongoIndexManager<TDocument>>();
        indexManager
            .Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(asyncCursor.Object);

        collection
            .SetupGet(x => x.Indexes)
            .Returns(indexManager.Object);

        return (asyncCursor, indexManager);
    }
}
