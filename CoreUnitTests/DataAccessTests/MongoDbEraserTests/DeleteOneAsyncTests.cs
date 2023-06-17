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
using MongoDbGenericRepository.DataAccess.Delete;
using Moq;
using Xunit;

namespace CoreUnitTests.DataAccessTests.MongoDbEraserTests;

public class DeleteOneAsyncTests : GenericTestContext<MongoDbEraser>
{
    [Fact]
    public async Task WithDocumentAndCancellationToken_DeletesOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var document = Fixture.Create<TestDocument>();
        var token = new CancellationToken(true);

        var collection = MockOf<IMongoCollection<TestDocument>>();
        collection
            .Setup(x => x.DeleteOneAsync(It.IsAny<FilterDefinition<TestDocument>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new DeleteResult.Acknowledged(count));

        var dbContext = MockOf<IMongoDbContext>();
        dbContext
            .Setup(x => x.GetCollection<TestDocument>(null))
            .Returns(collection.Object);

        // Act
        var result = await Sut.DeleteOneAsync<TestDocument, Guid>(document, token);

        // Assert
        result.Should().Be(count);

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        collection.Verify(
            x => x.DeleteOneAsync(
                It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                token),
            Times.Once());
    }

    [Fact]
    public async Task WithFilterAndPartitionKeyAndCancellationToken_DeletesOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var document = Fixture.Create<TestDocument>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        var collection = MockOf<IMongoCollection<TestDocument>>();
        collection
            .Setup(x => x.DeleteOneAsync(It.IsAny<FilterDefinition<TestDocument>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new DeleteResult.Acknowledged(count));

        var dbContext = MockOf<IMongoDbContext>();
        dbContext
            .Setup(x => x.GetCollection<TestDocument>(It.IsAny<string>()))
            .Returns(collection.Object);

        Expression<Func<TestDocument, bool>> filter = d => d.Id == document.Id;

        // Act
        var result = await Sut.DeleteOneAsync<TestDocument, Guid>(filter, partitionKey, token);

        // Assert
        result.Should().Be(count);
        collection.Verify(
            x => x.DeleteOneAsync(
                It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(filter)),
                token),
            Times.Once());

        dbContext.Verify(x => x.GetCollection<TestDocument>(partitionKey));
    }
}
