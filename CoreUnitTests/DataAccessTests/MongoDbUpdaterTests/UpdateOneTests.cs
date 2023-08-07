using System;
using System.Linq.Expressions;
using System.Threading;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbGenericRepository;
using MongoDbGenericRepository.DataAccess.Update;
using Moq;
using Xunit;

namespace CoreUnitTests.DataAccessTests.MongoDbUpdaterTests;

public class UpdateOneTests : GenericTestContext<MongoDbUpdater>
{
    [Fact]
    public void WithDocument_ReplacesOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();

        var collection = SetupCollection();

        // Act
        var result = Sut.UpdateOne<TestDocument, Guid>(document);

        // Assert
        result.Should().BeTrue();

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        ReplaceOptions expectedOptions = null;
        collection
            .Verify(
                x => x.ReplaceOne(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    document,
                    expectedOptions,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public void WithDocumentAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var token = new CancellationToken(true);
        var collection = SetupCollection();

        // Act
        var result = Sut.UpdateOne<TestDocument, Guid>(document, token);

        // Assert
        result.Should().BeTrue();

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        ReplaceOptions expectedOptions = null;
        collection
            .Verify(
                x => x.ReplaceOne(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    document,
                    expectedOptions,
                    token),
                Times.Once());
    }

    [Fact]
    public void WithDocumentAndUpdateDefinition_ReplacesOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var collection = SetupCollection();
        var updateDefinition = Builders<TestDocument>.Update.Set(x => x.Id, document.Id);

        // Act
        var result = Sut.UpdateOne<TestDocument, Guid>(document, updateDefinition);

        // Assert
        result.Should().BeTrue();

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        collection
            .Verify(
                x => x.UpdateOne(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    updateDefinition,
                    null,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public void WithDocumentAndUpdateDefinitionAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var collection = SetupCollection();
        var updateDefinition = Builders<TestDocument>.Update.Set(x => x.Id, document.Id);
        var token = new CancellationToken(true);

        // Act
        var result = Sut.UpdateOne<TestDocument, Guid>(document, updateDefinition, token);

        // Assert
        result.Should().BeTrue();

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        collection
            .Verify(
                x => x.UpdateOne(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    updateDefinition,
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public void WithDocumentAndFieldExpressionAndValue_ReplacesOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var value = Fixture.Create<string>();
        var collection = SetupCollection();
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = Sut.UpdateOne<TestDocument, Guid, string>(document, fieldExpression, value);

        // Assert
        result.Should().BeTrue();

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        collection
            .Verify(
                x => x.UpdateOne(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public void WithDocumentAndFieldExpressionAndValueAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var value = Fixture.Create<string>();
        var collection = SetupCollection();
        var token = new CancellationToken(true);
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = Sut.UpdateOne<TestDocument, Guid, string>(document, fieldExpression, value, token);

        // Assert
        result.Should().BeTrue();

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        collection
            .Verify(
                x => x.UpdateOne(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public void WithFilterAndFieldExpressionAndValueAndPartitionKey_ReplacesOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var collection = SetupCollection(partitionKey);
        var filter = Builders<TestDocument>.Filter.Eq(x => x.SomeContent, "SomeContent");
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = Sut.UpdateOne<TestDocument, Guid, string>(
            filter,
            fieldExpression,
            value,
            partitionKey);

        // Assert
        result.Should().BeTrue();

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        collection
            .Verify(
                x => x.UpdateOne(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(filter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public void WithFilterAndFieldExpressionAndValueAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var collection = SetupCollection();
        var token = new CancellationToken(true);
        var filter = Builders<TestDocument>.Filter.Eq(x => x.SomeContent, "SomeContent");
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = Sut.UpdateOne<TestDocument, Guid, string>(
            filter,
            fieldExpression,
            value,
            cancellationToken: token);

        // Assert
        result.Should().BeTrue();

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        collection
            .Verify(
                x => x.UpdateOne(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(filter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public void WithFilterAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var collection = SetupCollection(partitionKey);
        var token = new CancellationToken(true);
        var filter = Builders<TestDocument>.Filter.Eq(x => x.SomeContent, "SomeContent");
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = Sut.UpdateOne<TestDocument, Guid, string>(
            filter,
            fieldExpression,
            value,
            partitionKey,
            token);

        // Assert
        result.Should().BeTrue();

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        collection
            .Verify(
                x => x.UpdateOne(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(filter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public void WithFilterExpressionAndFieldExpressionAndValueAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var collection = SetupCollection();
        var token = new CancellationToken(true);
        Expression<Func<TestDocument, bool>> filterExpression = testDocument => testDocument.SomeContent == "SomeContent";
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = Sut.UpdateOne<TestDocument, Guid, string>(
            filterExpression,
            fieldExpression,
            value,
            cancellationToken: token);

        // Assert
        result.Should().BeTrue();

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        var expectedFilter = Builders<TestDocument>.Filter.Where(filterExpression);
        collection
            .Verify(
                x => x.UpdateOne(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public void WithFilterExpressionAndFieldExpressionAndValueAndPartitionKey_ReplacesOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var collection = SetupCollection(partitionKey);

        Expression<Func<TestDocument, bool>> filterExpression = testDocument => testDocument.SomeContent == "SomeContent";
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = Sut.UpdateOne<TestDocument, Guid, string>(
            filterExpression,
            fieldExpression,
            value,
            partitionKey);

        // Assert
        result.Should().BeTrue();

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        var expectedFilter = Builders<TestDocument>.Filter.Where(filterExpression);
        collection
            .Verify(
                x => x.UpdateOne(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public void WithFilterExpressionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var collection = SetupCollection(partitionKey);
        var token = new CancellationToken(true);
        Expression<Func<TestDocument, bool>> filterExpression = testDocument => testDocument.SomeContent == "SomeContent";
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = Sut.UpdateOne<TestDocument, Guid, string>(
            filterExpression,
            fieldExpression,
            value,
            partitionKey,
            token);

        // Assert
        result.Should().BeTrue();

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        var expectedFilter = Builders<TestDocument>.Filter.Where(filterExpression);
        collection
            .Verify(
                x => x.UpdateOne(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public void WithClientSessionAndDocument_ReplacesOne()
    {
        // Arrange
        var session = MockOf<IClientSessionHandle>();
        var document = Fixture.Create<TestDocument>();

        var collection = SetupCollection();

        // Act
        var result = Sut.UpdateOne<TestDocument, Guid>(session.Object, document);

        // Assert
        result.Should().BeTrue();

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        ReplaceOptions expectedOptions = null;
        collection
            .Verify(
                x => x.ReplaceOne(
                    session.Object,
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    document,
                    expectedOptions,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public void WithClientSessionAndDocumentAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var session = MockOf<IClientSessionHandle>();
        var document = Fixture.Create<TestDocument>();
        var token = new CancellationToken(true);
        var collection = SetupCollection();

        // Act
        var result = Sut.UpdateOne<TestDocument, Guid>(session.Object, document, token);

        // Assert
        result.Should().BeTrue();

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        ReplaceOptions expectedOptions = null;
        collection
            .Verify(
                x => x.ReplaceOne(
                    session.Object,
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    document,
                    expectedOptions,
                    token),
                Times.Once());
    }

    [Fact]
    public void WithClientSessionAndDocumentAndUpdateDefinition_ReplacesOne()
    {
        // Arrange
        var session = MockOf<IClientSessionHandle>();
        var document = Fixture.Create<TestDocument>();
        var collection = SetupCollection();
        var updateDefinition = Builders<TestDocument>.Update.Set(x => x.Id, document.Id);

        // Act
        var result = Sut.UpdateOne<TestDocument, Guid>(session.Object, document, updateDefinition);

        // Assert
        result.Should().BeTrue();

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        collection
            .Verify(
                x => x.UpdateOne(
                    session.Object,
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    updateDefinition,
                    null,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public void WithClientSessionAndDocumentAndUpdateDefinitionAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var session = MockOf<IClientSessionHandle>();
        var document = Fixture.Create<TestDocument>();
        var collection = SetupCollection();
        var updateDefinition = Builders<TestDocument>.Update.Set(x => x.Id, document.Id);
        var token = new CancellationToken(true);

        // Act
        var result = Sut.UpdateOne<TestDocument, Guid>(session.Object, document, updateDefinition, token);

        // Assert
        result.Should().BeTrue();

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        collection
            .Verify(
                x => x.UpdateOne(
                    session.Object,
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    updateDefinition,
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public void WithClientSessionAndDocumentAndFieldExpressionAndValue_ReplacesOne()
    {
        // Arrange
        var session = MockOf<IClientSessionHandle>();
        var document = Fixture.Create<TestDocument>();
        var value = Fixture.Create<string>();
        var collection = SetupCollection();
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = Sut.UpdateOne<TestDocument, Guid, string>(session.Object, document, fieldExpression, value);

        // Assert
        result.Should().BeTrue();

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        collection
            .Verify(
                x => x.UpdateOne(
                    session.Object,
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public void WithClientSessionAndDocumentAndFieldExpressionAndValueAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var session = MockOf<IClientSessionHandle>();
        var document = Fixture.Create<TestDocument>();
        var value = Fixture.Create<string>();
        var collection = SetupCollection();
        var token = new CancellationToken(true);
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = Sut.UpdateOne<TestDocument, Guid, string>(session.Object, document, fieldExpression, value, token);

        // Assert
        result.Should().BeTrue();

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        collection
            .Verify(
                x => x.UpdateOne(
                    session.Object,
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public void WithClientSessionAndFilterAndFieldExpressionAndValueAndPartitionKey_ReplacesOne()
    {
        // Arrange
        var session = MockOf<IClientSessionHandle>();
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var collection = SetupCollection(partitionKey);
        var filter = Builders<TestDocument>.Filter.Eq(x => x.SomeContent, "SomeContent");
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = Sut.UpdateOne<TestDocument, Guid, string>(
            session.Object,
            filter,
            fieldExpression,
            value,
            partitionKey);

        // Assert
        result.Should().BeTrue();

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        collection
            .Verify(
                x => x.UpdateOne(
                    session.Object,
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(filter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public void WithClientSessionAndFilterAndFieldExpressionAndValueAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var session = MockOf<IClientSessionHandle>();
        var value = Fixture.Create<string>();
        var collection = SetupCollection();
        var token = new CancellationToken(true);
        var filter = Builders<TestDocument>.Filter.Eq(x => x.SomeContent, "SomeContent");
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = Sut.UpdateOne<TestDocument, Guid, string>(
            session.Object,
            filter,
            fieldExpression,
            value,
            cancellationToken: token);

        // Assert
        result.Should().BeTrue();

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        collection
            .Verify(
                x => x.UpdateOne(
                    session.Object,
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(filter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public void WithClientSessionAndFilterAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var session = MockOf<IClientSessionHandle>();
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var collection = SetupCollection(partitionKey);
        var token = new CancellationToken(true);
        var filter = Builders<TestDocument>.Filter.Eq(x => x.SomeContent, "SomeContent");
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = Sut.UpdateOne<TestDocument, Guid, string>(
            session.Object,
            filter,
            fieldExpression,
            value,
            partitionKey,
            token);

        // Assert
        result.Should().BeTrue();

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        collection
            .Verify(
                x => x.UpdateOne(
                    session.Object,
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(filter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public void WithClientSessionAndFilterExpressionAndFieldExpressionAndValueAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var session = MockOf<IClientSessionHandle>();
        var value = Fixture.Create<string>();
        var collection = SetupCollection();
        var token = new CancellationToken(true);
        Expression<Func<TestDocument, bool>> filterExpression = testDocument => testDocument.SomeContent == "SomeContent";
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = Sut.UpdateOne<TestDocument, Guid, string>(
            session.Object,
            filterExpression,
            fieldExpression,
            value,
            cancellationToken: token);

        // Assert
        result.Should().BeTrue();

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        var expectedFilter = Builders<TestDocument>.Filter.Where(filterExpression);
        collection
            .Verify(
                x => x.UpdateOne(
                    session.Object,
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public void WithClientSessionAndFilterExpressionAndFieldExpressionAndValueAndPartitionKey_ReplacesOne()
    {
        // Arrange
        var session = MockOf<IClientSessionHandle>();
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var collection = SetupCollection(partitionKey);

        Expression<Func<TestDocument, bool>> filterExpression = testDocument => testDocument.SomeContent == "SomeContent";
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = Sut.UpdateOne<TestDocument, Guid, string>(
            session.Object,
            filterExpression,
            fieldExpression,
            value,
            partitionKey);

        // Assert
        result.Should().BeTrue();

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        var expectedFilter = Builders<TestDocument>.Filter.Where(filterExpression);
        collection
            .Verify(
                x => x.UpdateOne(
                    session.Object,
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public void WithClientSessionAndFilterExpressionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var session = MockOf<IClientSessionHandle>();
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var collection = SetupCollection(partitionKey);
        var token = new CancellationToken(true);
        Expression<Func<TestDocument, bool>> filterExpression = testDocument => testDocument.SomeContent == "SomeContent";
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = Sut.UpdateOne<TestDocument, Guid, string>(
            session.Object,
            filterExpression,
            fieldExpression,
            value,
            partitionKey,
            token);

        // Assert
        result.Should().BeTrue();

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        var expectedFilter = Builders<TestDocument>.Filter.Where(filterExpression);
        collection
            .Verify(
                x => x.UpdateOne(
                    session.Object,
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    private Mock<IMongoCollection<TestDocument>> SetupCollection(string partitionKey = null)
    {
        var replacedId = Fixture.Create<Guid>();
        var count = Fixture.Create<long>();
        var replaceResult = new ReplaceOneResult.Acknowledged(count, 1, new BsonBinaryData(replacedId, GuidRepresentation.Standard));
        var updateResult = new UpdateResult.Acknowledged(count, 1, new BsonBinaryData(replacedId, GuidRepresentation.Standard));

        var collection = MockOf<IMongoCollection<TestDocument>>();
        collection
            .Setup(
                x => x.ReplaceOne(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<TestDocument>(),
                    It.IsAny<ReplaceOptions>(),
                    It.IsAny<CancellationToken>()))
            .Returns(replaceResult);

        collection
            .Setup(
                x => x.UpdateOne(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<UpdateOptions>(),
                    It.IsAny<CancellationToken>()))
            .Returns(updateResult);

        collection
            .Setup(
                x => x.ReplaceOne(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<TestDocument>(),
                    It.IsAny<ReplaceOptions>(),
                    It.IsAny<CancellationToken>()))
            .Returns(replaceResult);

        collection
            .Setup(
                x => x.UpdateOne(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<UpdateOptions>(),
                    It.IsAny<CancellationToken>()))
            .Returns(updateResult);

        var dbContext = MockOf<IMongoDbContext>();
        dbContext
            .Setup(x => x.GetCollection<TestDocument>(partitionKey))
            .Returns(collection.Object);
        return collection;
    }
}
