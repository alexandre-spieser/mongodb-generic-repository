using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
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

public class UpdateOneAsyncTests : GenericTestContext<MongoDbUpdater>
{
    [Fact]
    public async Task WithDocument_ReplacesOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();

        var collection = SetupCollection();

        // Act
        var result = await Sut.UpdateOneAsync<TestDocument, Guid>(document);

        // Assert
        result.Should().BeTrue();

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        ReplaceOptions expectedOptions = null;
        collection
            .Verify(
                x => x.ReplaceOneAsync(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    document,
                    expectedOptions,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public async Task WithDocumentAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var token = new CancellationToken(true);
        var collection = SetupCollection();

        // Act
        var result = await Sut.UpdateOneAsync<TestDocument, Guid>(document, token);

        // Assert
        result.Should().BeTrue();

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        ReplaceOptions expectedOptions = null;
        collection
            .Verify(
                x => x.ReplaceOneAsync(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    document,
                    expectedOptions,
                    token),
                Times.Once());
    }

    [Fact]
    public async Task WithDocumentAndUpdateDefinition_ReplacesOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var collection = SetupCollection();
        var updateDefinition = Builders<TestDocument>.Update.Set(x => x.Id, document.Id);

        // Act
        var result = await Sut.UpdateOneAsync<TestDocument, Guid>(document, updateDefinition);

        // Assert
        result.Should().BeTrue();

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        collection
            .Verify(
                x => x.UpdateOneAsync(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    updateDefinition,
                    null,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public async Task WithDocumentAndUpdateDefinitionAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var collection = SetupCollection();
        var updateDefinition = Builders<TestDocument>.Update.Set(x => x.Id, document.Id);
        var token = new CancellationToken(true);

        // Act
        var result = await Sut.UpdateOneAsync<TestDocument, Guid>(document, updateDefinition, token);

        // Assert
        result.Should().BeTrue();

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        collection
            .Verify(
                x => x.UpdateOneAsync(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    updateDefinition,
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public async Task WithDocumentAndFieldExpressionAndValue_ReplacesOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var value = Fixture.Create<string>();
        var collection = SetupCollection();
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = await Sut.UpdateOneAsync<TestDocument, Guid, string>(document, fieldExpression, value);

        // Assert
        result.Should().BeTrue();

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        collection
            .Verify(
                x => x.UpdateOneAsync(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public async Task WithDocumentAndFieldExpressionAndValueAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var document = Fixture.Create<TestDocument>();
        var value = Fixture.Create<string>();
        var collection = SetupCollection();
        var token = new CancellationToken(true);
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = await Sut.UpdateOneAsync<TestDocument, Guid, string>(document, fieldExpression, value, token);

        // Assert
        result.Should().BeTrue();

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        collection
            .Verify(
                x => x.UpdateOneAsync(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public async Task WithFilterAndFieldExpressionAndValueAndPartitionKey_ReplacesOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var collection = SetupCollection(partitionKey);
        var filter = Builders<TestDocument>.Filter.Eq(x => x.SomeContent, "SomeContent");
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = await Sut.UpdateOneAsync<TestDocument, Guid, string>(
            filter,
            fieldExpression,
            value,
            partitionKey);

        // Assert
        result.Should().BeTrue();

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        collection
            .Verify(
                x => x.UpdateOneAsync(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(filter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public async Task WithFilterAndFieldExpressionAndValueAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var collection = SetupCollection();
        var token = new CancellationToken(true);
        var filter = Builders<TestDocument>.Filter.Eq(x => x.SomeContent, "SomeContent");
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = await Sut.UpdateOneAsync<TestDocument, Guid, string>(
            filter,
            fieldExpression,
            value,
            cancellationToken: token);

        // Assert
        result.Should().BeTrue();

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        collection
            .Verify(
                x => x.UpdateOneAsync(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(filter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public async Task WithFilterAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var collection = SetupCollection(partitionKey);
        var token = new CancellationToken(true);
        var filter = Builders<TestDocument>.Filter.Eq(x => x.SomeContent, "SomeContent");
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = await Sut.UpdateOneAsync<TestDocument, Guid, string>(
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
                x => x.UpdateOneAsync(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(filter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public async Task WithFilterExpressionAndFieldExpressionAndValueAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var collection = SetupCollection();
        var token = new CancellationToken(true);
        Expression<Func<TestDocument, bool>> filterExpression = testDocument => testDocument.SomeContent == "SomeContent";
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = await Sut.UpdateOneAsync<TestDocument, Guid, string>(
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
                x => x.UpdateOneAsync(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public async Task WithFilterExpressionAndFieldExpressionAndValueAndPartitionKey_ReplacesOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var collection = SetupCollection(partitionKey);

        Expression<Func<TestDocument, bool>> filterExpression = testDocument => testDocument.SomeContent == "SomeContent";
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = await Sut.UpdateOneAsync<TestDocument, Guid, string>(
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
                x => x.UpdateOneAsync(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public async Task WithFilterExpressionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var collection = SetupCollection(partitionKey);
        var token = new CancellationToken(true);
        Expression<Func<TestDocument, bool>> filterExpression = testDocument => testDocument.SomeContent == "SomeContent";
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = await Sut.UpdateOneAsync<TestDocument, Guid, string>(
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
                x => x.UpdateOneAsync(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public async Task WithClientSessionAndDocument_ReplacesOne()
    {
        // Arrange
        var session = MockOf<IClientSessionHandle>();
        var document = Fixture.Create<TestDocument>();

        var collection = SetupCollection();

        // Act
        var result = await Sut.UpdateOneAsync<TestDocument, Guid>(session.Object, document);

        // Assert
        result.Should().BeTrue();

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        ReplaceOptions expectedOptions = null;
        collection
            .Verify(
                x => x.ReplaceOneAsync(
                    session.Object,
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    document,
                    expectedOptions,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public async Task WithClientSessionAndDocumentAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var session = MockOf<IClientSessionHandle>();
        var document = Fixture.Create<TestDocument>();
        var token = new CancellationToken(true);
        var collection = SetupCollection();

        // Act
        var result = await Sut.UpdateOneAsync<TestDocument, Guid>(session.Object, document, token);

        // Assert
        result.Should().BeTrue();

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        ReplaceOptions expectedOptions = null;
        collection
            .Verify(
                x => x.ReplaceOneAsync(
                    session.Object,
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    document,
                    expectedOptions,
                    token),
                Times.Once());
    }

    [Fact]
    public async Task WithClientSessionAndDocumentAndUpdateDefinition_ReplacesOne()
    {
        // Arrange
        var session = MockOf<IClientSessionHandle>();
        var document = Fixture.Create<TestDocument>();
        var collection = SetupCollection();
        var updateDefinition = Builders<TestDocument>.Update.Set(x => x.Id, document.Id);

        // Act
        var result = await Sut.UpdateOneAsync<TestDocument, Guid>(session.Object, document, updateDefinition);

        // Assert
        result.Should().BeTrue();

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        collection
            .Verify(
                x => x.UpdateOneAsync(
                    session.Object,
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    updateDefinition,
                    null,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public async Task WithClientSessionAndDocumentAndUpdateDefinitionAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var session = MockOf<IClientSessionHandle>();
        var document = Fixture.Create<TestDocument>();
        var collection = SetupCollection();
        var updateDefinition = Builders<TestDocument>.Update.Set(x => x.Id, document.Id);
        var token = new CancellationToken(true);

        // Act
        var result = await Sut.UpdateOneAsync<TestDocument, Guid>(session.Object, document, updateDefinition, token);

        // Assert
        result.Should().BeTrue();

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        collection
            .Verify(
                x => x.UpdateOneAsync(
                    session.Object,
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    updateDefinition,
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public async Task WithClientSessionAndDocumentAndFieldExpressionAndValue_ReplacesOne()
    {
        // Arrange
        var session = MockOf<IClientSessionHandle>();
        var document = Fixture.Create<TestDocument>();
        var value = Fixture.Create<string>();
        var collection = SetupCollection();
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = await Sut.UpdateOneAsync<TestDocument, Guid, string>(session.Object, document, fieldExpression, value);

        // Assert
        result.Should().BeTrue();

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        collection
            .Verify(
                x => x.UpdateOneAsync(
                    session.Object,
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public async Task WithClientSessionAndDocumentAndFieldExpressionAndValueAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var session = MockOf<IClientSessionHandle>();
        var document = Fixture.Create<TestDocument>();
        var value = Fixture.Create<string>();
        var collection = SetupCollection();
        var token = new CancellationToken(true);
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = await Sut.UpdateOneAsync<TestDocument, Guid, string>(session.Object, document, fieldExpression, value, token);

        // Assert
        result.Should().BeTrue();

        var expectedFilter = Builders<TestDocument>.Filter.Eq("Id", document.Id);
        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        collection
            .Verify(
                x => x.UpdateOneAsync(
                    session.Object,
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public async Task WithClientSessionAndFilterAndFieldExpressionAndValueAndPartitionKey_ReplacesOne()
    {
        // Arrange
        var session = MockOf<IClientSessionHandle>();
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var collection = SetupCollection(partitionKey);
        var filter = Builders<TestDocument>.Filter.Eq(x => x.SomeContent, "SomeContent");
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = await Sut.UpdateOneAsync<TestDocument, Guid, string>(
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
                x => x.UpdateOneAsync(
                    session.Object,
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(filter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public async Task WithClientSessionAndFilterAndFieldExpressionAndValueAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var session = MockOf<IClientSessionHandle>();
        var value = Fixture.Create<string>();
        var collection = SetupCollection();
        var token = new CancellationToken(true);
        var filter = Builders<TestDocument>.Filter.Eq(x => x.SomeContent, "SomeContent");
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = await Sut.UpdateOneAsync<TestDocument, Guid, string>(
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
                x => x.UpdateOneAsync(
                    session.Object,
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(filter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public async Task WithClientSessionAndFilterAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ReplacesOne()
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
        var result = await Sut.UpdateOneAsync<TestDocument, Guid, string>(
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
                x => x.UpdateOneAsync(
                    session.Object,
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(filter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public async Task WithClientSessionAndFilterExpressionAndFieldExpressionAndValueAndCancellationToken_ReplacesOne()
    {
        // Arrange
        var session = MockOf<IClientSessionHandle>();
        var value = Fixture.Create<string>();
        var collection = SetupCollection();
        var token = new CancellationToken(true);
        Expression<Func<TestDocument, bool>> filterExpression = testDocument => testDocument.SomeContent == "SomeContent";
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = await Sut.UpdateOneAsync<TestDocument, Guid, string>(
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
                x => x.UpdateOneAsync(
                    session.Object,
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public async Task WithClientSessionAndFilterExpressionAndFieldExpressionAndValueAndPartitionKey_ReplacesOne()
    {
        // Arrange
        var session = MockOf<IClientSessionHandle>();
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var collection = SetupCollection(partitionKey);

        Expression<Func<TestDocument, bool>> filterExpression = testDocument => testDocument.SomeContent == "SomeContent";
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = await Sut.UpdateOneAsync<TestDocument, Guid, string>(
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
                x => x.UpdateOneAsync(
                    session.Object,
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public async Task WithClientSessionAndFilterExpressionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_ReplacesOne()
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
        var result = await Sut.UpdateOneAsync<TestDocument, Guid, string>(
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
                x => x.UpdateOneAsync(
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
        var replaceResult = new ReplaceOneResult.Acknowledged(count, 1, BsonValue.Create(replacedId));
        var updateResult = new UpdateResult.Acknowledged(count, 1, BsonValue.Create(replacedId));

        var collection = MockOf<IMongoCollection<TestDocument>>();
        collection
            .Setup(
                x => x.ReplaceOneAsync(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<TestDocument>(),
                    It.IsAny<ReplaceOptions>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(replaceResult);

        collection
            .Setup(
                x => x.UpdateOneAsync(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<UpdateOptions>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(updateResult);

        collection
            .Setup(
                x => x.ReplaceOneAsync(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<TestDocument>(),
                    It.IsAny<ReplaceOptions>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(replaceResult);

        collection
            .Setup(
                x => x.UpdateOneAsync(
                    It.IsAny<IClientSessionHandle>(),
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<UpdateOptions>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(updateResult);

        var dbContext = MockOf<IMongoDbContext>();
        dbContext
            .Setup(x => x.GetCollection<TestDocument>(partitionKey))
            .Returns(collection.Object);
        return collection;
    }
}
