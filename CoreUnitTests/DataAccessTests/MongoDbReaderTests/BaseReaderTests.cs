using System.Collections.Generic;
using System.Threading;
using CoreUnitTests.Infrastructure;
using MongoDB.Driver;
using MongoDbGenericRepository.DataAccess.Read;
using Moq;

namespace CoreUnitTests.DataAccessTests.MongoDbReaderTests;

public class BaseReaderTests : GenericTestContext<MongoDbReader>
{
    protected Mock<IAsyncCursor<TDocument>> SetupSyncCursor<TDocument>(List<TDocument> documents)
    {
        var asyncCursor = MockOf<IAsyncCursor<TDocument>>();

        var moveNextSequence = asyncCursor
            .SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()));

        var currentSequence = asyncCursor
            .SetupSequence(x => x.Current);

        foreach (var projection in documents)
        {
            moveNextSequence.Returns(true);
            currentSequence.Returns(new[] {projection});
        }

        moveNextSequence.Returns(false);
        return asyncCursor;
    }

    protected Mock<IAsyncCursor<TDocument>> SetupAsyncCursor<TDocument>(List<TDocument> documents)
    {
        var asyncCursor = MockOf<IAsyncCursor<TDocument>>();

        var moveNextSequence = asyncCursor
            .SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()));

        var currentSequence = asyncCursor
            .SetupSequence(x => x.Current);

        foreach (var projection in documents)
        {
            moveNextSequence.ReturnsAsync(true);
            currentSequence.Returns(new[] {projection});
        }

        moveNextSequence.ReturnsAsync(false);
        return asyncCursor;
    }

    protected static void SetupFindAsync<TDocument, TProjection>(Mock<IMongoCollection<TDocument>> collection, Mock<IAsyncCursor<TProjection>> asyncCursor) =>
        collection
            .Setup(
                x => x.FindAsync(
                    It.IsAny<FilterDefinition<TDocument>>(),
                    It.IsAny<FindOptions<TDocument, TProjection>>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(asyncCursor.Object);

    protected static void SetupFindSync<TDocument, TProjection>(Mock<IMongoCollection<TDocument>> collection, Mock<IAsyncCursor<TProjection>> asyncCursor) =>
        collection
            .Setup(
                x => x.FindSync(
                    It.IsAny<FilterDefinition<TDocument>>(),
                    It.IsAny<FindOptions<TDocument, TProjection>>(),
                    It.IsAny<CancellationToken>()))
            .Returns(asyncCursor.Object);
}
