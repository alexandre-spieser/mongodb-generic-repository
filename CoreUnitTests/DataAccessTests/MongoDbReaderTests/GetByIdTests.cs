using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoFixture;
using CoreUnitTests.Infrastructure.Model;
using FluentAssertions;
using MongoDB.Driver;
using MongoDbGenericRepository;
using Moq;
using Xunit;

namespace CoreUnitTests.DataAccessTests.MongoDbReaderTests;

public class GetByIdTests : BaseReaderTests
{
    [Fact]
    public void WithId_GetsMatchingDocument()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var (context, cursor) = SetupAsyncGet(documents, collection);

        // Act
        var result = Sut.GetById<TestDocument, Guid>(documents[0].Id);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(null), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        cursor.Verify(x => x.MoveNext(CancellationToken.None), Times.Once);
        result.Should().NotBeNull();
        result.Should().Be(documents[0]);
    }

    [Fact]
    public void WithIdAndCancellationToken_GetsMatchingDocument()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var token = new CancellationToken(true);
        var (context, cursor) = SetupAsyncGet(documents, collection);

        // Act
        var result = Sut.GetById<TestDocument, Guid>(documents[0].Id, cancellationToken: token);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(null), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        cursor.Verify(x => x.MoveNext(token), Times.Once);
        result.Should().NotBeNull();
        result.Should().Be(documents[0]);
    }

    [Fact]
    public void WithIdAndPartitionKey_GetsMatchingDocument()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var partitionKey = Fixture.Create<string>();
        var (context, cursor) = SetupAsyncGet(documents, collection, partitionKey);

        // Act
        var result = Sut.GetById<TestDocument, Guid>(documents[0].Id, partitionKey);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(partitionKey), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        cursor.Verify(x => x.MoveNext(CancellationToken.None), Times.Once);
        result.Should().NotBeNull();
        result.Should().Be(documents[0]);
    }

    [Fact]
    public void WithIdAndPartitionKeyAndCancellationToken_GetsMatchingDocument()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        var (context, cursor) = SetupAsyncGet(documents, collection, partitionKey);

        // Act
        var result = Sut.GetById<TestDocument, Guid>(documents[0].Id, partitionKey, token);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(partitionKey), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        cursor.Verify(x => x.MoveNext(token), Times.Once);
        result.Should().NotBeNull();
        result.Should().Be(documents[0]);
    }

    private (Mock<IMongoDbContext>, Mock<IAsyncCursor<TDocument>>) SetupAsyncGet<TDocument>(
        List<TDocument> documents,
        Mock<IMongoCollection<TDocument>> collection,
        string partitionKey = null)
    {
        var asyncCursor = SetupSyncCursor(documents);

        SetupFindSync(collection, asyncCursor);

        var context = MockOf<IMongoDbContext>();

        context
            .Setup(x => x.GetCollection<TDocument>(partitionKey))
            .Returns(collection.Object);

        return (context, asyncCursor);
    }
}
