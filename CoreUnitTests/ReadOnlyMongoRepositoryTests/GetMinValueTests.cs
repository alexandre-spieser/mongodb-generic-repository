using System;
using System.Linq.Expressions;
using System.Threading;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using FluentAssertions;
using MongoDbGenericRepository.DataAccess.Read;
using Moq;
using Xunit;

namespace CoreUnitTests.ReadOnlyMongoRepositoryTests;

public class GetMinValueTests : TestReadOnlyMongoRepositoryContext
{
    private readonly Expression<Func<TestDocument, bool>> filter = document => document.SomeContent == "SomeContent";
    private readonly Expression<Func<TestDocument, int>> selector = document => document.SomeValue;

    [Fact]
    public void WithFilterAndSelector_GetsMinValue()
    {
        // Arrange
        var value = Fixture.Create<int>();

        SetupReader(value);

        // Act
        var result = Sut.GetMinValue(filter, selector);

        // Assert
        result.Should().Be(value);
        Reader.Verify(
            x => x.GetMinValue<TestDocument, Guid, int>(filter, selector, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void WithFilterAndSelectorAndCancellationToken_GetsMinValue()
    {
        // Arrange
        var value = Fixture.Create<int>();
        var token = new CancellationToken(true);

        SetupReader(value);

        // Act
        var result = Sut.GetMinValue(filter, selector, token);

        // Assert
        result.Should().Be(value);
        Reader.Verify(
            x => x.GetMinValue<TestDocument, Guid, int>(filter, selector, null, token),
            Times.Once);
    }

    [Fact]
    public void WithFilterAndSelectorAndPartitionKey_GetsMinValue()
    {
        // Arrange
        var value = Fixture.Create<int>();
        var partitionKey = Fixture.Create<string>();

        SetupReader(value);

        // Act
        var result = Sut.GetMinValue(filter, selector, partitionKey);

        // Assert
        result.Should().Be(value);
        Reader.Verify(
            x => x.GetMinValue<TestDocument, Guid, int>(filter, selector, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void WithFilterAndSelectorAndPartitionKeyAndCancellationToken_GetsMinValue()
    {
        // Arrange
        var value = Fixture.Create<int>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupReader(value);

        // Act
        var result = Sut.GetMinValue(filter, selector, partitionKey, token);

        // Assert
        result.Should().Be(value);
        Reader.Verify(
            x => x.GetMinValue<TestDocument, Guid, int>(filter, selector, partitionKey, token),
            Times.Once);
    }

    private void SetupReader(int value)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.GetMinValue<TestDocument, Guid, int>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, int>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(value);
    }

    #region Keyed

    private readonly Expression<Func<TestDocumentWithKey<int>, bool>> keyedFilter = document => document.SomeContent == "SomeContent";
    private readonly Expression<Func<TestDocumentWithKey<int>, int>> keyedSelector = document => document.SomeValue;

    [Fact]
    public void Keyed_WithFilterAndSelector_GetsMinValue()
    {
        // Arrange
        var value = Fixture.Create<int>();

        SetupKeyedReader(value);

        // Act
        var result = Sut.GetMinValue<TestDocumentWithKey<int>, int, int>(keyedFilter, keyedSelector);

        // Assert
        result.Should().Be(value);
        Reader.Verify(
            x => x.GetMinValue<TestDocumentWithKey<int>, int, int>(keyedFilter, keyedSelector, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithFilterAndSelectorAndCancellationToken_GetsMinValue()
    {
        // Arrange
        var value = Fixture.Create<int>();
        var token = new CancellationToken(true);

        SetupKeyedReader(value);

        // Act
        var result = Sut.GetMinValue<TestDocumentWithKey<int>, int, int>(keyedFilter, keyedSelector, token);

        // Assert
        result.Should().Be(value);
        Reader.Verify(
            x => x.GetMinValue<TestDocumentWithKey<int>, int, int>(keyedFilter, keyedSelector, null, token),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithFilterAndSelectorAndPartitionKey_GetsMinValue()
    {
        // Arrange
        var value = Fixture.Create<int>();
        var partitionKey = Fixture.Create<string>();

        SetupKeyedReader(value);

        // Act
        var result = Sut.GetMinValue<TestDocumentWithKey<int>, int, int>(keyedFilter, keyedSelector, partitionKey);

        // Assert
        result.Should().Be(value);
        Reader.Verify(
            x => x.GetMinValue<TestDocumentWithKey<int>, int, int>(keyedFilter, keyedSelector, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithFilterAndSelectorAndPartitionKeyAndCancellationToken_GetsMinValue()
    {
        // Arrange
        var value = Fixture.Create<int>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupKeyedReader(value);

        // Act
        var result = Sut.GetMinValue<TestDocumentWithKey<int>, int, int>(keyedFilter, keyedSelector, partitionKey, token);

        // Assert
        result.Should().Be(value);
        Reader.Verify(
            x => x.GetMinValue<TestDocumentWithKey<int>, int, int>(keyedFilter, keyedSelector, partitionKey, token),
            Times.Once);
    }

    private void SetupKeyedReader(int value)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.GetMinValue<TestDocumentWithKey<int>, int, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, int>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(value);
    }

    #endregion
}
