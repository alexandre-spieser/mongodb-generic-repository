using CoreIntegrationTests.Infrastructure;
using MongoDbGenericRepository.Models;
using System;

namespace CoreIntegrationTests
{
    public class CorePartitionedDoc : TestDoc, IPartitionedDocument
    {
        public CorePartitionedDoc()
        {
            PartitionKey = "CoreTestPartitionKey";
        }

        public string PartitionKey { get; set; }
    }

    public class CRUDPartitionedTests : MongoDbDocumentTestBase<CorePartitionedDoc>
    {
        public CRUDPartitionedTests(MongoDbTestFixture<CorePartitionedDoc, Guid> fixture) : base(fixture)
        {
        }

        public override string GetClassName()
        {
            return "CoreCRUDPartitionedTests";
        }
    }
    public class CRUDPartitionedTestDefaultRepositorys : DefaultBaseMongodbRepositoryTestBase<CorePartitionedDoc>
    {
        public CRUDPartitionedTestDefaultRepositorys(MongoDbTestFixture<CorePartitionedDoc, Guid> fixture) :
            base(fixture)
        {
        }

        public override string GetClassName()
        {
            return nameof(CRUDPartitionedTestDefaultRepositorys);
        }
    }
}
