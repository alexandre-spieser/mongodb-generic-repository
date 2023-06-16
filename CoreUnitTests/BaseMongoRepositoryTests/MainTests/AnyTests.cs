using System;
using System.Threading;
using System.Threading.Tasks;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using MongoDB.Driver;
using MongoDbGenericRepository.DataAccess.Read;
using Moq;
using Xunit;

namespace CoreUnitTests.BaseMongoRepositoryTests.MainTests;

public class AnyTests : TestMongoRepositoryContext
{
    /*[Fact]
    public async Task AnyAsync_EnsureTokenPassed()
    {
        // Arrange
        var token = new CancellationToken();

        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(x => x.AnyAsync<TestDocument, Guid>(It.IsAny<ExpressionFilterDefinition<TestDocument>>(), null, null, token))
            .ReturnsAsync(true);

        // Act
        await Sut.AnyAsync<TestDocument>(t => string.IsNullOrWhiteSpace(t.SomeContent2), token);

        // Assert
        Reader
            .Verify(x => x.AnyAsync<TestDocument, Guid>(
            t => string.IsNullOrWhiteSpace(t.SomeContent2), null, token));
    }*/
}