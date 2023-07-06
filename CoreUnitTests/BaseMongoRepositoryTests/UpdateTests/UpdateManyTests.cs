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

namespace CoreUnitTests.BaseMongoRepositoryTests.UpdateTests;

public class UpdateManyTests : TestMongoRepositoryContext
{

    private readonly Expression<Func<TestDocument, string>> fieldExpression = x => x.SomeContent;
    private readonly FilterDefinition<TestDocument> filterDefinition = Builders<TestDocument>.Filter.Eq(x => x.SomeContent, "SomeContent");
    private readonly Expression<Func<TestDocument, bool>> filterExpression = x => x.SomeContent == "SomeContent";
    private readonly UpdateDefinition<TestDocument> updateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, "Updated");

    [Fact]
    public void WithFilterDefinitionAndFieldExpressionAndValue_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var count = Fixture.Create<long>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateMany<TestDocument, Guid, string>(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterDefinition, fieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocument, Guid, string>(
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
                x => x.UpdateMany<TestDocument, Guid, string>(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterDefinition, fieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocument, Guid, string>(
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
                x => x.UpdateMany<TestDocument, Guid, string>(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterDefinition, fieldExpression, value, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocument, Guid, string>(
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
                x => x.UpdateMany<TestDocument, Guid, string>(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterDefinition, fieldExpression, value, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocument, Guid, string>(
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
                x => x.UpdateMany<TestDocument, Guid, string>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterExpression, fieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocument, Guid, string>(
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
                x => x.UpdateMany<TestDocument, Guid, string>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterExpression, fieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocument, Guid, string>(
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
                x => x.UpdateMany<TestDocument, Guid, string>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterExpression, fieldExpression, value, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocument, Guid, string>(
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
                x => x.UpdateMany<TestDocument, Guid, string>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterExpression, fieldExpression, value, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocument, Guid, string>(
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
                x => x.UpdateMany<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterExpression, updateDefinition);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocument, Guid>(
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
                x => x.UpdateMany<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterExpression, updateDefinition, token);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocument, Guid>(
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
                x => x.UpdateMany<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterExpression, updateDefinition, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocument, Guid>(
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
                x => x.UpdateMany<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterExpression, updateDefinition, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocument, Guid>(
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
                x => x.UpdateMany<TestDocument, Guid>(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterDefinition, updateDefinition);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocument, Guid>(
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
                x => x.UpdateMany<TestDocument, Guid>(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterDefinition, updateDefinition, token);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocument, Guid>(
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
                x => x.UpdateMany<TestDocument, Guid>(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterDefinition, updateDefinition, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocument, Guid>(
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
                x => x.UpdateMany<TestDocument, Guid>(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.UpdateMany(filterDefinition, updateDefinition, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocument, Guid>(
                filterDefinition,
                updateDefinition,
                partitionKey,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    #region Keyed

    private readonly Expression<Func<TestDocumentWithKey<int>, string>> keyedFieldExpression = x => x.SomeContent;
    private readonly FilterDefinition<TestDocumentWithKey<int>> keyedFilterDefinition = Builders<TestDocumentWithKey<int>>.Filter.Eq(x => x.Id, 1);
    private readonly Expression<Func<TestDocumentWithKey<int>, bool>> keyedFilterExpression = x => x.SomeContent == "SomeContent";
    private readonly UpdateDefinition<TestDocumentWithKey<int>> keyedUpdateDefinition = Builders<TestDocumentWithKey<int>>.Update.Set(x => x.SomeContent, "Updated");

    [Fact]
    public void Keyed_WithFilterDefinitionAndFieldExpressionAndValue_ShouldUpdateOne()
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
        var result = Sut.UpdateMany<TestDocumentWithKey<int>, int, string>(keyedFilterDefinition, keyedFieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int, string>(
                keyedFilterDefinition,
                keyedFieldExpression,
                value,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void Keyed_WithFilterDefinitionAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
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
        var result = Sut.UpdateMany<TestDocumentWithKey<int>, int, string>(keyedFilterDefinition, keyedFieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int, string>(
                keyedFilterDefinition,
                keyedFieldExpression,
                value,
                null,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void Keyed_WithFilterDefinitionAndFieldExpressionAndValueAndPartitionKey_ShouldUpdateOne()
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
        var result = Sut.UpdateMany<TestDocumentWithKey<int>, int, string>(keyedFilterDefinition, keyedFieldExpression, value, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int, string>(
                keyedFilterDefinition,
                keyedFieldExpression,
                value,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void Keyed_WithFilterDefinitionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
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
        var result = Sut.UpdateMany<TestDocumentWithKey<int>, int, string>(keyedFilterDefinition, keyedFieldExpression, value, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int, string>(
                keyedFilterDefinition,
                keyedFieldExpression,
                value,
                partitionKey,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void Keyed_WithFilterExpressionAndFieldExpressionAndValue_ShouldUpdateOne()
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
        var result = Sut.UpdateMany<TestDocumentWithKey<int>, int, string>(keyedFilterExpression, keyedFieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int, string>(
                keyedFilterExpression,
                keyedFieldExpression,
                value,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void Keyed_WithFilterExpressionAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
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
        var result = Sut.UpdateMany<TestDocumentWithKey<int>, int, string>(keyedFilterExpression, keyedFieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int, string>(
                keyedFilterExpression,
                keyedFieldExpression,
                value,
                null,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void Keyed_WithFilterExpressionAndFieldExpressionAndValueAndPartitionKey_ShouldUpdateOne()
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
        var result = Sut.UpdateMany<TestDocumentWithKey<int>, int, string>(keyedFilterExpression, keyedFieldExpression, value, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int, string>(
                keyedFilterExpression,
                keyedFieldExpression,
                value,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void Keyed_WithFilterExpressionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
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
        var result = Sut.UpdateMany<TestDocumentWithKey<int>, int, string>(keyedFilterExpression, keyedFieldExpression, value, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int, string>(
                keyedFilterExpression,
                keyedFieldExpression,
                value,
                partitionKey,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void Keyed_WithFilterExpressionAndUpdateDefinition_ShouldUpdateOne()
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
        var result = Sut.UpdateMany<TestDocumentWithKey<int>, int>(keyedFilterExpression, keyedUpdateDefinition);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int>(
                keyedFilterExpression,
                keyedUpdateDefinition,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void Keyed_WithFilterExpressionAndUpdateDefinitionAndCancellationToken_ShouldUpdateOne()
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
        var result = Sut.UpdateMany<TestDocumentWithKey<int>, int>(keyedFilterExpression, keyedUpdateDefinition, token);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int>(
                keyedFilterExpression,
                keyedUpdateDefinition,
                null,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void Keyed_WithFilterExpressionAndUpdateDefinitionAndPartitionKey_ShouldUpdateOne()
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
        var result = Sut.UpdateMany<TestDocumentWithKey<int>, int>(keyedFilterExpression, keyedUpdateDefinition, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int>(
                keyedFilterExpression,
                keyedUpdateDefinition,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void Keyed_WithFilterExpressionAndUpdateDefinitionAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
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
        var result = Sut.UpdateMany<TestDocumentWithKey<int>, int>(keyedFilterExpression, keyedUpdateDefinition, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int>(
                keyedFilterExpression,
                keyedUpdateDefinition,
                partitionKey,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void Keyed_WithFilterDefinitionAndUpdateDefinition_ShouldUpdateOne()
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
        var result = Sut.UpdateMany<TestDocumentWithKey<int>, int>(keyedFilterDefinition, keyedUpdateDefinition);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int>(
                keyedFilterDefinition,
                keyedUpdateDefinition,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void Keyed_WithFilterDefinitionAndUpdateDefinitionCancellationToken_ShouldUpdateOne()
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
        var result = Sut.UpdateMany<TestDocumentWithKey<int>, int>(keyedFilterDefinition, keyedUpdateDefinition, token);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int>(
                keyedFilterDefinition,
                keyedUpdateDefinition,
                null,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void Keyed_WithFilterDefinitionAndUpdateDefinitionAndPartitionKey_ShouldUpdateOne()
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
        var result = Sut.UpdateMany<TestDocumentWithKey<int>, int>(keyedFilterDefinition, keyedUpdateDefinition, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int>(
                keyedFilterDefinition,
                keyedUpdateDefinition,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public void Keyed_WithFilterDefinitionAndUpdateDefinitionAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
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
        var result = Sut.UpdateMany<TestDocumentWithKey<int>, int>(keyedFilterDefinition, keyedUpdateDefinition, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateMany<TestDocumentWithKey<int>, int>(
                keyedFilterDefinition,
                keyedUpdateDefinition,
                partitionKey,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    #endregion
}
