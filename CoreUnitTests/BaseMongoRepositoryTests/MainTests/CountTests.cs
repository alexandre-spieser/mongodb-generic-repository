using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using MongoDbGenericRepository.DataAccess.Read;
using Moq;
using Xunit;

namespace CoreUnitTests.BaseMongoRepositoryTests.MainTests;

public class CountTests : TestMongoRepositoryContext
{
    /*[Fact]
    public async Task CountAsync_EnsureTokenPassed()
    {
        // Arrange
        var token = new CancellationToken();

        Reader = new Mock<IMongoDbReader>();
        Reader
            .Setup(x => x.CountAsync<TestDocument, Guid>(It.IsAny<Expression<Func<TestDocument, bool>>>(), null, token))
            .ReturnsAsync(10);

        // Act
        var result = await Sut.CountAsync<TestDocument>(t => string.IsNullOrWhiteSpace(t.SomeContent2), token);

        // Assert
        Assert.Equal(10, result);
        Reader.Verify(x => x.CountAsync<TestDocument, Guid>(
            t => string.IsNullOrWhiteSpace(t.SomeContent2), null, token));
    }*/
}