using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using AutoFixture;
using CoreUnitTests.Infrastructure.Model;
using FluentAssertions;
using MongoDB.Driver;
using MongoDbGenericRepository;
using Moq;
using Xunit;

namespace CoreUnitTests.DataAccessTests.MongoDbReaderTests;

public class GetOneTests : BaseReaderTests
{
    [Fact]
    public void WithFilter_GetsMatchingDocuments()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        Expression<Func<TestDocument, bool>> filter = x => x.Id == documents[0].Id;
        var (context, cursor) = SetupSyncGet(documents, collection);


        // Act
        var result = Sut.GetOne<TestDocument, Guid>(filter);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(null), Times.Once);
        cursor.Verify(x => x.MoveNext(CancellationToken.None), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        result.Should().NotBeNull();
        result.Should().Be(documents[0]);
    }

    [Fact]
    public void WithFilterAndCancellationToken_GetsMatchingDocuments()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var token = new CancellationToken(true);
        Expression<Func<TestDocument, bool>> filter = x => x.Id == documents[0].Id;
        var (context, cursor) = SetupSyncGet(documents, collection);

        // Act
        var result = Sut.GetOne<TestDocument, Guid>(filter, cancellationToken: token);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(null), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        cursor.Verify(x => x.MoveNext(token), Times.Once);
        result.Should().NotBeNull();
        result.Should().Be(documents[0]);
    }

    [Fact]
    public void WithFilterAndPartitionKey_GetsMatchingDocuments()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var partitionKey = Fixture.Create<string>();
        Expression<Func<TestDocument, bool>> filter = x => x.Id == documents[0].Id;
        var (context, cursor) = SetupSyncGet(documents, collection, partitionKey);

        // Act
        var result = Sut.GetOne<TestDocument, Guid>(filter, partitionKey);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(partitionKey), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        cursor.Verify(x => x.MoveNext(CancellationToken.None), Times.Once);
        result.Should().NotBeNull();
        result.Should().Be(documents[0]);
    }

    [Fact]
    public void WithFilterAndPartitionKeyAndCancellationToken_GetsMatchingDocuments()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Expression<Func<TestDocument, bool>> filter = x => x.Id == documents[0].Id;
        var (context, cursor) = SetupSyncGet(documents, collection, partitionKey);

        // Act
        var result = Sut.GetOne<TestDocument, Guid>(filter, partitionKey, token);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(partitionKey), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        cursor.Verify(x => x.MoveNext(token), Times.Once);
        result.Should().NotBeNull();
        result.Should().Be(documents[0]);
    }

    [Fact]
    public void WithCondition_GetsMatchingDocuments()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var condition = Builders<TestDocument>.Filter.Eq("Id", documents[0].Id);
        var (context, cursor) = SetupSyncGet(documents, collection);

        // Act
        var result = Sut.GetOne<TestDocument, Guid>(condition);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(null), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        cursor.Verify(x => x.MoveNext(CancellationToken.None), Times.Once);
        result.Should().NotBeNull();
        result.Should().Be(documents[0]);
    }

    [Fact]
    public void WithConditionAndCancellationToken_GetsMatchingDocuments()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var condition = Builders<TestDocument>.Filter.Eq("Id", documents[0].Id);
        var token = new CancellationToken(true);
        var (context, cursor) = SetupSyncGet(documents, collection);

        // Act
        var result = Sut.GetOne<TestDocument, Guid>(condition, cancellationToken: token);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(null), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        cursor.Verify(x => x.MoveNext(token), Times.Once);
        result.Should().NotBeNull();
        result.Should().Be(documents[0]);
    }

    [Fact]
    public void WithConditionAndPartitionKey_GetsMatchingDocuments()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var condition = Builders<TestDocument>.Filter.Eq("Id", documents[0].Id);
        var partitionKey = Fixture.Create<string>();
        var (context, cursor) = SetupSyncGet(documents, collection, partitionKey);

        // Act
        var result = Sut.GetOne<TestDocument, Guid>(condition, partitionKey: partitionKey);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(partitionKey), Times.Once);
        cursor.Verify(x => x.MoveNext(CancellationToken.None), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        result.Should().NotBeNull();
        result.Should().Be(documents[0]);
    }

    [Fact]
    public void WithConditionAndPartitionKeyAndCancellationToken_GetsMatchingDocuments()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var condition = Builders<TestDocument>.Filter.Eq("Id", documents[0].Id);
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        var (context, cursor) = SetupSyncGet(documents, collection, partitionKey);

        // Act
        var result = Sut.GetOne<TestDocument, Guid>(condition, partitionKey: partitionKey, cancellationToken: token);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(partitionKey), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        cursor.Verify(x => x.MoveNext(token), Times.Once);
        result.Should().NotBeNull();
        result.Should().Be(documents[0]);
    }

    [Fact]
    public void WithConditionAndFindOptions_GetsMatchingDocuments()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var options = Fixture
            .Build<FindOptions>()
            .Without(x => x.Comment)
            .Without(x => x.Hint)
            .Create();
        var condition = Builders<TestDocument>.Filter.Eq("Id", documents[0].Id);
        var (context, cursor) = SetupSyncGet(documents, collection);

        // Act
        var result = Sut.GetOne<TestDocument, Guid>(condition, options);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(null), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        cursor.Verify(x => x.MoveNext(CancellationToken.None), Times.Once);
        result.Should().NotBeNull();
        result.Should().Be(documents[0]);
    }

    [Fact]
    public void WithConditionAndFindOptionsAndCancellationToken_GetsMatchingDocuments()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var options = Fixture
            .Build<FindOptions>()
            .Without(x => x.Comment)
            .Without(x => x.Hint)
            .Create();
        var condition = Builders<TestDocument>.Filter.Eq("Id", documents[0].Id);
        var token = new CancellationToken(true);
        var (context, cursor) = SetupSyncGet(documents, collection);

        // Act
        var result = Sut.GetOne<TestDocument, Guid>(condition, options, cancellationToken: token);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(null), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        cursor.Verify(x => x.MoveNext(token), Times.Once);
        result.Should().NotBeNull();
        result.Should().Be(documents[0]);
    }

    [Fact]
    public void WithConditionAndFindOptionsAndPartitionKeyAndCancellationToken_GetsMatchingDocuments()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var options = Fixture
            .Build<FindOptions>()
            .Without(x => x.Comment)
            .Without(x => x.Hint)
            .Create();
        var condition = Builders<TestDocument>.Filter.Eq("Id", documents[0].Id);
        var token = new CancellationToken(true);
        var partitionKey = Fixture.Create<string>();
        var (context, cursor) = SetupSyncGet(documents, collection, partitionKey);

        // Act
        var result = Sut.GetOne<TestDocument, Guid>(condition, options, partitionKey, token);

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
