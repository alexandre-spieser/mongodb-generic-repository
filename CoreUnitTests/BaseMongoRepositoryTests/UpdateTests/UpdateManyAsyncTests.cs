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

namespace CoreUnitTests.BaseMongoRepositoryTests.UpdateTests;

public class UpdateManyAsyncTests : TestMongoRepositoryContext
{
    private readonly Expression<Func<TestDocument, string>> fieldExpression = x => x.SomeContent;
    private readonly FilterDefinition<TestDocument> filterDefinition = Builders<TestDocument>.Filter.Eq(x => x.SomeContent, "SomeContent");
    private readonly Expression<Func<TestDocument, bool>> filterExpression = x => x.SomeContent == "SomeContent";
    private readonly UpdateDefinition<TestDocument> updateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, "Updated");

    [Fact]
    public async Task WithFilterDefinitionAndFieldExpressionAndValue_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var count = Fixture.Create<long>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateManyAsync<TestDocument, Guid, string>(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterDefinition, fieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocument, Guid, string>(
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
                x => x.UpdateManyAsync<TestDocument, Guid, string>(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterDefinition, fieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocument, Guid, string>(
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
                x => x.UpdateManyAsync<TestDocument, Guid, string>(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterDefinition, fieldExpression, value, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocument, Guid, string>(
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
                x => x.UpdateManyAsync<TestDocument, Guid, string>(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterDefinition, fieldExpression, value, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocument, Guid, string>(
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
                x => x.UpdateManyAsync<TestDocument, Guid, string>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterExpression, fieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocument, Guid, string>(
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
                x => x.UpdateManyAsync<TestDocument, Guid, string>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterExpression, fieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocument, Guid, string>(
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
                x => x.UpdateManyAsync<TestDocument, Guid, string>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterExpression, fieldExpression, value, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocument, Guid, string>(
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
                x => x.UpdateManyAsync<TestDocument, Guid, string>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterExpression, fieldExpression, value, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocument, Guid, string>(
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
                x => x.UpdateManyAsync<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterExpression, updateDefinition);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocument, Guid>(
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
                x => x.UpdateManyAsync<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterExpression, updateDefinition, token);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocument, Guid>(
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
                x => x.UpdateManyAsync<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterExpression, updateDefinition, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocument, Guid>(
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
                x => x.UpdateManyAsync<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterExpression, updateDefinition, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocument, Guid>(
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
                x => x.UpdateManyAsync<TestDocument, Guid>(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterDefinition, updateDefinition);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocument, Guid>(
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
                x => x.UpdateManyAsync<TestDocument, Guid>(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterDefinition, updateDefinition, token);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocument, Guid>(
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
                x => x.UpdateManyAsync<TestDocument, Guid>(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterDefinition, updateDefinition, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocument, Guid>(
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
                x => x.UpdateManyAsync<TestDocument, Guid>(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);

        // Act
        var result = await Sut.UpdateManyAsync(filterDefinition, updateDefinition, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocument, Guid>(
                filterDefinition,
                updateDefinition,
                partitionKey,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    #region Keyed

    private readonly UpdateDefinition<TestDocumentWithKey<int>> keyedUpdateDefinition =
        Builders<TestDocumentWithKey<int>>.Update.Set(x => x.SomeContent, "Updated");

    private readonly Expression<Func<TestDocumentWithKey<int>, string>> keyedFieldExpression = x => x.SomeContent;
    private readonly FilterDefinition<TestDocumentWithKey<int>> keyedFilterDefinition = Builders<TestDocumentWithKey<int>>.Filter.Eq(x => x.Id, 1);
    private readonly Expression<Func<TestDocumentWithKey<int>, bool>> keyedFilterExpression = x => x.SomeContent == "SomeContent";

    [Fact]
    public async Task Keyed_WithFilterDefinitionAndFieldExpressionAndValue_ShouldUpdateOne()
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
        var result = await Sut.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(keyedFilterDefinition, keyedFieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(
                keyedFilterDefinition,
                keyedFieldExpression,
                value,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task Keyed_WithFilterDefinitionAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
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
        var result = await Sut.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(keyedFilterDefinition, keyedFieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(
                keyedFilterDefinition,
                keyedFieldExpression,
                value,
                null,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task Keyed_WithFilterDefinitionAndFieldExpressionAndValueAndPartitionKey_ShouldUpdateOne()
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
        var result = await Sut.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(keyedFilterDefinition, keyedFieldExpression, value, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(
                keyedFilterDefinition,
                keyedFieldExpression,
                value,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task Keyed_WithFilterDefinitionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
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
        var result = await Sut.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(keyedFilterDefinition, keyedFieldExpression, value, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(
                keyedFilterDefinition,
                keyedFieldExpression,
                value,
                partitionKey,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task Keyed_WithFilterExpressionAndFieldExpressionAndValue_ShouldUpdateOne()
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
        var result = await Sut.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(keyedFilterExpression, keyedFieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(
                keyedFilterExpression,
                keyedFieldExpression,
                value,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task Keyed_WithFilterExpressionAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
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
        var result = await Sut.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(keyedFilterExpression, keyedFieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(
                keyedFilterExpression,
                keyedFieldExpression,
                value,
                null,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task Keyed_WithFilterExpressionAndFieldExpressionAndValueAndPartitionKey_ShouldUpdateOne()
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
        var result = await Sut.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(keyedFilterExpression, keyedFieldExpression, value, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(
                keyedFilterExpression,
                keyedFieldExpression,
                value,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task Keyed_WithFilterExpressionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
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
        var result = await Sut.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(keyedFilterExpression, keyedFieldExpression, value, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int, string>(
                keyedFilterExpression,
                keyedFieldExpression,
                value,
                partitionKey,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task Keyed_WithFilterExpressionAndUpdateDefinition_ShouldUpdateOne()
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
        var result = await Sut.UpdateManyAsync<TestDocumentWithKey<int>, int>(keyedFilterExpression, keyedUpdateDefinition);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int>(
                keyedFilterExpression,
                keyedUpdateDefinition,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task Keyed_WithFilterExpressionAndUpdateDefinitionAndCancellationToken_ShouldUpdateOne()
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
        var result = await Sut.UpdateManyAsync<TestDocumentWithKey<int>, int>(keyedFilterExpression, keyedUpdateDefinition, token);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int>(
                keyedFilterExpression,
                keyedUpdateDefinition,
                null,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task Keyed_WithFilterExpressionAndUpdateDefinitionAndPartitionKey_ShouldUpdateOne()
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
        var result = await Sut.UpdateManyAsync<TestDocumentWithKey<int>, int>(keyedFilterExpression, keyedUpdateDefinition, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int>(
                keyedFilterExpression,
                keyedUpdateDefinition,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task Keyed_WithFilterExpressionAndUpdateDefinitionAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
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
        var result = await Sut.UpdateManyAsync<TestDocumentWithKey<int>, int>(keyedFilterExpression, keyedUpdateDefinition, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int>(
                keyedFilterExpression,
                keyedUpdateDefinition,
                partitionKey,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task Keyed_WithFilterDefinitionAndUpdateDefinition_ShouldUpdateOne()
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
        var result = await Sut.UpdateManyAsync<TestDocumentWithKey<int>, int>(keyedFilterDefinition, keyedUpdateDefinition);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int>(
                keyedFilterDefinition,
                keyedUpdateDefinition,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task Keyed_WithFilterDefinitionAndUpdateDefinitionCancellationToken_ShouldUpdateOne()
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
        var result = await Sut.UpdateManyAsync<TestDocumentWithKey<int>, int>(keyedFilterDefinition, keyedUpdateDefinition, token);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int>(
                keyedFilterDefinition,
                keyedUpdateDefinition,
                null,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task Keyed_WithFilterDefinitionAndUpdateDefinitionAndPartitionKey_ShouldUpdateOne()
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
        var result = await Sut.UpdateManyAsync<TestDocumentWithKey<int>, int>(keyedFilterDefinition, keyedUpdateDefinition, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int>(
                keyedFilterDefinition,
                keyedUpdateDefinition,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().Be(count);
    }

    [Fact]
    public async Task Keyed_WithFilterDefinitionAndUpdateDefinitionAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
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
        var result = await Sut.UpdateManyAsync<TestDocumentWithKey<int>, int>(keyedFilterDefinition, keyedUpdateDefinition, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateManyAsync<TestDocumentWithKey<int>, int>(
                keyedFilterDefinition,
                keyedUpdateDefinition,
                partitionKey,
                token),
            Times.Once);
        result.Should().Be(count);
    }

    #endregion
}
