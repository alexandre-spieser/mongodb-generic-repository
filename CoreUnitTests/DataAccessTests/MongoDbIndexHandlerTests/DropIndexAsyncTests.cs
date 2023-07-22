using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CoreUnitTests.Infrastructure.Model;
using MongoDB.Driver;
using MongoDbGenericRepository;
using Moq;
using Xunit;

namespace CoreUnitTests.DataAccessTests.MongoDbIndexHandlerTests;

public class DropIndexAsyncTests : BaseIndexTests
{
    [Fact]
    public async Task WithIndexName_DropsIndex()
    {
        // Arrange
        var expectedIndexName = Fixture.Create<string>();
        var collection = MockOf<IMongoCollection<TestDocument>>();
        SetupContext(collection);

        var indexManger = SetupIndexManager(collection, expectedIndexName);

        // Act
        await Sut.DropIndexAsync<TestDocument, Guid>(expectedIndexName);

        // Assert
        indexManger.Verify(x => x.DropOneAsync(expectedIndexName, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task WithIndexNameAndPartitionKey_DropsIndex()
    {
        // Arrange
        var expectedIndexName = Fixture.Create<string>();
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var partitionKey = Fixture.Create<string>();

        var context = SetupContext(collection);

        var indexManger = SetupIndexManager(collection, expectedIndexName);

        // Act
        await Sut.DropIndexAsync<TestDocument, Guid>(expectedIndexName, partitionKey);

        // Assert
        indexManger.Verify(x => x.DropOneAsync(expectedIndexName, CancellationToken.None), Times.Once);
        context.Verify(x => x.GetCollection<TestDocument>(partitionKey), Times.Once);
    }

    [Fact]
    public async Task WithIndexNameAndCancellationToken_DropsIndex()
    {
        // Arrange
        var expectedIndexName = Fixture.Create<string>();
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var token = new CancellationToken(true);

        SetupContext(collection);

        var indexManger = SetupIndexManager(collection, expectedIndexName);

        // Act
        await Sut.DropIndexAsync<TestDocument, Guid>(expectedIndexName, cancellationToken: token);

        // Assert
        indexManger.Verify(x => x.DropOneAsync(expectedIndexName, token), Times.Once);
    }

    [Fact]
    public async Task WithIndexNameAndPartitionKeyAndCancellationToken_DropsIndex()
    {
        // Arrange
        var expectedIndexName = Fixture.Create<string>();
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var token = new CancellationToken(true);
        var partitionKey = Fixture.Create<string>();

        var context = SetupContext(collection);

        var indexManger = SetupIndexManager(collection, expectedIndexName);

        // Act
        await Sut.DropIndexAsync<TestDocument, Guid>(expectedIndexName, partitionKey, token);

        // Assert
        indexManger.Verify(x => x.DropOneAsync(expectedIndexName, token), Times.Once);
        context.Verify(x => x.GetCollection<TestDocument>(partitionKey), Times.Once);
    }

    private Mock<IMongoDbContext> SetupContext<TDocument>(IMock<IMongoCollection<TDocument>> collection)
    {
        var context = MockOf<IMongoDbContext>();
        context
            .Setup(x => x.GetCollection<TDocument>(It.IsAny<string>()))
            .Returns(collection.Object);

        return context;
    }

    private Mock<IMongoIndexManager<TDocument>> SetupIndexManager<TDocument>(Mock<IMongoCollection<TDocument>> collection, string indexName)
    {
        var indexManager = MockOf<IMongoIndexManager<TDocument>>();
        indexManager
            .Setup(
                x => x.CreateOneAsync(
                    It.IsAny<CreateIndexModel<TDocument>>(),
                    It.IsAny<CreateOneIndexOptions>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(indexName);

        collection
            .SetupGet(x => x.Indexes)
            .Returns(indexManager.Object);

        return indexManager;
    }
}
