using IntegrationTests.Infrastructure;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Models;
using NUnit.Framework;
using System;

namespace IntegrationTests
{
    public class PartitionedTKeyTestDocument : TestDoc<Guid>, IPartitionedDocument
    {
        public PartitionedTKeyTestDocument()
        {
            PartitionKey = "TestPartitionKey";
        }
        public string PartitionKey { get; set; }
    }

    [TestFixture]
    public class CRUDTKeyPartitionedTests : MongoDBTestBase<PartitionedTKeyTestDocument, Guid>
    {
        public override string GetClassName()
        {
            return "CRUDTKeyPartitionedTests";
        }
    }
}
