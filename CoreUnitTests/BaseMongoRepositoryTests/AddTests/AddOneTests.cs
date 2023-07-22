using System;
using System.Threading;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using MongoDbGenericRepository.DataAccess.Create;
using Moq;
using Xunit;

namespace CoreUnitTests.BaseMongoRepositoryTests.AddTests;

public class AddOneTests : TestMongoRepositoryContext
{
    [Fact]
    public void WithDocument_ShouldAddOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        Creator = new Mock<IMongoDbCreator>();

        // Act
        Sut.AddOne(document);

        // Assert
        Creator.Verify(x => x.AddOne<TestDocument, Guid>(document, CancellationToken.None), Times.Once);
    }

    [Fact]
    public void WithDocumentAndCancellationToken_ShouldAddOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var token = new CancellationToken(true);
        Creator = new Mock<IMongoDbCreator>();

        // Act
        Sut.AddOne(document, token);

        // Assert
        Creator.Verify(x => x.AddOne<TestDocument, Guid>(document, token), Times.Once);
    }

    #region Keyed

    [Fact]
    public void Keyed_WithDocument_ShouldAddOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        Creator = new Mock<IMongoDbCreator>();

        // Act
        Sut.AddOne<TestDocumentWithKey<int>, int>(document);

        // Assert
        Creator.Verify(x => x.AddOne<TestDocumentWithKey<int>, int>(document, CancellationToken.None), Times.Once);
    }

    [Fact]
    public void Keyed_WithDocumentAndCancellationToken_ShouldAddOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocumentWithKey<int>>();
        var token = new CancellationToken(true);
        Creator = new Mock<IMongoDbCreator>();

        // Act
        Sut.AddOne<TestDocumentWithKey<int>, int>(document, token);

        // Assert
        Creator.Verify(x => x.AddOne<TestDocumentWithKey<int>, int>(document, token), Times.Once);
    }

    #endregion
}
