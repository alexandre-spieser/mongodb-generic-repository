using System;
using System.Collections.Generic;
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

public class GetByMaxTests : BaseReaderTests
{
    private readonly Expression<Func<TestDocument, bool>> filter = x => x.SomeContent == "SomeContent";
    private readonly Expression<Func<TestDocument, object>> selector = x => x.SomeValue;

    [Fact]
    public void WithFilterAndSelector_GetsMatchingDocument()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var (context, cursor) = SetupSyncGet(documents, collection);

        // Act
        var result = Sut.GetByMax<TestDocument, Guid>(filter, selector);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(null), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        cursor.Verify(x => x.MoveNext(CancellationToken.None), Times.Once);
        result.Should().NotBeNull();
        result.Should().Be(documents[0]);
    }

    [Fact]
    public void WithFilterAndSelectorAndCancellationToken_GetsMatchingDocument()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var token = new CancellationToken(true);
        var (context, cursor) = SetupSyncGet(documents, collection);

        // Act
        var result = Sut.GetByMax<TestDocument, Guid>(filter, selector, cancellationToken: token);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(null), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        cursor.Verify(x => x.MoveNext(token), Times.Once);
        result.Should().NotBeNull();
        result.Should().Be(documents[0]);
    }

    [Fact]
    public void WithFilterAndSelectorAndPartitionKey_GetsMatchingDocument()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var partitionKey = Fixture.Create<string>();
        var (context, cursor) = SetupSyncGet(documents, collection, partitionKey);

        // Act
        var result = Sut.GetByMax<TestDocument, Guid>(filter, selector, partitionKey);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(partitionKey), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        cursor.Verify(x => x.MoveNext(CancellationToken.None), Times.Once);
        result.Should().NotBeNull();
        result.Should().Be(documents[0]);
    }

    [Fact]
    public void WithFilterAndSelectorAndPartitionKeyAndCancellationToken_GetsMatchingDocument()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        var (context, cursor) = SetupSyncGet(documents, collection, partitionKey);

        // Act
        var result = Sut.GetByMax<TestDocument, Guid>(filter, selector, partitionKey, token);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(partitionKey), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        cursor.Verify(x => x.MoveNext(token), Times.Once);
        result.Should().NotBeNull();
        result.Should().Be(documents[0]);
    }

    private (Mock<IMongoDbContext>, Mock<IAsyncCursor<TDocument>>) SetupSyncGet<TDocument>(
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
