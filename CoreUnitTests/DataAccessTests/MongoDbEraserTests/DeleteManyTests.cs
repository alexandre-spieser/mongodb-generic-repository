using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using FluentAssertions;
using MongoDB.Driver;
using MongoDbGenericRepository;
using MongoDbGenericRepository.DataAccess.Delete;
using Moq;
using Xunit;

namespace CoreUnitTests.DataAccessTests.MongoDbEraserTests;

public class DeleteManyTests : GenericTestContext<MongoDbEraser>
{
    [Fact]
    public void WithEmptyDocuments_DeletesNothing()
    {
        // Arrange
        var documents = new List<TestDocument>(0);
        var token = new CancellationToken(true);

        var collection = MockOf<IMongoCollection<TestDocument>>();

        var dbContext = MockOf<IMongoDbContext>();
        dbContext
            .Setup(x => x.GetCollection<TestDocument>(null))
            .Returns(collection.Object);

        // Act
        var result = Sut.DeleteMany<TestDocument, Guid>(documents, token);

        // Assert
        result.Should().Be(0);

        var idsToDelete = documents.Select(e => e.Id).ToArray();
        Expression<Func<TestDocument, bool>> expectedFilter = x => idsToDelete.Contains(x.Id);
        collection.Verify(
            x => x.DeleteMany(
                It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                token),
            Times.Never);
    }

    [Fact]
    public void WithDocumentsAndCancellationToken_DeletesMany()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var token = new CancellationToken(true);

        var collection = MockOf<IMongoCollection<TestDocument>>();
        collection
            .Setup(x => x.DeleteMany(It.IsAny<FilterDefinition<TestDocument>>(), It.IsAny<CancellationToken>()))
            .Returns(new DeleteResult.Acknowledged(count));

        var dbContext = MockOf<IMongoDbContext>();
        dbContext
            .Setup(x => x.GetCollection<TestDocument>(null))
            .Returns(collection.Object);

        // Act
        var result = Sut.DeleteMany<TestDocument, Guid>(documents, token);

        // Assert
        result.Should().Be(count);

        var idsToDelete = documents.Select(e => e.Id).ToArray();
        Expression<Func<TestDocument, bool>> expectedFilter = x => idsToDelete.Contains(x.Id);
        collection.Verify(
            x => x.DeleteMany(
                It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                token),
            Times.Once);
    }

    [Fact]
    public void WithPartitionDocumentsAndCancellationToken_DeletesMany()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();
        var documents = Fixture
            .Build<PartitionedTestDocument>()
            .With(x => x.PartitionKey, partitionKey)
            .CreateMany()
            .ToList();
        var token = new CancellationToken(true);

        var collection = MockOf<IMongoCollection<PartitionedTestDocument>>();
        collection
            .Setup(x => x.DeleteMany(It.IsAny<FilterDefinition<PartitionedTestDocument>>(), It.IsAny<CancellationToken>()))
            .Returns(new DeleteResult.Acknowledged(count));

        var dbContext = MockOf<IMongoDbContext>();
        dbContext
            .Setup(x => x.GetCollection<PartitionedTestDocument>(It.IsAny<string>()))
            .Returns(collection.Object);

        // Act
        var result = Sut.DeleteMany<PartitionedTestDocument, Guid>(documents, token);

        // Assert
        result.Should().Be(count);

        var idsToDelete = documents.Select(e => e.Id).ToArray();
        Expression<Func<PartitionedTestDocument, bool>> expectedFilter = x => idsToDelete.Contains(x.Id);
        collection.Verify(
            x => x.DeleteMany(
                It.Is<FilterDefinition<PartitionedTestDocument>>(f => f.EquivalentTo(expectedFilter)),
                token),
            Times.Once);

        dbContext.Verify(x => x.GetCollection<PartitionedTestDocument>(partitionKey), Times.Once);
    }

    [Fact]
    public void WithFilterAndPartitionKeyAndCancellationToken_DeletesOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var id = Fixture.Create<Guid>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        var collection = MockOf<IMongoCollection<TestDocument>>();
        collection
            .Setup(x => x.DeleteMany(It.IsAny<FilterDefinition<TestDocument>>(), It.IsAny<CancellationToken>()))
            .Returns(new DeleteResult.Acknowledged(count));

        var dbContext = MockOf<IMongoDbContext>();
        dbContext
            .Setup(x => x.GetCollection<TestDocument>(It.IsAny<string>()))
            .Returns(collection.Object);

        Expression<Func<TestDocument, bool>> filter = d => d.Id == id;

        // Act
        var result = Sut.DeleteMany<TestDocument, Guid>(filter, partitionKey, token);

        // Assert
        result.Should().Be(count);
        collection.Verify(
            x => x.DeleteMany(
                It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(filter)),
                token),
            Times.Once);

        dbContext.Verify(x => x.GetCollection<TestDocument>(partitionKey), Times.Once);
    }
}
