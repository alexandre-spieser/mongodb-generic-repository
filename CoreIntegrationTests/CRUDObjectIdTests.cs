using CoreIntegrationTests.Infrastructure;
using MongoDB.Bson;

namespace CoreIntegrationTests
{
    public class CoreObjectIdTestDocument : TestDoc<ObjectId>
    {
    }

    public class CRUDObjectIdTests : MongoDbTKeyDocumentTestBase<CoreObjectIdTestDocument, ObjectId>
    {
        public CRUDObjectIdTests(MongoDbTestFixture<CoreObjectIdTestDocument, ObjectId> fixture) : base(fixture)
        {

        }

        public override string GetClassName()
        {
            return "CRUDObjectIdTests";
        }
    }
    public class CRUDObjectIdTestDefaultRepositorys : DefaultBaseTKeyMongodbRepositoryTestBase<CoreObjectIdTestDocument, ObjectId>
    {
        public CRUDObjectIdTestDefaultRepositorys(MongoDbTestFixture<CoreObjectIdTestDocument, ObjectId> fixture) :
            base(fixture)
        {
        }

        public override string GetClassName()
        {
            return nameof(CRUDObjectIdTestDefaultRepositorys);
        }
    }
}
