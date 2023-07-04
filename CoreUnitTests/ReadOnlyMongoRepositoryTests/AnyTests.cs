using System;
using System.Linq.Expressions;
using System.Threading;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using FluentAssertions;
using MongoDB.Driver;
using MongoDbGenericRepository.DataAccess.Read;
using Moq;
using Xunit;

namespace CoreUnitTests.ReadOnlyMongoRepositoryTests;

public class AnyTests : TestReadOnlyMongoRepositoryContext
{
    private readonly Expression<Func<TestDocument, bool>> expression = document => document.SomeContent == "SomeContent";

    [Fact]
    public void WithExpression_GetsResult()
    {
        // Arrange
        SetupReader();

        // Act
        var result = Sut.Any(expression);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.Any<TestDocument, Guid>(expression, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void WithExpressionAndCancellationToken_GetsResult()
    {
        // Arrange
        var token = new CancellationToken(true);

        SetupReader();

        // Act
        var result = Sut.Any(expression, token);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.Any<TestDocument, Guid>(expression, null, token),
            Times.Once);
    }

    [Fact]
    public void WithExpressionAndPartitionKey_GetsResult()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();

        SetupReader();

        // Act
        var result = Sut.Any(expression, partitionKey);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.Any<TestDocument, Guid>(expression, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void WithExpressionAndPartitionKeyAndCancellationToken_GetsResult()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupReader();

        // Act
        var result = Sut.Any(expression, partitionKey, token);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.Any<TestDocument, Guid>(expression, partitionKey, token),
            Times.Once);
    }

    private void SetupReader()
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.Any<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);
    }

    #region Keyed

    private readonly Expression<Func<TestDocumentWithKey<int>, bool>> keyedExpression = document => document.SomeContent == "SomeContent";
    private readonly FilterDefinition<TestDocumentWithKey<int>> keyedFilter = Builders<TestDocumentWithKey<int>>.Filter.Eq(document => document.SomeContent, "SomeContent");

    [Fact]
    public void Keyed_WithExpression_GetsResult()
    {
        // Arrange
        SetupKeyedReaderWithExpression();

        // Act
        var result = Sut.Any<TestDocumentWithKey<int>, int>(keyedExpression);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.Any<TestDocumentWithKey<int>, int>(keyedExpression, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithExpressionAndCancellationToken_GetsResult()
    {
        // Arrange
        var token = new CancellationToken(true);

        SetupKeyedReaderWithExpression();

        // Act
        var result = Sut.Any<TestDocumentWithKey<int>, int>(keyedExpression, token);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.Any<TestDocumentWithKey<int>, int>(keyedExpression, null, token),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithExpressionAndPartitionKey_GetsResult()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();

        SetupKeyedReaderWithExpression();

        // Act
        var result = Sut.Any<TestDocumentWithKey<int>, int>(keyedExpression, partitionKey);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.Any<TestDocumentWithKey<int>, int>(keyedExpression, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithExpressionAndPartitionKeyAndCancellationToken_GetsResult()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupKeyedReaderWithExpression();

        // Act
        var result = Sut.Any<TestDocumentWithKey<int>, int>(keyedExpression, partitionKey, token);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.Any<TestDocumentWithKey<int>, int>(keyedExpression, partitionKey, token),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithFilter_GetsResult()
    {
        // Arrange
        SetupKeyedReaderWithFilter();

        // Act
        var result = Sut.Any<TestDocumentWithKey<int>, int>(keyedFilter);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.Any<TestDocumentWithKey<int>, int>(keyedFilter, null, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithFilterAndCancellationToken_GetsResult()
    {
        // Arrange
        var token = new CancellationToken(true);

        SetupKeyedReaderWithFilter();

        // Act
        var result = Sut.Any<TestDocumentWithKey<int>, int>(keyedFilter, token);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.Any<TestDocumentWithKey<int>, int>(keyedFilter, null, null, token),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithFilterAndOptions_GetsResult()
    {
        // Arrange
        var options = new CountOptions();
        SetupKeyedReaderWithFilter();

        // Act
        var result = Sut.Any<TestDocumentWithKey<int>, int>(keyedFilter, options);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.Any<TestDocumentWithKey<int>, int>(keyedFilter, options, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithFilterAndOptionsAndCancellationToken_GetsResult()
    {
        // Arrange
        var options = new CountOptions();
        var token = new CancellationToken(true);
        SetupKeyedReaderWithFilter();

        // Act
        var result = Sut.Any<TestDocumentWithKey<int>, int>(keyedFilter, options, token);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.Any<TestDocumentWithKey<int>, int>(keyedFilter, options, null, token),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithFilterAndPartitionKey_GetsResult()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();

        SetupKeyedReaderWithFilter();

        // Act
        var result = Sut.Any<TestDocumentWithKey<int>, int>(keyedFilter, partitionKey);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.Any<TestDocumentWithKey<int>, int>(keyedFilter, null, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithFilterAndOptionsAndPartitionKey_GetsResult()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var options = new CountOptions();

        SetupKeyedReaderWithFilter();

        // Act
        var result = Sut.Any<TestDocumentWithKey<int>, int>(keyedFilter, options, partitionKey);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.Any<TestDocumentWithKey<int>, int>(keyedFilter, options, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithFilterAndPartitionKeyAndCancellationToken_GetsResult()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupKeyedReaderWithFilter();

        // Act
        var result = Sut.Any<TestDocumentWithKey<int>, int>(keyedFilter, partitionKey, token);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.Any<TestDocumentWithKey<int>, int>(keyedFilter, null, partitionKey, token),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithFilterAndOptionsAndPartitionKeyAndCancellationToken_GetsResult()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);
        var options = new CountOptions();

        SetupKeyedReaderWithFilter();

        // Act
        var result = Sut.Any<TestDocumentWithKey<int>, int>(keyedFilter, options, partitionKey, token);

        // Assert
        result.Should().BeTrue();
        Reader.Verify(
            x => x.Any<TestDocumentWithKey<int>, int>(keyedFilter, options, partitionKey, token),
            Times.Once);
    }

    private void SetupKeyedReaderWithExpression()
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.Any<TestDocumentWithKey<int>, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);
    }

    private void SetupKeyedReaderWithFilter()
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.Any<TestDocumentWithKey<int>, int>(
                    It.IsAny<FilterDefinition<TestDocumentWithKey<int>>>(),
                    It.IsAny<CountOptions>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(true);
    }

    #endregion
}
