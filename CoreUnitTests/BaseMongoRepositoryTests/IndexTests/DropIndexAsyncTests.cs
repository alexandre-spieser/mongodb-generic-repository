using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CoreUnitTests.Infrastructure.Model;
using MongoDbGenericRepository.DataAccess.Index;
using Moq;
using Xunit;

namespace CoreUnitTests.BaseMongoRepositoryTests.IndexTests;

public class DropIndexAsyncTests: BaseIndexTests
{
    [Fact]
    public async Task WitIndexName_DropsIndex()
    {
        // Arrange
        var indexName = Fixture.Create<string>();
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.DropIndexAsync<TestDocument>(indexName);

        // Assert
        IndexHandler.Verify(
            x => x.DropIndexAsync<TestDocument, Guid>(indexName, null, CancellationToken.None));
    }

    [Fact]
    public async Task WitIndexNameAndCancellationToken_DropsIndex()
    {
        // Arrange
        var indexName = Fixture.Create<string>();
        var token = new CancellationToken(true);
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.DropIndexAsync<TestDocument>(indexName, token);

        // Assert
        IndexHandler.Verify(
            x => x.DropIndexAsync<TestDocument, Guid>(indexName, null, token));
    }

    [Fact]
    public async Task WitIndexNameAndPartitionKey_DropsIndex()
    {
        // Arrange
        var indexName = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.DropIndexAsync<TestDocument>(indexName, partitionKey);

        // Assert
        IndexHandler.Verify(
            x => x.DropIndexAsync<TestDocument, Guid>(indexName, partitionKey, CancellationToken.None));
    }

    [Fact]
    public async Task WitIndexNameAndPartitionKeyAndCancellationToken_DropsIndex()
    {
        // Arrange
        var indexName = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.DropIndexAsync<TestDocument>(indexName, partitionKey, token);

        // Assert
        IndexHandler.Verify(
            x => x.DropIndexAsync<TestDocument, Guid>(indexName, partitionKey, token));
    }

    #region Keyed

    [Fact]
    public async Task Keyed_WithIndexName_DropsIndex()
    {
        // Arrange
        var indexName = Fixture.Create<string>();
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.DropIndexAsync<TestDocumentWithKey<int>, int>(indexName);

        // Assert
        IndexHandler.Verify(
            x => x.DropIndexAsync<TestDocumentWithKey<int>, int>(indexName, null, CancellationToken.None));
    }

    [Fact]
    public async Task Keyed_WithIndexNameAndCancellationToken_DropsIndex()
    {
        // Arrange
        var indexName = Fixture.Create<string>();
        var token = new CancellationToken(true);
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.DropIndexAsync<TestDocumentWithKey<int>, int>(indexName, token);

        // Assert
        IndexHandler.Verify(
            x => x.DropIndexAsync<TestDocumentWithKey<int>, int>(indexName, null, token));
    }

    [Fact]
    public async Task Keyed_WithIndexNameAndPartitionKey_DropsIndex()
    {
        // Arrange
        var indexName = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.DropIndexAsync<TestDocumentWithKey<int>, int>(indexName, partitionKey);

        // Assert
        IndexHandler.Verify(
            x => x.DropIndexAsync<TestDocumentWithKey<int>, int>(indexName, partitionKey, CancellationToken.None));
    }

    [Fact]
    public async Task Keyed_WithIndexNameAndPartitionKeyAndCancellationToken_DropsIndex()
    {
        // Arrange
        var indexName = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.DropIndexAsync<TestDocumentWithKey<int>, int>(indexName, partitionKey, token);

        // Assert
        IndexHandler.Verify(
            x => x.DropIndexAsync<TestDocumentWithKey<int>, int>(indexName, partitionKey, token));
    }

    #endregion
}
