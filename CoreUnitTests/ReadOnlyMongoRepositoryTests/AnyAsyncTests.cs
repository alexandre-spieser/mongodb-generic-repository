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

public class AnyAsyncTests : TestReadOnlyMongoRepositoryContext
{
    private readonly Expression<Func<TestDocument, bool>> expression = document => document.SomeContent == "SomeContent";
    private readonly Expression<Func<TestDocumentWithKey<int>, bool>> keyedExpression = document => document.SomeContent == "SomeContent";
    private readonly FilterDefinition<TestDocumentWithKey<int>> keyedFilter = Builders<TestDocumentWithKey<int>>.Filter.Eq(x => x.Id, 1);

    [Fact]
    public async Task WithExpression_GetsResult()
    {
        // Arrange
        SetupReader();

        // Act
        var result = await Sut.AnyAsync(expression);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.AnyAsync<TestDocument, Guid>(expression, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task WithExpressionAndCancellationToken_GetsResult()
    {
        // Arrange
        var token = new CancellationToken(true);

        SetupReader();

        // Act
        var result = await Sut.AnyAsync(expression, token);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.AnyAsync<TestDocument, Guid>(expression, null, token),
            Times.Once);
    }

    [Fact]
    public async Task WithExpressionAndPartitionKey_GetsResult()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();

        SetupReader();

        // Act
        var result = await Sut.AnyAsync(expression, partitionKey);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.AnyAsync<TestDocument, Guid>(expression, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task WithExpressionAndPartitionKeyAndCancellationToken_GetsResult()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupReader();

        // Act
        var result = await Sut.AnyAsync(expression, partitionKey, token);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.AnyAsync<TestDocument, Guid>(expression, partitionKey, token),
            Times.Once);
    }

    #region keyed

    [Fact]
    public async Task Keyed_WithExpression_GetsResult()
    {
        // Arrange
        SetupKeyedReaderWithExpression();

        // Act
        var result = await Sut.AnyAsync<TestDocumentWithKey<int>, int>(keyedExpression);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.AnyAsync<TestDocumentWithKey<int>, int>(keyedExpression, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithExpressionAndCancellationToken_GetsResult()
    {
        // Arrange
        var token = new CancellationToken(true);

        SetupKeyedReaderWithExpression();

        // Act
        var result = await Sut.AnyAsync<TestDocumentWithKey<int>, int>(keyedExpression, token);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.AnyAsync<TestDocumentWithKey<int>, int>(keyedExpression, null, token),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithExpressionAndPartitionKey_GetsResult()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();

        SetupKeyedReaderWithExpression();

        // Act
        var result = await Sut.AnyAsync<TestDocumentWithKey<int>, int>(keyedExpression, partitionKey);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.AnyAsync<TestDocumentWithKey<int>, int>(keyedExpression, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithExpressionAndPartitionKeyAndCancellationToken_GetsResult()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupKeyedReaderWithExpression();

        // Act
        var result = await Sut.AnyAsync<TestDocumentWithKey<int>, int>(keyedExpression, partitionKey, token);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.AnyAsync<TestDocumentWithKey<int>, int>(keyedExpression, partitionKey, token),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilter_GetsResult()
    {
        // Arrange
        SetupKeyedReaderWithFilter();

        // Act
        var result = await Sut.AnyAsync<TestDocumentWithKey<int>, int>(keyedFilter);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.AnyAsync<TestDocumentWithKey<int>, int>(keyedFilter, null, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndOptions_GetsResult()
    {
        // Arrange
        var options = new CountOptions();
        SetupKeyedReaderWithFilter();

        // Act
        var result = await Sut.AnyAsync<TestDocumentWithKey<int>, int>(keyedFilter, options);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.AnyAsync<TestDocumentWithKey<int>, int>(keyedFilter, options, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndCancellationToken_GetsResult()
    {
        // Arrange
        var token = new CancellationToken(true);

        SetupKeyedReaderWithFilter();

        // Act
        var result = await Sut.AnyAsync<TestDocumentWithKey<int>, int>(keyedFilter, token);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.AnyAsync<TestDocumentWithKey<int>, int>(keyedFilter, null, null, token),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndOptionsAndCancellationToken_GetsResult()
    {
        // Arrange
        var token = new CancellationToken(true);
        var options = new CountOptions();

        SetupKeyedReaderWithFilter();

        // Act
        var result = await Sut.AnyAsync<TestDocumentWithKey<int>, int>(keyedFilter, options, token);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.AnyAsync<TestDocumentWithKey<int>, int>(keyedFilter, options, null, token),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndPartitionKey_GetsResult()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();

        SetupKeyedReaderWithFilter();

        // Act
        var result = await Sut.AnyAsync<TestDocumentWithKey<int>, int>(keyedFilter, partitionKey);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.AnyAsync<TestDocumentWithKey<int>, int>(keyedFilter, null, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndCountOptionsAndPartitionKey_GetsResult()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var options = new CountOptions();

        SetupKeyedReaderWithFilter();

        // Act
        var result = await Sut.AnyAsync<TestDocumentWithKey<int>, int>(keyedFilter, options, partitionKey);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.AnyAsync<TestDocumentWithKey<int>, int>(keyedFilter, options, partitionKey, CancellationToken.None),
            Times.Once);
    }
    [Fact]
    public async Task Keyed_WithFilterAndPartitionKeyAndCancellationToken_GetsResult()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupKeyedReaderWithFilter();

        // Act
        var result = await Sut.AnyAsync<TestDocumentWithKey<int>, int>(keyedFilter, partitionKey, token);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.AnyAsync<TestDocumentWithKey<int>, int>(keyedFilter, null, partitionKey, token),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_WithFilterAndCountOptionsAndPartitionKeyAndCancellationToken_GetsResult()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        var options = new CountOptions();

        SetupKeyedReaderWithFilter();

        // Act
        var result = await Sut.AnyAsync<TestDocumentWithKey<int>, int>(keyedFilter, options, partitionKey, token);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.AnyAsync<TestDocumentWithKey<int>, int>(keyedFilter, options, partitionKey, token),
            Times.Once);
    }

    private void SetupKeyedReaderWithExpression()
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.AnyAsync<TestDocumentWithKey<int>, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
    }

    private void SetupKeyedReaderWithFilter()
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.AnyAsync<TestDocumentWithKey<int>, int>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<CountOptions>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
    }

    #endregion

    private void SetupReader()
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.AnyAsync<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
    }
}
