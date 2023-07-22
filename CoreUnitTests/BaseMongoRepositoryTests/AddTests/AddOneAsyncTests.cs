using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using MongoDbGenericRepository.DataAccess.Create;
using Moq;
using Xunit;

namespace CoreUnitTests.BaseMongoRepositoryTests.AddTests;

public class AddOneAsyncTests : TestMongoRepositoryContext
{
    [Fact]
    public async Task WithDocument_ShouldAddOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        Creator = new Mock<IMongoDbCreator>();

        // Act
        await Sut.AddOneAsync(document);

        // Assert
        Creator.Verify(x => x.AddOneAsync<TestDocument, Guid>(document, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task WithDocumentAndCancellationToken_ShouldAddOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var token = new CancellationToken(true);
        Creator = new Mock<IMongoDbCreator>();

        // Act
        await Sut.AddOneAsync(document, token);

        // Assert
        Creator.Verify(x => x.AddOneAsync<TestDocument, Guid>(document, token), Times.Once);
    }

    #region Keyed

    [Fact]
    public async Task Keyed_WithDocument_ShouldAddOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        Creator = new Mock<IMongoDbCreator>();

        // Act
        await Sut.AddOneAsync<TestDocumentWithKey<int>, int>(document);

        // Assert
        Creator.Verify(x => x.AddOneAsync<TestDocumentWithKey<int>, int>(document, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Keyed_WithDocumentAndCancellationToken_ShouldAddOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var token = new CancellationToken(true);
        Creator = new Mock<IMongoDbCreator>();

        // Act
        await Sut.AddOneAsync<TestDocumentWithKey<int>, int>(document, token);

        // Assert
        Creator.Verify(x => x.AddOneAsync<TestDocumentWithKey<int>, int>(document, token), Times.Once);
    }

    #endregion
}
