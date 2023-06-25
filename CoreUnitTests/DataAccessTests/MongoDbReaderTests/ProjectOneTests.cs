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

public class ProjectOneTests : BaseReaderTests
{
    private readonly Expression<Func<TestDocument, bool>> filter = t => string.IsNullOrWhiteSpace(t.SomeContent2);

    private readonly Expression<Func<TestDocument, TestProjection>>
        projectionExpression = t => new TestProjection {TestDocumentId = t.Id, NestedData = t.Nested.SomeDate};

    [Fact]
    public void WithFilterAndProjection_Projects()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var projections = Fixture.CreateMany<TestProjection>().ToList();
        var (context, cursor) = SetupProjection(projections, collection);

        // Act
        var result = Sut.ProjectOne<TestDocument, TestProjection, Guid>(filter, projectionExpression);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(null), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        cursor.Verify(x => x.MoveNext(CancellationToken.None), Times.Once);
        result.Should().NotBeNull();
        result.Should().Be(projections[0]);
    }

    [Fact]
    public void WithFilterAndProjectionAndCancellationToken_Projects()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var projections = Fixture.CreateMany<TestProjection>().ToList();
        var token = new CancellationToken(true);
        var (context, cursor) = SetupProjection(projections, collection);

        // Act
        var result = Sut.ProjectOne<TestDocument, TestProjection, Guid>(filter, projectionExpression, cancellationToken: token);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(null), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        cursor.Verify(x => x.MoveNext(token), Times.Once);
        result.Should().NotBeNull();
        result.Should().Be(projections[0]);
    }

    [Fact]
    public void WithFilterAndProjectionAndPartitionKey_Projects()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var projections = Fixture.CreateMany<TestProjection>().ToList();
        var partitionKey = Fixture.Create<string>();
        var (context, cursor) = SetupProjection(projections, collection, partitionKey);

        // Act
        var result = Sut.ProjectOne<TestDocument, TestProjection, Guid>(filter, projectionExpression, partitionKey);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(partitionKey), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        cursor.Verify(x => x.MoveNext(CancellationToken.None), Times.Once);
        result.Should().NotBeNull();
        result.Should().Be(projections[0]);
    }

    [Fact]
    public void WithFilterAndProjectionAndPartitionKeyAndCancellationToken_Projects()
    {
        // Arrange
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var projections = Fixture.CreateMany<TestProjection>().ToList();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        var (context, cursor) = SetupProjection(projections, collection, partitionKey);

        // Act
        var result = Sut.ProjectOne<TestDocument, TestProjection, Guid>(filter, projectionExpression, partitionKey, token);

        // Assert
        context.Verify(x => x.GetCollection<TestDocument>(partitionKey), Times.Once);
        cursor.Verify(x => x.Current, Times.Once);
        cursor.Verify(x => x.MoveNext(token), Times.Once);
        result.Should().NotBeNull();
        result.Should().Be(projections[0]);
    }

    private (Mock<IMongoDbContext>, Mock<IAsyncCursor<TProjection>>) SetupProjection<TDocument, TProjection>(
        List<TProjection> projections,
        Mock<IMongoCollection<TDocument>> collection,
        string partitionKey = null)
    {
        var asyncCursor = SetupSyncCursor(projections);

        SetupFindSync(collection, asyncCursor);

        var context = MockOf<IMongoDbContext>();

        context
            .Setup(x => x.GetCollection<TDocument>(partitionKey))
            .Returns(collection.Object);

        return (context, asyncCursor);
    }
}
