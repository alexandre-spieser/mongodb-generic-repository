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
using MongoDbGenericRepository.DataAccess.Delete;
using Moq;
using Xunit;

namespace CoreUnitTests.BaseMongoRepositoryTests.DeleteTests;

public class DeleteManyAsyncTests : TestMongoRepositoryContext
{
    [Fact]
    public async Task WithDocuments_ShouldDeleteMany()
    {
        // Arrange
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var count = Fixture.Create<long>();
        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteManyAsync<TestDocument, Guid>(It.IsAny<IEnumerable<TestDocument>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteManyAsync(documents);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteManyAsync<TestDocument, Guid>(documents, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task WithDocumentsAndCancellationToken_ShouldDeleteMany()
    {
        // Arrange
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var count = Fixture.Create<long>();
        var cancellationToken = new CancellationToken(true);
        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteManyAsync<TestDocument, Guid>(It.IsAny<IEnumerable<TestDocument>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteManyAsync(documents, cancellationToken);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteManyAsync<TestDocument, Guid>(documents, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task WithFilter_ShouldDeleteMany()
    {
        // Arrange
        var content = Fixture.Create<string>();
        var count = Fixture.Create<long>();

        Expression<Func<TestDocument, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteManyAsync<TestDocument, Guid>(
                It.IsAny<Expression<Func<TestDocument, bool>>>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteManyAsync(filter);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteManyAsync<TestDocument, Guid>(filter, null, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task WithFilterAndCancellationToken_ShouldDeleteMany()
    {
        // Arrange
        var content = Fixture.Create<string>();
        var count = Fixture.Create<long>();
        var token = new CancellationToken(true);

        Expression<Func<TestDocument, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteManyAsync<TestDocument, Guid>(
                It.IsAny<Expression<Func<TestDocument, bool>>>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteManyAsync(filter, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteManyAsync<TestDocument, Guid>(filter, null, token), Times.Once);
    }

    [Fact]
    public async Task WithFilterAndPartitionKey_ShouldDeleteMany()
    {
        // Arrange
        var content = Fixture.Create<string>();
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();

        Expression<Func<TestDocument, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteManyAsync<TestDocument, Guid>(
                It.IsAny<Expression<Func<TestDocument, bool>>>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteManyAsync(filter, partitionKey);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteManyAsync<TestDocument, Guid>(filter, partitionKey, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task WithFilterAndPartitionKeyAndCancellationToken_ShouldDeleteMany()
    {
        // Arrange
        var content = Fixture.Create<string>();
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        Expression<Func<TestDocument, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteManyAsync<TestDocument, Guid>(
                It.IsAny<Expression<Func<TestDocument, bool>>>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteManyAsync(filter, partitionKey, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteManyAsync<TestDocument, Guid>(filter, partitionKey, token), Times.Once);
    }

    #region Keyed

    [Fact]
    public async Task Keyed_WithDocuments_ShouldDeleteMany()
    {
        // Arrange
        var documents = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        var count = Fixture.Create<long>();
        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteManyAsync<TestDocumentWithKey<int>, int>(It.IsAny<IEnumerable<TestDocumentWithKey<int>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteManyAsync<TestDocumentWithKey<int>, int>(documents);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteManyAsync<TestDocumentWithKey<int>, int>(documents, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Keyed_WithDocumentsAndCancellationToken_ShouldDeleteMany()
    {
        // Arrange
        var documents = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        var count = Fixture.Create<long>();
        var cancellationToken = new CancellationToken(true);
        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteManyAsync<TestDocumentWithKey<int>, int>(It.IsAny<IEnumerable<TestDocumentWithKey<int>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteManyAsync<TestDocumentWithKey<int>, int>(documents, cancellationToken);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteManyAsync<TestDocumentWithKey<int>, int>(documents, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilter_ShouldDeleteMany()
    {
        // Arrange
        var content = Fixture.Create<string>();
        var count = Fixture.Create<long>();

        Expression<Func<TestDocumentWithKey<int>, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteManyAsync<TestDocumentWithKey<int>, int>(
                It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteManyAsync<TestDocumentWithKey<int>, int>(filter);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteManyAsync<TestDocumentWithKey<int>, int>(filter, null, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndCancellationToken_ShouldDeleteMany()
    {
        // Arrange
        var content = Fixture.Create<string>();
        var count = Fixture.Create<long>();
        var token = new CancellationToken(true);

        Expression<Func<TestDocumentWithKey<int>, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteManyAsync<TestDocumentWithKey<int>, int>(
                It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteManyAsync<TestDocumentWithKey<int>, int>(filter, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteManyAsync<TestDocumentWithKey<int>, int>(filter, null, token), Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndPartitionKey_ShouldDeleteMany()
    {
        // Arrange
        var content = Fixture.Create<string>();
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();

        Expression<Func<TestDocumentWithKey<int>, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteManyAsync<TestDocumentWithKey<int>, int>(
                It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteManyAsync<TestDocumentWithKey<int>, int>(filter, partitionKey);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteManyAsync<TestDocumentWithKey<int>, int>(filter, partitionKey, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndPartitionKeyAndCancellationToken_ShouldDeleteMany()
    {
        // Arrange
        var content = Fixture.Create<string>();
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        Expression<Func<TestDocumentWithKey<int>, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteManyAsync<TestDocumentWithKey<int>, int>(
                It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteManyAsync<TestDocumentWithKey<int>, int>(filter, partitionKey, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteManyAsync<TestDocumentWithKey<int>, int>(filter, partitionKey, token), Times.Once);
    }

    #endregion
}
