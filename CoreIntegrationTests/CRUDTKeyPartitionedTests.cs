using CoreIntegrationTests.Infrastructure;
using MongoDbGenericRepository.Models;
using System;

namespace CoreIntegrationTests
{
    public class CorePartitionedTKeyTestDocument : TestDoc<Guid>, IPartitionedDocument
    {
        public CorePartitionedTKeyTestDocument()
        {
            PartitionKey = "CoreTestPartitionKey";
        }
        public string PartitionKey { get; set; }
    }

    public class CRUDTKeyPartitionedTests : MongoDbTKeyDocumentTestBase<CorePartitionedTKeyTestDocument, Guid>
    {
        public CRUDTKeyPartitionedTests(MongoDbTestFixture<CorePartitionedTKeyTestDocument, Guid> fixture) : base(fixture)
        {

        }

        public override string GetClassName()
        {
            return "CoreCRUDTKeyPartitionedTests";
        }
    }
}
