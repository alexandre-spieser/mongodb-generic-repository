using System;
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

public class CountAsyncTests : TestReadOnlyMongoRepositoryContext
{
    private readonly Expression<Func<TestDocument, bool>> expression = document => document.SomeContent == "SomeContent";

    [Fact]
    public async Task WithExpression_Counts()
    {
        // Arrange
        var count = Fixture.Create<long>();

        SetupReader(count);

        // Act
        var result = await Sut.CountAsync(expression);

        // Assert
        result.Should().Be(count);
        Reader.Verify(
            x => x.CountAsync<TestDocument, Guid>(expression, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task WithExpressionAndCancellationToken_Counts()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var token = new CancellationToken(true);

        SetupReader(count);

        // Act
        var result = await Sut.CountAsync(expression, token);

        // Assert
        result.Should().Be(count);
        Reader.Verify(
            x => x.CountAsync<TestDocument, Guid>(expression, null, token),
            Times.Once);
    }

    [Fact]
    public async Task WithExpressionAndPartitionKey_Counts()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();

        SetupReader(count);

        // Act
        var result = await Sut.CountAsync(expression, partitionKey);

        // Assert
        result.Should().Be(count);
        Reader.Verify(
            x => x.CountAsync<TestDocument, Guid>(expression, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task WithExpressionAndPartitionKeyAndCancellationToken_Counts()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupReader(count);

        // Act
        var result = await Sut.CountAsync(expression, partitionKey, token);

        // Assert
        result.Should().Be(count);
        Reader.Verify(
            x => x.CountAsync<TestDocument, Guid>(expression, partitionKey, token),
            Times.Once);
    }

    private void SetupReader(long count)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.CountAsync<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);
    }

    #region Keyed

    private readonly Expression<Func<TestDocumentWithKey<int>, bool>> keyedExpression = document => document.SomeContent == "SomeContent";
    private readonly FilterDefinition<TestDocumentWithKey<int>> keyedFilter = Builders<TestDocumentWithKey<int>>.Filter.Eq(document => document.Id, 1);

    [Fact]
    public async Task Keyed_WithExpression_Counts()
    {
        // Arrange
        var count = Fixture.Create<long>();

        SetupKeyedReaderWithExpression(count);

        // Act
        var result = await Sut.CountAsync<TestDocumentWithKey<int>, int>(keyedExpression);

        // Assert
        result.Should().Be(count);
        Reader.Verify(
            x => x.CountAsync<TestDocumentWithKey<int>, int>(keyedExpression, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithExpressionAndCancellationToken_Counts()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var token = new CancellationToken(true);

        SetupKeyedReaderWithExpression(count);

        // Act
        var result = await Sut.CountAsync<TestDocumentWithKey<int>, int>(keyedExpression, token);

        // Assert
        result.Should().Be(count);
        Reader.Verify(
            x => x.CountAsync<TestDocumentWithKey<int>, int>(keyedExpression, null, token),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithExpressionAndPartitionKey_Counts()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();

        SetupKeyedReaderWithExpression(count);

        // Act
        var result = await Sut.CountAsync<TestDocumentWithKey<int>, int>(keyedExpression, partitionKey);

        // Assert
        result.Should().Be(count);
        Reader.Verify(
            x => x.CountAsync<TestDocumentWithKey<int>, int>(keyedExpression, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithExpressionAndPartitionKeyAndCancellationToken_Counts()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupKeyedReaderWithExpression(count);

        // Act
        var result = await Sut.CountAsync<TestDocumentWithKey<int>, int>(keyedExpression, partitionKey, token);

        // Assert
        result.Should().Be(count);
        Reader.Verify(
            x => x.CountAsync<TestDocumentWithKey<int>, int>(keyedExpression, partitionKey, token),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilter_Counts()
    {
        // Arrange
        var count = Fixture.Create<long>();

        SetupKeyedReaderWithFilter(count);

        // Act
        var result = await Sut.CountAsync<TestDocumentWithKey<int>, int>(keyedFilter);

        // Assert
        result.Should().Be(count);
        Reader.Verify(
            x => x.CountAsync<TestDocumentWithKey<int>, int>(keyedFilter, null, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndCountOptions_Counts()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var countOptions = new CountOptions();

        SetupKeyedReaderWithFilter(count);

        // Act
        var result = await Sut.CountAsync<TestDocumentWithKey<int>, int>(keyedFilter, countOptions);

        // Assert
        result.Should().Be(count);
        Reader.Verify(
            x => x.CountAsync<TestDocumentWithKey<int>, int>(keyedFilter, countOptions, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndCancellationToken_Counts()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var token = new CancellationToken(true);

        SetupKeyedReaderWithFilter(count);

        // Act
        var result = await Sut.CountAsync<TestDocumentWithKey<int>, int>(keyedFilter, token);

        // Assert
        result.Should().Be(count);
        Reader.Verify(
            x => x.CountAsync<TestDocumentWithKey<int>, int>(keyedFilter, null, null, token),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndCountOptionsAndCancellationToken_Counts()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var token = new CancellationToken(true);
        var countOptions = new CountOptions();

        SetupKeyedReaderWithFilter(count);

        // Act
        var result = await Sut.CountAsync<TestDocumentWithKey<int>, int>(keyedFilter, countOptions, token);

        // Assert
        result.Should().Be(count);
        Reader.Verify(
            x => x.CountAsync<TestDocumentWithKey<int>, int>(keyedFilter, countOptions, null, token),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndPartitionKey_Counts()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();

        SetupKeyedReaderWithFilter(count);

        // Act
        var result = await Sut.CountAsync<TestDocumentWithKey<int>, int>(keyedFilter, partitionKey);

        // Assert
        result.Should().Be(count);
        Reader.Verify(
            x => x.CountAsync<TestDocumentWithKey<int>, int>(keyedFilter, null, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndCountOptionsAndPartitionKey_Counts()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();
        var countOptions = new CountOptions();

        SetupKeyedReaderWithFilter(count);

        // Act
        var result = await Sut.CountAsync<TestDocumentWithKey<int>, int>(keyedFilter, countOptions, partitionKey);

        // Assert
        result.Should().Be(count);
        Reader.Verify(
            x => x.CountAsync<TestDocumentWithKey<int>, int>(keyedFilter, countOptions, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndPartitionKeyAndCancellationToken_Counts()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupKeyedReaderWithFilter(count);

        // Act
        var result = await Sut.CountAsync<TestDocumentWithKey<int>, int>(keyedFilter, partitionKey, token);

        // Assert
        result.Should().Be(count);
        Reader.Verify(
            x => x.CountAsync<TestDocumentWithKey<int>, int>(keyedFilter, null, partitionKey, token),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndCountOptionsPartitionKeyAndCancellationToken_Counts()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        var countOptions = new CountOptions();

        SetupKeyedReaderWithFilter(count);

        // Act
        var result = await Sut.CountAsync<TestDocumentWithKey<int>, int>(keyedFilter, countOptions, partitionKey, token);

        // Assert
        result.Should().Be(count);
        Reader.Verify(
            x => x.CountAsync<TestDocumentWithKey<int>, int>(keyedFilter, countOptions, partitionKey, token),
            Times.Once);
    }

    private void SetupKeyedReaderWithExpression(long count)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.CountAsync<TestDocumentWithKey<int>, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);
    }

    private void SetupKeyedReaderWithFilter(long count)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.CountAsync<TestDocumentWithKey<int>, int>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<CountOptions>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(count);
    }

    #endregion
}
