using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using FluentAssertions;
using MongoDbGenericRepository.DataAccess.Read;
using Moq;
using Xunit;

namespace CoreUnitTests.ReadOnlyMongoRepositoryTests;

public class SumByAsyncTests : TestReadOnlyMongoRepositoryContext
{
    private readonly Expression<Func<TestDocument, decimal>> decimalSelector = document => document.SomeDecimalValue;
    private readonly Expression<Func<TestDocument, bool>> filter = document => document.SomeContent == "SomeContent";
    private readonly Expression<Func<TestDocument, int>> intSelector = document => document.SomeValue;

    [Fact]
    public async Task Int_WithFilterAndSelector_Sums()
    {
        // Arrange
        var expected = Fixture.Create<int>();

        SetupReaderInt(expected);

        // Act
        var result = await Sut.SumByAsync(filter, intSelector);

        // Assert
        result.Should().Be(expected);
        Reader.Verify(
            x => x.SumByAsync<TestDocument, Guid>(filter, intSelector, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Int_WithFilterAndSelectorAndCancellationToken_Sums()
    {
        // Arrange
        var expected = Fixture.Create<int>();
        var token = new CancellationToken(true);

        SetupReaderInt(expected);

        // Act
        var result = await Sut.SumByAsync(filter, intSelector, token);

        // Assert
        result.Should().Be(expected);
        Reader.Verify(
            x => x.SumByAsync<TestDocument, Guid>(filter, intSelector, null, token),
            Times.Once);
    }

    [Fact]
    public async Task Int_WithFilterAndSelectorAndPartitionKey_Sums()
    {
        // Arrange
        var expected = Fixture.Create<int>();
        var partitionKey = Fixture.Create<string>();

        SetupReaderInt(expected);

        // Act
        var result = await Sut.SumByAsync(filter, intSelector, partitionKey);

        // Assert
        result.Should().Be(expected);
        Reader.Verify(
            x => x.SumByAsync<TestDocument, Guid>(filter, intSelector, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Int_WithFilterAndSelectorAndPartitionKeyAndCancellationToken_Sums()
    {
        // Arrange
        var expected = Fixture.Create<int>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupReaderInt(expected);

        // Act
        var result = await Sut.SumByAsync(filter, intSelector, partitionKey, token);

        // Assert
        result.Should().Be(expected);
        Reader.Verify(
            x => x.SumByAsync<TestDocument, Guid>(filter, intSelector, partitionKey, token),
            Times.Once);
    }

    [Fact]
    public async Task Decimal_WithFilterAndSelector_Sums()
    {
        // Arrange
        var expected = Fixture.Create<decimal>();

        SetupReaderDecimal(expected);

        // Act
        var result = await Sut.SumByAsync(filter, decimalSelector);

        // Assert
        result.Should().Be(expected);
        Reader.Verify(
            x => x.SumByAsync<TestDocument, Guid>(filter, decimalSelector, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Decimal_WithFilterAndSelectorAndCancellationToken_Sums()
    {
        // Arrange
        var expected = Fixture.Create<decimal>();
        var token = new CancellationToken(true);

        SetupReaderDecimal(expected);

        // Act
        var result = await Sut.SumByAsync(filter, decimalSelector, token);

        // Assert
        result.Should().Be(expected);
        Reader.Verify(
            x => x.SumByAsync<TestDocument, Guid>(filter, decimalSelector, null, token),
            Times.Once);
    }

    [Fact]
    public async Task Decimal_WithFilterAndSelectorAndPartitionKey_Sums()
    {
        // Arrange
        var expected = Fixture.Create<decimal>();
        var partitionKey = Fixture.Create<string>();

        SetupReaderDecimal(expected);

        // Act
        var result = await Sut.SumByAsync(filter, decimalSelector, partitionKey);

        // Assert
        result.Should().Be(expected);
        Reader.Verify(
            x => x.SumByAsync<TestDocument, Guid>(filter, decimalSelector, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Decimal_WithFilterAndSelectorAndPartitionKeyAndCancellationToken_Sums()
    {
        // Arrange
        var expected = Fixture.Create<decimal>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupReaderDecimal(expected);

        // Act
        var result = await Sut.SumByAsync(filter, decimalSelector, partitionKey, token);

        // Assert
        result.Should().Be(expected);
        Reader.Verify(
            x => x.SumByAsync<TestDocument, Guid>(filter, decimalSelector, partitionKey, token),
            Times.Once);
    }

    private void SetupReaderInt(int expected)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.SumByAsync<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, int>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);
    }

    private void SetupReaderDecimal(decimal expected)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.SumByAsync<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, decimal>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);
    }

    #region Keyed

    private readonly Expression<Func<TestDocumentWithKey<int>, bool>> keyedFilter = document => document.SomeContent == "SomeContent";
    private readonly Expression<Func<TestDocumentWithKey<int>, int>> keyedIntSelector = document => document.SomeValue;
    private readonly Expression<Func<TestDocumentWithKey<int>, decimal>> keyedDecimalSelector = document => document.SomeDecimalValue;

    [Fact]
    public async Task Keyed_Int_WithFilterAndSelector_Sums()
    {
        // Arrange
        var expected = Fixture.Create<int>();

        SetupKeyedReaderInt(expected);

        // Act
        var result = await Sut.SumByAsync<TestDocumentWithKey<int>, int>(keyedFilter, keyedIntSelector);

        // Assert
        result.Should().Be(expected);
        Reader.Verify(
            x => x.SumByAsync<TestDocumentWithKey<int>, int>(keyedFilter, keyedIntSelector, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_Int_WithFilterAndSelectorAndCancellationToken_Sums()
    {
        // Arrange
        var expected = Fixture.Create<int>();
        var token = new CancellationToken(true);

        SetupKeyedReaderInt(expected);

        // Act
        var result = await Sut.SumByAsync<TestDocumentWithKey<int>, int>(keyedFilter, keyedIntSelector, token);

        // Assert
        result.Should().Be(expected);
        Reader.Verify(
            x => x.SumByAsync<TestDocumentWithKey<int>, int>(keyedFilter, keyedIntSelector, null, token),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_Int_WithFilterAndSelectorAndPartitionKey_Sums()
    {
        // Arrange
        var expected = Fixture.Create<int>();
        var partitionKey = Fixture.Create<string>();

        SetupKeyedReaderInt(expected);

        // Act
        var result = await Sut.SumByAsync<TestDocumentWithKey<int>, int>(keyedFilter, keyedIntSelector, partitionKey);

        // Assert
        result.Should().Be(expected);
        Reader.Verify(
            x => x.SumByAsync<TestDocumentWithKey<int>, int>(keyedFilter, keyedIntSelector, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_Int_WithFilterAndSelectorAndPartitionKeyAndCancellationToken_Sums()
    {
        // Arrange
        var expected = Fixture.Create<int>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupKeyedReaderInt(expected);

        // Act
        var result = await Sut.SumByAsync<TestDocumentWithKey<int>, int>(keyedFilter, keyedIntSelector, partitionKey, token);

        // Assert
        result.Should().Be(expected);
        Reader.Verify(
            x => x.SumByAsync<TestDocumentWithKey<int>, int>(keyedFilter, keyedIntSelector, partitionKey, token),
            Times.Once);
    }

        [Fact]
    public async Task Keyed_Decimal_WithFilterAndSelector_Sums()
    {
        // Arrange
        var expected = Fixture.Create<decimal>();

        SetupKeyedReaderDecimal(expected);

        // Act
        var result = await Sut.SumByAsync<TestDocumentWithKey<int>, int>(keyedFilter, keyedDecimalSelector);

        // Assert
        result.Should().Be(expected);
        Reader.Verify(
            x => x.SumByAsync<TestDocumentWithKey<int>, int>(keyedFilter, keyedDecimalSelector, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_Decimal_WithFilterAndSelectorAndCancellationToken_Sums()
    {
        // Arrange
        var expected = Fixture.Create<decimal>();
        var token = new CancellationToken(true);

        SetupKeyedReaderDecimal(expected);

        // Act
        var result = await Sut.SumByAsync<TestDocumentWithKey<int>, int>(keyedFilter, keyedDecimalSelector, token);

        // Assert
        result.Should().Be(expected);
        Reader.Verify(
            x => x.SumByAsync<TestDocumentWithKey<int>, int>(keyedFilter, keyedDecimalSelector, null, token),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_Decimal_WithFilterAndSelectorAndPartitionKey_Sums()
    {
        // Arrange
        var expected = Fixture.Create<decimal>();
        var partitionKey = Fixture.Create<string>();

        SetupKeyedReaderDecimal(expected);

        // Act
        var result = await Sut.SumByAsync<TestDocumentWithKey<int>, int>(keyedFilter, keyedDecimalSelector, partitionKey);

        // Assert
        result.Should().Be(expected);
        Reader.Verify(
            x => x.SumByAsync<TestDocumentWithKey<int>, int>(keyedFilter, keyedDecimalSelector, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task Keyed_Decimal_WithFilterAndSelectorAndPartitionKeyAndCancellationToken_Sums()
    {
        // Arrange
        var expected = Fixture.Create<decimal>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupKeyedReaderDecimal(expected);

        // Act
        var result = await Sut.SumByAsync<TestDocumentWithKey<int>, int>(keyedFilter, keyedDecimalSelector, partitionKey, token);

        // Assert
        result.Should().Be(expected);
        Reader.Verify(
            x => x.SumByAsync<TestDocumentWithKey<int>, int>(keyedFilter, keyedDecimalSelector, partitionKey, token),
            Times.Once);
    }

    private void SetupKeyedReaderInt(int expected)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.SumByAsync<TestDocumentWithKey<int>, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>,int>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);
    }

    private void SetupKeyedReaderDecimal(decimal expected)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.SumByAsync<TestDocumentWithKey<int>, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, decimal>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);
    }

    #endregion
}
