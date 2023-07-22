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

public class UpdateOneAsyncTests : TestMongoRepositoryContext
{
    private readonly UpdateDefinition<TestDocument> updateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, "Updated");
    private readonly Expression<Func<TestDocument, string>> fieldExpression = x => x.SomeContent;
    private readonly FilterDefinition<TestDocument> filterDefinition = Builders<TestDocument>.Filter.Eq(x => x.SomeContent, "SomeContent");
    private readonly Expression<Func<TestDocument, bool>> filterExpression = x => x.SomeContent == "SomeContent";

    [Fact]
    public async Task WithDocument_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(x => x.UpdateOneAsync<TestDocument, Guid>(It.IsAny<TestDocument>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(document);

        // Assert
        Updater.Verify(x => x.UpdateOneAsync<TestDocument, Guid>(document, CancellationToken.None), Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task WithDocumentAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(x => x.UpdateOneAsync<TestDocument, Guid>(It.IsAny<TestDocument>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(document, token);

        // Assert
        Updater.Verify(x => x.UpdateOneAsync<TestDocument, Guid>(document, token), Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task WithDocumentAndUpdateDefinition_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocument, Guid>(
                    It.IsAny<TestDocument>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(document, updateDefinition);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocument, Guid>(
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
        var document = Fixture.Create<TestDocument>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocument, Guid>(
                    It.IsAny<TestDocument>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(document, updateDefinition, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocument, Guid>(
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
        var document = Fixture.Create<TestDocument>();
        var value = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocument, Guid, string>(
                    It.IsAny<TestDocument>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(document, fieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocument, Guid, string>(
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
        var document = Fixture.Create<TestDocument>();
        var value = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocument, Guid, string>(
                    It.IsAny<TestDocument>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(document, fieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocument, Guid, string>(
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
                x => x.UpdateOneAsync<TestDocument, Guid, string>(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(filterDefinition, fieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocument, Guid, string>(
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
                x => x.UpdateOneAsync<TestDocument, Guid, string>(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(filterDefinition, fieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocument, Guid, string>(
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
                x => x.UpdateOneAsync<TestDocument, Guid, string>(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(filterDefinition, fieldExpression, value, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocument, Guid, string>(
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
                x => x.UpdateOneAsync<TestDocument, Guid, string>(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(filterDefinition, fieldExpression, value, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocument, Guid, string>(
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
                x => x.UpdateOneAsync<TestDocument, Guid, string>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(filterExpression, fieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocument, Guid, string>(
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
                x => x.UpdateOneAsync<TestDocument, Guid, string>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(filterExpression, fieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocument, Guid, string>(
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
                x => x.UpdateOneAsync<TestDocument, Guid, string>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(filterExpression, fieldExpression, value, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocument, Guid, string>(
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
                x => x.UpdateOneAsync<TestDocument, Guid, string>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync(filterExpression, fieldExpression, value, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocument, Guid, string>(
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
    public async Task Keyed_WithDocument_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(x => x.UpdateOneAsync<TestDocumentWithKey<int>, int>(It.IsAny<TestDocumentWithKey<int>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int>(document);

        // Assert
        Updater.Verify(x => x.UpdateOneAsync<TestDocumentWithKey<int>, int>(document, CancellationToken.None), Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Keyed_WithDocumentAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(x => x.UpdateOneAsync<TestDocumentWithKey<int>, int>(It.IsAny<TestDocumentWithKey<int>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int>(document, token);

        // Assert
        Updater.Verify(x => x.UpdateOneAsync<TestDocumentWithKey<int>, int>(document, token), Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Keyed_WithDocumentAndUpdateDefinition_ShouldUpdateOne()
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
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int>(document, keyedUpdateDefinition);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int>(
                document,
                keyedUpdateDefinition,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Keyed_WithDocumentAndUpdateDefinitionAndCancellationToken_ShouldUpdateOne()
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
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int>(document, keyedUpdateDefinition, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int>(
                document,
                keyedUpdateDefinition,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Keyed_WithDocumentAndFieldExpressionAndValue_ShouldUpdateOne()
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
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(document, keyedFieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                document,
                keyedFieldExpression,
                value,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Keyed_WithDocumentAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
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
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(document, keyedFieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                document,
                keyedFieldExpression,
                value,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Keyed_WithFilterDefinitionAndFieldExpressionAndValue_ShouldUpdateOne()
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
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(keyedFilterDefinition, keyedFieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                keyedFilterDefinition,
                keyedFieldExpression,
                value,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Keyed_WithFilterDefinitionAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
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
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(keyedFilterDefinition, keyedFieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                keyedFilterDefinition,
                keyedFieldExpression,
                value,
                null,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Keyed_WithFilterDefinitionAndFieldExpressionAndValueAndPartitionKey_ShouldUpdateOne()
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
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(keyedFilterDefinition, keyedFieldExpression, value, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                keyedFilterDefinition,
                keyedFieldExpression,
                value,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Keyed_WithFilterDefinitionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
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
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(keyedFilterDefinition, keyedFieldExpression, value, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                keyedFilterDefinition,
                keyedFieldExpression,
                value,
                partitionKey,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Keyed_WithFilterExpressionAndFieldExpressionAndValue_ShouldUpdateOne()
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
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(keyedFilterExpression, keyedFieldExpression, value);

        // Assert
        var expectedFilter = Builders<TestDocumentWithKey<int>>.Filter.Where(keyedFilterExpression);
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                It.Is<FilterDefinition<TestDocumentWithKey<int>>>(y => y.EquivalentTo(expectedFilter)),
                keyedFieldExpression,
                value,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Keyed_WithFilterExpressionAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
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
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(keyedFilterExpression, keyedFieldExpression, value, token);

        // Assert
        var expectedFilter = Builders<TestDocumentWithKey<int>>.Filter.Where(keyedFilterExpression);
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                It.Is<FilterDefinition<TestDocumentWithKey<int>>>(y => y.EquivalentTo(expectedFilter)),
                keyedFieldExpression,
                value,
                null,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Keyed_WithFilterExpressionAndFieldExpressionAndValueAndPartitionKey_ShouldUpdateOne()
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
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(keyedFilterExpression, keyedFieldExpression, value, partitionKey);

        // Assert
        var expectedFilter = Builders<TestDocumentWithKey<int>>.Filter.Where(keyedFilterExpression);
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                It.Is<FilterDefinition<TestDocumentWithKey<int>>>(y => y.EquivalentTo(expectedFilter)),
                keyedFieldExpression,
                value,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Keyed_WithFilterExpressionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
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
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(keyedFilterExpression, keyedFieldExpression, value, partitionKey, token);

        // Assert
        var expectedFilter = Builders<TestDocumentWithKey<int>>.Filter.Where(keyedFilterExpression);
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                It.Is<FilterDefinition<TestDocumentWithKey<int>>>(y => y.EquivalentTo(expectedFilter)),
                keyedFieldExpression,
                value,
                partitionKey,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    #endregion

    #region ClientSession

    [Fact]
    public async Task Keyed_WithClientSessionHandlerAndDocument_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(x => x.UpdateOneAsync<TestDocumentWithKey<int>, int>(
                It.IsAny<IClientSessionHandle>(),
                It.IsAny<TestDocumentWithKey<int>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int>(session, document);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int>(
            session,
            document,
            CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Keyed_WithClientSessionHandlerAndDocumentAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(x => x.UpdateOneAsync<TestDocumentWithKey<int>, int>(
                It.IsAny<IClientSessionHandle>(),
                It.IsAny<TestDocumentWithKey<int>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int>(session, document, token);

        // Assert
        Updater.Verify(x => x.UpdateOneAsync<TestDocumentWithKey<int>, int>(session, document, token), Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Keyed_WithClientSessionHandlerAndDocumentAndUpdateDefinition_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocumentWithKey<int>, int>(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<TestDocumentWithKey<int>>(),
                    It.IsAny<UpdateDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int>(session, document, keyedUpdateDefinition);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int>(
                session,
                document,
                keyedUpdateDefinition,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Keyed_WithClientSessionHandlerAndDocumentAndUpdateDefinitionAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocumentWithKey<int>, int>(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<TestDocumentWithKey<int>>(),
                    It.IsAny<UpdateDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int>(session, document, keyedUpdateDefinition, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int>(
                session,
                document,
                keyedUpdateDefinition,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Keyed_WithClientSessionHandlerAndDocumentAndFieldExpressionAndValue_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var value = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<TestDocumentWithKey<int>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(session, document, keyedFieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                session,
                document,
                keyedFieldExpression,
                value,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Keyed_WithClientSessionHandlerAndDocumentAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var value = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<TestDocumentWithKey<int>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(session, document, keyedFieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                session,
                document,
                keyedFieldExpression,
                value,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Keyed_WithClientSessionHandlerAndFilterDefinitionAndFieldExpressionAndValue_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var value = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(session, keyedFilterDefinition, keyedFieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
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
    public async Task Keyed_WithClientSessionHandlerAndFilterDefinitionAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var value = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(session, keyedFilterDefinition, keyedFieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
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
    public async Task Keyed_WithClientSessionHandlerAndFilterDefinitionAndFieldExpressionAndValueAndPartitionKey_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(session, keyedFilterDefinition, keyedFieldExpression, value, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
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
    public async Task Keyed_WithClientSessionHandlerAndFilterDefinitionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(session, keyedFilterDefinition, keyedFieldExpression, value, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
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
    public async Task Keyed_WithClientSessionHandlerAndFilterExpressionAndFieldExpressionAndValue_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var value = Fixture.Create<string>();

        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(session, keyedFilterExpression, keyedFieldExpression, value);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                session,
                keyedFilterExpression,
                keyedFieldExpression,
                value,
                null,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Keyed_WithClientSessionHandlerAndFilterExpressionAndFieldExpressionAndValueAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var value = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(session, keyedFilterExpression, keyedFieldExpression, value, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                session,
                keyedFilterExpression,
                keyedFieldExpression,
                value,
                null,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Keyed_WithClientSessionHandlerAndFilterExpressionAndFieldExpressionAndValueAndPartitionKey_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(session, keyedFilterExpression, keyedFieldExpression, value, partitionKey);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                session,
                keyedFilterExpression,
                keyedFieldExpression,
                value,
                partitionKey,
                CancellationToken.None),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Keyed_WithClientSessionHandlerAndFilterExpressionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ShouldUpdateOne()
    {
        // Arrange
        var session = new Mock<IClientSessionHandle>().Object;
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        Updater = new Mock<IMongoDbUpdater>();
        Updater
            .Setup(
                x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, string>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await Sut.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(session, keyedFilterExpression, keyedFieldExpression, value, partitionKey, token);

        // Assert
        Updater.Verify(
            x => x.UpdateOneAsync<TestDocumentWithKey<int>, int, string>(
                session,
                keyedFilterExpression,
                keyedFieldExpression,
                value,
                partitionKey,
                token),
            Times.Once);
        result.Should().BeTrue();
    }

    #endregion
}
