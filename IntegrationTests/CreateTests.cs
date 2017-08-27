using IntegrationTests.Infrastructure;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class CreateTests
    {
        /// <summary>
        /// SUT: System Under Test
        /// </summary>
        private static ITestRepository SUT { get; set; }

        [OneTimeSetUp]
        public void Init()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MongoDbTests"].ConnectionString;
            SUT = new TestRepository(connectionString, "MongoDbTests");
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            // We drop the collection at the end of each test session.
            SUT.DropTestCollection<InsertTestsDocument>();
        }

        [Test]
        public void AddOne()
        {
            // Arrange
            var document = new InsertTestsDocument();
            // Act
            SUT.AddOne(document);
            // Assert
            long count = SUT.Count<InsertTestsDocument>(e => e.Id == document.Id);
            Assert.AreEqual(1, count);
        }

        [Test]
        public async Task AddOneAsync()
        {
            // Arrange
            var document = new InsertTestsDocument();
            // Act
            await SUT.AddOneAsync(document);
            // Assert
            long count = SUT.Count<InsertTestsDocument>(e => e.Id == document.Id);
            Assert.AreEqual(1, count);
        }

        [Test]
        public void AddMany()
        {
            // Arrange
            var documents = new List<InsertTestsDocument> { new InsertTestsDocument(), new InsertTestsDocument() };
            // Act
            SUT.AddMany(documents);
            // Assert
            long count = SUT.Count<InsertTestsDocument>(e => e.Id == documents[0].Id || e.Id == documents[1].Id);
            Assert.AreEqual(2, count);
        }

        [Test]
        public async Task AddManyAsync()
        {
            // Arrange
            var documents = new List<InsertTestsDocument> { new InsertTestsDocument(), new InsertTestsDocument() };
            // Act
            await SUT.AddManyAsync(documents);
            // Assert
            long count = SUT.Count<InsertTestsDocument>(e => e.Id == documents[0].Id || e.Id == documents[1].Id);
            Assert.AreEqual(2, count);
        }


        #region Utils

        private class InsertTestsDocument : Document
        {
            public InsertTestsDocument()
            {
                Version = 2;
            }
            public string SomeContent { get; set; }
        }

        #endregion
    }
}
