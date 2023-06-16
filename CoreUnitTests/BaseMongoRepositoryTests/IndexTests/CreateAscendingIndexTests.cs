using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CoreUnitTests.Infrastructure.Model;
using MongoDbGenericRepository.DataAccess.Index;
using Moq;
using Xunit;

namespace CoreUnitTests.BaseMongoRepositoryTests.IndexTests;

public class CreateAscendingIndexTests : BaseIndexTests
{
    /*[Fact]
    public async Task CreateAscendingIndexAsync_EnsureTokenPassed()
    {
        // Arrange
        IndexHandler = new Mock<IMongoDbIndexHandler>();
        var token = new CancellationToken();

        // Act
        Expression<Func<TestDocument, object>> fieldExpression = t => t.SomeContent2;
        // await Sut.CreateAscendingIndexAsync<TestDocument>(fieldExpression, token);

        // Assert
        IndexHandler.Verify(x => x.CreateAscendingIndexAsync<TestDocument, Guid>(
            fieldExpression, null, null, token));
    }*/
}