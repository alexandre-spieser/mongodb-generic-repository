using CoreIntegrationTests.Infrastructure;
using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using System;

namespace CoreIntegrationTests
{
    [CollectionName("TestingCollectionNameAttributePartitionedTKey")]
    public class CoreTKeyPartitionedCollectionNameDoc : TestDoc<Guid>, IPartitionedDocument
    {
        public CoreTKeyPartitionedCollectionNameDoc()
        {
            PartitionKey = "CoreTestPartitionKey";
        }

        public string PartitionKey { get; set; }
    }

    public class CRUDTKeyPartitionedCollectionNameAttributeTests : MongoDbTKeyDocumentTestBase<CoreTKeyPartitionedCollectionNameDoc, Guid>
    {
        public CRUDTKeyPartitionedCollectionNameAttributeTests(MongoDbTestFixture<CoreTKeyPartitionedCollectionNameDoc, Guid> fixture) : base(fixture)
        {

        }

        public override string GetClassName()
        {
            return "CoreCRUDTKeyPartitionedCollectionNameAttributeTests";
        }
    }
}
