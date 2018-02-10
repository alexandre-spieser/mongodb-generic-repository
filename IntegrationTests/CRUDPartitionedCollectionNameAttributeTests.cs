using IntegrationTests.Infrastructure;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using System;

namespace IntegrationTests
{
    [CollectionName("TestingCollectionNameAttributePartitionedTKey")]
    public class PartitionedCollectionNameDoc : TestDoc, IPartitionedDocument
    {
        public PartitionedCollectionNameDoc()
        {
            PartitionKey = "TestPartitionKey";
        }

        public string PartitionKey { get; set; }
    }

    public class CRUDPartitionedCollectionNameAttributeTests : MongoDbDocumentTestBase<PartitionedCollectionNameDoc>
    {
        public override string GetClassName()
        {
            return "CRUDPartitionedCollectionNameAttributeTests";
        }
    }
}
