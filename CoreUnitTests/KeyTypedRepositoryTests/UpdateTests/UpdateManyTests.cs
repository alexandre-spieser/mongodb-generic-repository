using System;
using System.Linq.Expressions;
using System.Threading;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using FluentAssertions;
using MongoDB.Driver;
using MongoDbGenericRepository.DataAccess.Update;
using Moq;
using Xunit;

namespace CoreUnitTests.KeyTypedRepositoryTests.UpdateTests;

public class UpdateManyTests : TestKeyedMongoRepositoryContext<int>
{
    private readonly Expression<Func<TestDocumentWithKey<int>, string>> fieldExpression = x => x.SomeContent;
    private readonly FilterDefinition<TestDocumentWithKey<int>> filterDefinition = Builders<TestDocumentWithKey<int>>.Filter.Eq(x => x.Id, 1);
    private readonly Expression<Func<TestDocumentWithKey<int>, bool>> filterExpression = x => x.SomeContent == "SomeContent";
    private readonly UpdateDefinition<TestDocumentWithKey<int>> updateDefinition = Builders<TestDocumentWithKey<int>>.Update.Set(x => x.SomeContent, "Updated");

    [Fact]
    public void WithFilterDefinitionAndFieldExpressionAndValue_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var count = Fixture.Create<long>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateMany<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterDefinition, fieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int, string>(
                filterDefinition,
                fieldExpression,
                value,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void WithFilterDefinitionAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateMany<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterDefinition, fieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int, string>(
                filterDefinition,
                fieldExpression,
                value,
                null,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void WithFilterDefinitionAndFieldExpressionAndValueAndPartitionKey_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateMany<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterDefinition, fieldExpression, value, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int, string>(
                filterDefinition,
                fieldExpression,
                value,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void WithFilterDefinitionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateMany<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterDefinition, fieldExpression, value, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int, string>(
                filterDefinition,
                fieldExpression,
                value,
                partitionKey,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void WithFilterExpressionAndFieldExpressionAndValue_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();

        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateMany<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterExpression, fieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int, string>(
                filterExpression,
                fieldExpression,
                value,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void WithFilterExpressionAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateMany<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterExpression, fieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int, string>(
                filterExpression,
                fieldExpression,
                value,
                null,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void WithFilterExpressionAndFieldExpressionAndValueAndPartitionKey_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateMany<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterExpression, fieldExpression, value, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int, string>(
                filterExpression,
                fieldExpression,
                value,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void WithFilterExpressionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateMany<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterExpression, fieldExpression, value, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int, string>(
                filterExpression,
                fieldExpression,
                value,
                partitionKey,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void WithFilterExpressionAndUpdateDefinition_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();

        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateMany<TestDocumentWithKey<int>, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<UpdateDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterExpression, updateDefinition);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int>(
                filterExpression,
                updateDefinition,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void WithFilterExpressionAndUpdateDefinitionAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();

        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateMany<TestDocumentWithKey<int>, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<UpdateDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterExpression, updateDefinition, token);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int>(
                filterExpression,
                updateDefinition,
                null,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void WithFilterExpressionAndUpdateDefinitionAndPartitionKey_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();

        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateMany<TestDocumentWithKey<int>, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<UpdateDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterExpression, updateDefinition, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int>(
                filterExpression,
                updateDefinition,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void WithFilterExpressionAndUpdateDefinitionAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateMany<TestDocumentWithKey<int>, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<UpdateDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterExpression, updateDefinition, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int>(
                filterExpression,
                updateDefinition,
                partitionKey,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void WithFilterDefinitionAndUpdateDefinition_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();

        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateMany<TestDocumentWithKey<int>, int>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<UpdateDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterDefinition, updateDefinition);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int>(
                filterDefinition,
                updateDefinition,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void WithFilterDefinitionAndUpdateDefinitionCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var token = new CancellationToken(true);

        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateMany<TestDocumentWithKey<int>, int>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<UpdateDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterDefinition, updateDefinition, token);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int>(
                filterDefinition,
                updateDefinition,
                null,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void WithFilterDefinitionAndUpdateDefinitionAndPartitionKey_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();

        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateMany<TestDocumentWithKey<int>, int>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<UpdateDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterDefinition, updateDefinition, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int>(
                filterDefinition,
                updateDefinition,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void WithFilterDefinitionAndUpdateDefinitionAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateMany<TestDocumentWithKey<int>, int>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<UpdateDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterDefinition, updateDefinition, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int>(
                filterDefinition,
                updateDefinition,
                partitionKey,
                token),
            Times.Once);
        result.Should().Be(count);
    }
}
