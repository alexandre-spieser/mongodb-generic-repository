using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using FluentAssertions;
using MongoDB.Driver;
using MongoDbGenericRepository;
using MongoDbGenericRepository.Models;
using Moq;
using Xunit;

namespace CoreUnitTests.DataAccessTests.MongoDbIndexHandlerTests;

public class CreateAscendingIndexAsyncTests : BaseIndexTests
{
    private readonly Expression<Func<TestDocument, object>> fieldExpression = t => t.SomeContent2;

    [Fact]
    public async Task WithFieldExpression_CreatesIndex()
    {
        // Arrange
        var expectedIndexName = Fixture.Create<string>();
        var collection = MockOf<IMongoCollection<TestDocument>>();
        SetupContext(collection);

        var indexManger = SetupIndexManager(collection, expectedIndexName);

        // Act
        var result = await Sut.CreateAscendingIndexAsync<TestDocument, Guid>(fieldExpression);

        // Assert
        result.Should().Be(expectedIndexName);
        indexManger.Verify(
            x => x.CreateOneAsync(
                It.Is<CreateIndexModel<TestDocument>>(t => t.Keys.EqualToJson("{\"SomeContent2\":1}")),
                null,
                CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task WithFieldExpressionAndOptions_CreatesIndex()
    {
        // Arrange
        var expectedIndexName = Fixture.Create<string>();
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var options = Fixture.Create<IndexCreationOptions>();

        SetupContext(collection);
        var indexManger = SetupIndexManager(collection, expectedIndexName);

        // Act
        var result = await Sut.CreateAscendingIndexAsync<TestDocument, Guid>(fieldExpression, options);

        // Assert
        result.Should().Be(expectedIndexName);
        indexManger.Verify(
            x => x.CreateOneAsync(
                It.Is<CreateIndexModel<TestDocument>>(
                    t => t.Keys.EqualToJson("{\"SomeContent2\":1}") &&
                         t.Options.EqualTo(options)),
                null,
                CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task WithFieldExpressionAndPartitionKey_CreatesIndex()
    {
        // Arrange
        var expectedIndexName = Fixture.Create<string>();
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var partitionKey = Fixture.Create<string>();

        var context = SetupContext(collection);

        var indexManger = SetupIndexManager(collection, expectedIndexName);

        // Act
        var result = await Sut.CreateAscendingIndexAsync<TestDocument, Guid>(fieldExpression, partitionKey: partitionKey);

        // Assert
        result.Should().Be(expectedIndexName);
        indexManger.Verify(
            x => x.CreateOneAsync(
                It.Is<CreateIndexModel<TestDocument>>(
                    t => t.Keys.EqualToJson("{\"SomeContent2\":1}") ),
                null,
                CancellationToken.None),
            Times.Once);
        context.Verify(x => x.GetCollection<TestDocument>(partitionKey), Times.Once);
    }

    [Fact]
    public async Task WithFieldExpressionAndCancellationToken_CreatesIndex()
    {
        // Arrange
        var expectedIndexName = Fixture.Create<string>();
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var token = new CancellationToken(true);

        SetupContext(collection);

        var indexManger = SetupIndexManager(collection, expectedIndexName);

        // Act
        var result = await Sut.CreateAscendingIndexAsync<TestDocument, Guid>(fieldExpression, cancellationToken: token);

        // Assert
        result.Should().Be(expectedIndexName);
        indexManger.Verify(
            x => x.CreateOneAsync(
                It.Is<CreateIndexModel<TestDocument>>(
                    t => t.Keys.EqualToJson("{\"SomeContent2\":1}") ),
                null,
                token),
            Times.Once);
    }

    [Fact]
    public async Task WithFieldExpressionAndOptionsAndPartitionKeyAndCancellationToken_CreatesIndex()
    {
        // Arrange
        var expectedIndexName = Fixture.Create<string>();
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var token = new CancellationToken(true);
        var partitionKey = Fixture.Create<string>();
        var options = Fixture.Create<IndexCreationOptions>();

        var context = SetupContext(collection);

        var indexManger = SetupIndexManager(collection, expectedIndexName);

        // Act
        var result = await Sut.CreateAscendingIndexAsync<TestDocument, Guid>(fieldExpression, options, partitionKey, token);

        // Assert
        result.Should().Be(expectedIndexName);
        indexManger.Verify(
            x => x.CreateOneAsync(
                It.Is<CreateIndexModel<TestDocument>>(
                    t => t.Keys.EqualToJson("{\"SomeContent2\":1}") &&
                         t.Options.EqualTo(options)),
                null,
                token),
            Times.Once);
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
