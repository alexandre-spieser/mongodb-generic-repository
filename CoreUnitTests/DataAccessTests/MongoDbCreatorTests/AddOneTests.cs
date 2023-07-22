using System;
using System.Threading;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using FluentAssertions;
using MongoDB.Driver;
using MongoDbGenericRepository;
using MongoDbGenericRepository.DataAccess.Create;
using Moq;
using Xunit;

namespace CoreUnitTests.DataAccessTests.MongoDbCreatorTests;

public class AddOneTests : GenericTestContext<MongoDbCreator>
{
    [Fact]
    public void WithDocument_AddsOne()
    {
        // Arrange
        var document = new TestDocument();
        var context = MockOf<IMongoDbContext>();
        var collection = MockOf<IMongoCollection<TestDocument>>();

        context
            .Setup(x => x.GetCollection<TestDocument>(It.IsAny<string>()))
            .Returns(collection.Object);

        // Act
        Sut.AddOne<TestDocument, Guid>(document);

        // Assert
        collection.Verify(
            x => x.InsertOne(
                It.Is<TestDocument>(d => d == document),
                null,
                default),
            Times.Once());
    }

    [Fact]
    public void WithDocumentHavingNoId_SetsId()
    {
        // Arrange
        var document = new TestDocumentWithKey<string>();
        var context = MockOf<IMongoDbContext>();
        var collection = MockOf<IMongoCollection<TestDocumentWithKey<string>>>();

        context
            .Setup(x => x.GetCollection<TestDocumentWithKey<string>>(It.IsAny<string>()))
            .Returns(collection.Object);

        // Act
        Sut.AddOne<TestDocumentWithKey<string>, string>(document);

        // Assert
        collection.Verify(
            x => x.InsertOne(
                It.Is<TestDocumentWithKey<string>>(d => d == document),
                null,
                default),
            Times.Once());

        document.Id.Should().NotBeNull();
    }

    [Fact]
    public void WithPartitionedDocument_AddsOne()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var document = Fixture.Build<PartitionedTestDocument>()
            .With(x => x.PartitionKey, partitionKey)
            .Create();

        var context = MockOf<IMongoDbContext>();
        var collection = MockOf<IMongoCollection<PartitionedTestDocument>>();

        context
            .Setup(x => x.GetCollection<PartitionedTestDocument>(It.IsAny<string>()))
            .Returns(collection.Object);

        // Act
        Sut.AddOne<PartitionedTestDocument, Guid>(document);

        // Assert
        collection.Verify(
            x => x.InsertOne(
                It.Is<PartitionedTestDocument>(d => d == document),
                null,
                default),
            Times.Once());

        context.Verify(x => x.GetCollection<PartitionedTestDocument>(partitionKey), Times.Once());
    }

    [Fact]
    public void WithDocumentAndCancellationToken_AddsOne()
    {
        // Arrange
        var document = new TestDocument();
        var context = MockOf<IMongoDbContext>();
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var token = new CancellationToken(true);

        context
            .Setup(x => x.GetCollection<TestDocument>(It.IsAny<string>()))
            .Returns(collection.Object);

        // Act
        Sut.AddOne<TestDocument, Guid>(document, token);

        // Assert
        collection.Verify(
            x => x.InsertOne(
                It.Is<TestDocument>(d => d == document),
                null,
                token),
            Times.Once());
    }

    [Fact]
    public void WithPartitionedDocumentAndCancellationToken_AddsOne()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var document = Fixture.Build<PartitionedTestDocument>()
            .With(x => x.PartitionKey, partitionKey)
            .Create();
        var token = new CancellationToken(true);

        var context = MockOf<IMongoDbContext>();
        var collection = MockOf<IMongoCollection<PartitionedTestDocument>>();

        context
            .Setup(x => x.GetCollection<PartitionedTestDocument>(It.IsAny<string>()))
            .Returns(collection.Object);

        // Act
        Sut.AddOne<PartitionedTestDocument, Guid>(document, token);

        // Assert
        collection.Verify(
            x => x.InsertOne(
                It.Is<PartitionedTestDocument>(d => d == document),
                null,
                token),
            Times.Once());

        context.Verify(x => x.GetCollection<PartitionedTestDocument>(partitionKey), Times.Once());
    }
}
