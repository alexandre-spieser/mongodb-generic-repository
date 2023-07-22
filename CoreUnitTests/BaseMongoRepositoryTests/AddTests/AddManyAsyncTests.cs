using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using MongoDbGenericRepository.DataAccess.Create;
using Moq;
using Xunit;

namespace CoreUnitTests.BaseMongoRepositoryTests.AddTests;

public class AddManyAsyncTests : TestMongoRepositoryContext
{
    [Fact]
    public async Task WithDocument_ShouldAddOne()
    {
        // Arrange
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        Creator = new Mock<IMongoDbCreator>();

        // Act
        await Sut.AddManyAsync(documents);

        // Assert
        Creator.Verify(x => x.AddManyAsync<TestDocument, Guid>(documents, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task WithDocumentAndCancellationToken_ShouldAddOne()
    {
        // Arrange
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var token = new CancellationToken(true);
        Creator = new Mock<IMongoDbCreator>();

        // Act
        await Sut.AddManyAsync(documents, token);

        // Assert
        Creator.Verify(x => x.AddManyAsync<TestDocument, Guid>(documents, token), Times.Once);
    }

    #region Keyed

    [Fact]
    public async Task Keyed_WithDocument_ShouldAddOne()
    {
        // Arrange
        var documents = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        Creator = new Mock<IMongoDbCreator>();

        // Act
        await Sut.AddManyAsync<TestDocumentWithKey<int>, int>(documents);

        // Assert
        Creator.Verify(x => x.AddManyAsync<TestDocumentWithKey<int>, int>(documents, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Keyed_WithDocumentAndCancellationToken_ShouldAddOne()
    {
        // Arrange
        var documents = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        var token = new CancellationToken(true);
        Creator = new Mock<IMongoDbCreator>();

        // Act
        await Sut.AddManyAsync<TestDocumentWithKey<int>, int>(documents, token);

        // Assert
        Creator.Verify(x => x.AddManyAsync<TestDocumentWithKey<int>, int>(documents, token), Times.Once);
    }

    #endregion
}
