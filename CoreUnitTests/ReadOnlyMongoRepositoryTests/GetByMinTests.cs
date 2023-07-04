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

public class GetByMinTests : TestReadOnlyMongoRepositoryContext
{
    private readonly Expression<Func<TestDocument, bool>> filter = document => document.SomeContent == "SomeContent";
    private readonly Expression<Func<TestDocument, object>> selector = document => document.SomeValue;

    [Fact]
    public void WithFilterAndSelector_GetsOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();

        SetupReader(document);

        // Act
        var result = Sut.GetByMin(filter, selector);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetByMin<TestDocument, Guid>(filter, selector, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void WithFilterAndSelectorAndCancellationToken_GetsOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var token = new CancellationToken(true);

        SetupReader(document);

        // Act
        var result = Sut.GetByMin(filter, selector, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetByMin<TestDocument, Guid>(filter, selector, null, token),
            Times.Once);
    }

    [Fact]
    public void WithFilterAndSelectorAndPartitionKey_GetsOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var partitionKey = Fixture.Create<string>();

        SetupReader(document);

        // Act
        var result = Sut.GetByMin(filter, selector, partitionKey);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetByMin<TestDocument, Guid>(filter, selector, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void WithFilterAndSelectorAndPartitionKeyAndCancellationToken_GetsOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupReader(document);

        // Act
        var result = Sut.GetByMin(filter, selector, partitionKey, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetByMin<TestDocument, Guid>(filter, selector, partitionKey, token),
            Times.Once);
    }

    private void SetupReader(TestDocument document)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.GetByMin<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<Expression<Func<TestDocument, object>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(document);
    }

    #region Keyed

    private readonly Expression<Func<TestDocumentWithKey<int>, bool>> keyedFilter = document => document.SomeContent == "SomeContent";
    private readonly Expression<Func<TestDocumentWithKey<int>, object>> keyedSelector = document => document.SomeValue;

    [Fact]
    public void Keyed_WithFilterAndSelector_GetsOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();

        SetupReader(document);

        // Act
        var result = Sut.GetByMin<TestDocumentWithKey<int>, int>(keyedFilter, keyedSelector);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetByMin<TestDocumentWithKey<int>, int>(keyedFilter, keyedSelector, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithFilterAndSelectorAndCancellationToken_GetsOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var token = new CancellationToken(true);

        SetupReader(document);

        // Act
        var result = Sut.GetByMin<TestDocumentWithKey<int>, int>(keyedFilter, keyedSelector, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetByMin<TestDocumentWithKey<int>, int>(keyedFilter, keyedSelector, null, token),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithFilterAndSelectorAndPartitionKey_GetsOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var partitionKey = Fixture.Create<string>();

        SetupReader(document);

        // Act
        var result = Sut.GetByMin<TestDocumentWithKey<int>, int>(keyedFilter, keyedSelector, partitionKey);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetByMin<TestDocumentWithKey<int>, int>(keyedFilter, keyedSelector, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithFilterAndSelectorAndPartitionKeyAndCancellationToken_GetsOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupReader(document);

        // Act
        var result = Sut.GetByMin<TestDocumentWithKey<int>, int>(keyedFilter, keyedSelector, partitionKey, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetByMin<TestDocumentWithKey<int>, int>(keyedFilter, keyedSelector, partitionKey, token),
            Times.Once);
    }

    private void SetupReader(TestDocumentWithKey<int> document)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.GetByMin<TestDocumentWithKey<int>, int>(
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(),
                    It.IsAny<Expression<Func<TestDocumentWithKey<int>, object>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(document);
    }

    #endregion
}
