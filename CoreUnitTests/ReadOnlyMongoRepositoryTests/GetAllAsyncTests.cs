using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using FluentAssertions;
using MongoDB.Driver;
using MongoDbGenericRepository.DataAccess.Read;
using Moq;
using Xunit;

namespace CoreUnitTests.ReadOnlyMongoRepositoryTests;

public class GetAllAsyncTests : TestReadOnlyMongoRepositoryContext
{
    private readonly Expression<Func<TestDocument, bool>> expression = document => document.SomeContent == "SomeContent";

    [Fact]
    public async Task WithExpression_GetsAll()
    {
        // Arrange
        var document = Fixture.CreateMany<TestDocument>().ToList();

        SetupReader(document);

        // Act
        var result = await Sut.GetAllAsync(expression);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetAllAsync<TestDocument, Guid>(expression, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task WithExpressionAndCancellationToken_GetsAll()
    {
        // Arrange
        var document = Fixture.CreateMany<TestDocument>().ToList();
        var token = new CancellationToken(true);

        SetupReader(document);

        // Act
        var result = await Sut.GetAllAsync(expression, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetAllAsync<TestDocument, Guid>(expression, null, token),
            Times.Once);
    }

    [Fact]
    public async Task WithExpressionAndPartitionKey_GetsAll()
    {
        // Arrange
        var document = Fixture.CreateMany<TestDocument>().ToList();
        var partitionKey = Fixture.Create<string>();

        SetupReader(document);

        // Act
        var result = await Sut.GetAllAsync(expression, partitionKey);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetAllAsync<TestDocument, Guid>(expression, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task WithExpressionAndPartitionKeyAndCancellationToken_GetsAll()
    {
        // Arrange
        var document = Fixture.CreateMany<TestDocument>().ToList();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupReader(document);

        // Act
        var result = await Sut.GetAllAsync(expression, partitionKey, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetAllAsync<TestDocument, Guid>(expression, partitionKey, token),
            Times.Once);
    }

    private void SetupReader(List<TestDocument> documents)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.GetAllAsync<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(documents);
    }

    #region Keyed

    private readonly Expression<Func<TestDocumentWithKey<int>, bool>> keyedExpression = document => document.SomeContent == "SomeContent";
    private readonly FilterDefinition<TestDocumentWithKey<int>> keyedFilter = Builders<TestDocumentWithKey<int>>.Filter.Eq(document => document.SomeContent, "SomeContent");

    [Fact]
    public async Task Keyed_WithExpression_GetsAll()
    {
        // Arrange
        var document = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();

        SetupReader(document);

        // Act
        var result = await Sut.GetAllAsync<TestDocumentWithKey<int>, int>(keyedExpression);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetAllAsync<TestDocumentWithKey<int>, int>(keyedExpression, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithExpressionAndCancellationToken_GetsAll()
    {
        // Arrange
        var document = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        var token = new CancellationToken(true);

        SetupReader(document);

        // Act
        var result = await Sut.GetAllAsync<TestDocumentWithKey<int>, int>(keyedExpression, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetAllAsync<TestDocumentWithKey<int>, int>(keyedExpression, null, token),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithExpressionAndPartitionKey_GetsAll()
    {
        // Arrange
        var document = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        var partitionKey = Fixture.Create<string>();

        SetupReader(document);

        // Act
        var result = await Sut.GetAllAsync<TestDocumentWithKey<int>, int>(keyedExpression, partitionKey);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetAllAsync<TestDocumentWithKey<int>, int>(keyedExpression, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithExpressionAndPartitionKeyAndCancellationToken_GetsAll()
    {
        // Arrange
        var document = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupReader(document);

        // Act
        var result = await Sut.GetAllAsync<TestDocumentWithKey<int>, int>(keyedExpression, partitionKey, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetAllAsync<TestDocumentWithKey<int>, int>(keyedExpression, partitionKey, token),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilter_GetsAll()
    {
        // Arrange
        var document = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();

        SetupReaderWithFilter(document);

        // Act
        var result = await Sut.GetAllAsync<TestDocumentWithKey<int>, int>(keyedFilter);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetAllAsync<TestDocumentWithKey<int>, int>(keyedFilter, null, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndOptions_GetsAll()
    {
        // Arrange
        var document = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        var options = new FindOptions();

        SetupReaderWithFilter(document);

        // Act
        var result = await Sut.GetAllAsync<TestDocumentWithKey<int>, int>(keyedFilter, options);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetAllAsync<TestDocumentWithKey<int>, int>(keyedFilter, options, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndCancellationToken_GetsAll()
    {
        // Arrange
        var document = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        var token = new CancellationToken(true);

        SetupReaderWithFilter(document);

        // Act
        var result = await Sut.GetAllAsync<TestDocumentWithKey<int>, int>(keyedFilter, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetAllAsync<TestDocumentWithKey<int>, int>(keyedFilter, null, null, token),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndFindOptionsAndCancellationToken_GetsAll()
    {
        // Arrange
        var document = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        var token = new CancellationToken(true);
        var options = new FindOptions();

        SetupReaderWithFilter(document);

        // Act
        var result = await Sut.GetAllAsync<TestDocumentWithKey<int>, int>(keyedFilter, options, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetAllAsync<TestDocumentWithKey<int>, int>(keyedFilter, options, null, token),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndPartitionKey_GetsAll()
    {
        // Arrange
        var document = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        var partitionKey = Fixture.Create<string>();

        SetupReaderWithFilter(document);

        // Act
        var result = await Sut.GetAllAsync<TestDocumentWithKey<int>, int>(keyedFilter, partitionKey);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetAllAsync<TestDocumentWithKey<int>, int>(keyedFilter, null, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndFindOptionsAndPartitionKey_GetsAll()
    {
        // Arrange
        var document = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        var partitionKey = Fixture.Create<string>();
        var options = new FindOptions();

        SetupReaderWithFilter(document);

        // Act
        var result = await Sut.GetAllAsync<TestDocumentWithKey<int>, int>(keyedFilter, options, partitionKey);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetAllAsync<TestDocumentWithKey<int>, int>(keyedFilter, options, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndPartitionKeyAndCancellationToken_GetsAll()
    {
        // Arrange
        var document = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupReaderWithFilter(document);

        // Act
        var result = await Sut.GetAllAsync<TestDocumentWithKey<int>, int>(keyedFilter, partitionKey, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetAllAsync<TestDocumentWithKey<int>, int>(keyedFilter, null, partitionKey, token),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndFindOptionsAndPartitionKeyAndCancellationToken_GetsAll()
    {
        // Arrange
        var document = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        var options = new FindOptions();

        SetupReaderWithFilter(document);

        // Act
        var result = await Sut.GetAllAsync<TestDocumentWithKey<int>, int>(keyedFilter, options, partitionKey, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetAllAsync<TestDocumentWithKey<int>, int>(keyedFilter, options, partitionKey, token),
            Times.Once);
    }

    private void SetupReaderWithFilter(List<TestDocumentWithKey<int>> documents)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.GetAllAsync<TestDocumentWithKey<int>, int>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<FindOptions>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(documents);
    }

    private void SetupReader(List<TestDocumentWithKey<int>> documents)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.GetAllAsync<TestDocumentWithKey<int>, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(documents);
    }

    #endregion
}
