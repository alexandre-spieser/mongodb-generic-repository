using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CoreUnitTests.Infrastructure.Model;
using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbGenericRepository;
using Moq;
using Xunit;

namespace CoreUnitTests.DataAccessTests.MongoDbIndexHandlerTests;

public class GetIndexNamesAsyncTests : BaseIndexTests
{
    [Fact]
    public async Task WithNoParameters_ReturnsAllIndexNames()
    {
        // Arrange
        var indexNames = Fixture.CreateMany<string>().ToList();
        var indexes = indexNames.Select(x => new BsonDocument {{"name", x}}).ToList();

        var collection = MockOf<IMongoCollection<TestDocument>>();

        var context = MockOf<IMongoDbContext>();
        context
            .Setup(x => x.GetCollection<TestDocument>(It.IsAny<string>()))
            .Returns(collection.Object);

        var (cursor, manager) = SetupIndexes(indexes, collection);

        // Act
        var result = await Sut.GetIndexesNamesAsync<TestDocument, Guid>();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(indexNames);
        context.Verify(x => x.GetCollection<TestDocument>(null));
        manager.Verify(x => x.ListAsync(CancellationToken.None));
        cursor.Verify(x => x.MoveNextAsync(CancellationToken.None));
    }

    [Fact]
    public async Task WithPartitionKey_ReturnsAllIndexNames()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var indexNames = Fixture.CreateMany<string>().ToList();
        var indexes = indexNames.Select(x => new BsonDocument {{"name", x}}).ToList();

        var collection = MockOf<IMongoCollection<TestDocument>>();

        var context = MockOf<IMongoDbContext>();
        context
            .Setup(x => x.GetCollection<TestDocument>(It.IsAny<string>()))
            .Returns(collection.Object);

        var (cursor, manager) = SetupIndexes(indexes, collection);

        // Act
        var result = await Sut.GetIndexesNamesAsync<TestDocument, Guid>(partitionKey);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(indexNames);
        context.Verify(x => x.GetCollection<TestDocument>(partitionKey));
        manager.Verify(x => x.ListAsync(CancellationToken.None));
        cursor.Verify(x => x.MoveNextAsync(CancellationToken.None));
    }

    [Fact]
    public async Task WithCancellationToken_ReturnsAllIndexNames()
    {
        // Arrange
        var token = new CancellationToken();
        var indexNames = Fixture.CreateMany<string>().ToList();
        var indexes = indexNames.Select(x => new BsonDocument {{"name", x}}).ToList();

        var collection = MockOf<IMongoCollection<TestDocument>>();

        var context = MockOf<IMongoDbContext>();
        context
            .Setup(x => x.GetCollection<TestDocument>(It.IsAny<string>()))
            .Returns(collection.Object);

        var (cursor, manager) = SetupIndexes(indexes, collection);

        // Act
        var result = await Sut.GetIndexesNamesAsync<TestDocument, Guid>(cancellationToken:token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(indexNames);
        context.Verify(x => x.GetCollection<TestDocument>(null));
        manager.Verify(x => x.ListAsync(token));
        cursor.Verify(x => x.MoveNextAsync(token));
    }

    [Fact]
    public async Task WithPartitionKeyCancellationToken_ReturnsAllIndexNames()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken();
        var indexNames = Fixture.CreateMany<string>().ToList();
        var indexes = indexNames.Select(x => new BsonDocument {{"name", x}}).ToList();

        var collection = MockOf<IMongoCollection<TestDocument>>();

        var context = MockOf<IMongoDbContext>();
        context
            .Setup(x => x.GetCollection<TestDocument>(It.IsAny<string>()))
            .Returns(collection.Object);

        var (cursor, manager) = SetupIndexes(indexes, collection);

        // Act
        var result = await Sut.GetIndexesNamesAsync<TestDocument, Guid>(partitionKey, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(indexNames);
        context.Verify(x => x.GetCollection<TestDocument>(partitionKey));
        manager.Verify(x => x.ListAsync(token));
        cursor.Verify(x => x.MoveNextAsync(token));
    }
}
