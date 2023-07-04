using System;
using System.Threading;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using FluentAssertions;
using MongoDbGenericRepository.DataAccess.Read;
using Moq;
using Xunit;

namespace CoreUnitTests.ReadOnlyMongoRepositoryTests;

public class GetByIdTests : TestReadOnlyMongoRepositoryContext
{
    [Fact]
    public void WithId_Gets()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();

        SetupReader(document);

        // Act
        var result = Sut.GetById<TestDocument>(document.Id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetById<TestDocument, Guid>(document.Id, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void WithIdAndCancellationToken_Gets()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var token = new CancellationToken(true);

        SetupReader(document);

        // Act
        var result = Sut.GetById<TestDocument>(document.Id, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetById<TestDocument, Guid>(document.Id, null, token),
            Times.Once);
    }

    [Fact]
    public void WithIdAndPartitionKey_Gets()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var partitionKey = Fixture.Create<string>();

        SetupReader(document);

        // Act
        var result = Sut.GetById<TestDocument>(document.Id, partitionKey);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetById<TestDocument, Guid>(document.Id, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void WithIdAndPartitionKeyAndCancellationToken_Gets()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupReader(document);

        // Act
        var result = Sut.GetById<TestDocument>(document.Id, partitionKey, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetById<TestDocument, Guid>(document.Id, partitionKey, token),
            Times.Once);
    }

    private void SetupReader(TestDocument document)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.GetById<TestDocument, Guid>(
                    It.IsAny<Guid>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(document);
    }

    #region Keyed

    [Fact]
    public void Keyed_WithId_Gets()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();

        SetupKeyedReader(document);

        // Act
        var result = Sut.GetById<TestDocumentWithKey<int>, int>(document.Id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetById<TestDocumentWithKey<int>, int>(document.Id, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithIdAndCancellationToken_Gets()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var token = new CancellationToken(true);

        SetupKeyedReader(document);

        // Act
        var result = Sut.GetById<TestDocumentWithKey<int>, int>(document.Id, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetById<TestDocumentWithKey<int>, int>(document.Id, null, token),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithIdAndPartitionKey_Gets()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var partitionKey = Fixture.Create<string>();

        SetupKeyedReader(document);

        // Act
        var result = Sut.GetById<TestDocumentWithKey<int>, int>(document.Id, partitionKey);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetById<TestDocumentWithKey<int>, int>(document.Id, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void Keyed_WithIdAndPartitionKeyAndCancellationToken_Gets()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupKeyedReader(document);

        // Act
        var result = Sut.GetById<TestDocumentWithKey<int>, int>(document.Id, partitionKey, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetById<TestDocumentWithKey<int>, int>(document.Id, partitionKey, token),
            Times.Once);
    }

    private void SetupKeyedReader(TestDocumentWithKey<int> document)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.GetById<TestDocumentWithKey<int>, int>(
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(document);
    }

    #endregion
}
