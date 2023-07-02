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

namespace CoreUnitTests.KeyedReadOnlyMongoRepositoryTests;

public class GetOneAsyncTests : TestKeyedReadOnlyMongoRepositoryContext<Guid>
{
    private readonly Expression<Func<TestDocument, bool>> filter = document => document.SomeContent == "SomeContent";

    [Fact]
    public async Task WithFilter_GetsOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();

        SetupReader(document);

        // Act
        var result = await Sut.GetOneAsync(filter);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetOneAsync<TestDocument, Guid>(filter, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task WithFilterAndCancellationToken_GetsOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var token = new CancellationToken(true);

        SetupReader(document);

        // Act
        var result = await Sut.GetOneAsync(filter, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetOneAsync<TestDocument, Guid>(filter, null, token),
            Times.Once);
    }

    [Fact]
    public async Task WithFilterAndPartitionKey_GetsOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var partitionKey = Fixture.Create<string>();

        SetupReader(document);

        // Act
        var result = await Sut.GetOneAsync(filter, partitionKey);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetOneAsync<TestDocument, Guid>(filter, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task WithFilterAndPartitionKeyAndCancellationToken_GetsOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupReader(document);

        // Act
        var result = await Sut.GetOneAsync(filter, partitionKey, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetOneAsync<TestDocument, Guid>(filter, partitionKey, token),
            Times.Once);
    }

    private void SetupReader(TestDocument document)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.GetOneAsync<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(document);
    }
}
