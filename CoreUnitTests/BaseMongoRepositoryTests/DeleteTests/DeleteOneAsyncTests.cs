using System;
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

public class DeleteOneAsyncTests : TestMongoRepositoryContext
{
    [Fact]
    public async Task WithDocument_ShouldDeleteOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var count = Fixture.Create<long>();
        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteOneAsync<TestDocument, Guid>(It.IsAny<TestDocument>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteOneAsync(document);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOneAsync<TestDocument, Guid>(document, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task WithDocumentAndCancellationToken_ShouldDeleteOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var count = Fixture.Create<long>();
        var token = new CancellationToken();

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteOneAsync<TestDocument, Guid>(It.IsAny<TestDocument>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteOneAsync(document, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOneAsync<TestDocument, Guid>(document, token), Times.Once);
    }

    [Fact]
    public async Task WithFilter_ShouldDeleteOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var content = Fixture.Create<string>();

        Expression<Func<TestDocument, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteOneAsync<TestDocument, Guid>(It.IsAny<Expression<Func<TestDocument, bool>>>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteOneAsync(filter);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOneAsync<TestDocument, Guid>(filter, null, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task WithFilterAndCancellationToken_ShouldDeleteOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var content = Fixture.Create<string>();
        var token = new CancellationToken();

        Expression<Func<TestDocument, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteOneAsync<TestDocument, Guid>(It.IsAny<Expression<Func<TestDocument, bool>>>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteOneAsync(filter, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOneAsync<TestDocument, Guid>(filter, null, token), Times.Once);
    }

    [Fact]
    public async Task WithFilterAndPartitionKey_ShouldDeleteOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var content = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();

        Expression<Func<TestDocument, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteOneAsync<TestDocument, Guid>(It.IsAny<Expression<Func<TestDocument, bool>>>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteOneAsync(filter, partitionKey);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOneAsync<TestDocument, Guid>(filter, partitionKey, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task WithFilterAndPartitionKeyAndCancellationToken_ShouldDeleteOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var content = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken();

        Expression<Func<TestDocument, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteOneAsync<TestDocument, Guid>(It.IsAny<Expression<Func<TestDocument, bool>>>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteOneAsync(filter, partitionKey, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOneAsync<TestDocument, Guid>(filter, partitionKey, token), Times.Once);
    }

    #region Keyed

    [Fact]
    public async Task Keyed_WithDocument_ShouldDeleteOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var count = Fixture.Create<long>();
        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteOneAsync<TestDocumentWithKey<int>, int>(It.IsAny<TestDocumentWithKey<int>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteOneAsync<TestDocumentWithKey<int>, int>(document);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOneAsync<TestDocumentWithKey<int>, int>(document, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Keyed_WithDocumentAndCancellationToken_ShouldDeleteOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var count = Fixture.Create<long>();
        var token = new CancellationToken();

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteOneAsync<TestDocumentWithKey<int>, int>(It.IsAny<TestDocumentWithKey<int>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteOneAsync<TestDocumentWithKey<int>, int>(document, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOneAsync<TestDocumentWithKey<int>, int>(document, token), Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilter_ShouldDeleteOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var content = Fixture.Create<string>();

        Expression<Func<TestDocumentWithKey<int>, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteOneAsync<TestDocumentWithKey<int>, int>(It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteOneAsync<TestDocumentWithKey<int>, int>(filter);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOneAsync<TestDocumentWithKey<int>, int>(filter, null, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndCancellationToken_ShouldDeleteOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var content = Fixture.Create<string>();
        var token = new CancellationToken();

        Expression<Func<TestDocumentWithKey<int>, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteOneAsync<TestDocumentWithKey<int>, int>(It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteOneAsync<TestDocumentWithKey<int>, int>(filter, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOneAsync<TestDocumentWithKey<int>, int>(filter, null, token), Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndPartitionKey_ShouldDeleteOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var content = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();

        Expression<Func<TestDocumentWithKey<int>, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteOneAsync<TestDocumentWithKey<int>, int>(It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteOneAsync<TestDocumentWithKey<int>, int>(filter, partitionKey);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOneAsync<TestDocumentWithKey<int>, int>(filter, partitionKey, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndPartitionKeyAndCancellationToken_ShouldDeleteOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var content = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken();

        Expression<Func<TestDocumentWithKey<int>, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteOneAsync<TestDocumentWithKey<int>, int>(It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteOneAsync<TestDocumentWithKey<int>, int>(filter, partitionKey, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOneAsync<TestDocumentWithKey<int>, int>(filter, partitionKey, token), Times.Once);
    }

    #endregion
}