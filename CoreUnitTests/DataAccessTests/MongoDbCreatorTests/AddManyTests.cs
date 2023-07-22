using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

public class AddManyTests : GenericTestContext<MongoDbCreator>
{
    [Fact]
    public void WithDocuments_AddsMany()
    {
        // Arrange
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var context = MockOf<IMongoDbContext>();
        var collection = MockOf<IMongoCollection<TestDocument>>();

        context
            .Setup(x => x.GetCollection<TestDocument>(It.IsAny<string>()))
            .Returns(collection.Object);

        // Act
        Sut.AddMany<TestDocument, Guid>(documents);

        // Assert
        collection.Verify(
            x => x.InsertMany(
                It.Is<List<TestDocument>>(l => l.All(d => documents.Contains(d))),
                null,
                default),
            Times.Once());
    }

    [Fact]
    public void WithDocumentsHavingNoId_SetsId()
    {
        // Arrange
        var documents = Fixture
            .Build<TestDocumentWithKey<string>>()
            .Without(x => x.Id)
            .CreateMany()
            .ToList();
        var context = MockOf<IMongoDbContext>();
        var collection = MockOf<IMongoCollection<TestDocumentWithKey<string>>>();

        context
            .Setup(x => x.GetCollection<TestDocumentWithKey<string>>(It.IsAny<string>()))
            .Returns(collection.Object);

        // Act
        Sut.AddMany<TestDocumentWithKey<string>, string>(documents);

        // Assert
        collection.Verify(
            x => x.InsertMany(
                It.Is<List<TestDocumentWithKey<string>>>(l => l.All(d => documents.Contains(d))),
                null,
                default),
            Times.Once());

        documents.Should().AllSatisfy(d => d.Id.Should().NotBeNull());
    }

    [Fact]
    public void WithPartitionedDocument_AddsMany()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var documents = Fixture.Build<PartitionedTestDocument>()
            .With(x => x.PartitionKey, partitionKey)
            .CreateMany()
            .ToList();

        var context = MockOf<IMongoDbContext>();
        var collection = MockOf<IMongoCollection<PartitionedTestDocument>>();

        context
            .Setup(x => x.GetCollection<PartitionedTestDocument>(It.IsAny<string>()))
            .Returns(collection.Object);

        // Act
        Sut.AddMany<PartitionedTestDocument, Guid>(documents);

        // Assert
        collection.Verify(
            x => x.InsertMany(
                It.Is<List<PartitionedTestDocument>>(l => l.All(d => documents.Contains(d))),
                null,
                default),
            Times.Once());

        context.Verify(x => x.GetCollection<PartitionedTestDocument>(partitionKey), Times.Once());
    }

    [Fact]
    public void WithDocumentsAndCancellationToken_AddsMany()
    {
        // Arrange
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var context = MockOf<IMongoDbContext>();
        var collection = MockOf<IMongoCollection<TestDocument>>();
        var token = new CancellationToken(true);

        context
            .Setup(x => x.GetCollection<TestDocument>(It.IsAny<string>()))
            .Returns(collection.Object);

        // Act
        Sut.AddMany<TestDocument, Guid>(documents, token);

        // Assert
        collection.Verify(
            x => x.InsertMany(
                It.Is<List<TestDocument>>(l => l.All(d => documents.Contains(d))),
                null,
                token),
            Times.Once());
    }

    [Fact]
    public void WithPartitionedDocumentAndCancellationToken_AddsOne()
    {
        // Arrange
        var partitionKey = Fixture.Create<string>();
        var documents = Fixture.Build<PartitionedTestDocument>()
            .With(x => x.PartitionKey, partitionKey)
            .CreateMany()
            .ToList();
        var token = new CancellationToken(true);

        var context = MockOf<IMongoDbContext>();
        var collection = MockOf<IMongoCollection<PartitionedTestDocument>>();

        context
            .Setup(x => x.GetCollection<PartitionedTestDocument>(It.IsAny<string>()))
            .Returns(collection.Object);

        // Act
        Sut.AddMany<PartitionedTestDocument, Guid>(documents, token);

        // Assert
        collection.Verify(
            x => x.InsertMany(
                It.Is<List<PartitionedTestDocument>>(l => l.All(d => documents.Contains(d))),
                null,
                token),
            Times.Once());

        context.Verify(x => x.GetCollection<PartitionedTestDocument>(partitionKey), Times.Once());
    }
}
