using IntegrationTests.Infrastructure;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using System;

namespace IntegrationTests
{
    [CollectionName("TestingCollectionNameAttributePartitionedTKey")]
    public class TKeyPartitionedCollectionNameDoc : TestDoc<Guid>, IPartitionedDocument
    {
        public TKeyPartitionedCollectionNameDoc()
        {
            PartitionKey = "TestPartitionKey";
        }

        public string PartitionKey { get; set; }
    }

    public class CRUDTKeyPartitionedCollectionNameAttributeTests : MongoDBTestBase<TKeyPartitionedCollectionNameDoc, Guid>
    {
        public override string GetClassName()
        {
            return "TKeyPartitionedCollectionNameAttributeTests";
        }
    }
}
