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

public class UpdateManyAsyncTests : TestKeyedMongoRepositoryContext<int>
{
    private readonly UpdateDefinition<TestDocumentWithKey<int>> updateDefinition = Builders<TestDocumentWithKey<int>>.Update.Set(x => x.SomeContent, "Updated");
    private readonly Expression<Func<TestDocumentWithKey<int>, string>> fieldExpression = x => x.SomeContent;
    private readonly FilterDefinition<TestDocumentWithKey<int>> filterDefinition = Builders<TestDocumentWithKey<int>>.Filter.Eq(x => x.Id, 1);
    private readonly Expression<Func<TestDocumentWithKey<int>, bool>> filterExpression = x => x.SomeContent == "SomeContent";

    [Fact]
    public async Task WithFilterDefinitionAndFieldExpressionAndValue_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var count = Fixture.Create<long>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterDefinition, fieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(
                filterDefinition,
                fieldExpression,
                value,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task WithFilterDefinitionAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterDefinition, fieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(
                filterDefinition,
                fieldExpression,
                value,
                null,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task WithFilterDefinitionAndFieldExpressionAndValueAndPartitionKey_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterDefinition, fieldExpression, value, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(
                filterDefinition,
                fieldExpression,
                value,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task WithFilterDefinitionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterDefinition, fieldExpression, value, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(
                filterDefinition,
                fieldExpression,
                value,
                partitionKey,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task WithFilterExpressionAndFieldExpressionAndValue_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();

        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterExpression, fieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(
                filterExpression,
                fieldExpression,
                value,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task WithFilterExpressionAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterExpression, fieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(
                filterExpression,
                fieldExpression,
                value,
                null,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task WithFilterExpressionAndFieldExpressionAndValueAndPartitionKey_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterExpression, fieldExpression, value, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(
                filterExpression,
                fieldExpression,
                value,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task WithFilterExpressionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterExpression, fieldExpression, value, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(
                filterExpression,
                fieldExpression,
                value,
                partitionKey,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task WithFilterExpressionAndUpdateDefinition_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();

        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateManyAsync<TestDocumentWithKey<int>, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<UpdateDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterExpression, updateDefinition);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int>(
                filterExpression,
                updateDefinition,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task WithFilterExpressionAndUpdateDefinitionAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();

        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateManyAsync<TestDocumentWithKey<int>, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<UpdateDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterExpression, updateDefinition, token);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int>(
                filterExpression,
                updateDefinition,
                null,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task WithFilterExpressionAndUpdateDefinitionAndPartitionKey_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();

        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateManyAsync<TestDocumentWithKey<int>, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<UpdateDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterExpression, updateDefinition, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int>(
                filterExpression,
                updateDefinition,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task WithFilterExpressionAndUpdateDefinitionAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateManyAsync<TestDocumentWithKey<int>, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<UpdateDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterExpression, updateDefinition, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int>(
                filterExpression,
                updateDefinition,
                partitionKey,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task WithFilterDefinitionAndUpdateDefinition_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();

        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateManyAsync<TestDocumentWithKey<int>, int>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<UpdateDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterDefinition, updateDefinition);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int>(
                filterDefinition,
                updateDefinition,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task WithFilterDefinitionAndUpdateDefinitionCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var token = new CancellationToken(true);

        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateManyAsync<TestDocumentWithKey<int>, int>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<UpdateDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterDefinition, updateDefinition, token);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int>(
                filterDefinition,
                updateDefinition,
                null,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task WithFilterDefinitionAndUpdateDefinitionAndPartitionKey_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();

        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateManyAsync<TestDocumentWithKey<int>, int>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<UpdateDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterDefinition, updateDefinition, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int>(
                filterDefinition,
                updateDefinition,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task WithFilterDefinitionAndUpdateDefinitionAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateManyAsync<TestDocumentWithKey<int>, int>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<UpdateDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterDefinition, updateDefinition, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int>(
                filterDefinition,
                updateDefinition,
                partitionKey,
                token),
            Times.Once);
        result.Should().Be(count);
    }
}
