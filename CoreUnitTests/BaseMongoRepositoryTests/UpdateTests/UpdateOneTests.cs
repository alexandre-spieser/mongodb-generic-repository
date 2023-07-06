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

public class UpdateOneTests : TestMongoRepositoryContext
{
    private readonly UpdateDefinition<TestDocument> updateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, "Updated");
    private readonly Expression<Func<TestDocument, string>> fieldExpression = x => x.SomeContent;
    private readonly FilterDefinition<TestDocument> filterDefinition = Builders<TestDocument>.Filter.Eq(x => x.SomeContent, "SomeContent");
    private readonly Expression<Func<TestDocument, bool>> filterExpression = x => x.SomeContent == "SomeContent";

    [Fact]
    public void WithDocument_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(x => x.UpdateOne<TestDocument, Guid>(It.IsAny<TestDocument>(), It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne(document);

        // Assert
        Updater.Verify(x => x.UpdateOne<TestDocument, Guid>(document, CancellationToken.None), Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void WithDocumentAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(x => x.UpdateOne<TestDocument, Guid>(It.IsAny<TestDocument>(), It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne(document, token);

        // Assert
        Updater.Verify(x => x.UpdateOne<TestDocument, Guid>(document, token), Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void WithDocumentAndUpdateDefinition_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocument, Guid>(
                    It.IsAny<TestDocument>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne(document, updateDefinition);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocument, Guid>(
                document,
                updateDefinition,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void WithDocumentAndUpdateDefinitionAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocument, Guid>(
                    It.IsAny<TestDocument>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne(document, updateDefinition, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocument, Guid>(
                document,
                updateDefinition,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void WithDocumentAndFieldExpressionAndValue_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var value = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocument, Guid, string>(
                    It.IsAny<TestDocument>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne(document, fieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocument, Guid, string>(
                document,
                fieldExpression,
                value,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void WithDocumentAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var value = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocument, Guid, string>(
                    It.IsAny<TestDocument>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne(document, fieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocument, Guid, string>(
                document,
                fieldExpression,
                value,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void WithFilterDefinitionAndFieldExpressionAndValue_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocument, Guid, string>(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne(filterDefinition, fieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocument, Guid, string>(
                filterDefinition,
                fieldExpression,
                value,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void WithFilterDefinitionAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocument, Guid, string>(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne(filterDefinition, fieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocument, Guid, string>(
                filterDefinition,
                fieldExpression,
                value,
                null,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void WithFilterDefinitionAndFieldExpressionAndValueAndPartitionKey_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocument, Guid, string>(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne(filterDefinition, fieldExpression, value, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocument, Guid, string>(
                filterDefinition,
                fieldExpression,
                value,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void WithFilterDefinitionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocument, Guid, string>(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne(filterDefinition, fieldExpression, value, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocument, Guid, string>(
                filterDefinition,
                fieldExpression,
                value,
                partitionKey,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void WithFilterExpressionAndFieldExpressionAndValue_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();

        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocument, Guid, string>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne(filterExpression, fieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocument, Guid, string>(
                filterExpression,
                fieldExpression,
                value,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void WithFilterExpressionAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocument, Guid, string>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne(filterExpression, fieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocument, Guid, string>(
                filterExpression,
                fieldExpression,
                value,
                null,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void WithFilterExpressionAndFieldExpressionAndValueAndPartitionKey_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocument, Guid, string>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne(filterExpression, fieldExpression, value, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocument, Guid, string>(
                filterExpression,
                fieldExpression,
                value,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void WithFilterExpressionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocument, Guid, string>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne(filterExpression, fieldExpression, value, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocument, Guid, string>(
                filterExpression,
                fieldExpression,
                value,
                partitionKey,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    #region Keyed

    private readonly UpdateDefinition<TestDocumentWithKey<int>> keyedUpdateDefinition = Builders<TestDocumentWithKey<int>>.Update.Set(x => x.SomeContent, "Updated");
    private readonly Expression<Func<TestDocumentWithKey<int>, string>> keyedFieldExpression = x => x.SomeContent;
    private readonly FilterDefinition<TestDocumentWithKey<int>> keyedFilterDefinition = Builders<TestDocumentWithKey<int>>.Filter.Eq(x => x.Id, 1);
    private readonly Expression<Func<TestDocumentWithKey<int>, bool>> keyedFilterExpression = x => x.SomeContent == "SomeContent";

    [Fact]
    public void Keyed_WithDocument_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(x => x.UpdateOne<TestDocumentWithKey<int>, int>(It.IsAny<TestDocumentWithKey<int>>(), It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int>(document);

        // Assert
        Updater.Verify(x => x.UpdateOne<TestDocumentWithKey<int>, int>(document, CancellationToken.None), Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithDocumentAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(x => x.UpdateOne<TestDocumentWithKey<int>, int>(It.IsAny<TestDocumentWithKey<int>>(), It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int>(document, token);

        // Assert
        Updater.Verify(x => x.UpdateOne<TestDocumentWithKey<int>, int>(document, token), Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithDocumentAndUpdateDefinition_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocumentWithKey<int>, int>(
                    It.IsAny<TestDocumentWithKey<int>>(),
                    It.IsAny<UpdateDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int>(document, keyedUpdateDefinition);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocumentWithKey<int>, int>(
                document,
                keyedUpdateDefinition,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithDocumentAndUpdateDefinitionAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocumentWithKey<int>, int>(
                    It.IsAny<TestDocumentWithKey<int>>(),
                    It.IsAny<UpdateDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int>(document, keyedUpdateDefinition, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocumentWithKey<int>, int>(
                document,
                keyedUpdateDefinition,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithDocumentAndFieldExpressionAndValue_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var value = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<TestDocumentWithKey<int>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int, string>(document, keyedFieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                document,
                keyedFieldExpression,
                value,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithDocumentAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var value = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<TestDocumentWithKey<int>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int, string>(document, keyedFieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                document,
                keyedFieldExpression,
                value,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithFilterDefinitionAndFieldExpressionAndValue_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int, string>(keyedFilterDefinition, keyedFieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                keyedFilterDefinition,
                keyedFieldExpression,
                value,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithFilterDefinitionAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int, string>(keyedFilterDefinition, keyedFieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                keyedFilterDefinition,
                keyedFieldExpression,
                value,
                null,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithFilterDefinitionAndFieldExpressionAndValueAndPartitionKey_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int, string>(keyedFilterDefinition, keyedFieldExpression, value, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                keyedFilterDefinition,
                keyedFieldExpression,
                value,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithFilterDefinitionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int, string>(keyedFilterDefinition, keyedFieldExpression, value, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                keyedFilterDefinition,
                keyedFieldExpression,
                value,
                partitionKey,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithFilterExpressionAndFieldExpressionAndValue_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();

        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int, string>(keyedFilterExpression, keyedFieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                keyedFilterExpression,
                keyedFieldExpression,
                value,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithFilterExpressionAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int, string>(keyedFilterExpression, keyedFieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                keyedFilterExpression,
                keyedFieldExpression,
                value,
                null,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithFilterExpressionAndFieldExpressionAndValueAndPartitionKey_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int, string>(keyedFilterExpression, keyedFieldExpression, value, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                keyedFilterExpression,
                keyedFieldExpression,
                value,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithFilterExpressionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int, string>(keyedFilterExpression, keyedFieldExpression, value, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                keyedFilterExpression,
                keyedFieldExpression,
                value,
                partitionKey,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    #endregion

    #region client session

    [Fact]
    public void Keyed_WithClientSessionHandlerAndDocument_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var session = new Mock<IClientSessionHandle>().Object;
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(x => x.UpdateOne<TestDocumentWithKey<int>, int>(
                It.IsAny<IClientSessionHandle>(),
                It.IsAny<TestDocumentWithKey<int>>(),
                It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int>(session, document);

        // Assert
        Updater.Verify(x => x.UpdateOne<TestDocumentWithKey<int>, int>(
            session,
            document,
            CancellationToken.None), Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithClientSessionHandlerAndDocumentAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var token = new CancellationToken(true);
        var session = new Mock<IClientSessionHandle>().Object;
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(x => x.UpdateOne<TestDocumentWithKey<int>, int>(
                It.IsAny<IClientSessionHandle>(),
                It.IsAny<TestDocumentWithKey<int>>(),
                It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int>(session, document, token);

        // Assert
        Updater.Verify(x => x.UpdateOne<TestDocumentWithKey<int>, int>(session, document, token), Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithClientSessionHandlerAndDocumentAndUpdateDefinition_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocumentWithKey<int>, int>(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<TestDocumentWithKey<int>>(),
                    It.IsAny<UpdateDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int>(session, document, keyedUpdateDefinition);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocumentWithKey<int>, int>(
                session,
                document,
                keyedUpdateDefinition,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithClientSessionHandlerAndDocumentAndUpdateDefinitionAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocumentWithKey<int>, int>(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<TestDocumentWithKey<int>>(),
                    It.IsAny<UpdateDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int>(session, document, keyedUpdateDefinition, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocumentWithKey<int>, int>(
                session,
                document,
                keyedUpdateDefinition,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithClientSessionHandlerAndDocumentAndFieldExpressionAndValue_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var value = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<TestDocumentWithKey<int>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int, string>(session, document, keyedFieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                session,
                document,
                keyedFieldExpression,
                value,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithClientSessionHandlerAndDocumentAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var value = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<TestDocumentWithKey<int>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int, string>(session, document, keyedFieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                session,
                document,
                keyedFieldExpression,
                value,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithClientSessionHandlerFilterDefinitionAndFieldExpressionAndValue_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var value = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int, string>(session, keyedFilterDefinition, keyedFieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                session,
                keyedFilterDefinition,
                keyedFieldExpression,
                value,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithClientSessionHandlerFilterDefinitionAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var value = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int, string>(session, keyedFilterDefinition, keyedFieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                session,
                keyedFilterDefinition,
                keyedFieldExpression,
                value,
                null,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithClientSessionHandlerFilterDefinitionAndFieldExpressionAndValueAndPartitionKey_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int, string>(session, keyedFilterDefinition, keyedFieldExpression, value, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                session,
                keyedFilterDefinition,
                keyedFieldExpression,
                value,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithClientSessionHandlerFilterDefinitionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int, string>(session, keyedFilterDefinition, keyedFieldExpression, value, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                session,
                keyedFilterDefinition,
                keyedFieldExpression,
                value,
                partitionKey,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithClientSessionHandlerFilterExpressionAndFieldExpressionAndValue_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var value = Fixture.Create<string>();

        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int, string>(session, keyedFilterExpression, keyedFieldExpression, value);

        // Assert
        var expectedFilterDefinition = new ExpressionFilterDefinition<TestDocumentWithKey<int>>(keyedFilterExpression);
        Updater.Verify(
            x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                session,
                It.Is<FilterDefinition<TestDocumentWithKey<int>>>(y => y.EquivalentTo(expectedFilterDefinition)),
                keyedFieldExpression,
                value,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithClientSessionHandlerFilterExpressionAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var value = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int, string>(session, keyedFilterExpression, keyedFieldExpression, value, token);

        // Assert
        var expectedFilterDefinition = new ExpressionFilterDefinition<TestDocumentWithKey<int>>(keyedFilterExpression);
        Updater.Verify(
            x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                session,
                It.Is<FilterDefinition<TestDocumentWithKey<int>>>(y => y.EquivalentTo(expectedFilterDefinition)),
                keyedFieldExpression,
                value,
                null,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithClientSessionHandlerFilterExpressionAndFieldExpressionAndValueAndPartitionKey_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int, string>(session, keyedFilterExpression, keyedFieldExpression, value, partitionKey);

        // Assert
        var expectedFilterDefinition = new ExpressionFilterDefinition<TestDocumentWithKey<int>>(keyedFilterExpression);
        Updater.Verify(
            x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                session,
                It.Is<FilterDefinition<TestDocumentWithKey<int>>>(y => y.EquivalentTo(expectedFilterDefinition)),
                keyedFieldExpression,
                value,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public void Keyed_WithClientSessionHandlerFilterExpressionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);

        // Act
        var result = Sut.UpdateOne<TestDocumentWithKey<int>, int, string>(session, keyedFilterExpression, keyedFieldExpression, value, partitionKey, token);

        // Assert
        var expectedFilterDefinition = new ExpressionFilterDefinition<TestDocumentWithKey<int>>(keyedFilterExpression);
        Updater.Verify(
            x => x.UpdateOne<TestDocumentWithKey<int>, int, string>(
                session,
                It.Is<FilterDefinition<TestDocumentWithKey<int>>>(y => y.EquivalentTo(expectedFilterDefinition)),
                keyedFieldExpression,
                value,
                partitionKey,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    #endregion
}
