using IntegrationTests.Infrastructure;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class UpdateTestsPartitionedDocument : PartitionedDocument
    {
        public UpdateTestsPartitionedDocument() : base("TestPartitionKey")
        {
            Version = 2;
        }
        public string SomeContent { get; set; }
    }

    public class UpdatePartitionedTests : BaseMongoDbRepositoryTests<UpdateTestsPartitionedDocument>
    {
        [Test]
        public void PartitionedUpdateOne()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            document.SomeContent = "UpdateOneContent";
            // Act
            var result = SUT.UpdateOne(document);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsPartitionedDocument>(document.Id, PartitionKey);
            Assert.IsNotNull(updatedDocument);
            Assert.AreEqual("UpdateOneContent", updatedDocument.SomeContent);
        }

        [Test]
        public async Task PartitionedUpdateOneAsync()
        {
            // Arrange
            var document = CreateTestDocument();
            SUT.AddOne(document);
            document.SomeContent = "UpdateOneAsyncContent";
            // Act
            var result = await SUT.UpdateOneAsync(document);
            // Assert
            Assert.IsTrue(result);
            var updatedDocument = SUT.GetById<UpdateTestsPartitionedDocument>(document.Id, PartitionKey);
            Assert.IsNotNull(updatedDocument);
            Assert.AreEqual("UpdateOneAsyncContent", updatedDocument.SomeContent);
        }
    }
}
