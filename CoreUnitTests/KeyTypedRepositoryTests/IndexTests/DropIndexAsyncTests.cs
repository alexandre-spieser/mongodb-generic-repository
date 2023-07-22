using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using MongoDbGenericRepository.DataAccess.Index;
using Moq;
using Xunit;

namespace CoreUnitTests.KeyTypedRepositoryTests.IndexTests;

public class DropIndexAsyncTests : TestKeyedMongoRepositoryContext<int>
{
    [Fact]
    public async Task WitIndexName_DropsIndex()
    {
        // Arrange
        var indexName = Fixture.Create<string>();
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.DropIndexAsync<TestDocumentWithKey<int>>(indexName);

        // Assert
        IndexHandler.Verify(
            x => x.DropIndexAsync<TestDocumentWithKey<int>, int>(indexName, null, CancellationToken.None));
    }

    [Fact]
    public async Task WitIndexNameAndCancellationToken_DropsIndex()
    {
        // Arrange
        var indexName = Fixture.Create<string>();
        var token = new CancellationToken(true);
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.DropIndexAsync<TestDocumentWithKey<int>>(indexName, token);

        // Assert
        IndexHandler.Verify(
            x => x.DropIndexAsync<TestDocumentWithKey<int>, int>(indexName, null, token));
    }

    [Fact]
    public async Task WitIndexNameAndPartitionKey_DropsIndex()
    {
        // Arrange
        var indexName = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        IndexHandler = new Mock<IMongoDbIndexHandler>();

        // Act
        await Sut.DropIndexAsync<TestDocumentWithKey<int>>(indexName, partitionKey);

        // Assert
        IndexHandler.Verify(
            x => x.DropIndexAsync<TestDocumentWithKey<int>, int>(indexName, partitionKey, CancellationToken.None));
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
        await Sut.DropIndexAsync<TestDocumentWithKey<int>>(indexName, partitionKey, token);

        // Assert
        IndexHandler.Verify(
            x => x.DropIndexAsync<TestDocumentWithKey<int>, int>(indexName, partitionKey, token));
    }
}
