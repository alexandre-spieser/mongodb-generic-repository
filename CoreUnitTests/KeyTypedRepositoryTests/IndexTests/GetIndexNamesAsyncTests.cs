using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using MongoDbGenericRepository.DataAccess.Index;
using Moq;
using Xunit;

namespace CoreUnitTests.KeyTypedRepositoryTests.IndexTests;

public class GetIndexNamesAsyncTests : TestKeyedMongoRepositoryContext<int>
{
    [Fact]
    public async Task WithNoParameters_ReturnsIndexNames()
    {
        // Arrange
        IndexHandler = new Mock<IMongoDbIndexHandler>();
        var indexName = Fixture.Create<string>();

        IndexHandler
            .Setup(x => x.GetIndexesNamesAsync<TestDocumentWithKey<int>, int>(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<string> { indexName });

        // Act
        var result = await Sut.GetIndexesNamesAsync<TestDocumentWithKey<int>>();

        // Assert
        Assert.NotNull(result);
        Assert.Contains(result, x => x == indexName);
        IndexHandler.Verify(x => x.GetIndexesNamesAsync<TestDocumentWithKey<int>, int>(null, CancellationToken.None), Times.Once());
    }

    [Fact]
    public async Task WithCancellationToken_ReturnsIndexNames()
    {
        // Arrange
        IndexHandler = new Mock<IMongoDbIndexHandler>();
        var indexName = Fixture.Create<string>();
        var token = new CancellationToken(true);

        IndexHandler
            .Setup(x => x.GetIndexesNamesAsync<TestDocumentWithKey<int>, int>(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<string> { indexName });

        // Act
        var result = await Sut.GetIndexesNamesAsync<TestDocumentWithKey<int>>(token);

        // Assert
        Assert.NotNull(result);
        Assert.Contains(result, x => x == indexName);
        IndexHandler.Verify(x => x.GetIndexesNamesAsync<TestDocumentWithKey<int>, int>(null, token), Times.Once());
    }

    [Fact]
    public async Task WithPartitionKey_ReturnsIndexNames()
    {
        // Arrange
        IndexHandler = new Mock<IMongoDbIndexHandler>();
        var indexName = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();

        IndexHandler
            .Setup(x => x.GetIndexesNamesAsync<TestDocumentWithKey<int>, int>(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<string> { indexName });

        // Act
        var result = await Sut.GetIndexesNamesAsync<TestDocumentWithKey<int>>(partitionKey);

        // Assert
        Assert.NotNull(result);
        Assert.Contains(result, x => x == indexName);
        IndexHandler.Verify(x => x.GetIndexesNamesAsync<TestDocumentWithKey<int>, int>(partitionKey, CancellationToken.None), Times.Once());
    }

    [Fact]
    public async Task WithPartitionKeyAndCancellationToken_ReturnsIndexNames()
    {
        // Arrange
        IndexHandler = new Mock<IMongoDbIndexHandler>();
        var indexName = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        IndexHandler
            .Setup(x => x.GetIndexesNamesAsync<TestDocumentWithKey<int>, int>(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<string> { indexName });

        // Act
        var result = await Sut.GetIndexesNamesAsync<TestDocumentWithKey<int>>(partitionKey, token);

        // Assert
        Assert.NotNull(result);
        Assert.Contains(result, x => x == indexName);
        IndexHandler.Verify(x => x.GetIndexesNamesAsync<TestDocumentWithKey<int>, int>(partitionKey, token), Times.Once());
    }
}
