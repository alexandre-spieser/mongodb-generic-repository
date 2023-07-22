using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using FluentAssertions;
using MongoDB.Driver;
using MongoDbGenericRepository.DataAccess.Update;
using Moq;
using Xunit;

namespace CoreUnitTests.KeyTypedRepositoryTests.UpdateTests;

public class UpdateOneAsyncTests : TestKeyedMongoRepositoryContext<int>
{
    private readonly UpdateDefinition<TestDocumentWithKey<int>> updateDefinition = Builders<TestDocumentWithKey<int>>.Update.Set(x => x.SomeContent, "Updated");
    private readonly Expression<Func<TestDocumentWithKey<int>, string>> fieldExpression = x => x.SomeContent;
    private readonly FilterDefinition<TestDocumentWithKey<int>> filterDefinition = Builders<TestDocumentWithKey<int>>.Filter.Eq(x => x.Id, 1);
    private readonly Expression<Func<TestDocumentWithKey<int>, bool>> filterExpression = x => x.SomeContent == "SomeContent";

    [Fact]
    public async Task WithDocument_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(x => x.UpdateOneAsync<TestDocumentWithKey<int>, int>(It.IsAny<TestDocumentWithKey<int>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(document);

        // Assert
        Updater.Verify(x => x.UpdateOneAsync<TestDocumentWithKey<int>, int>(document, CancellationToken.None), Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task WithDocumentAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(x => x.UpdateOneAsync<TestDocumentWithKey<int>, int>(It.IsAny<TestDocumentWithKey<int>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(document, token);

        // Assert
        Updater.Verify(x => x.UpdateOneAsync<TestDocumentWithKey<int>, int>(document, token), Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task WithDocumentAndUpdateDefinition_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocumentWithKey<int>, int>(
                    It.IsAny<TestDocumentWithKey<int>>(),
                    It.IsAny<UpdateDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(document, updateDefinition);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int>(
                document,
                updateDefinition,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task WithDocumentAndUpdateDefinitionAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocumentWithKey<int>, int>(
                    It.IsAny<TestDocumentWithKey<int>>(),
                    It.IsAny<UpdateDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(document, updateDefinition, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int>(
                document,
                updateDefinition,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task WithDocumentAndFieldExpressionAndValue_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var value = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<TestDocumentWithKey<int>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(document, fieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                document,
                fieldExpression,
                value,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task WithDocumentAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var value = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<TestDocumentWithKey<int>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(document, fieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                document,
                fieldExpression,
                value,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task WithFilterDefinitionAndFieldExpressionAndValue_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(filterDefinition, fieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                filterDefinition,
                fieldExpression,
                value,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task WithFilterDefinitionAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(filterDefinition, fieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                filterDefinition,
                fieldExpression,
                value,
                null,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task WithFilterDefinitionAndFieldExpressionAndValueAndPartitionKey_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(filterDefinition, fieldExpression, value, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                filterDefinition,
                fieldExpression,
                value,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task WithFilterDefinitionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(filterDefinition, fieldExpression, value, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                filterDefinition,
                fieldExpression,
                value,
                partitionKey,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task WithFilterExpressionAndFieldExpressionAndValue_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();

        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(filterExpression, fieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                filterExpression,
                fieldExpression,
                value,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task WithFilterExpressionAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(filterExpression, fieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                filterExpression,
                fieldExpression,
                value,
                null,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task WithFilterExpressionAndFieldExpressionAndValueAndPartitionKey_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(filterExpression, fieldExpression, value, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                filterExpression,
                fieldExpression,
                value,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task WithFilterExpressionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(filterExpression, fieldExpression, value, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                filterExpression,
                fieldExpression,
                value,
                partitionKey,
                token),
            Times.Once);
        result.Should().BeTrue();
    }
}
