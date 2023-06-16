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

namespace CoreUnitTests.KeyTypedRepositoryTests.DeleteTests;

public class DeleteOneAsyncTests : TestKeyedMongoRepositoryContext<int>
{
    [Fact]
    public async Task WithDocument_ShouldDeleteOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var count = Fixture.Create<long>();
        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteOneAsync<TestDocumentWithKey<int>, int>(It.IsAny<TestDocumentWithKey<int>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.DeleteOneAsync(document);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOneAsync<TestDocumentWithKey<int>, int>(document, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task WithDocumentAndCancellationToken_ShouldDeleteOne()
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
        var result = await Sut.DeleteOneAsync(document, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOneAsync<TestDocumentWithKey<int>, int>(document, token), Times.Once);
    }

    [Fact]
    public async Task WithFilter_ShouldDeleteOne()
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
        var result = await Sut.DeleteOneAsync(filter);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOneAsync<TestDocumentWithKey<int>, int>(filter, null, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task WithFilterAndCancellationToken_ShouldDeleteOne()
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
        var result = await Sut.DeleteOneAsync(filter, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOneAsync<TestDocumentWithKey<int>, int>(filter, null, token), Times.Once);
    }

    [Fact]
    public async Task WithFilterAndPartitionKey_ShouldDeleteOne()
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
        var result = await Sut.DeleteOneAsync(filter, partitionKey);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOneAsync<TestDocumentWithKey<int>, int>(filter, partitionKey, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task WithFilterAndPartitionKeyAndCancellationToken_ShouldDeleteOne()
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
        var result = await Sut.DeleteOneAsync(filter, partitionKey, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteOneAsync<TestDocumentWithKey<int>, int>(filter, partitionKey, token), Times.Once);
    }
}