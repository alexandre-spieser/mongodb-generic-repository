using System;
using System.Linq;
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

namespace CoreUnitTests.KeyedReadOnlyMongoRepositoryTests;

public class GetByIdAsyncTests : TestKeyedReadOnlyMongoRepositoryContext<Guid>
{
    [Fact]
    public async Task WithId_Gets()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();

        SetupReader(document);

        // Act
        var result = await Sut.GetByIdAsync<TestDocument>(document.Id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetByIdAsync<TestDocument, Guid>(document.Id, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task WithIdAndCancellationToken_Gets()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var token = new CancellationToken(true);

        SetupReader(document);

        // Act
        var result = await Sut.GetByIdAsync<TestDocument>(document.Id, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetByIdAsync<TestDocument, Guid>(document.Id, null, token),
            Times.Once);
    }

    [Fact]
    public async Task WithIdAndPartitionKey_Gets()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var partitionKey = Fixture.Create<string>();

        SetupReader(document);

        // Act
        var result = await Sut.GetByIdAsync<TestDocument>(document.Id, partitionKey);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetByIdAsync<TestDocument, Guid>(document.Id, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task WithIdAndPartitionKeyAndCancellationToken_Gets()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupReader(document);

        // Act
        var result = await Sut.GetByIdAsync<TestDocument>(document.Id, partitionKey, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetByIdAsync<TestDocument, Guid>(document.Id, partitionKey, token),
            Times.Once);
    }

    private void SetupReader(TestDocument document)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.GetByIdAsync<TestDocument, Guid>(
                    It.IsAny<Guid>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(document);
    }
}
