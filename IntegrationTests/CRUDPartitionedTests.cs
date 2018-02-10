using IntegrationTests.Infrastructure;
using MongoDbGenericRepository.Models;

namespace IntegrationTests
{
    public class PartitionedDoc : TestDoc, IPartitionedDocument
    {
        public PartitionedDoc()
        {
            PartitionKey = "TestPartitionKey";
        }

        public string PartitionKey { get; set; }
    }

    public class CRUDPartitionedTests : MongoDbDocumentTestBase<PartitionedDoc>
    {
        public override string GetClassName()
        {
            return "CRUDPartitionedCollectionNameAttributeTests";
        }
    }
}
