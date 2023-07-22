using System;
using System.Linq;
using System.Threading;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using MongoDbGenericRepository.DataAccess.Create;
using Moq;
using Xunit;

namespace CoreUnitTests.BaseMongoRepositoryTests.AddTests;

public class AddManyTests : TestMongoRepositoryContext
{

    [Fact]
    public void WithDocument_ShouldAddOne()
    {
        // Arrange
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        Creator = new Mock<IMongoDbCreator>();

        // Act
        Sut.AddMany(documents);

        // Assert
        Creator.Verify(x => x.AddMany<TestDocument, Guid>(documents, CancellationToken.None), Times.Once);
    }

    [Fact]
    public void WithDocumentAndCancellationToken_ShouldAddOne()
    {
        // Arrange
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var token = new CancellationToken(true);
        Creator = new Mock<IMongoDbCreator>();

        // Act
        Sut.AddMany(documents, token);

        // Assert
        Creator.Verify(x => x.AddMany<TestDocument, Guid>(documents, token), Times.Once);
    }

    #region Keyed

    [Fact]
    public void Keyed_WithDocument_ShouldAddOne()
    {
        // Arrange
        var documents = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        Creator = new Mock<IMongoDbCreator>();

        // Act
        Sut.AddMany<TestDocumentWithKey<int>, int>(documents);

        // Assert
        Creator.Verify(x => x.AddMany<TestDocumentWithKey<int>, int>(documents, CancellationToken.None), Times.Once);
    }

    [Fact]
    public void Keyed_WithDocumentAndCancellationToken_ShouldAddOne()
    {
        // Arrange
        var documents = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        var token = new CancellationToken(true);
        Creator = new Mock<IMongoDbCreator>();

        // Act
        Sut.AddMany<TestDocumentWithKey<int>, int>(documents, token);

        // Assert
        Creator.Verify(x => x.AddMany<TestDocumentWithKey<int>, int>(documents, token), Times.Once);
    }

    #endregion
}
