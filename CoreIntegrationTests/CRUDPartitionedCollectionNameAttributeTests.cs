using CoreIntegrationTests.Infrastructure;
using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using System;

namespace CoreIntegrationTests
{
    [CollectionName("CoreTestingCollectionNameAttributePartitionedTKey")]
    public class CorePartitionedCollectionNameDoc : TestDoc, IPartitionedDocument
    {
        public CorePartitionedCollectionNameDoc()
        {
            PartitionKey = "CoreTestPartitionKeyCollectionName";
        }

        public string PartitionKey { get; set; }
    }

    public class CRUDPartitionedCollectionNameAttributeTests : MongoDbDocumentTestBase<CorePartitionedCollectionNameDoc>
    {
        public CRUDPartitionedCollectionNameAttributeTests(MongoDbTestFixture<CorePartitionedCollectionNameDoc, Guid> fixture) : base(fixture)
        {

        }

        public override string GetClassName()
        {
            return "CoreCRUDPartitionedCollectionNameAttributeTests";
        }
    }
}
