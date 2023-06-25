using System;
using System.Collections.Generic;
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

public class GetMinValueAsyncTests : BaseReaderTests
{
    private readonly Expression<Func<TestDocument, bool>> filter = x => x.SomeContent == "SomeContent";
    private readonly Expression<Func<TestDocument, int>> selector = x => x.SomeValue;

    [Fact]
    public async Task WithFilterAndSelector_GetsMatchingDocument()
    {
        // Arrange
        var value = Fixture.Create<int>();
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var (context, cursor) = SetupAsyncGet(value, collection);

        // Act
        var result = await Sut.GetMinValueAsync<TestDocument, Guid, int>(filter, selector);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(null), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        cursor.Verify(x => x.MoveNextAsync(CancellationToken.None), Times.Once);
        result.Should().Be(value);
    }

    [Fact]
    public async Task WithFilterAndSelectorAndCancellationToken_GetsMatchingDocument()
    {
        // Arrange
        var value = Fixture.Create<int>();
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var token = new CancellationToken(true);
        var (context, cursor) = SetupAsyncGet(value, collection);

        // Act
        var result = await Sut.GetMinValueAsync<TestDocument, Guid, int>(filter, selector, cancellationToken: token);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(null), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        cursor.Verify(x => x.MoveNextAsync(token), Times.Once);
        result.Should().Be(value);
    }

    [Fact]
    public async Task WithFilterAndSelectorAndPartitionKey_GetsMatchingDocument()
    {
        // Arrange
        var value = Fixture.Create<int>();
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var partitionKey = Fixture.Create<string>();
        var (context, cursor) = SetupAsyncGet(value, collection, partitionKey);

        // Act
        var result = await Sut.GetMinValueAsync<TestDocument, Guid, int>(filter, selector, partitionKey);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(partitionKey), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        cursor.Verify(x => x.MoveNextAsync(CancellationToken.None), Times.Once);
        result.Should().Be(value);
    }

    [Fact]
    public async Task WithFilterAndSelectorAndPartitionKeyAndCancellationToken_GetsMatchingDocument()
    {
        // Arrange
        var value = Fixture.Create<int>();
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        var (context, cursor) = SetupAsyncGet(value, collection, partitionKey);

        // Act
        var result = await Sut.GetMinValueAsync<TestDocument, Guid, int>(filter, selector, partitionKey, token);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(partitionKey), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        cursor.Verify(x => x.MoveNextAsync(token), Times.Once);
        result.Should().Be(value);
    }

    private (Mock<IMongoDbContext>, Mock<IAsyncCursor<TValue>>) SetupAsyncGet<TDocument, TValue>(
        TValue result,
        Mock<IMongoCollection<TDocument>> collection,
        string partitionKey = null)
    {
        var asyncCursor = SetupAsyncCursor(new List<TValue> {result});

        SetupFindAsync(collection, asyncCursor);

        var context = MockOf<IMongoDbContext>();

        context
            .Setup(x => x.GetCollection<TDocument>(partitionKey))
            .Returns(collection.Object);

        return (context, asyncCursor);
    }
}
