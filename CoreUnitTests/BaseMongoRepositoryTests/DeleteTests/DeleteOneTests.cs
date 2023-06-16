namespace CoreUnitTests.BaseMongoRepositoryTests.DeleteTests;

using System;
using System.Linq.Expressions;
using System.Threading;
using AutoFixture;
using FluentAssertions;
using Infrastructure;
using Infrastructure.Model;
using MongoDbGenericRepository.DataAccess.Delete;
using Moq;
using Xunit;

public class DeleteOneTests : TestMongoRepositoryContext
{
    [Fact]
    public void WithDocument_ShouldDeleteOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var count = Fixture.Create<long>();
        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteOne<TestDocument, Guid>(It.IsAny<TestDocument>(), It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.DeleteOne(document);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOne<TestDocument, Guid>(document, CancellationToken.None), Times.Once);
    }

    [Fact]
    public void WithDocumentAndCancellationToken_ShouldDeleteOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var count = Fixture.Create<long>();
        var token = new CancellationToken(true);
        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteOne<TestDocument, Guid>(It.IsAny<TestDocument>(), It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.DeleteOne(document, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOne<TestDocument, Guid>(document, token), Times.Once);
    }

    [Fact]
    public void WithFilter_ShouldDeleteOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var content = Fixture.Create<string>();

        Expression<Func<TestDocument, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteOne<TestDocument, Guid>(It.IsAny<Expression<Func<TestDocument, bool>>>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.DeleteOne(filter);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOne<TestDocument, Guid>(filter, null, CancellationToken.None), Times.Once);
    }

    [Fact]
    public void WithFilterAndCancellationToken_ShouldDeleteOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var content = Fixture.Create<string>();
        var token = new CancellationToken(true);

        Expression<Func<TestDocument, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(
                x => x.DeleteOne<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.DeleteOne(filter, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOne<TestDocument, Guid>(filter, null, token), Times.Once);
    }

    [Fact]
    public void WithFilterAndPartitionKey_ShouldDeleteOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var content = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();

        Expression<Func<TestDocument, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(
                x => x.DeleteOne<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.DeleteOne(filter, partitionKey);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOne<TestDocument, Guid>(filter, partitionKey, CancellationToken.None), Times.Once);
    }

    [Fact]
    public void WithFilterAndPartitionKeyAndCancellationToken_ShouldDeleteOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var content = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        Expression<Func<TestDocument, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(
                x => x.DeleteOne<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.DeleteOne(filter, partitionKey, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOne<TestDocument, Guid>(filter, partitionKey, token), Times.Once);
    }

    [Fact]
    public void WithKeyedDocument_ShouldDeleteOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var count = Fixture.Create<long>();
        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteOne<TestDocumentWithKey<int>, int>(It.IsAny<TestDocumentWithKey<int>>(), It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.DeleteOne<TestDocumentWithKey<int>, int>(document);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOne<TestDocumentWithKey<int>, int>(document, CancellationToken.None), Times.Once);
    }

    [Fact]
    public void WithKeyedDocumentAndCancellationToken_ShouldDeleteOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var count = Fixture.Create<long>();
        var token = new CancellationToken(true);
        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteOne<TestDocumentWithKey<int>, int>(It.IsAny<TestDocumentWithKey<int>>(), It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.DeleteOne<TestDocumentWithKey<int>, int>(document, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOne<TestDocumentWithKey<int>, int>(document, token), Times.Once);
    }

    [Fact]
    public void Keyed_WithFilter_ShouldDeleteOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var content = Fixture.Create<string>();

        Expression<Func<TestDocumentWithKey<int>, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(
                x => x.DeleteOne<TestDocumentWithKey<int>, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.DeleteOne<TestDocumentWithKey<int>, int>(filter);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOne<TestDocumentWithKey<int>, int>(filter, null, CancellationToken.None), Times.Once);
    }

    [Fact]
    public void Keyed_WithFilterAndCancellationToken_ShouldDeleteOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var content = Fixture.Create<string>();
        var token = new CancellationToken(true);

        Expression<Func<TestDocumentWithKey<int>, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(
                x => x.DeleteOne<TestDocumentWithKey<int>, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.DeleteOne<TestDocumentWithKey<int>, int>(filter, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOne<TestDocumentWithKey<int>, int>(filter, null, token), Times.Once);
    }

    [Fact]
    public void Keyed_WithFilterAndPartitionKey_ShouldDeleteOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var content = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();

        Expression<Func<TestDocumentWithKey<int>, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(
                x => x.DeleteOne<TestDocumentWithKey<int>, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.DeleteOne<TestDocumentWithKey<int>, int>(filter, partitionKey);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOne<TestDocumentWithKey<int>, int>(filter, partitionKey, CancellationToken.None), Times.Once);
    }

    [Fact]
    public void Keyed_WithFilterAndPartitionKeyAndCancellationToken_ShouldDeleteOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var content = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        Expression<Func<TestDocumentWithKey<int>, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(
                x => x.DeleteOne<TestDocumentWithKey<int>, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.DeleteOne<TestDocumentWithKey<int>, int>(filter, partitionKey, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOne<TestDocumentWithKey<int>, int>(filter, partitionKey, token), Times.Once);
    }
}
