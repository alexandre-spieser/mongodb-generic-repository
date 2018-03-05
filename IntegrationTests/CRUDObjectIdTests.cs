using IntegrationTests.Infrastructure;
using MongoDB.Bson;
using NUnit.Framework;

namespace IntegrationTests
{
    public class ObjectIdTestDocument : TestDoc<ObjectId>
    {
    }

    [TestFixture]
    public class CRUDObjectIdTests : MongoDbTKeyDocumentTestBase<ObjectIdTestDocument, ObjectId>
    {
        public override string GetClassName()
        {
            return "CRUDObjectIdTests";
        }
    }
}
