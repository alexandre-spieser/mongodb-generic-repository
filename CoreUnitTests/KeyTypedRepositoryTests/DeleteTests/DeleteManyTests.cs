using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using FluentAssertions;
using MongoDbGenericRepository.DataAccess.Delete;
using Moq;
using Xunit;

namespace CoreUnitTests.KeyTypedRepositoryTests.DeleteTests;

public class DeleteManyTests : TestKeyedMongoRepositoryContext<int>
{
    [Fact]
    public void WithDocuments_ShouldDeleteMany()
    {
        // Arrange
        var documents = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        var count = Fixture.Create<long>();
        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteMany<TestDocumentWithKey<int>, int>(It.IsAny<IEnumerable<TestDocumentWithKey<int>>>()))
            .Returns(count);

        // Act
        var result = Sut.DeleteMany(documents);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteMany<TestDocumentWithKey<int>, int>(documents), Times.Once);
    }

    [Fact]
    public void WithFilter_ShouldDeleteMany()
    {
        // Arrange
        var content = Fixture.Create<string>();
        var count = Fixture.Create<long>();

        Expression<Func<TestDocumentWithKey<int>, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteMany<TestDocumentWithKey<int>, int>(It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(), null))
            .Returns(count);

        // Act
        var result = Sut.DeleteMany(filter);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteMany<TestDocumentWithKey<int>, int>(filter, null), Times.Once);
    }
    [Fact]
    public void WithFilterAndPartitionKey_ShouldDeleteMany()
    {
        // Arrange
        var content = Fixture.Create<string>();
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();

        Expression<Func<TestDocumentWithKey<int>, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteMany<TestDocumentWithKey<int>, int>(It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(), null))
            .Returns(count);

        // Act
        var result = Sut.DeleteMany(filter, partitionKey);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteMany<TestDocumentWithKey<int>, int>(filter, partitionKey), Times.Once);
    }
}