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

public class UpdateManyTests : GenericTestContext<MongoDbUpdater>
{
    [Fact]
    public void WithFilterAndUpdateDefinition_UpdatesMany()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();
        var updateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        var collection = SetupCollection(count);
        var filter = Builders<TestDocument>.Filter.Eq(x => x.SomeContent, "SomeContent");

        // Act
        var result = Sut.UpdateMany<TestDocument, Guid>(
            filter,
            updateDefinition);

        // Assert
        result.Should().Be(count);

        collection
            .Verify(
                x => x.UpdateMany(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(filter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(updateDefinition)),
                    null,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public void WithFilterAndUpdateDefinitionAndCancellationToken_UpdatesMany()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();
        var updateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        var collection = SetupCollection(count);
        var filter = Builders<TestDocument>.Filter.Eq(x => x.SomeContent, "SomeContent");
        var token = new CancellationToken(true);

        // Act
        var result = Sut.UpdateMany<TestDocument, Guid>(
            filter,
            updateDefinition,
            cancellationToken: token);

        // Assert
        result.Should().Be(count);

        collection
            .Verify(
                x => x.UpdateMany(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(filter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(updateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public void WithFilterAndUpdateDefinitionAndPartitionKey_UpdatesMany()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var updateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        var collection = SetupCollection(count, partitionKey);
        var filter = Builders<TestDocument>.Filter.Eq(x => x.SomeContent, "SomeContent");

        // Act
        var result = Sut.UpdateMany<TestDocument, Guid>(
            filter,
            updateDefinition,
            partitionKey);

        // Assert
        result.Should().Be(count);

        collection
            .Verify(
                x => x.UpdateMany(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(filter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(updateDefinition)),
                    null,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public void WithFilterAndUpdateDefinitionAndPartitionKeyAndCancellationToken_UpdatesMany()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var updateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        var collection = SetupCollection(count, partitionKey);
        var filter = Builders<TestDocument>.Filter.Eq(x => x.SomeContent, "SomeContent");
        var token = new CancellationToken(true);

        // Act
        var result = Sut.UpdateMany<TestDocument, Guid>(
            filter,
            updateDefinition,
            partitionKey,
            token);

        // Assert
        result.Should().Be(count);

        collection
            .Verify(
                x => x.UpdateMany(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(filter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(updateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public void WithFilterAndFieldExpressionAndValue_UpdatesMany()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();
        var collection = SetupCollection(count);
        var filter = Builders<TestDocument>.Filter.Eq(x => x.SomeContent, "SomeContent");
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = Sut.UpdateMany<TestDocument, Guid, string>(
            filter,
            fieldExpression,
            value);

        // Assert
        result.Should().Be(count);

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        collection
            .Verify(
                x => x.UpdateMany(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(filter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    CancellationToken.None),
                Times.Once());
    }


    [Fact]
    public void WithFilterAndFieldExpressionAndValueAndPartitionKey_UpdatesMany()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var collection = SetupCollection(count, partitionKey);
        var filter = Builders<TestDocument>.Filter.Eq(x => x.SomeContent, "SomeContent");
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = Sut.UpdateMany<TestDocument, Guid, string>(
            filter,
            fieldExpression,
            value,
            partitionKey);

        // Assert
        result.Should().Be(count);

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        collection
            .Verify(
                x => x.UpdateMany(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(filter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public void WithFilterAndFieldExpressionAndValueAndCancellationToken_UpdatesMany()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();
        var collection = SetupCollection(count);
        var token = new CancellationToken(true);
        var filter = Builders<TestDocument>.Filter.Eq(x => x.SomeContent, "SomeContent");
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = Sut.UpdateMany<TestDocument, Guid, string>(
            filter,
            fieldExpression,
            value,
            cancellationToken: token);

        // Assert
        result.Should().Be(count);

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        collection
            .Verify(
                x => x.UpdateMany(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(filter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public void WithFilterAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_UpdatesMany()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var collection = SetupCollection(count, partitionKey);
        var token = new CancellationToken(true);
        var filter = Builders<TestDocument>.Filter.Eq(x => x.SomeContent, "SomeContent");
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = Sut.UpdateMany<TestDocument, Guid, string>(
            filter,
            fieldExpression,
            value,
            partitionKey,
            token);

        // Assert
        result.Should().Be(count);

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        collection
            .Verify(
                x => x.UpdateMany(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(filter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public void WithFilterExpressionAndFieldExpressionAndValue_UpdatesMany()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();
        var collection = SetupCollection(count);
        Expression<Func<TestDocument, bool>> filterExpression = testDocument => testDocument.SomeContent == "SomeContent";
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = Sut.UpdateMany<TestDocument, Guid, string>(
            filterExpression,
            fieldExpression,
            value);

        // Assert
        result.Should().Be(count);

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        var expectedFilter = Builders<TestDocument>.Filter.Where(filterExpression);
        collection
            .Verify(
                x => x.UpdateMany(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public void WithFilterExpressionAndFieldExpressionAndValueAndCancellationToken_UpdatesMany()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();
        var collection = SetupCollection(count);
        var token = new CancellationToken(true);
        Expression<Func<TestDocument, bool>> filterExpression = testDocument => testDocument.SomeContent == "SomeContent";
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = Sut.UpdateMany<TestDocument, Guid, string>(
            filterExpression,
            fieldExpression,
            value,
            cancellationToken: token);

        // Assert
        result.Should().Be(count);

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        var expectedFilter = Builders<TestDocument>.Filter.Where(filterExpression);
        collection
            .Verify(
                x => x.UpdateMany(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public void WithFilterExpressionAndFieldExpressionAndValueAndPartitionKey_UpdatesMany()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var count = Fixture.Create<long>();
        var collection = SetupCollection(count, partitionKey);

        Expression<Func<TestDocument, bool>> filterExpression = testDocument => testDocument.SomeContent == "SomeContent";
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = Sut.UpdateMany<TestDocument, Guid, string>(
            filterExpression,
            fieldExpression,
            value,
            partitionKey);

        // Assert
        result.Should().Be(count);

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        var expectedFilter = Builders<TestDocument>.Filter.Where(filterExpression);
        collection
            .Verify(
                x => x.UpdateMany(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public void WithFilterExpressionAndFieldExpressionAndValueAndPartitionKeyAndCancellationToken_UpdatesMany()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();
        var collection = SetupCollection(count, partitionKey);
        var token = new CancellationToken(true);
        Expression<Func<TestDocument, bool>> filterExpression = testDocument => testDocument.SomeContent == "SomeContent";
        Expression<Func<TestDocument, string>> fieldExpression = testDocument => testDocument.SomeContent;

        // Act
        var result = Sut.UpdateMany<TestDocument, Guid, string>(
            filterExpression,
            fieldExpression,
            value,
            partitionKey,
            token);

        // Assert
        result.Should().Be(count);

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        var expectedFilter = Builders<TestDocument>.Filter.Where(filterExpression);
        collection
            .Verify(
                x => x.UpdateMany(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public void WithFilterExpressionAndUpdateDefinition_UpdatesMany()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();
        var updateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        var collection = SetupCollection(count);
        Expression<Func<TestDocument, bool>> filterExpression = testDocument => testDocument.SomeContent == "SomeContent";

        // Act
        var result = Sut.UpdateMany<TestDocument, Guid>(
            filterExpression,
            updateDefinition);

        // Assert
        result.Should().Be(count);

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        var expectedFilter = Builders<TestDocument>.Filter.Where(filterExpression);
        collection
            .Verify(
                x => x.UpdateMany(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public void WithFilterExpressionAndUpdateDefinitionAndCancellationToken_UpdatesMany()
    {
        // Arrange
        var count = Fixture.Create<long>();
        var value = Fixture.Create<string>();
        var updateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        var collection = SetupCollection(count);
        var token = new CancellationToken(true);
        Expression<Func<TestDocument, bool>> filterExpression = testDocument => testDocument.SomeContent == "SomeContent";

        // Act
        var result = Sut.UpdateMany<TestDocument, Guid>(
            filterExpression,
            updateDefinition,
            cancellationToken: token);

        // Assert
        result.Should().Be(count);

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        var expectedFilter = Builders<TestDocument>.Filter.Where(filterExpression);
        collection
            .Verify(
                x => x.UpdateMany(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    [Fact]
    public void WithFilterExpressionAndUpdateDefinitionAndPartitionKey_UpdatesMany()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var partitionKey = Fixture.Create<string>();
        var updateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        var count = Fixture.Create<long>();
        var collection = SetupCollection(count, partitionKey);

        Expression<Func<TestDocument, bool>> filterExpression = testDocument => testDocument.SomeContent == "SomeContent";

        // Act
        var result = Sut.UpdateMany<TestDocument, Guid>(
            filterExpression,
            updateDefinition,
            partitionKey);

        // Assert
        result.Should().Be(count);

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        var expectedFilter = Builders<TestDocument>.Filter.Where(filterExpression);
        collection
            .Verify(
                x => x.UpdateMany(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    CancellationToken.None),
                Times.Once());
    }

    [Fact]
    public void WithFilterExpressionAndUpdateDefinitionAndPartitionKeyAndCancellationToken_UpdatesMany()
    {
        // Arrange
        var value = Fixture.Create<string>();
        var count = Fixture.Create<long>();
        var updateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        var partitionKey = Fixture.Create<string>();
        var collection = SetupCollection(count, partitionKey);
        var token = new CancellationToken(true);
        Expression<Func<TestDocument, bool>> filterExpression = testDocument => testDocument.SomeContent == "SomeContent";

        // Act
        var result = Sut.UpdateMany<TestDocument, Guid>(
            filterExpression,
            updateDefinition,
            partitionKey,
            token);

        // Assert
        result.Should().Be(count);

        var expectedUpdateDefinition = Builders<TestDocument>.Update.Set(x => x.SomeContent, value);
        var expectedFilter = Builders<TestDocument>.Filter.Where(filterExpression);
        collection
            .Verify(
                x => x.UpdateMany(
                    It.Is<FilterDefinition<TestDocument>>(f => f.EquivalentTo(expectedFilter)),
                    It.Is<UpdateDefinition<TestDocument>>(u => u.EquivalentTo(expectedUpdateDefinition)),
                    null,
                    token),
                Times.Once());
    }

    private Mock<IMongoCollection<TestDocument>> SetupCollection(long count, string partitionKey = null)
    {
        var replacedId = Fixture.Create<Guid>();
        var replaceResult = new ReplaceOneResult.Acknowledged(count, count, new BsonBinaryData(replacedId, GuidRepresentation.Standard));
        var updateResult = new UpdateResult.Acknowledged(count, count, new BsonBinaryData(replacedId, GuidRepresentation.Standard));

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
                x => x.UpdateMany(
                    It.IsAny<FilterDefinition<TestDocument>>(),
                    It.IsAny<UpdateDefinition<TestDocument>>(),
                    It.IsAny<UpdateOptions>(),
                    It.IsAny<CancellationToken>()))
            .Returns(updateResult);

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
                x => x.UpdateMany(
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
