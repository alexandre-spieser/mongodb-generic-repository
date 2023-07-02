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

public class GetOneTests : TestKeyedReadOnlyMongoRepositoryContext<Guid>
{
    private readonly Expression<Func<TestDocument, bool>> filter = document => document.SomeContent == "SomeContent";

    [Fact]
    public void WithId_GetsOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();

        SetupReader(document);

        // Act
        var result = Sut.GetOne(filter);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetOne<TestDocument, Guid>(filter, null, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void WithIdAndCancellationToken_GetsOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var token = new CancellationToken(true);

        SetupReader(document);

        // Act
        var result = Sut.GetOne(filter, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetOne<TestDocument, Guid>(filter, null, token),
            Times.Once);
    }

    [Fact]
    public void WithIdAndPartitionKey_GetsOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var partitionKey = Fixture.Create<string>();

        SetupReader(document);

        // Act
        var result = Sut.GetOne(filter, partitionKey);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetOne<TestDocument, Guid>(filter, partitionKey, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public void WithIdAndPartitionKeyAndCancellationToken_GetsOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        SetupReader(document);

        // Act
        var result = Sut.GetOne(filter, partitionKey, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(document);
        Reader.Verify(
            x => x.GetOne<TestDocument, Guid>(filter, partitionKey, token),
            Times.Once);
    }

    private void SetupReader(TestDocument document)
    {
        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(
                x => x.GetOne<TestDocument, Guid>(
                    It.IsAny<Expression<Func<TestDocument, bool>>>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(document);
    }
}
