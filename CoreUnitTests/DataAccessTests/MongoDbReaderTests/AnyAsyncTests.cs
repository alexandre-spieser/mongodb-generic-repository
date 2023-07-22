using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CoreUnitTests.Infrastructure.Model;
using FluentAssertions;
using MongoDB.Driver;
using MongoDbGenericRepository;
using Moq;
using Xunit;

namespace CoreUnitTests.DataAccessTests.MongoDbReaderTests;

public class AnyAsyncTests : BaseReaderTests
{
    private readonly Expression<Func<TestDocument, bool>> filter = x => x.SomeContent == "SomeContent";

    [Fact]
    public async Task WithFilter_ReturnsResult()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();

        var context = MockOf<IMongoDbContext>();

        context
            .Setup(x => x.GetCollection<TestDocument>(null))
            .Returns(collection.Object);

        collection
            .Setup(
                x => x.CountDocumentsAsync(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<CountOptions>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(documents.Count);

        // Act
        var result = await Sut.AnyAsync<TestDocument, Guid>(filter);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(null), Times.Once);
        collection.Verify(
            x => x.CountDocumentsAsync(It.Is<ExpressionFilterDefinition<TestDocument>>(y => y.Expression == filter), null, CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task WithFilterAndCancellationToken_ReturnsResult()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var token = new CancellationToken(true);
        var context = MockOf<IMongoDbContext>();

        context
            .Setup(x => x.GetCollection<TestDocument>(null))
            .Returns(collection.Object);

        collection
            .Setup(
                x => x.CountDocumentsAsync(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<CountOptions>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(documents.Count);

        // Act
        var result = await Sut.AnyAsync<TestDocument, Guid>(filter, cancellationToken: token);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(null), Times.Once);
        collection.Verify(
            x => x.CountDocumentsAsync(It.Is<ExpressionFilterDefinition<TestDocument>>(y => y.Expression == filter), null, token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task WithFilterAndPartitionKey_ReturnsResult()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var partitionKey = Fixture.Create<string>();

        var context = MockOf<IMongoDbContext>();

        context
            .Setup(x => x.GetCollection<TestDocument>(partitionKey))
            .Returns(collection.Object);

        collection
            .Setup(
                x => x.CountDocumentsAsync(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<CountOptions>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(documents.Count);

        // Act
        var result = await Sut.AnyAsync<TestDocument, Guid>(filter, partitionKey);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(partitionKey), Times.Once);
        collection.Verify(
            x => x.CountDocumentsAsync(It.Is<ExpressionFilterDefinition<TestDocument>>(y => y.Expression == filter), null, CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task WithFilterAndPartitionKeyAndCancellationToken_ReturnsResult()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        var context = MockOf<IMongoDbContext>();

        context
            .Setup(x => x.GetCollection<TestDocument>(partitionKey))
            .Returns(collection.Object);

        collection
            .Setup(
                x => x.CountDocumentsAsync(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<CountOptions>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(documents.Count);

        // Act
        var result = await Sut.AnyAsync<TestDocument, Guid>(filter, partitionKey, token);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(partitionKey), Times.Once);
        collection.Verify(
            x => x.CountDocumentsAsync(It.Is<ExpressionFilterDefinition<TestDocument>>(y => y.Expression == filter), null, token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task WithCondition_ReturnsResult()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var condition = Builders<TestDocument>.Filter.Eq("Id", documents[0].Id);
        var context = MockOf<IMongoDbContext>();

        context
            .Setup(x => x.GetCollection<TestDocument>(null))
            .Returns(collection.Object);

        collection
            .Setup(
                x => x.CountDocumentsAsync(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<CountOptions>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(documents.Count);

        // Act
        var result = await Sut.AnyAsync<TestDocument, Guid>(condition);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(null), Times.Once);
        collection.Verify(
            x => x.CountDocumentsAsync(condition, null, CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task WithConditionAndCancellationToken_ReturnsResult()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var condition = Builders<TestDocument>.Filter.Eq("Id", documents[0].Id);
        var token = new CancellationToken(true);
        var context = MockOf<IMongoDbContext>();

        context
            .Setup(x => x.GetCollection<TestDocument>(null))
            .Returns(collection.Object);

        collection
            .Setup(
                x => x.CountDocumentsAsync(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<CountOptions>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(documents.Count);

        // Act
        var result = await Sut.AnyAsync<TestDocument, Guid>(condition, cancellationToken: token);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(null), Times.Once);
        collection.Verify(
            x => x.CountDocumentsAsync(condition, null, token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task WithConditionAndPartitionKey_ReturnsResult()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var condition = Builders<TestDocument>.Filter.Eq("Id", documents[0].Id);
        var partitionKey = Fixture.Create<string>();
        var context = MockOf<IMongoDbContext>();

        context
            .Setup(x => x.GetCollection<TestDocument>(partitionKey))
            .Returns(collection.Object);

        collection
            .Setup(
                x => x.CountDocumentsAsync(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<CountOptions>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(documents.Count);

        // Act
        var result = await Sut.AnyAsync<TestDocument, Guid>(condition, partitionKey: partitionKey);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(partitionKey), Times.Once);
        collection.Verify(
            x => x.CountDocumentsAsync(condition, null, CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task WithConditionAndPartitionKeyAndCancellationToken_ReturnsResult()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var condition = Builders<TestDocument>.Filter.Eq("Id", documents[0].Id);
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        var context = MockOf<IMongoDbContext>();

        context
            .Setup(x => x.GetCollection<TestDocument>(partitionKey))
            .Returns(collection.Object);

        collection
            .Setup(
                x => x.CountDocumentsAsync(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<CountOptions>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(documents.Count);

        // Act
        var result = await Sut.AnyAsync<TestDocument, Guid>(condition, partitionKey: partitionKey, cancellationToken:token);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(partitionKey), Times.Once);
        collection.Verify(
            x => x.CountDocumentsAsync(condition, null, token),
            Times.Once);
        result.Should().BeTrue();
    }
}
